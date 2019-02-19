using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace ERP.ClassFile
{
    public class clsDatabaseTransactions
    {
        MySqlConnection conn;

        public string ConnectionString()
        {
            string result = string.Empty;
            try
            {
                clsIni ci = new clsIni(Environment.CurrentDirectory + "\\" + "settings.ini");
                string ServerName = ci.Read("Servername", "Database");
                string DatabaseName = ci.Read("Databasename", "Database");
                string UserName = ci.Read("Username", "Database");
                string Password = ci.Read("Password", "Database");
                result = "Server = " + ServerName + "; Database = " + DatabaseName + "; Uid = " + UserName + "; Pwd = " + Password + "";
            }
            catch
            {
            }
            return result;
        }

        public bool InsertData(string MySQLStatement)
        {
            bool result = false;
            try
            {
                if (conn == null)
                {
                    conn = new MySqlConnection(ConnectionString());
                }
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(MySQLStatement, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                result = true;
            }
            catch(Exception ex)
            {
                
            }
            return result;
        }
        public DataTable SelectData(string MySQLStatement)
        {
            DataTable result = new DataTable();
            try
            {
                if (conn == null)
                {
                    conn = new MySqlConnection(ConnectionString());
                }
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();                  
                }
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(MySQLStatement, conn);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(result);
                conn.Close();
            }
            catch
            {
            }
            return result;
        }
    }
}
