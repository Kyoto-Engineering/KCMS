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
    public partial class UpdateUserForAdmin : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public UpdateUserForAdmin()
        {
            InitializeComponent();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (cmbUserName.Text == "")
            {
                MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string cb = "update registration set Designation=@d1, Department=@d2,ContactNo=@d3,Email=@d4,UserType=@d5 where UserName='" + cmbUserName.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", designationTextBox.Text);
                cmd.Parameters.AddWithValue("@d2", departmentTextBox.Text);
                cmd.Parameters.AddWithValue("@d3", txtContact_no.Text);
                cmd.Parameters.AddWithValue("@d4", txtEmail_Address.Text);
                cmd.Parameters.AddWithValue("@d5", cmbUserType.Text);
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
        public void Reset()
        {
            cmbUserName.SelectedIndex = -1;
            cmbUserType.SelectedIndex = -1;
            designationTextBox.Clear();
            departmentTextBox.Clear();
            txtContact_no.Clear();
            txtEmail_Address.Clear();
        }
        public void FilUserNameForSuperAdmin()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(UserName) from Registration  order by UserId desc";
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
        private void UpdateUserForAdmin_Load(object sender, EventArgs e)
        {
            FilUserNameForSuperAdmin();
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

        private void UpdateUserForAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            UserManagementUI frm=new UserManagementUI();
                   frm.Show();
        }
    }
}
