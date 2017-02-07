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
        public int affectedRows6, affectedRows5;
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
                string cb2 = "Update FollowUp Set Statuss='Done', NextFeedBackId='" + affectedRows6 + "' Where FollowUp='" + cmbSFollowUpId.Text + "'";
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
            if (txtHaveToDo.Text == "")
            {
                MessageBox.Show("You must write your Action Before Submit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHaveToDo.Focus();
                return;

            }
            
            try
            {                                
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string insertQuery = "insert into IClientFeedbackDairy(SClientId,ClientInquiry,Feedback,DateTimes,UserId) Values(@cd1,@cd2,@cd3,@cd4,@cd5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(insertQuery,con);
                    DateTime myTime = Convert.ToDateTime(txtDeadlineTime.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                    cmd.Parameters.AddWithValue("@cd1", txtSClientId.Text);
                    cmd.Parameters.AddWithValue("@cd2", txtSInquiryClient);
                    cmd.Parameters.AddWithValue("@cd3", txtHaveToDo.Text);
                    cmd.Parameters.AddWithValue("@cd4", myTime);                 
                    cmd.Parameters.AddWithValue("@cd5", nUserId);                                                       
                    affectedRows6 = (int)cmd.ExecuteScalar();
                    con.Close();
                    SaveStatus();
                    MessageBox.Show("Saccesfully Submitted ", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtHaveToDo.Text = "";
            txtHaveDone.Text = "";
            txtSClientId.Text = "";
            txtSCPName.Text = "";
            txtSReferredBy.Text = "";
            txtSCPName.Text = "";
            txtSCContactNo.Text = "";
            //txtSalesClientStatus.SelectedIndex = -1;
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
        //public void GetData()
        //{
        //    try
        //    {
        //        con = new SqlConnection(cs.DBConn);
        //        con.Open();
        //        cmd = new SqlCommand("Select RTRIM(FollowUp.SClientId),RTRIM(FollowUp.DeadLineDateTime) as Date,RTRIM(FollowUp.Actions) as Action,RTRIM(FollowUp.SBUserId),RTRIM(FollowUp.Statuss) from FollowUp Where FollowUp.Statuss='Pending'", con);
        //        //cmd = new SqlCommand("Select FollowUp.SClientId,FollowUp.DeadLineDateTime,FollowUp.Actions, FollowUp.SubmittedBy,FollowUp.Statuss from FollowUp Where Statuss='Pending' ", con);
        //        rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        dataGridView1.Rows.Clear();
        //        while (rdr.Read() == true)
        //        {
        //            dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //public void PopulateFollowUpId()
        //{
        //    try
        //    {
        //        con = new SqlConnection(cs.DBConn);
        //        con.Open();
        //        string sqt = "Select Name from Registration Where UserId='" + nUserId + "'";
        //        cmd = new SqlCommand(sqt);
        //        cmd.Connection = con;
        //        rdr = cmd.ExecuteReader();
        //        if (rdr.Read())
        //        {
        //            sbName5 = (rdr.GetString(0));
        //        }
        //        con.Close();

        //        con = new SqlConnection(cs.DBConn);
        //        con.Open();
        //        string cty = "select RTRIM(FollowUp.FollowUpId) from FollowUp Where FollowUp.Statuss='Pending' and FollowUp.RPUserId='" + nUserId + "' order by FollowUpId";
        //        cmd = new SqlCommand(cty);
        //        cmd.Connection = con;
        //        rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            cmbSFollowUpId.Items.Add(rdr[0]);
        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
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
            //GetData();
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
    }
}
