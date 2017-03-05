using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DBGateway;

namespace ClientManagementSystem.LoginUI
{
    public partial class UpdateUserInfo : Form
    {
        private SqlCommand cmd;
        private SqlConnection con;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public string userTypeUP;
        public UpdateUserInfo()
        {
            InitializeComponent();
        }
        public void FilUserName()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(UserName) from Registration where UserType != 'SuperAdmin' and  UserType !='Admin' order by UserId desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbUserName.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateUserInfo_Load(object sender, EventArgs e)
        {
            userTypeUP = LoginForm.userType;
            FilUserName();
        }
        public void Reset()
        {
            cmbUserName.SelectedIndex = -1;
            designationTextBox.Clear();
            departmentTextBox.Clear();
            txtContact_no.Clear();
            txtEmail_Address.Clear();
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (cmbUserName.Text == "")
            {
                MessageBox.Show("Please Select User Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUserName.Focus();
                return;
            }

            if (txtContact_no.Text == "")
            {
                MessageBox.Show("Please enter contact no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContact_no.Focus();
                return;
            }
            if (txtEmail_Address.Text == "")
            {
                MessageBox.Show("Please enter email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail_Address.Focus();
                return;
            }
            try
            {


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update registration set Designation='" + designationTextBox.Text + "', Department='" + departmentTextBox.Text + "',ContactNo='" + txtContact_no.Text + "',Email='" + txtEmail_Address.Text + "' where UserName='" + cmbUserName.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();

                MessageBox.Show("Successfully updated", "User Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbUserName.Text = cmbUserName.Text.TrimEnd();
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT Designation,Department,ContactNo,Email FROM Registration WHERE UserName = '" + cmbUserName.Text.Trim() + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    designationTextBox.Text = (rdr.GetString(0).Trim());
                    departmentTextBox.Text = (rdr.GetString(1).Trim());
                    txtContact_no.Text = (rdr.GetString(2).Trim());
                    txtEmail_Address.Text = (rdr.GetString(3).Trim());
                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUserInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
           this.Hide();
            UserManagementUI frm=new UserManagementUI();
            frm.Show();
        }
    }
}
