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
namespace POS
{
    public partial class GetRecords : Form
    {
        public string ConnectionString;
        public string TableName;
        public string FieldNames;
        public string Criteria;
        public string Orderby;
        public DataGridViewRow ResultOutput;
        
        string QueryStatement = string.Empty;

        DataTable dtRecords;

        Database db = new Database();

        public GetRecords()
        {
            InitializeComponent();
        }

        private void GetRecords_Load(object sender, EventArgs e)
        {
            try
            {
                dtRecords = new DataTable();
                db.DBPath = ConnectionString;
                db.SQLConnString = db.DBPath;
                QueryStatement = QueryBuilder(TableName, FieldNames, Criteria); 
                dtRecords = db.ExecuteQuery(QueryStatement);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtRecords;
                dataGridView1.Refresh();
            }
            catch
            {
            }
        }
        private string QueryBuilder(string tablename, string FieldName, string Criteria)
        {
            string result = string.Empty;
            try
            {
                result = "Select " + FieldName + " from " + tablename + (string.IsNullOrEmpty(Criteria) ? "" : " where " + Criteria);
            }
            catch
            {
            }
            return result;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ResultOutput = dataGridView1.SelectedRows[0];
                this.Close();
            }
            catch
            {
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    ResultOutput = dataGridView1.SelectedRows[0];
                    this.Close();
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                ResultOutput = dataGridView1.SelectedRows[0];
                this.Close();
            }
        }
    }
}
