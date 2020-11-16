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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressSeparationRibbon));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.AddressSeparationGroup = this.Factory.CreateRibbonGroup();
            this.btnProcess = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.toggleFindMatch = this.Factory.CreateRibbonToggleButton();
            this.cbOutputFormat = this.Factory.CreateRibbonComboBox();
            this.tab1.SuspendLayout();
            this.AddressSeparationGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.AddressSeparationGroup);
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Name = "tab1";
            // 
            // AddressSeparationGroup
            // 
            resources.ApplyResources(ribbonDialogLauncherImpl1, "ribbonDialogLauncherImpl1");
            this.AddressSeparationGroup.DialogLauncher = ribbonDialogLauncherImpl1;
            this.AddressSeparationGroup.Items.Add(this.btnProcess);
            this.AddressSeparationGroup.Items.Add(this.separator1);
            this.AddressSeparationGroup.Items.Add(this.toggleFindMatch);
            this.AddressSeparationGroup.Items.Add(this.cbOutputFormat);
            resources.ApplyResources(this.AddressSeparationGroup, "AddressSeparationGroup");
            this.AddressSeparationGroup.Name = "AddressSeparationGroup";
            this.AddressSeparationGroup.DialogLauncherClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddressSeparationGroup_DialogLauncherClick);
            // 
            // btnProcess
            // 
            this.btnProcess.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            resources.ApplyResources(this.btnProcess, "btnProcess");
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.OfficeImageId = "ConvertTextToTable";
            this.btnProcess.ShowImage = true;
            this.btnProcess.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnProcess_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // toggleFindMatch
            // 
            resources.ApplyResources(this.toggleFindMatch, "toggleFindMatch");
            this.toggleFindMatch.Name = "toggleFindMatch";
            this.toggleFindMatch.OfficeImageId = "FindDialog";
            this.toggleFindMatch.ShowImage = true;
            this.toggleFindMatch.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleFindMatch_Click);
            // 
            // cbOutputFormat
            // 
            resources.ApplyResources(this.cbOutputFormat, "cbOutputFormat");
            this.cbOutputFormat.Name = "cbOutputFormat";
            this.cbOutputFormat.OfficeImageId = "CalloutOptions";
            this.cbOutputFormat.ShowImage = true;
            this.cbOutputFormat.ShowItemImage = false;
            this.cbOutputFormat.ShowLabel = false;
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
    }

    partial class ThisRibbonCollection
    {
        internal AddressSeparationRibbon Ribbon1
        {
            get { return this.GetRibbon<AddressSeparationRibbon>(); }
        }
    }
}
