using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace POS.Classes
{       
    public class clsFunction
    {
        Database DB = new Database();

        string iniPath = Environment.CurrentDirectory + "\\Resources\\Settings.ini";

        public bool checkDBConnection(string ConnectionType)
        {
            bool result = false;
            string ErrMsg = string.Empty;
            string ConnectionString = string.Empty;
            try
            {
                if(ConnectionType.ToLower() == "local")
                {
                    ConnectionString = ConnectionStringBuilder("LocalServer");
                    if (DB.testConnection(ConnectionString, ref ErrMsg))
                    {
                        result = true;
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        public string ConnectionStringBuilder(string Section)
        {   
            IniFile ini = new IniFile(iniPath);
            string result = string.Empty;
            try
            {   
                string servername = ini.Read("Servername", Section); 
                string databasename = ini.Read("Databasename", Section);
                string username = ini.Read("Username", Section);
                string password = ini.Read("Password", Section);
                string ConnectionString = "Server="+ servername +";Database="+ databasename +";User Id="+ username +";Password="+ password +";";

                result = ConnectionString;
            }
            catch
            {
            }
            return result;
        }

        public bool isAppValid()
        {
            bool result = false;
            try
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                var fileInfo = new FileInfo(entryAssembly.Location);
                var buildDate = fileInfo.LastWriteTime;

                int DaysLeft = DateTime.Now.Subtract(DateTime.Parse(buildDate.ToShortDateString())).Days;

                if (DaysLeft <= 60)
                {
                    result = true;
                }

            }
            catch
            {
            }
            return result;
        }

    }
}
