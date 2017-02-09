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
            try
            {


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "insert into EmailBank(Email,UserId,DateAndTime) Values(@d1,@d2,@d3) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(ct,con);              
                cmd.Parameters.AddWithValue("@d1", txtBankEmailId.Text);
                cmd.Parameters.AddWithValue("@d2",userId );   
                cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());     
                currentId = (int)cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("Saccesfully Submitted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void EmailBank_Load(object sender, EventArgs e)
        {
            userId = LoginForm.uId.ToString();
        }
    }
}
