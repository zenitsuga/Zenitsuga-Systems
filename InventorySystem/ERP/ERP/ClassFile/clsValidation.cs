using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ERP.ClassFile
{
    public class clsValidation
    {
        SecurityClass.Security sec = new SecurityClass.Security();
        public string ConnectionString(string Servername,string Databasename,string Username,string Password)
        {
            string result = string.Empty;
            try
            {
                result = "Server = "+ Servername +"; Database = "+ Databasename +"; Uid = "+ Username +"; Pwd = "+ Password + "";
            }
            catch
            {
            }
            return result;
        }
        public bool TestDatabaseConnection(string Servername,string Databasename, string Username, string Password)
        {
            bool result = false;
            try
            {
                MySqlConnection con = new MySqlConnection(ConnectionString(Servername,Databasename,Username,Password));
                con.Open();
                result = true;
            }
            catch
            {
            }
            return result;
        }
        public void SaveIni(string Key,string Val,String Section)
        {
            try
            {
                string IniPath = Environment.CurrentDirectory + "\\settings.ini";
                clsIni ci = new clsIni(IniPath);
                ci.Write(Key, Val, Section);
            }
            catch
            {
            }
        }
        public clsVariableSettings ValidateIni()
        {
            clsVariableSettings cvResult = new clsVariableSettings();
            try
            {
                string IniPath = Environment.CurrentDirectory + "\\settings.ini";
                clsIni ci = new clsIni(IniPath);
                cvResult = new clsVariableSettings();
                cvResult.CompanyName = ci.Read("CompanyName", "SystemSettings");

                cvResult.LicenseCode = ci.Read("LicenseCode", "Licensing");
                cvResult.ActivationCode = ci.Read("ActivationCode", "Licensing");

                cvResult.ServerName = ci.Read("Servername","Database");
                cvResult.DatabaseName = ci.Read("Databasename", "Database");
                cvResult.UserName = ci.Read("Username", "Database");
                cvResult.Password = ci.Read("Password", "Database");
            }
            catch
            {
            }
            return cvResult;
        }
        public bool CheckActivationCode(string ActivationCode, string CompanyName)
        {
            bool result = false;
            try
            {
                CompanyName = CompanyName.ToLower();
                ActivationCode = SecurityClass.Security.Decrypt(ActivationCode, sec.showKeys());
                if (CompanyName.Contains(" "))
                {
                    result = CompanyName.Contains(ActivationCode);
                }
                else
                {
                    CompanyName = CompanyName + " "; 
                    result = CompanyName.Contains(ActivationCode.Trim());
                }
            }
            catch
            {
            }
            return result;
        }
        public bool CheckLicensing(string MachineID, string LicenseCode)
        {
            bool result = false;
            try
            {   
                string License = SecurityClass.Security.Decrypt(LicenseCode, sec.showKeys());
                result = MachineID.Equals(License);
            }
            catch
            {
            }
            return result;
        }
    }
}
