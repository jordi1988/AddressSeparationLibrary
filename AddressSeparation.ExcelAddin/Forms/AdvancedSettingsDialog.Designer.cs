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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.gridInputManipulations = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridInputManipulations)).BeginInit();
            this.SuspendLayout();
            // 
            // gridInputManipulations
            // 
            this.gridInputManipulations.AllowUserToAddRows = false;
            this.gridInputManipulations.AllowUserToDeleteRows = false;
            this.gridInputManipulations.AllowUserToResizeColumns = false;
            this.gridInputManipulations.AllowUserToResizeRows = false;
            this.gridInputManipulations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.gridInputManipulations.Location = new System.Drawing.Point(2, 1);
            this.gridInputManipulations.MultiSelect = false;
            this.gridInputManipulations.Name = "gridInputManipulations";
            this.gridInputManipulations.RowHeadersVisible = false;
            this.gridInputManipulations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridInputManipulations.Size = new System.Drawing.Size(668, 167);
            this.gridInputManipulations.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selected options will stay active until this dialog is opened again.";
            // 
            // colName
            // 
            this.colName.DataPropertyName = "DisplayName";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 200;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Description";
            this.colDescription.MinimumWidth = 200;
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colType
            // 
            this.colType.DataPropertyName = "Type";
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colType.Visible = false;
            this.colType.Width = 5;
            // 
            // colActivate
            // 
            this.colActivate.HeaderText = "Activate";
            this.colActivate.Name = "colActivate";
            this.colActivate.Width = 75;
            // 
            // AdvancedSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 193);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridInputManipulations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AdvancedSettingsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Input manipulations";
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