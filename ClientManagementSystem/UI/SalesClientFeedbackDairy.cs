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
using ClientManagementSystem.DAO;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.LoginUI;
using ClientManagementSystem.Manager;

namespace ClientManagementSystem.UI
{
    public partial class SalesClientFeedbackDairy : Form
    {
        public int affectedRows1, affectedRows2;
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public string fName, designation, department ,userId;
        public int rPUserId;
        public SalesClientFeedbackDairy()
        {
            InitializeComponent();
        }
        
       
        private void Reset()
        {
            txt2SClientId.Clear();
            txt2SClientName.Clear();
            feedback2STextBox.Clear();
            feedback2SDateTime.Text = DateTime.Today.ToString();
            action2SMultiTextBox.Clear();
            txtResposible2SPerson.Clear();
            followUpDeadline2STextBox.Clear();
            followUpDeadline2STextBox.Clear();
            txtClientInquiry.Clear();

        }
        
        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQuery = "insert into IClientFeedbackDairy(SClientId,ClientInquiry,Feedback,DateTimes,UserId) Values(@sd1,@sd2,@sd3,@sd4,@sd5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQuery);
                cmd.Connection = con;             
                cmd.Parameters.AddWithValue("@sd1", txt2SClientId.Text);
                cmd.Parameters.AddWithValue("@sd2", txtClientInquiry.Text);
                cmd.Parameters.AddWithValue("@sd3", feedback2STextBox.Text);                            
                cmd.Parameters.AddWithValue("@sd4", Convert.ToDateTime(feedback2SDateTime.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                cmd.Parameters.AddWithValue("@sd5", userId); 
                affectedRows1 = (int)cmd.ExecuteScalar();
                con.Close();



                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQuery2 = "insert into FollowUp(SClientId,IClientFeedbackId,Actions,DeadLineDateTime,RPUserId,SBUserId,CurrentDate,Statuss) values(@sd1,@sd2,@sd3,@sd4,@sd5,@sd6,@sd7,@sd8) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQuery2);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@sd1", txt2SClientId.Text);
                cmd.Parameters.AddWithValue("@sd2", affectedRows1);
                cmd.Parameters.AddWithValue("@sd3", action2SMultiTextBox.Text);
                cmd.Parameters.AddWithValue("@sd4", Convert.ToDateTime(followUpDeadline2STextBox.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                cmd.Parameters.AddWithValue("@sd5", rPUserId);               
                cmd.Parameters.AddWithValue("@sd6", userId);                
                cmd.Parameters.AddWithValue("@sd7", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@sd8", "Pending");
                affectedRows2 = (int)cmd.ExecuteScalar();
                con.Close();

                MessageBox.Show("FeedBack Submitted Successfully.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT SalesClient.SClientId, SalesClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM  SalesClient INNER JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId INNER JOIN EmailBank ON SalesClient.EmailBankId = EmailBank.EmailBankId  order by SalesClient.SClientId desc", con);                
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView2.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3],rdr[4]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SalesClientFeedbackDairy_Load(object sender, EventArgs e)
        {
            GetData();
           
            userId = LoginForm.uId.ToString();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txt2SClientId.Text = dr.Cells[0].Value.ToString();
                txt2SClientName.Text = dr.Cells[1].Value.ToString();

                label7.Text = labelh.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtResposible2SPerson_TextChanged(object sender, EventArgs e)
        {
            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select UserId from Registration where Name='" + txtResposible2SPerson.Text + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        rPUserId = (rdr.GetInt32(0));
                    }

                    con.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FollowUpGridLoad()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending'  and FollowUp.SClientId='" + txt2SClientId.Text + "' order by FollowUp.FollowUpId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt2SClientId_TextChanged(object sender, EventArgs e)
        {
            FollowUpGridLoad();
        }
    }
}
