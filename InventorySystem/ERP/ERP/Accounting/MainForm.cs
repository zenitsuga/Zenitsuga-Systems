using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ERP.Accounting
{
    public partial class MainForm : Telerik.WinControls.UI.RadRibbonForm
    {
        ClassFile.clsValidation cv = new ClassFile.clsValidation();
        public MainForm()
        {
            InitializeComponent();
        }

        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    DialogResult dr = MessageBox.Show("Are you sure you want to close the Application?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (dr == System.Windows.Forms.DialogResult.No)
        //    {
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        Environment.Exit(0);
        //    }
        //}

        //private void radRibbonBarGroup1_Click(object sender, EventArgs e)
        //{
        //    Company_Set_up csu = new Company_Set_up();
        //    csu.MdiParent = this;
        //    csu.StartPosition = FormStartPosition.Manual;
        //    csu.Left = 0;
        //    csu.Top = 0;
        //    if(!cv.CheckChildFormOpen(csu.Name))
        //    {
        //        csu.Show();
        //    }
        //}

        //private void radRibbonBarGroup2_Click(object sender, EventArgs e)
        //{
        //    AccountingSetup asp = new AccountingSetup();
        //    asp.MdiParent = this;
        //    asp.parent = this;
        //    if (!cv.CheckChildFormOpen(asp.Name))
        //    {
        //        asp.Show();
        //    }
        //}

        //private void MainForm_Load(object sender, EventArgs e)
        //{
           
        //}

        //private void SidePanelButton_Click(object sender, EventArgs e)
        //{
        //    if (SidePanelButton.Text == ">>")
        //    {
        //        SidePanelButton.Text = "<<";
        //        radSidePanel.Width = 197;
        //        SidePanelButton.Left = SidePanelButton.Left + 160; 
        //    }
        //    else
        //    {
        //        SidePanelButton.Text = ">>";
        //        SidePanelButton.Left = SidePanelButton.Left - 160;
        //        radSidePanel.Width = 40;
        //    }
        //}

        //private void radRibbonBar1_Click(object sender, EventArgs e)
        //{

        //}
    }
}
