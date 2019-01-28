using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ERP
{
    public partial class Form1 : Form
    {
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
            if (!File.Exists(Environment.CurrentDirectory + "\\Settings.ini"))
            {
                MessageBox.Show("Error: Cannot find Configuration File. Please check or click Configuration Settings for set-up","File is missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
                counterLogin = 0;
                progressBar1.Value = counterLogin;
                return;
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
            LoadSettings(false);
        }

        private void tmrLoading_Tick(object sender, EventArgs e)
        {
            counterLogin += 10;
            progressBar1.Value = counterLogin;
            if (counterLogin == 10)
            {
                CheckConfigFile();
            }
            LoadImageSplash();
            if (counterLogin == 100)
            {
                LoadSettings(true);
                tmrLoading.Stop();
                tbUsername.Focus();
            }
        }

        private void lnkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Configuration.frmConfiguration fc = new Configuration.frmConfiguration();
            fc.ShowDialog();
            tmrLoading.Start();
        }
    }
}
