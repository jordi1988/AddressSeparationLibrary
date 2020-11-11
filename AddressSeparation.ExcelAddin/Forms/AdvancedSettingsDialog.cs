using AddressSeparation.Helper;
using AddressSeparation.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressSeparation.ExcelAddin
{
    public partial class AdvancedSettingsDialog : Form
    {
        /// <summary>
        /// Initialize the form
        /// </summary>
        public AdvancedSettingsDialog()
        {
            InitializeComponent();
            RegisterEvents();
            LoadInputManipulationOptions();
        }

        /// <summary>
        /// Get the checkbox-selected options
        /// </summary>
        /// <returns></returns>
        public List<DescriptionMapper> GetActiveInputManipulationOptions()
        {
            var output = new List<DescriptionMapper>();
            foreach (DataGridViewRow row in gridInputManipulations.Rows)
            {
                bool isSelected = (bool)(row.Cells["colActivate"]?.Value ?? false);
                if (isSelected)
                {
                    output.Add(new DescriptionMapper()
                    {
                        DisplayName = row.Cells["colName"].Value.ToString(),
                        Description = row.Cells["colDescription"].Value.ToString(),
                        Type = (Type)row.Cells["colType"].Value,
                    });
                }
            }
            return output;
        }

        /// <summary>
        /// Register events on this form
        /// </summary>
        private void RegisterEvents()
        {
            // commit changes that have not been committed before closing
            this.FormClosing += (object sender, FormClosingEventArgs e) =>
                            gridInputManipulations.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        /// <summary>
        /// Populate the grid view with options
        /// </summary>
        private void LoadInputManipulationOptions() 
        {
            if (gridInputManipulations.DataSource != null)
            {
                return;
            }

            gridInputManipulations.DataSource = InputManipulationHelper
                .GetMappings()
                .ToList();
        }

    }
}
