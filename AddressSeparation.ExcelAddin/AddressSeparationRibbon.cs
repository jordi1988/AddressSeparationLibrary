using AddressSeparation.Factories;
using AddressSeparation.Helper;
using AddressSeparation.Manipulations;
using AddressSeparation.Mapper;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AddressSeparation.ExcelAddin
{
    public partial class AddressSeparationRibbon
    {
        #region Fields

        /// <summary>
        /// Dictionary for mapping string value in combobox and type
        /// </summary>
        private static Dictionary<string, DescriptionMapper> _outputFormatsDictionary =
            new Dictionary<string, DescriptionMapper>();

        /// <summary>
        /// Access to the options dialog
        /// </summary>
        private AdvancedSettingsDialog _optionsDialog = new AdvancedSettingsDialog();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Load sequence of plugin.
        /// </summary>
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            var outputFormats = OutputFormatHelper.GetMappings();
            this.PopulateOutputFormats(outputFormats);
        }

        /// <summary>
        /// Processes the selected cells
        /// </summary>
        private void btnProcess_Click(object sender, RibbonControlEventArgs e)
        {
            // sanity check
            var selection = Globals.ThisAddIn.Application.Selection as Range;
            if (selection == null)
            {
                
                MessageBox.Show(Resources.Messages.PlaceCursorOntoAddress, Resources.Messages.ProcessTitle, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // get selected output format type
            string selectedOutputFormatText = cbOutputFormat.Text;
            Type selectedOutputFormatType = _outputFormatsDictionary[selectedOutputFormatText]?.Type;
            if (selectedOutputFormatType == null)
            {
                MessageBox.Show(Resources.Messages.SelectOutputFormat, Resources.Messages.ProcessTitle, 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // create processor
            var queue = GetInputManipulationQueue();
            dynamic processor = AddressSeparationProcessorFactory.CreateInstance(selectedOutputFormatType, null, queue);
            var matchedProperties = OutputFormatHelper.GetPropertyRegexGroups(selectedOutputFormatType).ToList();

            // try resolving every address
            foreach (Range cell in selection.Cells)
            {
                // skip empty cells
                if (cell.Value == null)
                {
                    continue;
                }

                // process address
                object address = processor.Process(cell.Value).ResolvedAddress;

                // place properties next to active cell
                for (int i = 0; i < matchedProperties.Count(); i++)
                {
                    string propertyName = matchedProperties[i].Property.Name;
                    cell.Offset[0, i + 1].Value = address.GetType()
                        .GetProperty(propertyName)
                        .GetValue(address);
                }
            }
        }

        /// <summary>
        /// Generate queue with selected input manipulation functions
        /// </summary>
        /// <returns>ready-to-use queue</returns>
        private Queue<IInputManipulation> GetInputManipulationQueue()
        {
            var queue = new Queue<IInputManipulation>();
            var activeInputManipulations = _optionsDialog.GetActiveInputManipulationOptions();
            foreach (var item in activeInputManipulations)
            {
                var instance = Activator.CreateInstance(item.Type) as IInputManipulation;
                queue.Enqueue(instance);
            }
            return queue;
        }

        /// <summary>
        /// Populate dropdown containing output formats of the Address Separation library.
        /// </summary>
        /// <param name="outputFormats">List of output formats to populate the combo box with.</param>
        private void PopulateOutputFormats(IEnumerable<DescriptionMapper> outputFormats)
        {
            // clear all items and repopulate combobox and dictionary
            cbOutputFormat.Items.Clear();
            _outputFormatsDictionary.Clear();
            foreach (var format in outputFormats)
            {
                // create combobox item
                var item = Globals.Factory
                    .GetRibbonFactory()
                    .CreateRibbonDropDownItem();
                item.Label = format.DisplayName;
                item.ScreenTip = format.Description;

                // add item
                cbOutputFormat.Items.Add(item);
                _outputFormatsDictionary.Add(format.DisplayName, format);
            }

            // select first item if it exists
            cbOutputFormat.Text = cbOutputFormat.Items.FirstOrDefault()?.Label;
        }

        /// <summary>
        /// Toggle all or matching output formats.
        /// </summary>
        /// <param name="sender">Sender of type <see cref="RibbonToggleButton"/>.</param>
        /// <param name="e">Event of ribbon control.</param>
        private void toggleFindMatch_Click(object sender, RibbonControlEventArgs e)
        {
            // sanity check
            var button = sender as RibbonToggleButton;
            var selection = Globals.ThisAddIn.Application.ActiveCell as Range;
            if (button == null || selection == null)
            {
                return;
            }

            IEnumerable<DescriptionMapper> outputFormats = null;
            if (button.Checked == false)
            {
                // show all output formats
                outputFormats = OutputFormatHelper.GetMappings();
            }
            else
            {
                // show only matching output formats
                var firstSelectedCellValue = selection?.Value as string;
                if (string.IsNullOrWhiteSpace(firstSelectedCellValue))
                {
                    button.Checked = false;
                    MessageBox.Show(
                        Resources.Messages.PlaceCursorOntoAddress, Resources.Messages.IdentifyOutputFormats, 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                outputFormats = OutputFormatHelper.GetCompatibleOutputFormats(firstSelectedCellValue);
            }

            // repopulate dropdown
            this.PopulateOutputFormats(outputFormats);
        }

        /// <summary>
        /// Calls the dialog launcher to load custom output formats.
        /// </summary>
        private void AddressSeparationGroup_DialogLauncherClick(object sender, RibbonControlEventArgs e)
        {
            _optionsDialog.ShowDialog();
        }

        #endregion Methods

        //private void button1_Click(object sender, RibbonControlEventArgs e)
        //{
        //    var folderBrowserDialog = new FolderBrowserDialog()
        //    {
        //        Description = "Select a folder containing an assembly with custom output formats."
        //    };

        //    // sanity check dialog
        //    var result = folderBrowserDialog.ShowDialog();
        //    if (result == DialogResult.Cancel)
        //    {
        //        return;
        //    }

        //    // sanity check directory
        //    var directory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
        //    if (directory.Exists == false)
        //    {
        //        return;
        //    }

        //    foreach (var file in directory.GetFiles("*.dll"))
        //    {
        //        var userAssembly = Assembly.LoadFrom(file.FullName);
        //        // userAssembly = Assembly.Load(userAssembly.FullName);
        //        var outputFormats = OutputFormatHelper.GetMappings(userAssembly);
        //        this.PopulateOutputFormats(outputFormats);
        //    }

        //    // throw new NotImplementedException();
        //}
    }
}
