using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP.CONDO
{
    public partial class MainForm : Form
    {
        int sidepanelwidth;
        bool dashboardClick = true;
        string ProcessCode = string.Empty;
        bool islogoffUser = false;
        string QueryRecords = string.Empty;
        string Modules = string.Empty;
        ClassFile.clsDatabaseTransactions cdtrans = new ClassFile.clsDatabaseTransactions();
        string QueryTable = string.Empty;
        DataTable dtSearchResult = new DataTable();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sidepanelwidth = panel4.Width;
            splitContainer1.SplitterDistance = 220;
            DateTime dtTest = new DateTime(2019, 3, 30);
            
        }

        private void floorInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dashboardClick)
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    tabControl1.SelectedTab = tabPage1;
                }
            }
            else
            {
                dashboardClick = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
        //    if (button4.Text == "<<")
        //    {
        //        button4.Text = ">>";
        //    }
        //    else
        //    {
        //        button4.Text = "<<";
        //    }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //For Billing Menu
            dashboardClick = false;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void LoadRecords(string QUERY,string ModuleName)
        {
            dataGridView1.DataSource = null;
            try
            {
                DataTable dtShowRecords = cdtrans.SelectData(QUERY);
                dataGridView1.DataSource = dtShowRecords;
                dtSearchResult = dtShowRecords;
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error in Fetching Record :" + ModuleName);
            }
            if (dataGridView1.Rows.Count > 0)
            {
                cbSearchFilter.Items.Clear();
                foreach (DataGridViewColumn dc in dataGridView1.Columns)
                {
                    cbSearchFilter.Items.Add(dc.Name);
                }
            }
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void LoadRecords()
        {
            try
            {
                if (!string.IsNullOrEmpty(ProcessCode))
                {
                    switch (ProcessCode)
                    {
                        case "Floor_Info":
                            LoadRecords(QueryRecords, Modules);
                            break;
                        case "Unit_Info":
                            LoadRecords(QueryRecords, Modules);
                            break;
                        default:
                            break;
                    }
                    tbSearch.Enabled = false;
                }
            }
            catch
            {
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {   
                DialogResult drMSG = MessageBox.Show("Are you sure you want to Exit the Application?", "Confirm close application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drMSG == DialogResult.No)
                {
                    e.Cancel = true;
                    islogoffUser = false;
                }
                else
                {
                    Environment.Exit(0);
                }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            islogoffUser = true;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbSearchFilter.Text))
                {
                    string Filter = "[" + cbSearchFilter.Text + "] LIKE '" + tbSearch.Text + "*'";
                    if (!string.IsNullOrEmpty(tbSearch.Text))
                    {
                        DataTable dtDG = new DataTable();
                        dtDG = dtSearchResult;
                        DataView dv = dtDG.DefaultView;
                        dv.RowFilter = Filter;
                        dataGridView1.DataSource = dv.ToTable();
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dtSearchResult;
                        dataGridView1.Refresh();
                    }
                }
            }
            catch
            {
            }
        }

        private void cbSearchFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            tbSearch.Text = string.Empty;
            if (!string.IsNullOrEmpty(cbSearchFilter.Text))
            {
                tbSearch.Enabled = true;
            }
            else
            {
                tbSearch.Enabled = false;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            lblTitle.Text = "CUSTOMER INFORMATION";
            lblDescription.Text = "Configuration for All Customer information and details";
            dashboardClick = false;
            ProcessCode = "Customer_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "SELECT cu.sysid as 'ID',cu.LastName,cu.FirstName,cu.MiddleName,cu.Alias,cu.UseAlias,cu.Contactnumber,ui.UnitName,cu.Notes,cu.isTenant,concat(cuo.LastName , ',' , cuo.FirstName , ' ' , cuo.MiddleName) AS 'Owner Info'from tbl_CONDO_CustomerInfo cu LEFT JOIN tbl_CONDO_CustomerInfo cuo ON cu.CustomerRef = cuo.sysid LEFT JOIN tbl_CONDO_UnitInfo ui ON cu.UnitNo = ui.sysid LEFT JOIN tbl_SYSTEM_Users u ON cu.createdby = u.sysID LEFT Join tbl_SYSTEM_Users p ON cu.Updatedby = p.sysid WHERE cu.isEnabled = 1;";
            Modules = "Customer Information";
            LoadRecords(QueryRecords, Modules);
            QueryTable = "tbl_CONDO_CustomerInfo";
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            lblTitle.Text = "TENANT INFORMATION";
            lblDescription.Text = "Configuration for All Tenant information and details";
            dashboardClick = false;
            ProcessCode = "Tenant_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "SELECT cu.sysid as 'ID',cu.LastName,cu.FirstName,cu.MiddleName,cu.Alias,cu.UseAlias,cu.Contactnumber,ui.UnitName,cu.Notes,cu.isTenant,Concat(cuo.LastName , ',' , cuo.FirstName , ' ' , cuo.MiddleName) AS 'Owner Info' from tbl_CONDO_CustomerInfo cu LEFT JOIN tbl_CONDO_CustomerInfo cuo ON cu.CustomerRef = cuo.sysid LEFT JOIN tbl_CONDO_UnitInfo ui ON cu.UnitNo = ui.sysid LEFT JOIN tbl_SYSTEM_Users u ON cu.createdby = u.sysID LEFT Join tbl_SYSTEM_Users p ON cu.Updatedby = p.sysid WHERE cu.isEnabled = 1 and cu.isTenant = 1;";
            Modules = "Tenant Information";
            LoadRecords(QueryRecords, Modules);
            QueryTable = "tbl_CONDO_CustomerInfo";
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            lblTitle.Text = "UNIT INFORMATION";
            lblDescription.Text = "Configuration for All Unit/Room information";
            dashboardClick = false;
            ProcessCode = "Unit_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "Select ui.SysID as 'ID',ui.UnitName as 'Name',f.FloorName as 'Floor',ui.Description, ui.AreaSQM as 'Size',ui.MonthlyDue as 'Monthly Due per SQM',ui.TotalDue as 'Total Due' from tbl_CONDO_UnitInfo ui LEFT JOIN tbl_CONDO_FloorInfo f ON ui.FloorAssociate = f.sysid LEFT JOIN tbl_SYSTEM_Users u ON ui.createdby = u.sysID LEFT Join tbl_SYSTEM_Users p on ui.Updatedby = p.sysid WHERE ui.isEnabled = 1;";
            Modules = "Unit Information";
            LoadRecords(QueryRecords, Modules);
            QueryTable = "tbl_CONDO_UnitInfo";
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            lblTitle.Text = "FLOOR INFORMATION";
            lblDescription.Text = "Configuration for FLOOR LEVELS";
            dashboardClick = false;
            ProcessCode = "Floor_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "Select f.sysid as 'ID',f.FloorName as 'FLOOR',f.FloorDescription as 'DESCRIPTION',u.Username as 'CREATED BY',f.DateDefined as 'CREATION DATE',p.Username as 'UPDATED BY',f.LastDateDefined as 'UPDATED DATE' from tbl_CONDO_FloorInfo f LEFT JOIN tbl_SYSTEM_Users u ON f.userID = u.sysID LEFT Join tbl_SYSTEM_Users p on f.LastUpdateUser = p.sysid WHERE f.isEnabled = 1;";
            Modules = "Floor Information";
            LoadRecords(QueryRecords, Modules);
            QueryTable = "tbl_CONDO_FloorInfo";
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            QueryRecords = string.Empty;
            dashboardClick = true;
            tabControl1.SelectedTab = tabPage1;
        }
        //Add Records
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProcessCode))
            {
                switch (ProcessCode)
                {
                    case "Cutoff_Info":
                        frm_CutoffInfo co = new frm_CutoffInfo();
                        co.StartPosition = FormStartPosition.CenterScreen;
                        co.QueryRecords = QueryRecords;
                        co.ShowDialog();
                        LoadRecords(QueryRecords, Modules);
                        break;
                    case "Floor_Info":
                        frm_FloorInformation fi = new frm_FloorInformation();
                        fi.StartPosition = FormStartPosition.CenterScreen;
                        fi.ShowDialog();
                        LoadRecords(QueryRecords, Modules);
                        break;
                    case "Unit_Info":
                        frm_UnitInformation ui = new frm_UnitInformation();
                        ui.StartPosition = FormStartPosition.CenterScreen;
                        ui.ShowDialog();
                        LoadRecords(QueryRecords, Modules);
                        break;
                    case "Billing_Info":
                        frm_BillingInfo bi = new frm_BillingInfo();
                        bi.StartPosition = FormStartPosition.CenterScreen;
                        bi.ShowDialog();
                        break;
                    case "Tenant_Info":case "Customer_Info":
                        frm_CustomerInformation ci = new frm_CustomerInformation();
                        ci.StartPosition = FormStartPosition.CenterScreen;
                        ci.LoadQuery = QueryRecords;
                        ci.ShowDialog();
                        LoadRecords(QueryRecords, Modules);
                        break;
                    default:
                        break;
                }
                btnRefresh.PerformClick();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(QueryTable))
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    DialogResult drSelected = MessageBox.Show("Are you sure you want to modify this selected entry?", "Modify Entries", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drSelected == DialogResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(ProcessCode))
                        {
                            switch (ProcessCode)
                            {
                                case "":
                                    frm_BillingInfo bi = new frm_BillingInfo();
                                    bi.isUpdate = true;
                                    bi.ForUpdate_SysID = int.Parse(dataGridView1["ID", dataGridView1.SelectedRows[0].Index].Value.ToString());
                                    bi.SelectedDG = dataGridView1;
                                    bi.ShowDialog();
                                    break;
                                case "Floor_Info":
                                    frm_FloorInformation fi = new frm_FloorInformation();
                                    fi.isUpdate = true;
                                    fi.ForUpdate_SysID = int.Parse(dataGridView1["ID", dataGridView1.SelectedRows[0].Index].Value.ToString());
                                    fi.SelectedDG = dataGridView1;
                                    fi.ShowDialog();
                                    break;
                                case "Unit_Info":
                                    frm_UnitInformation ui = new frm_UnitInformation();
                                    ui.isUpdate = true;
                                    ui.ForUpdate_SysID = int.Parse(dataGridView1["ID", dataGridView1.SelectedRows[0].Index].Value.ToString());
                                    ui.SelectedDG = dataGridView1;
                                    ui.ShowDialog();
                                    break;
                                case "Customer_Info":
                                case "Tenant_Info":
                                    frm_CustomerInformation cu = new frm_CustomerInformation();
                                    cu.isUpdate = true;
                                    cu.ForUpdate_SysID = int.Parse(dataGridView1["ID", dataGridView1.SelectedRows[0].Index].Value.ToString());
                                    cu.SelectedDG = dataGridView1;
                                    cu.ShowDialog();
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Warning: Please select row first.", "Select only one row to", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                btnRefresh.PerformClick();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(QueryTable))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult drSelected = MessageBox.Show("Are you sure you want to delete this selected entry?", "Delete Entries", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drSelected == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dgrow in dataGridView1.SelectedRows)
                        {
                            string Query = "Delete from " + QueryTable + " where sysid=" + dgrow.Cells["ID"].Value.ToString();
                            cdtrans.InsertData(Query);
                        }
                        LoadRecords(QueryRecords, Modules);
                    }
                }
                else
                {
                    MessageBox.Show("Error: No data selected. Please highligh all row or click the left pane to select row.", "No Selected Row Define", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRecords(QueryRecords, Modules);
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            lblTitle.Text = "BILLING INFORMATION";
            lblDescription.Text = "Configuration for BILLING INFORMATION";
            dashboardClick = false;
            ProcessCode = "Billing_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "SELECT  bi.sysID as 'ID',cu.BillStart,cu.BillEnd,ci.LastName,ci.FirstName,ci.MiddleName,bi.TotalAmountDue,bi.PreviousBalanceAsOf,bi.LastPaymentEntry,bi.Balances,bi.CurrentCharges FROM tbl_condo_billinginfo bi LEFT JOIN  tbl_condo_cutoffinfo cu ON bi.CutoffID = cu.sysID LEFT JOIN tbl_condo_customerinfo ci ON bi.CustomerID = ci.sysID WHERE bi.isEnabled = 1;";
            Modules = "Billing Information";
            LoadRecords(QueryRecords, Modules);
            QueryTable = "tbl_CONDO_BillingInfo";
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "CUT-OFF INFORMATION";
            lblDescription.Text = "Configuration for CUT-OFF INFORMATION";
            dashboardClick = false;
            ProcessCode = "Cutoff_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "SELECT co.YEAR,co.MONTH,co.BILLSTART,co.BILLEND,CONCAT(cu.LastName,',',cu.FirstName) AS 'CREATED BY',co.CREATEDDATE,CONCAT(cu.LastName,',',cu.FirstName) AS 'MODIFY BY',co.MODIFIEDDATE FROM tbl_condo_cutoffinfo co LEFT JOIN tbl_condo_customerinfo cu ON co.CreatedBy = cu.sysid LEFT JOIN tbl_condo_customerinfo cus ON co.ModifyBy = cus.sysid";
            Modules = "Cut-off Information";
            LoadRecords(QueryRecords, Modules);
            QueryTable = "tbl_CONDO_cutoffInfo";
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "PAYMENT INFORMATION";
            lblDescription.Text = "Configuration for PAYMENT INFORMATION";
            dashboardClick = false;
            ProcessCode = "Payment_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "";
            Modules = "Billing Information";
            LoadRecords(QueryRecords, Modules);
            QueryTable = "tbl_CONDO_BillingInfo";
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            ViewSOA vs = new ViewSOA();
            vs.ShowDialog();
        }
    }
}
