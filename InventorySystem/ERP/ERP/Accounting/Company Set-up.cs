using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ERP.Accounting
{
    public partial class Company_Set_up : Form
    {
        public Company_Set_up()
        {
            InitializeComponent();
        }

        private void InitializeSetup()
        {
            string SourceDir = Environment.CurrentDirectory + "\\Accounting\\Resources\\";
            try{
            pbCompanyICON.Image = Image.FromFile(SourceDir + "\\CompanySetup.png");
            btnAdd.BackgroundImage = Image.FromFile(SourceDir + "\\Add.png");
            }catch
            {
            }
        }

        private void Company_Set_up_Load(object sender, EventArgs e)
        {
            InitializeSetup();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Save();
        }
        #region Process
        private void Save()
        {
            string ModuleSelected = tabControl1.SelectedTab.Text;
            DialogResult dr = MessageBox.Show("Are you sure you want to add entry on " + ModuleSelected + "?", "Confirm to Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
