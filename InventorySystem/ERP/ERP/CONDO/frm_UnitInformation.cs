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
            DataColumn dcIsEnabled = new DataColumn("IsEnabled", typeof(bool));
            dtData.Columns.Add(dcIsEnabled);
            DataColumn dcIsMDComputed = new DataColumn("IsMDComputed", typeof(bool));
            dtData.Columns.Add(dcIsMDComputed);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_FloorInformation fi = new frm_FloorInformation();
            fi.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

            }
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
                dtRows["IsEnabled"] = cbIsEnabled.Checked;
                dtRows["UnitName"] = tbUnitName.Text;
                dtRows["FloorName"] = cbFloorList.Text;
                dtRows["UnitDescription"] = tbDescription.Text;
                dtRows["isSaved"] = false;
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
        }

        private void frm_UnitInformation_Load(object sender, EventArgs e)
        {
            dtrans = new clsDatabaseTransactions();
            ConstructDataTable();
            LoadFloor();
        }
        private void LoadFloor()
        {
            string Query = "SELECT sysID,FloorName FROM tbl_CONDO_FloorInfo WHERE isEnabled = 1 ORDER BY Sysid asc";
            cbFloorList.DataSource = dtrans.SelectData(Query);
            cbFloorList.DisplayMember = "FloorName";
            cbFloorList.ValueMember = "sysID";
            cbFloorList.Refresh();
        }
    }
}
