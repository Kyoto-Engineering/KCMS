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
        public string fName, designation, department,tempRpDesig,tempRpDepartment, userId;
        public SalesClientFeedbackDairy()
        {
            InitializeComponent();
        }
        
        public void FollowUpGridLoad()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending' order by FollowUp.FollowUpId desc", con);
                //cmd = new SqlCommand("SELECT RTRIM(SClientFeedbackDairy.DateTimes),RTRIM(SClientFeedbackDairy.Feedback),RTRIM(SFollowUp.Actions),RTRIM(SFollowUp.ResponsiblePerson),RTRIM(SFollowUp.Status) from SClientFeedbackDairy,SFollowUp where SClientFeedbackDairy.SClientFeedbackId=SFollowUp.SClientFeedbackId order by DateTimes", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset()
        {
            txt2SClientId.Text = "";
            txt2SClientName.Text = "";
            feedback2STextBox.Text = "";
            feedback2SDateTime.Text = "";
            action2SMultiTextBox.Text = "";
            txtResposible2SPerson.Text = "";
            followUpDeadline2STextBox.Text = "";
            followUpDeadline2STextBox.Text = "";
        }
        
        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Name,Designation,Department from Registration where UserId='" + userId + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    fName = (rdr.GetString(0));
                    designation = (rdr.GetString(1));
                    department = (rdr.GetString(2));


                }

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQuery = "insert into SClientFeedbackDairy(SClientId,DateTimes,Feedback,SubmittedBy,SBSDesignation,SBSDepartment,CurrentDate) Values(@sd1,@sd2,@sd3,@sd4,@sd5,@sd6,@sd7)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQuery);
                cmd.Connection = con;
               
                cmd.Parameters.AddWithValue("@sd1", txt2SClientId.Text);
                cmd.Parameters.AddWithValue("@sd2", Convert.ToDateTime(feedback2SDateTime.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                cmd.Parameters.AddWithValue("@sd3", feedback2STextBox.Text);
                cmd.Parameters.AddWithValue("@sd4", fName);
                cmd.Parameters.AddWithValue("@sd5", designation);
                cmd.Parameters.AddWithValue("@sd6", department);
                cmd.Parameters.AddWithValue("@sd7", DateTime.UtcNow.ToLocalTime());
                affectedRows1 = (int)cmd.ExecuteScalar();
                con.Close();



                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQuery2 = "insert into SFollowUp(SClientId,SClientFeedbackId,Actions,DeadLineDateTime,ResponsiblePerson,SubmittedBy,SDesignation,SDepartment,CurrentDate,Status) values(@sd1,@sd2,@sd3,@sd4,@sd5,@sd8,@sd9,@sd10,@sd11,@sd12) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQuery2);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@sd1", txt2SClientId.Text);
                cmd.Parameters.AddWithValue("@sd2", affectedRows1);
                cmd.Parameters.AddWithValue("@sd3", action2SMultiTextBox.Text);
                cmd.Parameters.AddWithValue("@sd4", Convert.ToDateTime(followUpDeadline2STextBox.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                //cmd.Parameters.AddWithValue("@sd4",followUpDeadline2STextBox.Text);
                cmd.Parameters.AddWithValue("@sd5", txtResposible2SPerson.Text);
                //cmd.Parameters.AddWithValue("@sd6", tempRpDesig);
                //cmd.Parameters.AddWithValue("@sd7", tempRpDepartment);
                cmd.Parameters.AddWithValue("@sd8", fName);
                cmd.Parameters.AddWithValue("@sd9", designation);
                cmd.Parameters.AddWithValue("@sd10", department);
                cmd.Parameters.AddWithValue("@sd11", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@sd12", "Pending");
                affectedRows2 = (int)cmd.ExecuteScalar();
                con.Close();

                MessageBox.Show("FeedBack Submitted Successfully.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                
                submitButton.Enabled = false;
                

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

        private void responsiblePersonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                SqlConnection myConnection = default(SqlConnection);
                myConnection = new SqlConnection(cs.DBConn);

                SqlCommand myCommand = default(SqlCommand);

                myCommand = new SqlCommand("SELECT Name FROM Registration WHERE Name = @username ", myConnection);
                SqlParameter fullName = new SqlParameter("@username", SqlDbType.VarChar);
                fullName.Value = txtResposible2SPerson.Text;
                myCommand.Parameters.Add(fullName);
                myCommand.Connection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (myReader.Read() == true)
                {

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select Designation,Department from Registration where Name='" + txtResposible2SPerson.Text + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        tempRpDesig = (rdr.GetString(0));
                        tempRpDepartment = (rdr.GetString(1));
                    }

                    con.Close();
                }
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
                cmd = new SqlCommand("SELECT RTRIM(SalesClient.SClientId),RTRIM(SalesClient.ClientName),RTRIM(SalesClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from SalesClient,ContactPersonDetails  where SalesClient.SClientId=ContactPersonDetails.SClientId  order by SalesClient.SClientId desc", con); 
               // cmd = new SqlCommand("SELECT RTRIM(SalesClient.SClientId),RTRIM(SalesClient.ClientName),RTRIM(SalesClient.EmailAddress),RTRIM(SalesClient.ContactPersonName),RTRIM(SalesClient.CellNumber) from SalesClient  order by SClientId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView2.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
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
            FollowUpGridLoad();
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
    }
}
