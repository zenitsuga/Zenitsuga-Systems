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
    public partial class frm_CutoffInfo : Form
    {
        clsValidation cv = new clsValidation();
        clsDatabaseTransactions dtrans = new clsDatabaseTransactions();
        public string QueryRecords;
        public int UserID = -1;

        public frm_CutoffInfo()
        {
            InitializeComponent();
        }

        private void LoadDefault()
        {
            tbYear.Text = DateTime.Now.Year.ToString();
            cbMonth.SelectedIndex = DateTime.Now.Month - 1;
            DateTime startDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
            DateTime startDateNextMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            int daysLastMonth = startDateNextMonth.AddDays(-1).Day;
            DateTime endDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,daysLastMonth);
            dtStart.Value = startDate;
            dtEnd.Value = endDate;
        }

        private void LoadRecords()
        {
            if (!string.IsNullOrEmpty(QueryRecords))
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtrans.SelectData(QueryRecords);
                dataGridView1.Refresh();
            }
        }

        private void frm_CutoffInfo_Load(object sender, EventArgs e)
        {
            LoadDefault();
            LoadRecords();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbYear.Text))
            {
                MessageBox.Show("Error: Invalid Year. Please check your year","Year Invalid.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tbYear.Focus();
                return;
            }
            if (!cv.isInteger(tbYear.Text))
            {
                MessageBox.Show("Error: Invalid Year. Please check your year","Year Invalid.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tbYear.Focus();
                return;
            }
            if (dtStart.Value > dtEnd.Value)
            {
                MessageBox.Show("Error: Date End must be greater than Date Start. Please check your Covered Date", "Year Invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtStart.Focus();
                return;
            }
            DialogResult drSaveConfirm = MessageBox.Show("Are you sure you want to save this cutoff for month : " + cbMonth.Text + " "+ tbYear.Text + "?","Confirm to Save Cut-off for " + cbMonth.Text + " " + tbYear.Text + "?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (drSaveConfirm == DialogResult.Yes)
            {
                string PrimaryKey = tbYear.Text + ":" + cbMonth.Text; //(int.Parse(cbMonth.SelectedIndex.ToString()) + 1);
                string QueryInsert = "Insert into tbl_condo_cutoffinfo(PrimaryKey,Year,Month,BillStart,BillEnd,isEnabled,CreatedBy)values('" + PrimaryKey +"'," + tbYear.Text + "," + (cbMonth.SelectedIndex + 1) + ",'" + dtStart.Value.ToString("yyyy-MM-dd 00:00:00") + "','" + dtEnd.Value.ToString("yyyy-MM-dd 00:00:00") + "'," + (cbIsEnabled.Checked ? 1 : 0) + "," + UserID + ")";
                dtrans.InsertData(QueryInsert);
                LoadRecords();
            }
        }
    }
}
