namespace AddressSeparation.ExcelAddin
{
    partial class AddressSeparationRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AddressSeparationRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.AddressSeparationGroup = this.Factory.CreateRibbonGroup();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.cbOutputFormat = this.Factory.CreateRibbonComboBox();
            this.btnProcess = this.Factory.CreateRibbonButton();
            this.toggleFindMatch = this.Factory.CreateRibbonToggleButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.box1 = this.Factory.CreateRibbonBox();
            this.tab1.SuspendLayout();
            this.AddressSeparationGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.AddressSeparationGroup);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // AddressSeparationGroup
            // 
            ribbonDialogLauncherImpl1.ScreenTip = "Load custom output formats";
            ribbonDialogLauncherImpl1.SuperTip = "Select a folder containing an assembly with custom output formats.";
            ribbonDialogLauncherImpl1.Visible = false;
            this.AddressSeparationGroup.DialogLauncher = ribbonDialogLauncherImpl1;
            this.AddressSeparationGroup.Items.Add(this.btnProcess);
            this.AddressSeparationGroup.Items.Add(this.separator1);
            this.AddressSeparationGroup.Items.Add(this.toggleFindMatch);
            this.AddressSeparationGroup.Items.Add(this.cbOutputFormat);
            this.AddressSeparationGroup.Items.Add(this.separator2);
            this.AddressSeparationGroup.Items.Add(this.box1);
            this.AddressSeparationGroup.Label = "Address Separation";
            this.AddressSeparationGroup.Name = "AddressSeparationGroup";
            this.AddressSeparationGroup.DialogLauncherClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddressSeparationGroup_DialogLauncherClick);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // cbOutputFormat
            // 
            this.cbOutputFormat.Label = "Output Format";
            this.cbOutputFormat.Name = "cbOutputFormat";
            this.cbOutputFormat.OfficeImageId = "CalloutOptions";
            this.cbOutputFormat.ScreenTip = "Output Format";
            this.cbOutputFormat.ShowItemImage = false;
            this.cbOutputFormat.ShowLabel = false;
            this.cbOutputFormat.SizeString = "123456789012345678901234567890";
            this.cbOutputFormat.Text = null;
            // 
            // btnProcess
            // 
            this.btnProcess.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnProcess.Label = "Process";
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.OfficeImageId = "ConvertTextToTable";
            this.btnProcess.ScreenTip = "Process your address";
            this.btnProcess.ShowImage = true;
            this.btnProcess.SuperTip = "Select cells containing your addresses and process them. The output will be writt" +
    "en right next to the active cell.";
            this.btnProcess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnProcess_Click);
            // 
            // toggleFindMatch
            // 
            this.toggleFindMatch.Label = "Identify output formats";
            this.toggleFindMatch.Name = "toggleFindMatch";
            this.toggleFindMatch.OfficeImageId = "FindDialog";
            this.toggleFindMatch.ScreenTip = "Find formats matching your address";
            this.toggleFindMatch.ShowImage = true;
            this.toggleFindMatch.SuperTip = "Place your active cell onto an address and activate this button for repopulating " +
    "the list of matching output formats.";
            this.toggleFindMatch.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleFindMatch_Click);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // box1
            // 
            this.box1.Name = "box1";
            // 
            // AddressSeparationRibbon
            // 
            this.Name = "AddressSeparationRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.AddressSeparationGroup.ResumeLayout(false);
            this.AddressSeparationGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup AddressSeparationGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnProcess;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cbOutputFormat;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleFindMatch;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
    }

    partial class ThisRibbonCollection
    {
        internal AddressSeparationRibbon Ribbon1
        {
            get { return this.GetRibbon<AddressSeparationRibbon>(); }
        }
    }
}
