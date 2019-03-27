using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.ClassFile;
namespace ERP.CONDO
{
    public partial class frm_CustomerInformation : Form
    {
        public int UserID = -1;
        public bool isUpdate;
        public int ForUpdate_SysID;
        public DataGridView SelectedDG;
        public string LoadQuery;
        clsDatabaseTransactions dtrans = new clsDatabaseTransactions();
        DataTable dtSource;
        DataTable dtResult;
        public frm_CustomerInformation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(tbLastname.Text) || string.IsNullOrEmpty(tbFirstname.Text) || string.IsNullOrEmpty(tbMiddleName.Text)) && !checkBox1.Checked )
            {
                MessageBox.Show("Error: Please provide customer information correctly", "Name is invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBox1.Checked && string.IsNullOrEmpty(tbAlias.Text))
            {
                MessageBox.Show("Error: Please provide customer information correctly", "Alias is invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtResult.Select("UnitName=" + cbUnit.Text).Length  > 0)
            {
                MessageBox.Show("Error: Cannot save this customer under this Unit No. It's Already assigned to another customer", "Assigning Unit No. Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dr = MessageBox.Show("Are you sure you want to add this customer?","Confirm to Save",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (SaveCustomer())
                {
                    MessageBox.Show("Record successfully saved.","Save Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                LoadRecords();
            }
        }

        private bool SaveCustomer()
        {
            bool result = false;
            try
            {
                string PrimaryName = checkBox1.Checked ? tbAlias.Text : tbLastname.Text + "_" + tbFirstname.Text + "_" + tbMiddleName.Text;
                int isAlias = checkBox1.Checked ? 1:0;
                string InsertQuery = "insert into tbl_CONDO_CustomerInfo(PrimaryNames,Lastname,FirstName,MiddleName,Alias,UseAlias,ContactNumber,UnitNo,PhotoPath,Notes,CreatedBy) Values ('"+ PrimaryName +"','" + tbLastname.Text + "','" + tbFirstname.Text + "','" + tbMiddleName.Text + "','" + tbAlias.Text + "'," + isAlias + ",'" + tbContact.Text + "'," + cbUnit.SelectedValue.ToString() + ",'" + lblPicPath.Text + "','" + tbNotes.Text + "'," + UserID + ")" ;
                if (dtrans.InsertData(InsertQuery))
                {
                    MessageBox.Show("Customer Information saved.","Done Saving Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    result = true;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message.ToString(), "Error Saving Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void LoadRecords()
        {
            if (!string.IsNullOrEmpty(LoadQuery))
            {
                dtResult = new DataTable();
                dtResult = dtrans.SelectData(LoadQuery);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtResult;
                dataGridView1.Refresh();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbAlias.ReadOnly = !checkBox1.Checked;   
        }

        private void frm_CustomerInformation_Load(object sender, EventArgs e)
        {
            LoadFloor();
            LoadUnit();
            LoadRecords();
            LoadTenant();
        }
        private void LoadTenant()
        {
            try
            {
                cbSearchTenant.DataSource = null;
                string QueryTenant = "SELECT cu.sysid,case when cu.UseAlias = 1 then cu.Alias else CONCAT(cu.LastName , ',' , cu.FirstName , ' ' , cu.MiddleName) end as TenantName, cu.UseAlias from tbl_CONDO_CustomerInfo cu LEFT JOIN tbl_CONDO_CustomerInfo cuo ON cu.CustomerRef = cuo.sysid LEFT JOIN tbl_CONDO_UnitInfo ui ON cu.UnitNo = ui.sysid LEFT JOIN tbl_SYSTEM_Users u ON cu.createdby = u.sysID LEFT Join tbl_SYSTEM_Users p ON cu.Updatedby = p.sysid WHERE cu.isEnabled = 1 and cu.isTenant = 0;";
                dtSource = new DataTable();
                dtSource = dtrans.SelectData(QueryTenant);
                AutoCompleteStringCollection AutoCompleteData = new AutoCompleteStringCollection();
                foreach (DataRow dr in dtSource.Rows)
                {
                    AutoCompleteData.Add(dr["TenantName"].ToString());
                }
                cbSearchTenant.DataSource = dtSource;
                cbSearchTenant.DisplayMember = "TenantName";
                cbSearchTenant.ValueMember = "sysid";
                cbSearchTenant.AutoCompleteMode = AutoCompleteMode.Suggest;
                cbSearchTenant.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbSearchTenant.AutoCompleteCustomSource = AutoCompleteData;
                cbSearchTenant.Refresh();
                lblSearchID.Text = GetIDCombo(cbSearchTenant.Text.ToString(), cbSearchTenant);
            }catch(Exception ex)
            {
            }
        }

        private void ListOwner(DataTable dtSource)
        {
            try
            {
                cbOwner.DataSource = null;
                DataView dv = new DataView(dtSource);
                if (cbSearchTenant.SelectedValue != null)
                {
                    dv.RowFilter = "sysid <> " + cbSearchTenant.SelectedValue.ToString();
                }
                else
                {
                    dv.RowFilter = "sysid <> 0";
                }
                cbOwner.DataSource = dv.ToTable();
                cbOwner.DisplayMember = "TenantName";
                cbOwner.ValueMember = "sysid";
                if (dv.Count > 0)
                {
                    AutoCompleteStringCollection AutoCompleteData = new AutoCompleteStringCollection();
                    foreach (DataRow dr in dv.ToTable().Rows)
                    {
                        AutoCompleteData.Add(dr["TenantName"].ToString());
                    }
                    cbOwner.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cbOwner.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cbOwner.AutoCompleteCustomSource = AutoCompleteData;
                }
                cbOwner.Refresh();
                lblOwnerID.Text = GetIDCombo(cbOwner.Text.ToString(), cbOwner);
            }
            catch
            {
                LoadTenant();
            }
        }

        private void LoadUnit()
        {
                cbUnit.DataSource = null;
                string Query = "SELECT sysID,UnitName FROM tbl_CONDO_UnitInfo WHERE isEnabled = 1 ORDER BY Sysid asc";
                cbUnit.DataSource = dtrans.SelectData(Query);
                cbUnit.DisplayMember = "UnitName";
                cbUnit.ValueMember = "sysID";
                cbUnit.Refresh();
        }
        private void LoadFloor()
        {
                string Query = "SELECT sysID,FloorName FROM tbl_CONDO_FloorInfo WHERE isEnabled = 1 ORDER BY Sysid asc";
                cbFloor.DataSource = dtrans.SelectData(Query);
                cbFloor.DisplayMember = "FloorName";
                cbFloor.ValueMember = "sysID";
                cbFloor.Refresh();
        }

        private void btnFloorInfo_Click(object sender, EventArgs e)
        {
            frm_FloorInformation fi = new frm_FloorInformation();
            fi.ShowDialog();
            LoadFloor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_UnitInformation fi = new frm_UnitInformation();
            fi.ShowDialog();
            LoadUnit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete the assigned picture on this customer?","Delete Picture?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                lblPicPath.Text = Environment.CurrentDirectory + "\\Resources\\profile.jpg";
                pbImage.Image = Image.FromFile(lblPicPath.Text);
                lblPicPath.Text = string.Empty;
            }
        }

        private void btnAddPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pbImage.Image = Image.FromFile(open.FileName);
                lblPicPath.Text = open.FileName.Replace("\\","\\\\");
            }  
        }

        private void cbSearchTenant_Leave(object sender, EventArgs e)
        {
            DataTable dtSource = (DataTable)cbSearchTenant.DataSource;
            ListOwner(dtSource);
            lblSearchID.Text = GetIDCombo(cbSearchTenant.Text.ToString(), cbSearchTenant);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Tenant Information")
            {
                cbSearchTenant.Focus();
            }
        }

        private void btnAddOwner_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchID.Text = GetIDCombo(cbSearchTenant.Text.ToString(), cbSearchTenant);
                lblOwnerID.Text = GetIDCombo(cbOwner.Text.ToString(), cbOwner);

                if (lblOwnerID.Text == "0" || lblSearchID.Text == "0")
                {
                    MessageBox.Show("Error: Please check your entry. Cannot save entry", "Invalid Assignee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (lblOwnerID.Text == lblSearchID.Text)
                {
                    MessageBox.Show("Error: Please check your entry. Cannot save same entry", "Invalid Assignee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to add this Customer as Tenant?\nTenant:" + cbSearchTenant.Text + "\nOwner:" + cbOwner.Text , "Assign to " + cbOwner.Text + "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string UpdateQuery = "update tbl_CONDO_CustomerInfo set isTenant = 1,CustomerRef = " + lblOwnerID.Text + " where sysID=" + lblSearchID.Text + " and isEnabled = 1";
                    if (dtrans.InsertData(UpdateQuery))
                    {
                        MessageBox.Show("Successfully updated. New Owner has been defined", "Done Assigning Owner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblOwnerID.Text = "0"; lblSearchID.Text = "0";
                    }
                    else
                    {
                        MessageBox.Show("Error: Cannot assign Tenant to the Owner. Please check your entry", "Assigning Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                cbSearchTenant.DataSource = null;
                cbOwner.DataSource = null;
                LoadRecords();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: Please select valid customer for this one.","Invalid Customer",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cbSearchTenant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchTenant.SelectedValue != null && cbSearchTenant.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                lblSearchID.Text = cbSearchTenant.SelectedValue.ToString();
            }
            else
            {
                if(cbSearchTenant.SelectedText.ToString() != "")
                lblSearchID.Text = GetIDCombo(cbSearchTenant.SelectedText.ToString(),cbSearchTenant);
            }
        }
        private string GetIDCombo(string SearchValue,ComboBox cmb)
        {
            string result = "0";
            try
            {
                DataTable dtCBSearch = new DataTable();
                dtCBSearch = (DataTable)cmb.DataSource;
                DataView dv = new DataView(dtCBSearch);
                dv.RowFilter = "TenantName ='" + SearchValue + "'";
                if (dv.Count > 0)
                {
                    result = dv.ToTable().Rows[0][0].ToString();
                }
            }
            catch
            {
            }
            return result;
        }

        private void cbOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOwner.SelectedValue != null && cbOwner.Text != "System.Data.DataRowView")
            {
                lblOwnerID.Text = GetIDCombo(cbOwner.Text.ToString(), cbOwner);
            }
            else
            {
                lblOwnerID.Text = GetIDCombo(cbOwner.SelectedText.ToString(), cbOwner);
            }
        }

        private void cbOwner_Leave(object sender, EventArgs e)
        {
            lblOwnerID.Text = GetIDCombo(cbOwner.SelectedText.ToString(), cbOwner);
        }
    }
}