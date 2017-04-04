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
    public partial class SActionFollowUpProceedForm : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public string sbName, sbDesignation, sbDepartment, userId, sClientFeedBackId;
        public int affectedRows4, rpUserId, modeOfConductId;
        private SqlDataAdapter sda;
        //public static int userId;

        public SActionFollowUpProceedForm()
        {
            InitializeComponent();
        }


        private void Reset()
        {
            txt3SClientId.Clear();
            txt3SClientName.Clear();
            sClientFeedBackId ="";
            cmb3SRP.SelectedIndex = -1;
            action3SMultiTextBox.Clear();

        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (txt3SClientId.Text == "")
            {
                MessageBox.Show("Please Select  a Client Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt3SClientId.Focus();
                return;

            }
            if (action3SMultiTextBox.Text == "")
            {
                MessageBox.Show("You must write your Action Before Submit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                action3SMultiTextBox.Focus();
                return;

            }
            if (cmb3SRP.Text == "")
            {
                MessageBox.Show("You must write your Action Before Submit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb3SRP.Focus();
                return;

            }

            try
            {


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "insert into FollowUp(IClientId,IClientFeedbackId,Actions,DeadLineDateTime,RPUserId,SBUserId,CurrentDate,Statuss) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txt3SClientId.Text);
                cmd.Parameters.AddWithValue("@d2", sClientFeedBackId);
                cmd.Parameters.AddWithValue("@d3", action3SMultiTextBox.Text);
                cmd.Parameters.AddWithValue("@d4", Convert.ToDateTime(followUp3SDeadlineDateTimePicker.Value, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                cmd.Parameters.AddWithValue("@d5", rpUserId);
                cmd.Parameters.AddWithValue("@d6", userId);
                cmd.Parameters.AddWithValue("@d7", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@d8", "Pending");
                affectedRows4 = (int)cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("Saccesfully Submitted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                //Report();
               
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

        private void responsible3SPerson2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cst = "Select Registration.UserId from Registration Where  Registration.Name='" + cmb3SRP.Text + "'";
                    cmd = new SqlCommand(cst);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        rpUserId = (rdr.GetInt32(0));

                    }
                    con.Close();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
           
        }
        public void PopulateResponsiblePerson()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Name) from Registration where Statuss!='InActive' order by Name";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmb3SRP.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ProductDetailsGrid()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = new SqlCommand("SELECT RTRIM(SalesClient.SClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(SalesClient.ClientName),RTRIM(SalesClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from SalesClient,ContactPersonDetails,FollowUp  where SalesClient.SClientId=ContactPersonDetails.SClientId  and  SalesClient.SClientId=FollowUp.SClientId order by SalesClient.SClientId desc", con);
            sda = new SqlDataAdapter("SELECT RTRIM(SalesClient.SClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(SalesClient.ClientName),RTRIM(SalesClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from SalesClient,ContactPersonDetails,FollowUp  where SalesClient.SClientId=ContactPersonDetails.SClientId  and  SalesClient.SClientId=FollowUp.SClientId order by SalesClient.SClientId desc", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 120;

            dataGridView1.Columns[6].Width = 180; // or whatever width works well for abbrev
            //dataGridView1.Columns[2].Width = dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[1].Width - 72;  
            con.Close();
        }
        public void SalesClientFollowUpGrid()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending' and IClientFeedbackDairy.SClientId='" + txt3SClientId.Text + "' order by FollowUp.FollowUpId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView2.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadClientgGrid()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT SalesClient.SClientId, FollowUp.IClientFeedbackId, SalesClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM  SalesClient INNER JOIN FollowUp ON SalesClient.SClientId = FollowUp.SClientId  INNER JOIN EmailBank ON SalesClient.EmailBankId = EmailBank.EmailBankId  INNER JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId order by SalesClient.SClientId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SActionFollowUpProceedForm_Load(object sender, EventArgs e)
        {
            LoadClientgGrid();
           //PopulateClientIdCombo();
           // PopulateClientName();
            PopulateResponsiblePerson();
            userId = LoginForm.uId.ToString();
            followUp3SDeadlineDateTimePicker.MinDate = DateTime.Today;
            //followUp1DeadlineDateTimePicker.MaxDate = DateTime.Today.AddMonths(1); 
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

    

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.CurrentRow;
                txt3SClientId.Text = dr.Cells[0].Value.ToString();
                sClientFeedBackId = dr.Cells[1].Value.ToString();
                txt3SClientName.Text = dr.Cells[2].Value.ToString();

                h.Text = k.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt3SClientId_TextChanged(object sender, EventArgs e)
        {
            SalesClientFollowUpGrid();
        }

        private void cmbModeOfConduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void searchByIDTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                //string sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails,FollowUp  where InquieryClient.IClientId=ContactPersonDetails.IClientId  and  InquieryClient.IClientId=FollowUp.IClientId and InquieryClient.IClientId like '" + searchByClientIDTextBox.Text + "%' order by InquieryClient.IClientId desc";
                // String sql = "SELECT RTRIM(IClientId),RTRIM(ClientName),RTRIM(EmailAddress),RTRIM(ContactPersonName),RTRIM(CellNumber) from InquieryClient where InquieryClient.IClientId like '" + textBox6.Text + "%'order by InquieryClient.IClientId desc";
                String sql = "SELECT InquieryClient.IClientId, FollowUp.IClientFeedbackId, InquieryClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM InquieryClient INNER JOIN FollowUp ON InquieryClient.IClientId = FollowUp.IClientId INNER JOIN ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId INNER JOIN EmailBank ON InquieryClient.EmailBankId = EmailBank.EmailBankId WHERE (InquieryClient.IClientId LIKE '" + searchByIDTextBox.Text + "%')";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                //string sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails,FollowUp  where InquieryClient.IClientId=ContactPersonDetails.IClientId  and  InquieryClient.IClientId=FollowUp.IClientId and InquieryClient.IClientId like '" + searchByClientIDTextBox.Text + "%' order by InquieryClient.IClientId desc";
                // String sql = "SELECT RTRIM(IClientId),RTRIM(ClientName),RTRIM(EmailAddress),RTRIM(ContactPersonName),RTRIM(CellNumber) from InquieryClient where InquieryClient.IClientId like '" + textBox6.Text + "%'order by InquieryClient.IClientId desc";
                String sql = "SELECT InquieryClient.IClientId, FollowUp.IClientFeedbackId, InquieryClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM InquieryClient INNER JOIN FollowUp ON InquieryClient.IClientId = FollowUp.IClientId INNER JOIN ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId INNER JOIN EmailBank ON InquieryClient.EmailBankId = EmailBank.EmailBankId WHERE (InquieryClient.ClientName LIKE '" + txtClientName.Text + "%')";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
