﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ERP.ClassFile
{
    public class clsValidation
    {
        clsDatabaseTransactions cdt = new clsDatabaseTransactions();
        #region DATABASE 
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
                con.Close();
            }
            catch
            {
            }
            return result;
        }
        #endregion
        #region INI(Settings)
        public bool UnMaskPassword()
        {
            bool result = false;
            try
            {
                string IniPath = Environment.CurrentDirectory + "\\settings.ini";
                clsIni ci = new clsIni(IniPath);
                string Value = ci.Read("UnMaskPassword", "SystemSettings");
                result = bool.Parse(Value);
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
        #endregion
        #region ValidationProcess
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
                cvResult.SystemCode = ci.Read("ProgramCode", "Licensing");
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
        
        public string Crypt(string Value)
        {
            string Result = string.Empty;
            try
            {
                Result = SecurityClass.Security.Encrypt(Value, sec.showKeys());
            }
            catch
            {
            }
            return Result;
        }
        public string ShowsCrypt(string Value)
        {
            string Result = string.Empty;
            try
            {
               Result = SecurityClass.Security.Decrypt(Value, sec.showKeys());
            }
            catch
            {
            }
            return Result;
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
        #endregion
        #region Login Module
        public bool CheckUsers(string Username, string Password,ref string ErrMSG)
        {
            bool result = false;
            try{
                if (string.IsNullOrEmpty(Username))
                {
                    ErrMSG = "Invalid User. Please check your Username.";
                    return result;
                }
                if (string.IsNullOrEmpty(Username))
                {
                    ErrMSG = "Invalid User. Please check your Password.";
                    return result;
                }
                string QueryCheckUsers = "Select * from tbl_SYSTEM_Users where Username ='" + Username + "' and Password='" + Crypt(Password) + "' and isEnabled = 1";
                if (cdt.SelectData(QueryCheckUsers).Rows.Count == 0)
                {
                    ErrMSG = "Unregistered Users. Please check your entry";
                    return result;
                }
                else
                {
                    result = true;
                    return result;
                }
            }catch
            {
            }
            return result;
        }
        #endregion
    }
}
