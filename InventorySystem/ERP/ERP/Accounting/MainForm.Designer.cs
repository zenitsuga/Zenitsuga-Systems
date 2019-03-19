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
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.radSidePanel = new Telerik.WinControls.UI.RadPanel();
            this.SidePanelButton = new Telerik.WinControls.UI.RadButton();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSidePanel)).BeginInit();
            this.radSidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SidePanelButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
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
            this.radSidePanel.Location = new System.Drawing.Point(0, 0);
            this.radSidePanel.Name = "radSidePanel";
            this.radSidePanel.Size = new System.Drawing.Size(197, 399);
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
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 425);
            this.Controls.Add(this.radSidePanel);
            this.Controls.Add(this.radStatusStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = null;
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSidePanel)).EndInit();
            this.radSidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SidePanelButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadPanel radSidePanel;
        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private Telerik.WinControls.UI.RadButton SidePanelButton;
    }
}
