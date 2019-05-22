using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.ClassFile;
using System.Threading;
namespace ERP.CONDO
{
    public partial class frm_FloorInformation : Form
    {
        public int UserID =-1;
        public bool isUpdate;
        public int ForUpdate_SysID;
        public DataGridView SelectedDG;
        DataTable dtData;
        clsDatabaseTransactions dtrans;

        public frm_FloorInformation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void frm_FloorInformation_Load(object sender, EventArgs e)
        {
            dtrans = new clsDatabaseTransactions();
            dtData = new DataTable();
            ConstructDataTable();
            dataGridView1.DataSource = dtData;
            if (isUpdate)
            {
                if (SelectedDG.SelectedRows.Count == 1)
                {
                    tbFloorName.Text = SelectedDG.SelectedRows[0].Cells["Floor"].Value.ToString();
                    tbDescription.Text = SelectedDG.SelectedRows[0].Cells["Description"].Value.ToString();
                    button2.Enabled = false;
                }
            }
            if (dataGridView1.Rows.Count == 0)
            {
                LoadRecords();
            }
        }
        private void LoadRecords()
        {
           string QueryRecords = "Select f.sysid as 'ID',f.FloorName as 'FLOOR',f.FloorDescription as 'DESCRIPTION',u.Username as 'CREATED BY',f.DateDefined as 'CREATION DATE',p.Username as 'UPDATED BY',f.LastDateDefined as 'UPDATED DATE' from tbl_CONDO_FloorInfo f LEFT JOIN tbl_system_users u ON f.userID = u.sysID LEFT Join tbl_system_users p on f.LastUpdateUser = p.sysid WHERE f.isEnabled = 1;";
           dataGridView1.DataSource = dtrans.SelectData(QueryRecords);
        }
        private void ConstructDataTable()
        {
            DataColumn dcFloorName = new DataColumn("FloorName", typeof(string));
            dtData.Columns.Add(dcFloorName);
            DataColumn dcFloorDescription = new DataColumn("FloorDescription", typeof(string));
            dtData.Columns.Add(dcFloorDescription);
            DataColumn dcIsEnabled = new DataColumn("IsEnabled", typeof(bool));
            dtData.Columns.Add(dcIsEnabled);
            DataColumn dcIsSaved = new DataColumn("IsSaved", typeof(bool));
            dtData.Columns.Add(dcIsSaved);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (isUpdate)
                {
                    DialogResult drResult = MessageBox.Show("Are you sure you want to update the entry?", "Saving Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drResult == DialogResult.Yes)
                    {
                        string Query = "Update tbl_CONDO_FloorInfo set FloorName = '" + tbFloorName.Text + "',FloorDescription='" + tbDescription.Text + "',LastUpdateUser=" + UserID + ",LastDateDefined='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sysID=" + ForUpdate_SysID;
                        bool isUpdateSuccess = dtrans.InsertData(Query);
                        if (isUpdateSuccess)
                        {   
                            MessageBox.Show("Entry successfully updated.", "Update Done", MessageBoxButtons.OK);
                            tbDescription.Text = string.Empty;
                            tbFloorName.Text = string.Empty;
                            isUpdate = false;
                        }
                    }
                    button2.Enabled = true;
                    return;
                }

                if (dataGridView1.Rows.Count > 0)
                {
                    DialogResult drResult = MessageBox.Show("Are you sure you want to save the following entries?", "Saving Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dgrow in dataGridView1.Rows)
                        {
                            Thread.Sleep(100);
                            string Query = "Insert into tbl_CONDO_FloorInfo(FloorName,FloorDescription,userid) values('" + dgrow.Cells["FloorName"].Value.ToString() + "','" + dgrow.Cells["FloorDescription"].Value.ToString() + "',-1)";
                            dgrow.Cells["IsSaved"].Value = dtrans.InsertData(Query);
                        }
                        MessageBox.Show("Data Entry was successfully saved.", "Save Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Error: No Information to Saved.", "Saved Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFloorName.Text))
            {
                MessageBox.Show("Error: Cannot save information. Please provide the right entry", "Blank Value is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult drResult = MessageBox.Show("Are you sure you want to Add this Record?", "Add Record: Floor Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (drResult == DialogResult.Yes)
            {
                DataRow dtRows = dtData.NewRow();
                dtRows["IsEnabled"] = cbIsEnabled.Checked;
                dtRows["FloorName"] = tbFloorName.Text;
                dtRows["FloorDescription"] = tbDescription.Text;
                dtRows["isSaved"] = false;
                if (dtData.Select("FloorName='" + tbFloorName.Text + "'").ToList().Count == 0)
                {
                    dtData.Rows.Add(dtRows);
                }
                else
                {
                    MessageBox.Show("Warning: Duplicate Entry found. Please add a new value", "Floo Information Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                tbFloorName.Text = tbDescription.Text = string.Empty;
            }
            dataGridView1.DataSource = dtData;
        }
    }
}
