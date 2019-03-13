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
    public partial class frm_FloorInformation : Form
    {
        DataTable dtData;

        public frm_FloorInformation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFloorName.Text))
            {
                MessageBox.Show("Error: Cannot save information. Please provide the right entry","Blank Value is Invalid",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

          DialogResult drResult = MessageBox.Show("Are you sure you want to Add this Record?","Add Record: Floor Information",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

          if (drResult == DialogResult.Yes)
          {
              DataRow dtRows = dtData.NewRow();
              dtRows["IsEnabled"] = cbIsEnabled.Checked;
              dtRows["FloorName"] = tbFloorName.Text;
              dtRows["FloorDescription"] = tbDescription.Text;
              if (dtData.Select("FloorName='" + tbFloorName.Text + "'").ToList().Count == 0)
              {
                  dtData.Rows.Add(dtRows);
              }
              else
              {
                  MessageBox.Show("Warning: Duplicate Entry found. Please add a new value","Floo Information Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
              }
              tbFloorName.Text = tbDescription.Text = string.Empty;
          }
          dataGridView1.DataSource = dtData;
        }

        private void frm_FloorInformation_Load(object sender, EventArgs e)
        {
            dtData = new DataTable();
            ConstructDataTable();
            dataGridView1.DataSource = dtData;
        }
        private void ConstructDataTable()
        {
            DataColumn dcFloorName = new DataColumn("FloorName", typeof(string));
            dtData.Columns.Add(dcFloorName);
            DataColumn dcFloorDescription = new DataColumn("FloorDescription", typeof(string));
            dtData.Columns.Add(dcFloorDescription);
            DataColumn dcIsEnabled = new DataColumn("IsEnabled", typeof(bool));
            dtData.Columns.Add(dcIsEnabled);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

            }
        }
    }
}
