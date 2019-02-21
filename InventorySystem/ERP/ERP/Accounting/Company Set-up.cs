using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace ERP.Accounting
{
    public partial class Company_Set_up : Telerik.WinControls.UI.RadForm
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
    }
}
