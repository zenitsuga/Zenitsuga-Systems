using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace ERP.Accounting
{
    public partial class AccountingSetup : Telerik.WinControls.UI.RadForm
    {
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
                    cam.Left = 0;
                    cam.Top = 0;
                    cam.Show();
                    break;
            }
        }

        private void AccountingSetup_Load(object sender, EventArgs e)
        {

        }
    }
}
