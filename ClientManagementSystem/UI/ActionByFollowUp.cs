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
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class ActionByFollowUp : Form
    {
         SqlConnection con;
         ConnectionString cs=new ConnectionString();
         SqlCommand cmd;
         SqlDataReader rdr;
        public int affectedRows5, affectedRows6;
        public string sbName5, sbDesignation, sbDepartment, nUserId,rPDesig,rPDept;
        private ClientGateway clientGateway;
        private InquiryFollowUp iaction;
        private InqueryClient clients;
        public string submittedBy;

        public ActionByFollowUp()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            cmbFollowUpId.SelectedIndex = -1;
            clientIdTextBox.Clear();
            youHaveToDoTextBox.Clear();
            whDoneTextBox.Clear();
            cPNameTextBox.Clear();
            cellNoTextBox.Clear();                        
            txtReferredBy.Clear();
            clientNameTextBox.Clear();
            
        }

        public void PopulateFollowUpId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cty = "Select RTRIM(FollowUpId) from FollowUp Where FollowUp.IClientId is not NULL and  FollowUp.Statuss='Pending' order by FollowUp.FollowUpId desc";
                cmd = new SqlCommand(cty);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbFollowUpId.Items.Add(rdr[0]);
                }
                con.Close();
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string qry = "SELECT InquieryClient.IClientId,InquieryClient.ClientName,ContactPersonDetails.ContactPersonName,ContactPersonDetails.CellNumber,FollowUp.Actions,FollowUp.DeadLineDateTime, Registration.Name FROM InquieryClient  INNER JOIN  ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId  INNER JOIN  FollowUp ON InquieryClient.IClientId = FollowUp.IClientId  INNER JOIN  Registration ON InquieryClient.UserId = Registration.UserId Where FollowUp.SBUserId=Registration.UserId and  FollowUp.FollowUpId='" + cmbFollowUpId.Text + "' ";
                cmd = new SqlCommand(qry, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    clientIdTextBox.Text = Convert.ToString((rdr.GetInt32(0)));
                    clientNameTextBox.Text = (rdr.GetString(1).Trim());
                    cPNameTextBox.Text = (rdr.GetString(2).Trim());
                    cellNoTextBox.Text = (rdr.GetString(3).Trim());
                    youHaveToDoTextBox.Text = (rdr.GetString(4));
                    txtDeadlineTime.Text = Convert.ToString((rdr.GetDateTime(5)));
                    txtReferredBy.Text = (rdr.GetString(6));

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
        private void followUpIdCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetFollowUpDetails();


            //try { 
            //clientGateway = new ClientGateway();
            //decimal followUpId = Convert.ToInt64(cmbFollowUpId.Text);
            //iaction = clientGateway.SearchFollowUp(followUpId);

            //    clientIdTextBox.Text = Convert.ToString(iaction.IClientId);
            //    youHaveToDoTextBox.Text = iaction.IAction;
            //    txtDeadlineTime.Text = Convert.ToString(iaction.IDeadFLine);
            //    txtReferredBy.Text = iaction.ISubmittedBy;

            //   }


            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
        public void SaveStatus()
        {
           
             try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb2 = "Update FollowUp Set Statuss='Done', NextFeedBackId='" + affectedRows6 + "' Where FollowUpId='" + cmbFollowUpId.Text + "'";
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
        public void SaveStatus2()
        {

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb2 = "Update FollowUp Set Status='NotDone', NextFollowUpId='"+affectedRows5+"'  Where FollowUpId='" + cmbFollowUpId.Text + "'";
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
   
       
        private void ActionByFollowUp_Load(object sender, EventArgs e)
        {           
            nUserId = LoginForm.uId.ToString();           
            PopulateFollowUpId();                       
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }       
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (clientIdTextBox.Text == "")
            {
                MessageBox.Show("Please enter  a Client Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientIdTextBox.Focus();
                return;

            }
            if (youHaveToDoTextBox.Text == "")
            {
                MessageBox.Show("You must write your Action Before Submit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                youHaveToDoTextBox.Focus();
                return;

            }
            
            try
            {                             
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string insertQuery = "insert into IClientFeedbackDairy(IClientId,DateTimes,Feedback,UserId,ClientInquiry) Values(@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(insertQuery);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", clientIdTextBox.Text);
                    cmd.Parameters.AddWithValue("@d2", Convert.ToDateTime(followUpDeadlineDateTimePicker.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                    cmd.Parameters.AddWithValue("@d3", youHaveToDoTextBox.Text);
                    cmd.Parameters.AddWithValue("@d4", nUserId);
                    cmd.Parameters.AddWithValue("@d5", txtClientInquiryOrFeedback.Text); 
                    affectedRows6 = (int) cmd.ExecuteScalar();
                    con.Close();
                    SaveStatus();
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
        private void Report2()
        {
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = "id";
            paramDiscreteValue.Value = affectedRows6;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            ReportViewer f2 = new ReportViewer();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "NewProductList";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            FeedBackInput cr = new FeedBackInput();
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields;
            f2.crystalReportViewer1.ReportSource = cr;


            f2.ShowDialog();

        }
        private void Report()
        {
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = "id";
            paramDiscreteValue.Value = affectedRows5;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);
            ReportViewer f2 = new ReportViewer();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "NewProductList";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            FollowUpInput cr = new FollowUpInput();
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields;
            f2.crystalReportViewer1.ReportSource = cr;


            f2.ShowDialog();

        }

        private void clientIdTextBox_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    clientGateway = new ClientGateway();
            //    decimal clientId = Convert.ToInt64(clientIdTextBox.Text);
            //    clients = clientGateway.SearchClients(clientId);

            //    clientIdTextBox.Text = Convert.ToString(clients.IClientId);
            //    clientNameTextBox.Text = clients.ClientName;
            //    //cPNameTextBox.Text = clients.ContactPersonName;
            //    //cellNoTextBox.Text = clients.CellNumber;

            //}


            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
           
        }

        private void cellNoTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
