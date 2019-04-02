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
        public Decimal AmountEnteted;
        public frm_ZeroAmount()
        {
            InitializeComponent();
        }

        private void frm_ZeroAmount_Load(object sender, EventArgs e)
        {
            AmountEnteted = decimal.Parse("0.00");
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
            AmountEnteted = decimal.Parse(textBox1.Text);
        }
    }
}
