using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ERP.Accounting
{
    public partial class AccountingSetup : Form
    {
        ClassFile.clsValidation cv = new ClassFile.clsValidation();
        public Form parent;
        public AccountingSetup()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Chart of Accounts":
                    ChartOfAccount.ChartAccountMasterFile cam = new ChartOfAccount.ChartAccountMasterFile();
                    cam.MdiParent = parent;
                    cam.MainParent = parent;
                    cam.Left = 0;
                    cam.Top = 0;
                    if (!cv.CheckChildFormOpen(cam.Name))
                    {
                        cam.Show();
                    }
                        break;
            }
        }

        private void AccountingSetup_Load(object sender, EventArgs e)
        {

        }
    }
}
