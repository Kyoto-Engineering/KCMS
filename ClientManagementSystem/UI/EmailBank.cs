using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.LoginUI;

namespace ClientManagementSystem.UI
{
    public partial class EmailBank : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        ConnectionString cs=new ConnectionString();
        public string userId;
        public int currentId;
        public string userTypemU;

        private SqlDataReader rdr;
        public EmailBank()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            txtBankEmailId.Clear();
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBankEmailId.Text))
            {
                MessageBox.Show("Please Input Email", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(txtBankEmailId.Text))
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct3 = "select Email from EmailBank where Email='" + txtBankEmailId.Text + "'";
                cmd = new SqlCommand(ct3, con);

                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    MessageBox.Show("This email already exists", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    txtBankEmailId.Clear();
                }


                else
                {
                    try
                    {


                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string ct = "insert into EmailBank(Email,UserId,DateAndTime) Values(@d1,@d2,@d3) " +
                                    "SELECT CONVERT(int, SCOPE_IDENTITY())";
                        cmd = new SqlCommand(ct, con);
                        cmd.Parameters.AddWithValue("@d1", txtBankEmailId.Text);
                        cmd.Parameters.AddWithValue("@d2", userId);
                        cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                        currentId = (int) cmd.ExecuteScalar();
                        con.Close();
                        MessageBox.Show("Saccesfully Submitted", "Record", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Reset();


                    }
                    catch (FormatException formatException)
                    {
                        MessageBox.Show("Please Enter Input in Correct Format", formatException.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void EmailBank_Load(object sender, EventArgs e)
        {
            userId = LoginForm.uId.ToString();
            userTypemU = LoginForm.userType;
        }

        private void EmailBank_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (userTypemU == "Admin")
            {
                this.Hide();
                MainUI frm = new MainUI();
                frm.Show();
            }
            else if (userTypemU == "User")
            {
                this.Hide();
                MainUIForUser frm = new MainUIForUser();
                frm.Show();
            }
        }

        private void txtBankEmailId_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBankEmailId.Text))
            {
                string emailId = txtBankEmailId.Text.Trim();
                Regex mRegxExpression;

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(emailId))
                {

                    MessageBox.Show("Please type your  valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBankEmailId.Clear();
                    txtBankEmailId.Focus();

                }
            }
        }
    }
}
