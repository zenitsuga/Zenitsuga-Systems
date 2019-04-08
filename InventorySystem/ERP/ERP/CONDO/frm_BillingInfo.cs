using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.ClassFile;
using Microsoft.Reporting.WinForms;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
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
            string QueryRoom = "SELECT ui.sysID as 'ID',ui.UnitName,fi.FloorName,ui.AreaSQM,ui.isMontlyDueComputed,ui.MonthlyDue,ui.TotalDue FROM tbl_condo_unitinfo ui LEFT JOIN tbl_condo_floorinfo fi ON ui.FloorAssociate = fi.sysid WHERE ui.isEnabled = 1  ORDER BY ui.UnitName asc";
            dtRoomInfo = dtrans.SelectData(QueryRoom);
            if (dtRoomInfo.Rows.Count > 0)
            {
                cbUnitNo.DataSource = dtRoomInfo;
                cbUnitNo.ValueMember = "ID";
                cbUnitNo.DisplayMember = "UnitName";
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
                    tbPrevCharge.Text = PreviousBalance.ToString();
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
                    tbPrevCharge.Text = PreviousBalance.ToString();
                }
            }
        }

        private void CheckPreviousCharges(string Query)
        {
            try
            {
                if(Query.ToUpper() == "ALL")
                {

                }
            }
            catch
            {
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
            if (cbCutoff.Text == "" || cbUnitNo.Text == "")
            {
                MessageBox.Show("Error: Cannot add transaction. Please select first customer","No Customer Selected",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


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
                    ManualNotes = tc.ManualNotes;
                }
                else
                {
                    Amount = tc.PriceAmount.ToString();
                    Transaction += " : " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("yyyy");
                    ManualNotes = tc.ManualNotes;

                }

                //if (Amount == "0.00")
                //{   
                //    frm_ZeroAmount za = new frm_ZeroAmount();
                //    za.ShowDialog();
                //    Amount = za.AmountEntered.ToString();
                //    ManualNotes = za.ManualNotes;
                //}

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
                dr["ManualNotes"] = string.IsNullOrEmpty(ManualNotes) ? string.Empty:ManualNotes;

                dtTransactions.Rows.Add(dr);

            }
            dataGridView1.DataSource = null;
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
            tbTotalAmount.Text = (Decimal.Parse(TotalAmount.ToString()) + Decimal.Parse(tbPrevCharge.Text)).ToString();

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
                    return 1;
                }else if (cv.isInteger(ResultData))
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
                    return int.Parse(ResultData);
                }

                if (cv.isInteger(ResultData))
                {
                    return int.Parse(ResultData) + 1;    
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
                string checkUnitCutoff = "SELECT * FROM tbl_condo_billinginfo WHERE cutoffID = " + cv.GetSysID("where PrimaryKey ='" + cbCutoff.Text.ToString() + "'", "tbl_condo_cutoffinfo") + " AND unitno = " + cbUnitNo.SelectedValue;
                bool isUnitCheckAgainstCutoff = dtrans.SelectData(checkUnitCutoff).Rows.Count > 0 ? true : false;
                if (isUnitCheckAgainstCutoff)
                {
                    MessageBox.Show("Error: Cannot save details for this cut-off. Please check", "Cut-off already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string BillingInfoQuery = "Insert into tbl_condo_billinginfo(PrimaryKey,CutoffID,CustomerID,UnitNo,TotalAmountDue,PreviousBalanceAsOf,CurrentCharges,CreatedBy)Values('" + PrimaryKey + "'," + cv.isInteger(cv.GetSysID("where PrimaryKey ='" + cbCutoff.Text.ToString() + "'", "tbl_condo_cutoffinfo").ToString()) + "," + lblCustomerID.Text + "," + cbUnitNo.SelectedValue + "," + tbTotalAmount.Text + "," + PreviousBalance + "," + lblCurrenttotal.Text + "," + UserID + ")";
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
                            string BillingDetailsQuery = "Insert into tbl_condo_billingdetails(PrimaryKey,BillingRefID,BillingDescription,BillingAmount,BillingNotes,CreatedBy)values('" + PrimaryKey + "_" + BillingRefID + "'," + GetSySID + ",'" + dgrow.Cells["Transaction"].Value.ToString() + "'," + dgrow.Cells["Amount"].Value.ToString() + ",'" + GetManualNotes + "'," + UserID + ")";
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
                    TransactionTable();
                    dataGridView1.DataSource = dtTransactions;

                }
            }
        }

        private void cbUnitNo_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            TransactionTable();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string file = Environment.CurrentDirectory + "\\CONDO\\Reporting\\SOA.pdf";

            PreparePDF_SOA(file);

            if (this.webBrowser1 != null)
            {
                this.webBrowser1.Navigate(file);
            }
        }

        private void PreparePDF_SOA(string OutputPath)
        {
            try
            {
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }

                Document doc = new Document(PageSize.A4);
                var output = new FileStream(OutputPath, FileMode.Create);
                var writer = PdfWriter.GetInstance(doc, output);

                doc.Open();

                //var barcode = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ABsIS_Logo.jpg"));
                //barcode.SetAbsolutePosition(430, 770);
                //barcode.ScaleAbsoluteHeight(30);
                //barcode.ScaleAbsoluteWidth(70);
                //doc.Add(barcode);

                PdfPTable table1 = new PdfPTable(1);
                table1.DefaultCell.Border = 0;
                table1.WidthPercentage = 100;


                PdfPCell cell11 = new PdfPCell();
                cell11.Colspan = 1;
                cell11.AddElement(new Paragraph("Transaction No."));

                cell11.AddElement(new Paragraph("Thankyou for shoping at ABC traders,your order details are below"));


                cell11.VerticalAlignment = Element.ALIGN_LEFT;

                PdfPCell cell12 = new PdfPCell();


                cell12.VerticalAlignment = Element.ALIGN_CENTER;


                table1.AddCell(cell11);

                table1.AddCell(cell12);


                PdfPTable table2 = new PdfPTable(3);

                //One row added

                PdfPCell cell21 = new PdfPCell();

                cell21.AddElement(new Paragraph("Photo Type"));

                PdfPCell cell22 = new PdfPCell();

                cell22.AddElement(new Paragraph("No. of Copies"));

                PdfPCell cell23 = new PdfPCell();

                cell23.AddElement(new Paragraph("Amount"));


                table2.AddCell(cell21);

                table2.AddCell(cell22);

                table2.AddCell(cell23);


                //New Row Added

                PdfPCell cell31 = new PdfPCell();

                cell31.AddElement(new Paragraph("Safe"));

                cell31.FixedHeight = 300.0f;

                PdfPCell cell32 = new PdfPCell();

                cell32.AddElement(new Paragraph("2"));

                cell32.FixedHeight = 300.0f;

                PdfPCell cell33 = new PdfPCell();

                cell33.AddElement(new Paragraph("20.00 * " + "2" + " = " + (20 * Convert.ToInt32("2")) + ".00"));

                cell33.FixedHeight = 300.0f;



                table2.AddCell(cell31);

                table2.AddCell(cell32);

                table2.AddCell(cell33);


                PdfPCell cell2A = new PdfPCell(table2);

                cell2A.Colspan = 2;

                table1.AddCell(cell2A);

                PdfPCell cell41 = new PdfPCell();

                cell41.AddElement(new Paragraph("Name : " + "ABC"));

                cell41.AddElement(new Paragraph("Advance : " + "advance"));

                cell41.VerticalAlignment = Element.ALIGN_LEFT;

                PdfPCell cell42 = new PdfPCell();

                cell42.AddElement(new Paragraph("Customer ID : " + "011"));

                cell42.AddElement(new Paragraph("Balance : " + "3993"));

                cell42.VerticalAlignment = Element.ALIGN_RIGHT;


                table1.AddCell(cell41);

                table1.AddCell(cell42);


                doc.Add(table1);

                doc.Close();
            }
            catch
            {
            }
        }
    }
}
