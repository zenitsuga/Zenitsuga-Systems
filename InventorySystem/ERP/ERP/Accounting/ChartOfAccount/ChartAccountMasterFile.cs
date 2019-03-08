using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP.Accounting.ChartOfAccount
{
    public partial class ChartAccountMasterFile : Form
    {
        ClassFile.clsValidation cv = new ClassFile.clsValidation();
        public Form MainParent;
        public ChartAccountMasterFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccountType at = new AccountType();
            at.MdiParent = MainParent;
            at.Top = 0;
            at.Left = 0;
            if (!cv.CheckChildFormOpen(at.Name))
            {
                at.Show();
            }
        }

        private void ChartAccountMasterFile_Load(object sender, EventArgs e)
        {
            
        }
    }
}
