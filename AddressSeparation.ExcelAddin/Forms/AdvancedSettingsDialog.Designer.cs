namespace AddressSeparation.ExcelAddin
{
    partial class AdvancedSettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSettingsDialog));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.gridInputManipulations = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridInputManipulations)).BeginInit();
            this.SuspendLayout();
            // 
            // gridInputManipulations
            // 
            this.gridInputManipulations.AllowUserToAddRows = false;
            this.gridInputManipulations.AllowUserToDeleteRows = false;
            this.gridInputManipulations.AllowUserToResizeColumns = false;
            this.gridInputManipulations.AllowUserToResizeRows = false;
            resources.ApplyResources(this.gridInputManipulations, "gridInputManipulations");
            this.gridInputManipulations.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridInputManipulations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridInputManipulations.CausesValidation = false;
            this.gridInputManipulations.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridInputManipulations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInputManipulations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDescription,
            this.colType,
            this.colActivate});
            this.gridInputManipulations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridInputManipulations.GridColor = System.Drawing.SystemColors.Control;
            this.gridInputManipulations.MultiSelect = false;
            this.gridInputManipulations.Name = "gridInputManipulations";
            this.gridInputManipulations.RowHeadersVisible = false;
            this.gridInputManipulations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "DisplayName";
            resources.ApplyResources(this.colName, "colName");
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.DataPropertyName = "Description";
            resources.ApplyResources(this.colDescription, "colDescription");
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colType
            // 
            this.colType.DataPropertyName = "Type";
            resources.ApplyResources(this.colType, "colType");
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colActivate
            // 
            resources.ApplyResources(this.colActivate, "colActivate");
            this.colActivate.Name = "colActivate";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // AdvancedSettingsDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridInputManipulations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AdvancedSettingsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gridInputManipulations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataGridView gridInputManipulations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colActivate;
    }
}