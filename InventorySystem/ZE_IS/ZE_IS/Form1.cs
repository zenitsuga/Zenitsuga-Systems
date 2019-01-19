using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZE_IS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void enabled_menu(string MenuName,bool state)
        {
            foreach (ToolStripItem tsi in menuStrip1.Items)
                {      
                    if (MenuName != "All")
                    {
                        if (MenuName.Contains(tsi.Text.Replace("&","")))
                        {
                            tsi.Enabled = state;
                            foreach (var tsm in menuStrip1.ContextMenuStrip.Items)
                            {

                            }
                        }
                    }else
                    {
                        tsi.Enabled = state;
                    }
                }
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            tssToday.Text = DateTime.Now.ToString("dddd MM, yyyy HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string ItemActive = "All";
            enabled_menu(ItemActive, false);
            ItemActive = "File,Log-in";
            enabled_menu(ItemActive,true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
