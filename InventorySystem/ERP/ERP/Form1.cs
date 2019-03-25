using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ERP.ClassFile;
namespace ERP
{
    public partial class Form1 : Form
    {
        clsValidation cv;
        clsVariableSettings cvs;
        clsDatabaseTransactions cdt;

        bool UnmaskPassword = false;

        string SystemName;

        string programname = string.Empty;
        string Servername = string.Empty;
        string Databasename = string.Empty;
        string DBUsername = string.Empty;
        string DBPassword = string.Empty;

        int counterLogin = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadSettings(bool status)
        {
            try
            {
                pbSplash.Image = Image.FromFile(Environment.CurrentDirectory + "\\Resources\\login.jpg");
                pbKeys.Image = Image.FromFile(Environment.CurrentDirectory + "\\Resources\\keys.jpg");
            }
            catch
            {
            }
            btnClear.Enabled = status;
            btnLogin.Enabled = status;
            tbUsername.Enabled = status;
            tbPassword.Enabled = status;
        }
        private void CheckConfigFile()
        {
            tmrLoading.Stop();
            lblSys.Text = "Checking Settings file (Settings.ini)";
            if (!File.Exists(Environment.CurrentDirectory + "\\Settings.ini"))
            {
                lblSys.Text = "Error: Cannot find Configuration File. Please check or click Configuration Settings for set-up";
                lblSys.ForeColor = Color.Red;
                MessageBox.Show(lblSys.Text,"File is missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
                counterLogin = 0;
                progressBar1.Value = counterLogin;
                return;
            }
            tmrLoading.Start();
        }
        private void GetDBSettings()
        {
            tmrLoading.Stop();
            try
            {
                cvs = new clsVariableSettings();
                cvs = cv.ValidateIni();
                if (cvs != null)
                {
                    Servername = cvs.ServerName;
                    Databasename = cvs.DatabaseName;
                    DBUsername = cvs.UserName;
                    DBPassword = cvs.Password;
                    SystemName = cvs.SystemCode;
                }
            }
            catch
            {
            }
            tmrLoading.Start();
        }
        private void LoadImageSplash()
        {
            int CounterLogin = counterLogin / 10;
            string picLocation = Environment.CurrentDirectory + "\\Resources\\SPLASH\\splash" + CounterLogin + ".jpg";
            if (File.Exists(picLocation))
            {
                pbSplash.Image = Image.FromFile(picLocation);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cv = new clsValidation();
            LoadSettings(false);
            UnmaskPassword = cv.UnMaskPassword();
        }
        private bool CheckandInjectCode(clsVariableSettings cvs)
        {
            bool result = false;
            try
            {
                string SQLSelectSystemInfo = "select Count(*) from tbl_SYSTEM_INFO where isEnabled = 1 and ProgramCode='"+ cv.Crypt(programname) +"' and LicenseCode = '" + cvs.LicenseCode + "' and ActivationCode = '" + cvs.ActivationCode + "'";
                cdt = new clsDatabaseTransactions();
                DataTable dtResult =cdt.SelectData(SQLSelectSystemInfo); 
                int RowAffected = dtResult.Rows.Count;
                RowAffected = RowAffected == 1 ? int.Parse(dtResult.Rows[0][0].ToString()) : 0;
                if (RowAffected == 0)
                {
                    string SQLInsertSystemInfo = "Insert into tbl_SYSTEM_INFO(ProgramCode,LicenseCode,ActivationCode)" + " Values ('" + cv.Crypt(programname) + "','" + cvs.LicenseCode + "','" + cvs.ActivationCode + "')";
                    result = cdt.InsertData(SQLInsertSystemInfo);
                }
                else
                {
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }

        private void CheckDatabaseConnection()
        {
            tmrLoading.Stop();
            GetDBSettings();
            if (!cv.TestDatabaseConnection(Servername, Databasename, DBUsername, DBPassword))
            {
                tmrLoading.Enabled = false;
                tmrLoading.Stop();
                tmrLoading.Dispose();
                lblSys.Text = "Error: Database cannot connect. Please check configuration first";
                lblSys.ForeColor = Color.Red;
                MessageBox.Show(lblSys.Text, "Failed to connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                counterLogin = 0;
                progressBar1.Value = counterLogin;               
                return;
            }
            tmrLoading.Start();
        }
        private string ShowProgram()
        {
            string result = string.Empty;
            try
            {
                if (cv.ShowsCrypt(SystemName).Contains("SYSTEM"))
                {
                    result = cv.ShowsCrypt(SystemName);
                    this.Text = "Welcome to " + result;
                }
            }
            catch
            {
            }
            return result;
        }

        private void tmrLoading_Tick(object sender, EventArgs e)
        {
            lblSys.ForeColor = Color.Black;
            if (progressBar1.Value < 100)
            {
                counterLogin += 10;
                progressBar1.Value = counterLogin;
                if (counterLogin == 30)
                {
                    CheckConfigFile();
                }
                LoadImageSplash();
                if (counterLogin == 50)
                {
                    CheckDatabaseConnection();
                }
                if (counterLogin == 60)
                {
                    lblSys.Text = "Checking System...";
                    programname = ShowProgram();
                    SystemName = programname;
                    if (!string.IsNullOrEmpty(programname))
                    {
                        lblSys.Text += "OK";
                    }
                    else
                    {
                        lblSys.Text = "Error: System Code is Invalid. Please check with the vendor.";
                        lblSys.ForeColor = Color.Red;
                        tmrLoading.Stop();
                        progressBar1.Value = 0;
                        MessageBox.Show(lblSys.Text,"SYSTEM ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                }
                if (counterLogin == 70)
                {
                    lblSys.Text = "Validating System...";
                    if (!string.IsNullOrEmpty(programname))
                    {
                        lblSys.Text += "OK";
                    }
                }
                if (counterLogin == 90)
                {
                    lblSys.Text = "Preparing " + SystemName + "...";
                    if (!CheckandInjectCode(cvs))
                    {
                        lblSys.Text = "Error: Cannot Start the Program . Please check System Information (DB)";
                        lblSys.ForeColor = Color.Red;
                        tmrLoading.Stop();
                        progressBar1.Value = 0;
                        MessageBox.Show(lblSys.Text, "SYSTEM ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (counterLogin == 100)
                {
                    LoadSettings(true);
                    tmrLoading.Stop();
                    tbUsername.Focus();
                    progressBar1.Value = 100;
                    tmrLoading.Enabled = false;
                    lblSys.Text = "Please login your username to logon " + SystemName + "...";
                }
            }
        }

        private void lnkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tmrLoading.Stop();
            Configuration.frmConfiguration fc = new Configuration.frmConfiguration();
            fc.ShowDialog();
            counterLogin = 0;
            lblSys.Text = "Ready...";
            tmrLoading.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string ErrMessage = string.Empty;
            if (!cv.CheckUsers(tbUsername.Text, tbPassword.Text, ref ErrMessage, programname))
            {
                MessageBox.Show("Error: " + ErrMessage,"Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (programname.ToUpper() == "ACCOUNTING SYSTEM")
            {
                Accounting.MainForm mf = new Accounting.MainForm();
                mf.Show();
                this.Hide();
            }
            else if (programname.ToUpper() == "CONDOMINIUM SYSTEM")
            {
                CONDO.MainForm mf = new CONDO.MainForm();
                mf.Show();
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error: No Module Found. Please check your configuration","Invalid Program Code",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void tbPassword_Leave(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = '*';
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            if(UnmaskPassword)
            tbPassword.PasswordChar = '\0';
        }
    }
}
