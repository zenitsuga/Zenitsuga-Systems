﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERP.CONDO
{
    public partial class FrmModification : Form
    {
        public string TableName;
        public string SysID;
        public string QueryString;

        public FrmModification()
        {
            InitializeComponent();
        }

        private void FrmModification_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SysID) && !string.IsNullOrEmpty(TableName))
            {

            }
        }
    }
}
