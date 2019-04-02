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
    public partial class frm_TransactionCost : Form
    {
        clsDatabaseTransactions dtrans = new clsDatabaseTransactions();
        public string SelectedTransaction;
        public decimal PriceAmount;
        public int ID;

        public frm_TransactionCost()
        {
            InitializeComponent();
        }

        private void LoadRecords()
        {
            string query = "SELECT sysID as 'ID', Transaction_Name AS 'Transaction',Amount FROM tbl_condo_transactionlistinfo WHERE isenabled = 1";
            DataTable dtRecords = new DataTable();
            dtRecords = dtrans.SelectData(query);
            if (dtRecords.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtRecords;
            }
        }

        private void frm_TransactionCost_Load(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void insertWithPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedPrice = "0.00";
            selectedPrice = dataGridView1.SelectedRows[0].Cells["Amount"].Value.ToString();
            if (dataGridView1.SelectedRows.Count == 1)
            {
                SelectedTransaction = dataGridView1.SelectedRows[0].Cells["Transaction"].Value.ToString();
                PriceAmount = !string.IsNullOrEmpty(selectedPrice) ? decimal.Parse(selectedPrice) : decimal.Parse("0.00");
                ID = int.Parse(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                this.Close();
            }
        }
    }
}
