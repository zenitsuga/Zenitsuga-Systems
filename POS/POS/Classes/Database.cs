using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace POS.Classes
{
    public class Database
    {
        public string DBPath;
        public string SQLConnString;

        public bool testConnection(string ConnectionString, ref string ErrMsg)
        {
            bool result = false;
            try
            {
                //string sqlConstr = "Data Source=" + DbPath + ";Version=3;New=True;Compress=True;"; 
                SQLConnString = ConnectionString;
                DBPath = ConnectionString;
                string sqlConstr = DBPath; //SQLConnBuilder(DBPath);//SQLConnBuilder(ConnectionString);
                //SQLiteConnection sconn = new SQLiteConnection(sqlConstr);
                SqlConnection sconn = new SqlConnection(sqlConstr);
                sconn.Open();
                result = sconn.State == System.Data.ConnectionState.Open ? true : false;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message.ToString();
            }
            return result;
        }
        public bool ExecuteNonQuery(string SQLStatement)
        {
            bool result = false;
            try
            {
                //string SqlConnstr = SQLConnBuilder(DBPath);
                string sqlConstr = SQLConnString;//SQLConnBuilder(DBPath);
                SqlConnection sconn = new SqlConnection(sqlConstr);
                sconn.Open();
                if (sconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcom = new SqlCommand(SQLStatement, sconn);
                    sqlcom.ExecuteReader();
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }
        public DataTable ExecuteQuery(string SQLStatement)
        {
            DataTable dtResult = new DataTable();
            try
            {
                string SqlConnstr = SQLConnString; //SQLConnBuilder(DBPath);
                SqlConnection sconn = new SqlConnection(SqlConnstr);
                sconn.Open();
                if (sconn.State == ConnectionState.Open)
                {
                    SqlDataAdapter sda = new SqlDataAdapter(SQLStatement, sconn);
                    sda.Fill(dtResult);
                }
            }
            catch
            {
            }
            return dtResult;
        }
    }
}
