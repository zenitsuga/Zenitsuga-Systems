using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Security
{
    public partial class Form1 : Form
    {
        SecurityClass.Security sec = new SecurityClass.Security();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Error: Please input value","Blank is Invalid",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            textBox2.Text = SecurityClass.Security.Encrypt(textBox1.Text, sec.showKeys());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Error: Please input value", "Blank is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            textBox2.Text = SecurityClass.Security.Decrypt(textBox1.Text, sec.showKeys());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Text = GetHardDiskSerialNo();
            }
        }
        public string GetHardDiskSerialNo()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
            {
                result += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            return result;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
