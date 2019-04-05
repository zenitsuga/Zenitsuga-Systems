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
    public partial class frm_ZeroAmount : Form
    {
        public Decimal AmountEntered;
        public string ManualNotes;
        
        public frm_ZeroAmount()
        {
            InitializeComponent();
        }

        private void frm_ZeroAmount_Load(object sender, EventArgs e)
        {
            AmountEntered = decimal.Parse("0.00");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Error: No amount defined. Please check your amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "0.00";
                    return;
                }
                this.Close();
            }
        }

        private void frm_ZeroAmount_FormClosed(object sender, FormClosedEventArgs e)
        {
            AmountEntered = decimal.Parse(textBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            if (checkBox1.Checked)
            {
                this.Height = 266;
            }
            else
            {
                this.Height = 133;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           DialogResult dr = MessageBox.Show("This will insert a manual price. Would you like to continue?","Continue Manual Pricing?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
           if (dr == DialogResult.Yes)
           {
               ManualNotes = textBox2.Text;
               this.Close();
           }
        }
    }
}
