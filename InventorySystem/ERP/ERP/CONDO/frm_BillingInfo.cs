using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.ClassFile;
namespace ERP.CONDO
{
    public partial class frm_BillingInfo : Form
    {
        public int UserID = -1;
        public bool isUpdate;
        public int ForUpdate_SysID;
        public DataGridView SelectedDG;
        public string LoadQuery;
        clsDatabaseTransactions dtrans = new clsDatabaseTransactions();
        DataTable dtSource;

        public frm_BillingInfo()
        {
            InitializeComponent();
        }

        private void frm_BillingInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
