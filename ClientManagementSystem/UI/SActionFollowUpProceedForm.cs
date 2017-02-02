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
        public int affectedRows4,rpUserId;
        private SqlDataAdapter sda;
        //public static int userId;

        public SActionFollowUpProceedForm()
        {
            InitializeComponent();
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
            if (responsible3SPerson2ComboBox.Text == "")
            {
                MessageBox.Show("You must write your Action Before Submit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                responsible3SPerson2ComboBox.Focus();
                return;

            }

            try
            {


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "insert into FollowUp(IClientId,IClientFeedbackId,Actions,DeadLineDateTime,RPUserId,SBUserId,CurrentDate,Status) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";

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
                //Reset();
                //Report();
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

        private void responsible3SPerson2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cst = "Select Registration.UserId from Registration Where  Registration.Name='" + responsible3SPerson2ComboBox.Text + "'";
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
                string ct = "select RTRIM(Name) from Registration order by Name";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    responsible3SPerson2ComboBox.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void PopulateClientIdCombo()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cty = "select RTRIM(SalesClient.SClientId) from SalesClient order by SalesClient.SClientId  desc";
                cmd = new SqlCommand(cty);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   txt3SClientId .Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void PopulateClientName()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cty = "select RTRIM(SalesClient.ClientName) from SalesClient order by  SalesClient.SClientId  desc";
                cmd = new SqlCommand(cty);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmb3SClientName.Items.Add(rdr[0]);
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
        //public void LoadClientgGrid()
        //{
        //    try
        //    {
        //        con = new SqlConnection(cs.DBConn);
        //        con.Open();
        //       // cmd = new SqlCommand("SELECT RTRIM(InquieryClient.IClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails,FollowUp  where InquieryClient.IClientId=ContactPersonDetails.IClientId  and  InquieryClient.IClientId=FollowUp.IClientId order by InquieryClient.IClientId desc", con);
        //        cmd = new SqlCommand("SELECT RTRIM(SalesClient.SClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(SalesClient.ClientName),RTRIM(SalesClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from SalesClient,ContactPersonDetails,FollowUp  where SalesClient.SClientId=ContactPersonDetails.SClientId  and  SalesClient.SClientId=FollowUp.SClientId order by SalesClient.SClientId desc", con);
        //        rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        dataGridView2.Rows.Clear();
        //        while (rdr.Read() == true)
        //        {
        //            dataGridView2.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5]);
        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void SActionFollowUpProceedForm_Load(object sender, EventArgs e)
        {
           // LoadClientgGrid();
           PopulateClientIdCombo();
            PopulateClientName();
            PopulateResponsiblePerson();
            userId = LoginForm.uId.ToString();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
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
                txtClientName.Text = dr.Cells[1].Value.ToString();

                h.Text = k.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
