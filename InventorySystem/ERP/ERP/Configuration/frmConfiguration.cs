using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SecurityClass;
using ERP.ClassFile;

namespace ERP.Configuration
{
    public partial class frmConfiguration : Form
    {
        SecurityClass.Security sec = new SecurityClass.Security();
        clsValidation cv;

        clsVariableSettings cvs;

        public frmConfiguration()
        {
            InitializeComponent();
        }
        private void SaveINI()
        {
            try
            {
                cv.SaveIni("CompanyName", tbCompanyName.Text, "SystemSettings");
                cv.SaveIni("Servername", tbServerName.Text, "Database");
                cv.SaveIni("Databasename", tbDatabasename.Text, "Database");
                cv.SaveIni("Username", tbDBUsername.Text, "Database");
                cv.SaveIni("Password", tbDBPassword.Text, "Database");
                cv.SaveIni("LicenseCode", tbLicense.Text, "Licensing");
                cv.SaveIni("ActivationCode", tbActivation.Text, "Licensing");
                    
            }catch(Exception ex)
            {
            }
        }
        private void LoadINI()
        {
            try
            {
                cvs = new clsVariableSettings();
                cvs = cv.ValidateIni();
                if (cvs != null)
                {
                    tbCompanyName.Text = cvs.CompanyName;
                    tbLicense.Text = cvs.LicenseCode;
                    tbActivation.Text = cvs.ActivationCode;

                    tbServerName.Text = cvs.ServerName;
                    tbDatabasename.Text = cvs.DatabaseName;
                    tbDBUsername.Text = cvs.UserName;
                    tbDBPassword.Text = cvs.Password;
                }
            }
            catch
            {
            }
        }

        private void LoadSetting()
        {
            pbSettings.Image = Image.FromFile(Environment.CurrentDirectory + "\\Resources\\settings.jpg");
            pbDatabaseImage.Image = Image.FromFile(Environment.CurrentDirectory + "\\Resources\\dbnotconnect.jpg");
            tbMacID.Text = sec.GetMachineID();
            //tbLicense.Text = sec.showLicense(tbMacID.Text);
        }

        private void CheckDatabase()
        {
            try
            {
                bool isconnect = false;
                if (isconnect)
                {
                    lblDBStatus.Text = "CONNECTED";
                    lblDBStatus.ForeColor = Color.Black;
                    pbDatabaseImage.Image = Image.FromFile(Environment.CurrentDirectory + "\\Resources\\dbconnect.jpg");
                }
                else
                {
                    lblDBStatus.Text = "DISCONNECTED";
                    lblDBStatus.ForeColor = Color.Red;
                    pbDatabaseImage.Image = Image.FromFile(Environment.CurrentDirectory + "\\Resources\\dbnotconnect.jpg");
                }
            }
            catch
            {
            }
        }

        private void frmConfiguration_Load(object sender, EventArgs e)
        {
            cv = new clsValidation();
            LoadINI();
            LoadSetting();
            CheckDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!cv.CheckActivationCode(tbActivation.Text, tbCompanyName.Text))
            {
                MessageBox.Show("Error: Invalid Activation Code. Please check to your vendor","Activation Code Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (!cv.CheckLicensing(tbMacID.Text, tbLicense.Text))
            {
                MessageBox.Show("Error: Invalid License Code. Please check to your vendor", "License Code Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveINI();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Database")
            {
                CheckDatabase();
            }
        }
    }
}
