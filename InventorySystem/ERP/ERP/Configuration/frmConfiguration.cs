using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP.Configuration
{
    public partial class frmConfiguration : Form
    {
        public frmConfiguration()
        {
            InitializeComponent();
        }

        private void LoadSetting()
        {
            pbSettings.Image = Image.FromFile(Environment.CurrentDirectory + "\\Resources\\settings.jpg");
        }

        private void frmConfiguration_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
