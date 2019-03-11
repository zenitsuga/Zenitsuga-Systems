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
            dashboardClick = true; 
        }
    }
}
