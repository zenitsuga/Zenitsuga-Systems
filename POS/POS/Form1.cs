using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Classes;
using System.Reflection;
using System.IO;
namespace POS
{
    public partial class Form1 : Form
    {
        clsFunction cf = new clsFunction();
        bool isLocalConnected = false;
        bool isRemoteConnected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigSettings cs = new frmConfigSettings();
            cs.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tssDate.Text = "Date : " + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss");
            isLocalConnected  = (cf.checkDBConnection("Local") ? true : false);
            tssDBLocalStatus.Text = "Local: " + (isLocalConnected == true ? "Connected":"Disconnected");
        }

        private void FocusBarcode()
        {
            tbBarcode.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {   
            if (!cf.isAppValid())
            {
                Application.Exit();
                return;
            }
            FocusBarcode();
        }

        private void customerInquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GetRecords gr = new GetRecords();
                gr.ConnectionString = cf.ConnectionStringBuilder("LocalServer");
                gr.TableName = "tblCustomer";
                gr.FieldNames = "sysID,LastName,FirstName,Address,ContactNumber";
                gr.Criteria = " isActive =1";
                gr.ShowDialog();
                if (gr.ResultOutput != null)
                {
                    string CustomerName = gr.ResultOutput.Cells["LastName"].Value.ToString() + "," + gr.ResultOutput.Cells["Biagtan"].Value.ToString(); 
                    tbCustormerName.Text = CustomerName;
                }
            }
            catch
            {
            }
        }
    }
}
