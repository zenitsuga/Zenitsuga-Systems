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
        public decimal PreviousBalance;
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
                DataColumn dcManualNotes = new DataColumn("ManualNotes", typeof(string));
                dtTransactions.Columns.Add(dcManualNotes);
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
                string Query = "SELECT ci.sysid,concat(ci.LastName,',',ci.FirstName,' ',ci.MiddleName) AS 'Customer', ui.AreaSQM,ui.MonthlyDue,ui.TotalDue FROM tbl_condo_customerinfo ci LEFT JOIN tbl_condo_unitinfo ui on ci.UnitNo = ui.SysID WHERE ui.UnitName = '"+ cbUnitNo.Text +"'";
                DataTable dtResult = dtrans.SelectData(Query);
                if (dtResult.Rows.Count > 0)
                {
                    lblCustomerID.Text = dtResult.Rows[0]["sysid"].ToString();
                    tbName.Text = dtResult.Rows[0]["Customer"].ToString();
                    tbArea.Text = dtResult.Rows[0]["AreaSQM"].ToString();
                    tbSQM.Text = dtResult.Rows[0]["MonthlyDue"].ToString();
                    tbMonthlyDue.Text = dtResult.Rows[0]["TotalDue"].ToString();
                    PreviousBalance = GetPreviousBalance(lblCustomerID.Text);
                }
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
        }

        private void cbCutoff_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            if (dtCutoff.Rows.Count > 0)
            {
                DataView dvCutoff = new DataView(dtCutoff);
                dvCutoff.RowFilter = "PrimaryKey = '" + cbCutoff.Text + "'";
                if (dvCutoff.Count > 0)
                {
                    tbStartBillDate.Text = dvCutoff.ToTable().Rows[0]["BILLSTART"].ToString();
                    tbEndDate.Text = dvCutoff.ToTable().Rows[0]["BILLEND"].ToString();
                    PreviousBalance = GetPreviousBalance(lblCustomerID.Text);
                }
            }
        }

        private decimal convertMoney(string value)
        {
            decimal result = decimal.Parse("0.00");
            try
            {
                result = decimal.Parse(value);
            }
            catch
            {
            }
            return result;
        }

        private decimal GetPreviousBalance(string customerID)
        {
            decimal result = decimal.Parse("0.00");
            try
            {
                string PreviousQuery = "SELECT bi.PrimaryKey as 'TransactionCode', ci.BillStart, ci.BillEnd,cu.PrimaryNames,bi.TotalAmountDue,bi.LastPaymentEntry,bi.Balances from tbl_condo_billinginfo bi LEFT JOIN tbl_condo_cutoffinfo ci ON bi.CutoffID = ci.sysid LEFT JOIN tbl_condo_customerinfo cu ON bi.CustomerID = cu.sysid WHERE (bi.LastPaymentEntry IS NULL OR bi.LastPaymentEntry = 0.00) and cu.sysid = " + customerID + " order by bi.sysID desc";
               
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = dtrans.SelectData(PreviousQuery);
               
                if (dataGridView2.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgrow in dataGridView2.Rows)
                    {
                        result += convertMoney(dgrow.Cells["TotalAmountDue"].Value.ToString());
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_TransactionCost tc = new frm_TransactionCost();
            tc.ShowDialog();
            string Transaction = string.Empty;
            string ManualNotes = string.Empty;
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
                    Amount = za.AmountEntered.ToString();
                    ManualNotes = za.ManualNotes;
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
                //if(!string.IsNullOrEmpty(ManualNotes))
                dr["ManualNotes"] = ManualNotes;

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
            
            lblCurrenttotal.Text = TotalAmount.ToString();
            tbTotalAmount.Text = TotalAmount.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private int GetLastIDofBillingDetails(string BillingRefID)
        {
            clsValidation cv = new clsValidation();
            int result = 0;
            try
            {
                string Query = "SELECT sysID from tbl_condo_billingdetails where BillingRefID = "+ BillingRefID +" order by sysID DESC LIMIT 1";
                DataTable dtResult = dtrans.SelectData(Query);
                string ResultData = string.Empty;

                if (dtResult.Rows.Count > 0)
                    ResultData = dtResult.Rows[0]["sysId"].ToString();

                if (ResultData == "")
                {
                    ResultData = "1";
                }

                if (cv.isInteger(ResultData))
                {
                    return int.Parse(ResultData) + 1;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        private int GetLastIDofBilling()
        {
            clsValidation cv = new clsValidation();
            int result = 0;
            try
            {
                string Query = "SELECT sysID from tbl_condo_billinginfo order by sysID DESC LIMIT 1";
                DataTable dtResult = dtrans.SelectData(Query);
                string ResultData = string.Empty;
                
                if(dtResult.Rows.Count > 0)
                ResultData = dtResult.Rows[0]["sysId"].ToString();

                if (ResultData == "")
                {
                    ResultData = "1";
                }

                if (cv.isInteger(ResultData))
                {
                    return int.Parse(ResultData);    
                }
            }
            catch(Exception ex)
            {
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsValidation cv = new clsValidation();

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Error: No Details to save on this Billing. Please check","Cannot save Billing Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (cbCutoff.Text == "")
            {
                MessageBox.Show("Error: No Cut-off defined on this Billing. Please check", "Cannot save Billing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to save this Billing Information?", "Confirm Saving Billing Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string PrimaryKey = GetLastIDofBilling().ToString("000000000000");
                string BillingInfoQuery = "Insert into tbl_condo_billinginfo(PrimaryKey,CutoffID,CustomerID,TotalAmountDue,PreviousBalanceAsOf,CurrentCharges)Values('" + PrimaryKey + "'," + cv.isInteger(cv.GetSysID("where PrimaryKey ='" + cbCutoff.Text.ToString() + "'", "tbl_condo_cutoffinfo").ToString()) + "," + lblCustomerID.Text + "," + tbTotalAmount.Text + "," + PreviousBalance + "," + lblCurrenttotal.Text + ")";
                bool isErrorFound = false;
                if (dtrans.InsertData(BillingInfoQuery))
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                       
                        string GetSySID = cv.GetSysID("where PrimaryKey='" + PrimaryKey + "'", "tbl_condo_billinginfo").ToString();
                        if (GetSySID == "0")
                        {
                            MessageBox.Show("Error: Cannot save billing. Please check", "Error Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string DeleteDetails = "Delete from tbl_condo_billinginfo where primarykey='" + PrimaryKey + "'";
                            dtrans.InsertData(DeleteDetails);
                            isErrorFound = true;
                            return;
                        }
                        foreach (DataGridViewRow dgrow in dataGridView1.Rows)
                        {
                            string GetManualNotes = string.IsNullOrEmpty(dgrow.Cells["ManualNotes"].Value.ToString()) ? dgrow.Cells["ManualNotes"].Value.ToString() : string.Empty;
                            string BillingRefID = GetLastIDofBillingDetails(GetSySID).ToString();
                            string BillingDetailsQuery = "Insert into tbl_condo_billingdetails(PrimaryKey,BillingRefID,BillingDescription,BillingAmount,BillingNotes)values('" + PrimaryKey + "_" + GetSySID + "'," + GetSySID + ",'" + dgrow.Cells["Transaction"].Value.ToString() + "'," + dgrow.Cells["Amount"].Value.ToString() + ",'" + GetManualNotes + "')";
                            if (!dtrans.InsertData(BillingDetailsQuery))
                            {
                                MessageBox.Show("Error: Cannot save billing details. Please check", "Error Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                string DeleteDetails = "Delete from tbl_condo_billingdetails where billingRefID=" + GetSySID;
                                dtrans.InsertData(DeleteDetails);
                                isErrorFound = true;
                                break;
                            }
                        }
                    }
                    if(!isErrorFound)
                    MessageBox.Show("Billing information saved", "Done Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cbUnitNo_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }
    }
}
