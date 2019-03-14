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
        DataTable dtData;
        clsDatabaseTransactions dtrans;

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
              dtRows["isSaved"] = false;
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
            dtrans = new clsDatabaseTransactions();
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
            DataColumn dcIsSaved = new DataColumn("IsSaved", typeof(bool));
            dtData.Columns.Add(dcIsSaved);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DialogResult drResult = MessageBox.Show("Are you sure you want to save the following entries?", "Saving Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drResult == DialogResult.Yes)
                {
                    foreach (DataGridViewRow dgrow in dataGridView1.Rows)
                    {
                        Thread.Sleep(100);
                        string Query = "Insert into tbl_CONDO_FloorInfo(FloorName,FloorDescription,userid) values('" + dgrow.Cells["FloorName"].Value.ToString() +"','" + dgrow.Cells["FloorDescription"].Value.ToString() + "',-1)";
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
    }
}
