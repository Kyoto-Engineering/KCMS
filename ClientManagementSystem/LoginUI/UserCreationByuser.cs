using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.Manager;
using Microsoft.VisualBasic.ApplicationServices;

namespace ClientManagementSystem.LoginUI
{
    public partial class UserCreationByuser : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public string readyPassword;
        public int currentId;
        public UserCreationByuser()
        {
            InitializeComponent();
        }

        public string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            string readyPassword1 = Convert.ToBase64String(inArray);
            readyPassword = readyPassword1;
            return readyPassword1;
        }
        private void Reset()
        {
            userNameTextBox.Text = "";
            passwordTextBox.Text = "";
           
            nameTextBox.Text = "";
            emailTextBox.Text = "";
            designationTextBox.Text = "";
            departmentTextBox.Text = "";
            contactNoTextBox.Text = "";
            userButton.Enabled = true;
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            if (userNameTextBox.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                userNameTextBox.Focus();
                return;
            }
            if (passwordTextBox.Text == "")
            {
                MessageBox.Show("Please Type Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordTextBox.Focus();
                return;
            }
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Please Type your Full Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nameTextBox.Focus();
                return;
            }
            if (designationTextBox.Text == "")
            {
                MessageBox.Show("Please Type your designation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                designationTextBox.Focus();
                return;
            }
            if (departmentTextBox.Text == "")
            {
                MessageBox.Show("Please Type your Department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                departmentTextBox.Focus();
                return;
            }
            string password = passwordTextBox.Text.Trim();
            EncodePasswordToBase64(password);
            int us = 0;
           
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "Select Username from Registration where Username='" + userNameTextBox.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("This User Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset();
                    userNameTextBox.Focus();

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string qry = "insert into Registration(Username,Usertype,Password,Name,Email,Designation,Department,ContactNo) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@d1",userNameTextBox.Text);
                cmd.Parameters.AddWithValue("@d2", "User");
                cmd.Parameters.AddWithValue("@d3", passwordTextBox.Text);
                cmd.Parameters.AddWithValue("@d4", nameTextBox.Text);
                cmd.Parameters.AddWithValue("@d5", emailTextBox.Text);
                cmd.Parameters.AddWithValue("@d6", designationTextBox.Text);
                cmd.Parameters.AddWithValue("@d7", departmentTextBox.Text);
                cmd.Parameters.AddWithValue("@d8", contactNoTextBox.Text);
                currentId = (int)cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("Successfully  User Created.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                userButton.Enabled = false;
                Reset();

                LoginForm lg = new LoginForm();
                this.Visible = false;
                lg.ShowDialog();
                this.Visible = true;
            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Please Enter Input in Correct Format", formatException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
    }
}
