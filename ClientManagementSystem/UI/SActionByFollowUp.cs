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
using ClientManagementSystem.Gateway;
using ClientManagementSystem.LoginUI;

namespace ClientManagementSystem.UI
{
    public partial class SActionByFollowUp : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public string sbName5, sbDesignation, sbDepartment, nUserId;
        public int currentId, affectedRows5, modeOfConductId;
        public ClientGateway clientGateway;
        private SalesFollowUp sAction;
        private SalesClient sClient;

        public SActionByFollowUp()
        {
            InitializeComponent();
        }
        public void SaveStatus()
        {

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb2 = "Update FollowUp Set Statuss='Done', NextFeedBackId='" + currentId + "' Where FollowUp.FollowUpId='" + cmbSFollowUpId.Text + "'";
                cmd = new SqlCommand(cb2,con);               
                cmd.ExecuteReader();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (txtSClientId.Text == "")
            {
                MessageBox.Show("Please enter  a Client Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSClientId.Focus();
                return;

            }
            if (txtSInquiryClient.Text == "")
            {
                MessageBox.Show("Please Write Client Inquiry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSInquiryClient.Focus();
                return;

            }
            if (txtHaveDone.Text == "")
            {
                MessageBox.Show("Please Write Feedback", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHaveDone.Focus();
                return;

            }
            if (cmbModeOfConduct.Text == "")
            {
                MessageBox.Show("Please Select Mode of Conduct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbModeOfConduct.Focus();
                return;

            }
            try
            {                                
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string insertQuery = "insert into IClientFeedbackDairy(SClientId,ClientInquiry,Feedback,DateTimes,UserId,ModeOfConductId) Values(@cd1,@cd2,@cd3,@cd4,@cd5,@cd6)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(insertQuery,con);                    
                    cmd.Parameters.AddWithValue("@cd1", txtSClientId.Text);
                    cmd.Parameters.AddWithValue("@cd2", txtSInquiryClient.Text);
                    cmd.Parameters.AddWithValue("@cd3", txtHaveDone.Text);
                    cmd.Parameters.AddWithValue("@cd4", txtDeadlineTime.Text);                 
                    cmd.Parameters.AddWithValue("@cd5", nUserId);
                    cmd.Parameters.AddWithValue("@cd6", modeOfConductId);
                    currentId = (int) cmd.ExecuteScalar();
                    con.Close();
                    SaveStatus();
                    MessageBox.Show("Saccessfully Submitted ", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Reset()
        {
            cmbSFollowUpId.SelectedIndex = -1;
            txtHaveToDo.Clear();
            txtHaveDone.Clear();
            txtSClientId.Clear();
            txtSClientName.Clear();
            txtSInquiryClient.Clear();
            txtSReferredBy.Clear();
            txtSCPName.Clear();
            txtSCContactNo.Clear();
            cmbModeOfConduct.SelectedIndex = -1;

        }
        public void SaveStatus2()
        {

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb2 = "Update SFollowUp Set SFollowUp.Status='NotDone', NextFollowUpId='" + affectedRows5 + "'  Where SFollowUpId='" + cmbSFollowUpId.Text + "'";
                cmd = new SqlCommand(cb2);
                cmd.Connection = con;
                cmd.ExecuteReader();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
       
        public void PopulateFollowUpId()
        {
            try
            {


                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cty = "Select RTRIM(FollowUpId) from FollowUp Where FollowUp.SClientId is not NULL and  FollowUp.Statuss='Pending' order by FollowUp.FollowUpId desc";
                cmd = new SqlCommand(cty);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbSFollowUpId.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
        private void SActionByFollowUp_Load(object sender, EventArgs e)
        {
            PopulateFollowUpId();
            ModeOfConduct();
            nUserId = LoginForm.uId.ToString();
        }
        private void GetClientDetails()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string qry = "SELECT SalesClient.ClientName,ContactPersonDetails.ContactPersonName,ContactPersonDetails.CellNumber FROM  SalesClient INNER JOIN  ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId  where SalesClient.SClientId='" + txtSClientId.Text + "'";
                cmd = new SqlCommand(qry, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    

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
        private void GetFollowUpDetails()
        {
            try
            {
                con=new SqlConnection(cs.DBConn);
                con.Open();
                string qry ="SELECT SalesClient.SClientId,SalesClient.ClientName,ContactPersonDetails.ContactPersonName,ContactPersonDetails.CellNumber,FollowUp.Actions,FollowUp.DeadLineDateTime, Registration.Name FROM SalesClient  INNER JOIN  ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId  INNER JOIN  FollowUp ON SalesClient.SClientId = FollowUp.SClientId  INNER JOIN  Registration ON SalesClient.UserId = Registration.UserId Where FollowUp.SBUserId=Registration.UserId and  FollowUp.FollowUpId='"+cmbSFollowUpId.Text+"' ";                
                cmd=new SqlCommand(qry,con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    txtSClientId.Text = Convert.ToString((rdr.GetInt32(0)));
                    txtSClientName.Text = (rdr.GetString(1).Trim());
                    txtSCPName.Text = (rdr.GetString(2).Trim());
                    txtSCContactNo.Text = (rdr.GetString(3).Trim());
                    txtHaveToDo.Text = (rdr.GetString(4));
                    txtDeadlineTime.Text = Convert.ToString((rdr.GetDateTime(5)));
                    txtSReferredBy.Text = (rdr.GetString(6));

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
        private void cmbSFollowUpId_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetFollowUpDetails();

            //try
            //{
            //    clientGateway = new ClientGateway();
            //    decimal followUpId = Convert.ToInt64(cmbSFollowUpId.Text);
            //    sAction = clientGateway.SearchSFollowUp(followUpId);

            //    txtSClientId.Text = Convert.ToString(sAction.SClientId);
            //    txtHaveToDo.Text = sAction.SAction;
            //    txtSReferredBy.Text = sAction.SSubmittedBy;

            //}


            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void txtSClientId_TextChanged(object sender, EventArgs e)
        {

           // GetClientDetails();

            //try
            //{
            //    clientGateway = new ClientGateway();
            //    decimal sdClientId = Convert.ToInt64(txtSClientId.Text);
            //    sClient = clientGateway.SearchSalesClients(sdClientId);

            //    txtSClientId.Text = Convert.ToString(sClient.IClientId);
            //    txtSClientName.Text = sClient.SClientName;
            //    txtSCPName.Text = sClient.SContactPersonName;
            //    txtSCContactNo.Text = sClient.SCellNumber;

            //}


            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void txtSCContactNo_TextChanged(object sender, EventArgs e)
        {

        }
        private void ModeOfConduct()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "select ModesOfConduct from ModeOfConducts";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbModeOfConduct.Items.Add(rdr.GetValue(0).ToString());
                }
                cmbModeOfConduct.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbModeOfConduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModeOfConduct.Text == "Not In The List")
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Please Input Mode Of Conduct  Here", "Input Here", "", -1, -1);
                if (string.IsNullOrWhiteSpace(input))
                {
                    cmbModeOfConduct.SelectedIndex = -1;
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select ModesOfConduct from ModeOfConducts where ModesOfConduct='" + input + "'";
                    cmd = new SqlCommand(ct2, con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read() && !rdr.IsDBNull(0))
                    {
                        MessageBox.Show("This ModesOfConduct  Already Exists,Please Select From List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        cmbModeOfConduct.SelectedIndex = -1;
                    }
                    else
                    {
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string query1 = "insert into ModeOfConducts (ModesOfConduct, UserId,DateAndTime) values (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                            cmd = new SqlCommand(query1, con);
                            cmd.Parameters.AddWithValue("@d1", input);
                            cmd.Parameters.AddWithValue("@d2", nUserId);
                            cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                            cmd.ExecuteNonQuery();

                            con.Close();
                            cmbModeOfConduct.Items.Clear();
                            ModeOfConduct();
                            cmbModeOfConduct.SelectedText = input;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT ModeOfConductId from ModeOfConducts WHERE ModesOfConduct= '" + cmbModeOfConduct.Text + "'";

                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        modeOfConductId = rdr.GetInt32(0);
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
        }
    }
}
