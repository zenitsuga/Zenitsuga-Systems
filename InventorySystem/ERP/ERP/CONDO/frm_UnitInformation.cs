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
    public partial class frm_UnitInformation : Form
    {
        public int UserID = -1;
        public bool isUpdate;
        public int ForUpdate_SysID;
        public DataGridView SelectedDG;
        DataTable dtData;
        clsDatabaseTransactions dtrans;

        public frm_UnitInformation()
        {
            InitializeComponent();
        }

        private void ConstructDataTable()
        {
            DataColumn dcUnitName = new DataColumn("UnitName", typeof(string));
            dtData.Columns.Add(dcUnitName);
            DataColumn dcFloorName = new DataColumn("FloorName", typeof(string));
            dtData.Columns.Add(dcFloorName);
            DataColumn dcUnitDescription = new DataColumn("UnitDescription", typeof(string));
            dtData.Columns.Add(dcUnitDescription);
            DataColumn dcSize = new DataColumn("Size", typeof(int));
            dtData.Columns.Add(dcSize);
            DataColumn dcMonthlyDueSQM = new DataColumn("MonthlyDueSQM", typeof(int));
            dtData.Columns.Add(dcMonthlyDueSQM);
            DataColumn dcTotalDue = new DataColumn("TotalDue", typeof(int));
            dtData.Columns.Add(dcTotalDue);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_FloorInformation fi = new frm_FloorInformation();
            fi.ShowDialog();
            LoadFloor();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                tbMonthlyDue.Enabled = true;
                ComputeTotalDues();
            }
            else
            {
                tbMonthlyDue.Enabled = false;
            }
        }

        private void ComputeTotalDues()
        {
            tbTotalDues.Text = (int.Parse(tbArea.Text) * int.Parse(tbMonthlyDue.Text)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUnitName.Text))
            {
                MessageBox.Show("Warning: Cannot save information. Unit name not defined.", "Unable to save data");
                return;
            }
            DialogResult drResult = MessageBox.Show("Are you sure you want to Add this Record?", "Add Record: Unit Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (drResult == DialogResult.Yes)
            {
                DataRow dtRows = dtData.NewRow();
                dtRows["UnitName"] = tbUnitName.Text;
                dtRows["FloorName"] = cbFloorList.Text;
                dtRows["UnitDescription"] = tbDescription.Text;
                dtRows["Size"] = tbArea.Text;
                dtRows["MonthlyDueSQM"] = tbMonthlyDue.Text;
                dtRows["TotalDue"] = tbTotalDues.Text;
                
                if (dtData.Select("UnitName='" + tbUnitName.Text + "'").ToList().Count == 0)
                {
                    dtData.Rows.Add(dtRows);
                }
                else
                {
                    MessageBox.Show("Warning: Duplicate Entry found. Please add a new value", "Floo Information Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                tbUnitName.Text = tbDescription.Text = string.Empty;
            }
            dataGridView1.DataSource = dtData;
            tbArea.Text = tbMonthlyDue.Text = tbTotalDues.Text = "0";
            checkBox1.Checked = false;
        }

        private void frm_UnitInformation_Load(object sender, EventArgs e)
        {
            dtrans = new clsDatabaseTransactions();
            dtData = new DataTable();
            ConstructDataTable();
            LoadFloor();

            if (isUpdate)
            {
                if (SelectedDG.SelectedRows.Count == 1)
                {
                    tbUnitName.Text = SelectedDG.SelectedRows[0].Cells["Unit"].Value.ToString();
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
            string QueryRecords = "Select ui.SysID as 'ID',ui.UnitName as 'Name',f.FloorName as 'Floor',ui.Description, ui.AreaSQM as 'Size',ui.MonthlyDue as 'Monthly Due per SQM',ui.TotalDue as 'Total Due' from tbl_CONDO_UnitInfo ui LEFT JOIN tbl_CONDO_FloorInfo f ON ui.FloorAssociate = f.sysid LEFT JOIN tbl_SYSTEM_Users u ON ui.createdby = u.sysID LEFT Join tbl_SYSTEM_Users p on ui.Updatedby = p.sysid WHERE ui.isEnabled = 1;";
            dataGridView1.DataSource = dtrans.SelectData(QueryRecords);
        }
        private void LoadFloor()
        {
            cbFloorList.Items.Clear();
            string Query = "SELECT sysID,FloorName FROM tbl_CONDO_FloorInfo WHERE isEnabled = 1 ORDER BY Sysid asc";
            cbFloorList.DataSource = dtrans.SelectData(Query);
            cbFloorList.DisplayMember = "FloorName";
            cbFloorList.ValueMember = "sysID";
            cbFloorList.Refresh();
        }

        private void tbArea_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ComputeTotalDues();
            }
        }

        private void tbMonthlyDue_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ComputeTotalDues();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Error: Cannot save record. Please check your entry.","No Record to add",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (isUpdate)
            {
                DialogResult drResult = MessageBox.Show("Are you sure you want to update the entry?", "Saving Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drResult == DialogResult.Yes)
                {
                    string Query = "Update tbl_CONDO_UnitInfo set UnitName = '" + tbUnitName.Text + "',UnitDescription='" + tbDescription.Text + "',FloorAssociate=" + cbFloorList.SelectedValue  + ",AreaSQM="+ tbArea.Text +",MonthlyDue=" + tbMonthlyDue.Text +",IsMonthlyDueComputed="+ (checkBox1.Checked ? 1:0).ToString() +",LastUpdateUser=" + UserID + ",LastDateDefined='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where sysID=" + ForUpdate_SysID;
                    bool isUpdateSuccess = dtrans.InsertData(Query);
                    if (isUpdateSuccess)
                    {
                        MessageBox.Show("Entry successfully updated.", "Update Done", MessageBoxButtons.OK);
                        tbDescription.Text = string.Empty;
                        tbUnitName.Text = string.Empty;
                        isUpdate = false;
                    }
                }
                button2.Enabled = true;
                return;
            }

            foreach (DataGridViewRow dgrow in dataGridView1.Rows)
            {
                int FloorID = int.Parse(dtrans.SelectData("Select sysID from tbl_CONDO_FloorInfo where Floorname='" + dgrow.Cells["FloorName"].Value.ToString() + "'").Rows[0][0].ToString());
                string Query = "Insert into tbl_CONDO_UnitInfo(UnitName,FloorAssociate,Description,AreaSQM,isMontlyDueComputed,MonthlyDue,TotalDue,CreatedBy)Values('" + dgrow.Cells["UnitName"].Value.ToString() + "'," + FloorID + ",'" + dgrow.Cells["UnitDescription"].Value.ToString() + "'," + dgrow.Cells["Size"].Value.ToString() + "," + (dgrow.Cells["MonthlyDueSQM"].Value.ToString() != "0" ? 1:0) + "," + dgrow.Cells["MonthlyDueSQM"].Value.ToString() + "," + dgrow.Cells["TotalDue"].Value.ToString() + "," + UserID + ")";
                if(dtrans.InsertData(Query))
                {
                    MessageBox.Show("Record successfully saved. ","Record Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error: Unable to insert record. Kindly Check your entry","Saving Record Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
                }
            }
        }
    }
}
