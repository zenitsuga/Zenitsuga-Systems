namespace ERP.Accounting
{
    partial class MainForm
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
            this.radRibbonBar1 = new Telerik.WinControls.UI.RadRibbonBar();
            this.ribbonTab3 = new Telerik.WinControls.UI.RibbonTab();
            this.ribbonTab4 = new Telerik.WinControls.UI.RibbonTab();
            this.ribbonTab6 = new Telerik.WinControls.UI.RibbonTab();
            this.ribbonTab5 = new Telerik.WinControls.UI.RibbonTab();
            this.ribbonTab2 = new Telerik.WinControls.UI.RibbonTab();
            this.Accounting_DASHBOARD = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup3 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.ribbonTab1 = new Telerik.WinControls.UI.RibbonTab();
            this.radRibbonBarGroup1 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radRibbonBarGroup2 = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.radSidePanel = new Telerik.WinControls.UI.RadPanel();
            this.SidePanelButton = new Telerik.WinControls.UI.RadButton();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSidePanel)).BeginInit();
            this.radSidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SidePanelButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radRibbonBar1
            // 
            this.radRibbonBar1.CommandTabs.AddRange(new Telerik.WinControls.RadItem[] {
            this.ribbonTab3,
            this.ribbonTab4,
            this.ribbonTab6,
            this.ribbonTab5,
            this.ribbonTab2,
            this.ribbonTab1});
            // 
            // 
            // 
            this.radRibbonBar1.ExitButton.Text = "Exit";
            this.radRibbonBar1.ExitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radRibbonBar1.Location = new System.Drawing.Point(0, 0);
            this.radRibbonBar1.Name = "radRibbonBar1";
            // 
            // 
            // 
            this.radRibbonBar1.OptionsButton.Text = "Options";
            this.radRibbonBar1.OptionsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // 
            // 
            this.radRibbonBar1.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.radRibbonBar1.Size = new System.Drawing.Size(664, 155);
            this.radRibbonBar1.TabIndex = 0;
            this.radRibbonBar1.Text = "MainForm";
            this.radRibbonBar1.ThemeName = "Office2007Black";
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.IsSelected = true;
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Text = "EMPLOYEE";
            // 
            // ribbonTab4
            // 
            this.ribbonTab4.Name = "ribbonTab4";
            this.ribbonTab4.Text = "TIME KEEPING";
            // 
            // ribbonTab6
            // 
            this.ribbonTab6.Name = "ribbonTab6";
            this.ribbonTab6.Text = "INVENTORY";
            // 
            // ribbonTab5
            // 
            this.ribbonTab5.Name = "ribbonTab5";
            this.ribbonTab5.Text = "PAYROLL";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.IsSelected = false;
            this.ribbonTab2.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.Accounting_DASHBOARD,
            this.radRibbonBarGroup3});
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Text = "ACCOUNTING";
            // 
            // Accounting_DASHBOARD
            // 
            this.Accounting_DASHBOARD.Name = "Accounting_DASHBOARD";
            this.Accounting_DASHBOARD.Text = "DASHBOARD";
            // 
            // radRibbonBarGroup3
            // 
            this.radRibbonBarGroup3.Name = "radRibbonBarGroup3";
            this.radRibbonBarGroup3.Text = "REPORTS";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.IsSelected = false;
            this.ribbonTab1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radRibbonBarGroup1,
            this.radRibbonBarGroup2});
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "MASTERFILE";
            // 
            // radRibbonBarGroup1
            // 
            this.radRibbonBarGroup1.Name = "radRibbonBarGroup1";
            this.radRibbonBarGroup1.Text = "COMPANY SET-UP";
            this.radRibbonBarGroup1.Click += new System.EventHandler(this.radRibbonBarGroup1_Click);
            // 
            // radRibbonBarGroup2
            // 
            this.radRibbonBarGroup2.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radRibbonBarGroup2.Name = "radRibbonBarGroup2";
            this.radRibbonBarGroup2.Text = "ACCOUNTING SET-UP";
            this.radRibbonBarGroup2.Click += new System.EventHandler(this.radRibbonBarGroup2_Click);
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 399);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(664, 26);
            this.radStatusStrip1.SizingGrip = false;
            this.radStatusStrip1.TabIndex = 1;
            this.radStatusStrip1.Text = "radStatusStrip1";
            // 
            // radSidePanel
            // 
            this.radSidePanel.AutoSize = true;
            this.radSidePanel.Controls.Add(this.SidePanelButton);
            this.radSidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.radSidePanel.Location = new System.Drawing.Point(0, 155);
            this.radSidePanel.Name = "radSidePanel";
            this.radSidePanel.Size = new System.Drawing.Size(197, 244);
            this.radSidePanel.TabIndex = 3;
            this.radSidePanel.ThemeName = "Office2007Black";
            // 
            // SidePanelButton
            // 
            this.SidePanelButton.Location = new System.Drawing.Point(166, 3);
            this.SidePanelButton.Name = "SidePanelButton";
            this.SidePanelButton.Size = new System.Drawing.Size(30, 24);
            this.SidePanelButton.TabIndex = 4;
            this.SidePanelButton.Text = "<<";
            this.SidePanelButton.Click += new System.EventHandler(this.SidePanelButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 425);
            this.Controls.Add(this.radSidePanel);
            this.Controls.Add(this.radStatusStrip1);
            this.Controls.Add(this.radRibbonBar1);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSidePanel)).EndInit();
            this.radSidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SidePanelButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadRibbonBar radRibbonBar1;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RibbonTab ribbonTab1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup1;
        private Telerik.WinControls.UI.RibbonTab ribbonTab2;
        private Telerik.WinControls.UI.RibbonTab ribbonTab3;
        private Telerik.WinControls.UI.RibbonTab ribbonTab4;
        private Telerik.WinControls.UI.RibbonTab ribbonTab6;
        private Telerik.WinControls.UI.RibbonTab ribbonTab5;
        private Telerik.WinControls.UI.RadRibbonBarGroup Accounting_DASHBOARD;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup2;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroup3;
        private Telerik.WinControls.UI.RadPanel radSidePanel;
        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private Telerik.WinControls.UI.RadButton SidePanelButton;
    }
}
