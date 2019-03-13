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
        ClassFile.clsDatabaseTransactions cdtrans = new ClassFile.clsDatabaseTransactions();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sidepanelwidth = panel4.Width;
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
            if (button4.Text == "<<")
            {
                button4.Text = ">>";
            }
            else
            {
                button4.Text = "<<";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //For Billing Menu
            dashboardClick = false;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            QueryRecords = string.Empty;
            dashboardClick = true;
            tabControl1.SelectedTab = tabPage1;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "FLOOR INFORMATION";
            lblDescription.Text = "Configuration for FLOOR LEVELS";
            dashboardClick = false;
            ProcessCode = "Floor_Info";
            tabControl1.SelectedTab = tabPage2;
            QueryRecords = "SELECT 'FALSE' AS SELECTED, f.sysid as 'ID',f.FloorName as 'FLOOR',f.FloorDescription as 'DESCRIPTION',u.Username,f.DateDefined as 'CREATION DATE' from tbl_CONDO_FloorInfo f LEFT JOIN tbl_SYSTEM_Users u ON f.userID = u.sysID WHERE f.isEnabled = 1;";
            LoadRecords(QueryRecords,"Floor Information");
        }

        private void LoadRecords(string QUERY,string ModuleName)
        {
            dataGridView1.DataSource = null;
            try
            {
                DataTable dtShowRecords = cdtrans.SelectData(QUERY);
                dataGridView1.DataSource = dtShowRecords;
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error in Fetching Record :" + ModuleName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProcessCode))
            {
                switch (ProcessCode)
                {
                    case "Floor_Info":
                        frm_FloorInformation fi = new frm_FloorInformation();
                        fi.StartPosition = FormStartPosition.CenterScreen;
                        fi.ShowDialog();
                        break;
                    default:
                        break;
                }
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
    }
}
