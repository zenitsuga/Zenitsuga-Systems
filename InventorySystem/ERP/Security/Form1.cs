using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
