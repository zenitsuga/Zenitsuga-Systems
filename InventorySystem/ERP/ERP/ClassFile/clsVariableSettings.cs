using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.ClassFile
{
    public class clsVariableSettings
    {
        public string CompanyName { get; set; }
        public string LicenseCode { get; set; }
        public string ActivationCode { get; set; }
        public string SystemCode { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UnMaskPassword { get; set; }
    }
}
