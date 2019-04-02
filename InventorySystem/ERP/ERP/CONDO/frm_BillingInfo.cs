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
        public decimal TotalAmount;
        public int UserID = -1;
        public bool isUpdate;
        public int ForUpdate_SysID;
        public DataGridView SelectedDG;
        public string LoadQuery;
        clsDatabaseTransactions dtrans = new clsDatabaseTransactions();
        DataTable dtRoomInfo;
        DataTable dtCutoff;
        DataTable dtTransactions;
        public frm_BillingInfo()
        {
            InitializeComponent();
        }

        private void TransactionTable()
        {
            try
            {
                dtTransactions = new DataTable();
                DataColumn dcID = new DataColumn("ID", typeof(int));
                dtTransactions.Columns.Add(dcID);
                DataColumn dcAmount = new DataColumn("Amount", typeof(decimal));
                dtTransactions.Columns.Add(dcAmount);
                DataColumn dcTransaction = new DataColumn("Transaction", typeof(string));
                dtTransactions.Columns.Add(dcTransaction);
                
            }
            catch
            {
            }
        }

        private void loadRoomNumber()
        {
            string QueryRoom = "SELECT ui.UnitName,fi.FloorName,ui.AreaSQM,ui.isMontlyDueComputed,ui.MonthlyDue,ui.TotalDue FROM tbl_condo_unitinfo ui LEFT JOIN tbl_condo_floorinfo fi ON ui.FloorAssociate = fi.sysid WHERE ui.isEnabled = 1  ORDER BY ui.UnitName asc";
            dtRoomInfo = dtrans.SelectData(QueryRoom);
            if (dtRoomInfo.Rows.Count > 0)
            {
                foreach (DataRow dgr in dtRoomInfo.Rows)
                {
                    cbUnitNo.Items.Add(dgr["UnitName"].ToString());

                }
            }
        }
        private void loadCutoff()
        {
            string QueryCutoff = "SELECT co.PrimaryKey,co.YEAR,co.MONTH,co.BILLSTART,co.BILLEND,CONCAT(cu.LastName,',',cu.FirstName) AS 'CREATED BY',co.CREATEDDATE,CONCAT(cu.LastName,',',cu.FirstName) AS 'MODIFY BY',co.MODIFIEDDATE FROM tbl_condo_cutoffinfo co LEFT JOIN tbl_condo_customerinfo cu ON co.CreatedBy = cu.sysid LEFT JOIN tbl_condo_customerinfo cus ON co.ModifyBy = cus.sysid WHERE co.isenabled = 1";
            dtCutoff = dtrans.SelectData(QueryCutoff);
            if (dtCutoff.Rows.Count > 0)
            {
                foreach (DataRow dgr in dtCutoff.Rows)
                {
                    cbCutoff.Items.Add(dgr["PrimaryKey"].ToString());
                }
            }
        }

        private void frm_BillingInfo_Load(object sender, EventArgs e)
        {
            loadRoomNumber();
            loadCutoff();
            TransactionTable();
            dataGridView1.DataSource = dtTransactions;
            TotalAmount = decimal.Parse("0.00");
        }

        private void cbUnitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbUnitNo.Text))
            {
                string Query = "SELECT  concat(ci.LastName,',',ci.FirstName,' ',ci.MiddleName) AS 'Customer', ui.AreaSQM,ui.MonthlyDue,ui.TotalDue FROM tbl_condo_customerinfo ci LEFT JOIN tbl_condo_unitinfo ui on ci.UnitNo = ui.SysID WHERE ui.UnitName = '"+ cbUnitNo.Text +"'";
                DataTable dtResult = dtrans.SelectData(Query);
                if (dtResult.Rows.Count > 0)
                {
                    tbName.Text = dtResult.Rows[0]["Customer"].ToString();
                    tbArea.Text = dtResult.Rows[0]["AreaSQM"].ToString();
                    tbSQM.Text = dtResult.Rows[0]["MonthlyDue"].ToString();
                    tbMonthlyDue.Text = dtResult.Rows[0]["TotalDue"].ToString();
                }
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
        }

        private void cbCutoff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtCutoff.Rows.Count > 0)
            {
                DataView dvCutoff = new DataView(dtCutoff);
                dvCutoff.RowFilter = "PrimaryKey = '" + cbCutoff.Text + "'";
                if (dvCutoff.Count > 0)
                {
                    tbStartBillDate.Text = dvCutoff.ToTable().Rows[0]["BILLSTART"].ToString();
                    tbEndDate.Text = dvCutoff.ToTable().Rows[0]["BILLEND"].ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_TransactionCost tc = new frm_TransactionCost();
            tc.ShowDialog();
            string Transaction = string.Empty;
            string Amount = "0.00";
            if (!string.IsNullOrEmpty(tc.SelectedTransaction))
            {
                Transaction = tc.SelectedTransaction;
                if (tc.PriceAmount.ToString() == "0.00" && (tc.SelectedTransaction.ToUpper() == "MONTHLY DUES" || tc.SelectedTransaction.ToUpper() == "MONTHLY DUE"))
                {
                    Amount = tbMonthlyDue.Text;
                    Transaction += " For Month : " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("yyyy");
                }
                else
                {
                    Amount = tc.PriceAmount.ToString();
                    Transaction += " : " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("yyyy");
                }
                if (Amount == "0.00")
                {   
                    frm_ZeroAmount za = new frm_ZeroAmount();
                    za.ShowDialog();
                    Amount = za.AmountEnteted.ToString();
                }

                DataRow[] drFind = dtTransactions.Select("Transaction ='" + Transaction + "'");

                if (drFind.Count() > 0)
                {
                    MessageBox.Show("Error: Entry Already Inserted. Please select another transaction","Transaction Exists",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                DataRow dr = dtTransactions.NewRow();
                dr["ID"] = tc.ID;
                dr["Amount"] = Amount;
                dr["Transaction"] = Transaction;


                dtTransactions.Rows.Add(dr);

            }
            dataGridView1.DataSource = dtTransactions;
            dataGridView1.Refresh();
            ComputeTotal();
        }

        private void ComputeTotal()
        {
            TotalAmount = decimal.Parse("0.00");
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    if(!string.IsNullOrEmpty(dgr.Cells["Transaction"].Value.ToString()))
                    {
                        TotalAmount += decimal.Parse(dgr.Cells["Amount"].Value.ToString());
                    }
                }
            }
            if (dataGridView2.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView2.Rows)
                {
                    if (!string.IsNullOrEmpty(dgr.Cells["Transactions"].Value.ToString()))
                    {
                        TotalAmount += decimal.Parse(dgr.Cells["Amount"].Value.ToString());
                    }
                }
            }
            tbTotalAmount.Text = TotalAmount.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
