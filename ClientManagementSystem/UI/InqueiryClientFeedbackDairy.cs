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
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class InqueiryClientFeedbackDairy : Form
    {
        private int currentClientId,affectedRows2;
        private  SqlConnection con;
        private SqlCommand cmd;
        ConnectionString cs=new ConnectionString();
        private  SqlDataReader rdr;
     
        public string userId,rpUserId;
        public int modeOfConductId;
        


        public InqueiryClientFeedbackDairy()
        {
            InitializeComponent();
        }
       
        private void Reset2()
        {
            txt2ClientId.Clear();
            txt2ClientName.Clear();
            feedback2TextBox.Clear();
            action2MultiTextBox.Clear();
            txtResposible2Person.Clear();
            txtClentInquiry.Clear();
            txtModeOfConduct.Clear();

        }
        private void submitButton_Click(object sender, EventArgs e)

        {
            FirstStepOfIClientFeedbackDairy fg = new  FirstStepOfIClientFeedbackDairy();
            fg.ResetFirstStepOfIClientfeedbackDairy();
            
            if (feedback2TextBox.Text == "")
            {
                MessageBox.Show("Please must Write Some FeedBack", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                feedback2TextBox.Focus();
                return;
            }
            if (action2MultiTextBox.Text == "")
            {
                MessageBox.Show("Please  Write your action.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                action2MultiTextBox.Focus();
                return;
            }
            try
            {
               
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQuery = "insert into IClientFeedbackDairy(IClientId,DateTimes,Feedback,ModeOfConductId,UserId,CurrentDate) Values(@d1,@d2,@d3,@d4,@d5,@d6)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQuery);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1",txt2ClientId.Text);
                cmd.Parameters.AddWithValue("@d2", Convert.ToDateTime(feedback2DateTime.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                cmd.Parameters.AddWithValue("@d3", feedback2TextBox.Text);
                cmd.Parameters.AddWithValue("@d4", modeOfConductId);
                cmd.Parameters.AddWithValue("@d5", userId);
                cmd.Parameters.AddWithValue("@d6", DateTime.UtcNow.ToLocalTime());
                currentClientId = (int)cmd.ExecuteScalar();
                con.Close();



                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQuery2 = "insert into FollowUp(IClientId,IClientFeedbackId,Actions,DeadLineDateTime,RPUserId,SBUserId,CurrentDate,Statuss) values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQuery2);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txt2ClientId.Text);
                cmd.Parameters.AddWithValue("@d2", currentClientId);
                cmd.Parameters.AddWithValue("@d3", action2MultiTextBox.Text);
                cmd.Parameters.AddWithValue("@d4", Convert.ToDateTime(followUp2Deadlinedatetime.Value, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                cmd.Parameters.AddWithValue("@d5", rpUserId);
                cmd.Parameters.AddWithValue("@d6", userId);
                cmd.Parameters.AddWithValue("@d7", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@d8", "Pending");
                affectedRows2 = (int)cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("FeedBack Submitted Successfully.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);                               
                Reset2();

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

     public void FollowUpGridLoad()
     {
                try
                 {
                  con = new SqlConnection(cs.DBConn);
                  con.Open();
                  cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending' and IClientFeedbackDairy.IClientId='" + txt2ClientId.Text + "' order by FollowUp.FollowUpId desc", con);
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
        public void GetClientDetails()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT InquieryClient.IClientId,InquieryClient.ClientName,EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM  InquieryClient INNER JOIN ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId  INNER JOIN  EmailBank ON InquieryClient.EmailBankId = EmailBank.EmailBankId order by InquieryClient.IClientId desc", con);
               // cmd = new SqlCommand("SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(InquieryClient.ContactPersonName),RTRIM(InquieryClient.CellNumber) from InquieryClient  order by InquieryClient.IClientId desc", con);
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
        private void InqueiryClientFeedbackDairy_Load(object sender, EventArgs e)
        {
            GetClientDetails();
            DisableMethod();
            userId = LoginForm.uId.ToString();
            feedback2DateTime.MaxDate = DateTime.Now;
            followUp2Deadlinedatetime.MinDate=DateTime.Today;
            followUp2Deadlinedatetime.MaxDate = DateTime.Today.AddMonths(1); 
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void DisableMethod()
        {
            txt2ClientId.Enabled = false;
            txt2ClientName.Enabled = false;
            txtClentInquiry.Enabled = false;
            feedback2TextBox.Enabled = false;
            action2MultiTextBox.Enabled = false;
            txtResposible2Person.Enabled = false;
            txtModeOfConduct.Enabled = false;
            feedback2DateTime.Enabled = false;
            followUp2Deadlinedatetime.Enabled = false;
            dataGridView2.Enabled = false;
        }
      
        private void Report2()
        {
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = "id";
            paramDiscreteValue.Value = currentClientId;
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
            paramDiscreteValue.Value = affectedRows2;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  where InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.IClientId like '" + textBox6.Text + "%' order by InquieryClient.IClientId desc";
                //String sql = "SELECT InquieryClient.IClientId,InquieryClient.ClientName,InquieryClient.EmailAddress,InquieryClient.ContactPersonName,InquieryClient.CellNumber from InquieryClient where InquieryClient.IClientId like '" + textBox6.Text + "%'order by InquieryClient.IClientId desc";
                cmd = new SqlCommand(sql, con);
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

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  where InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.ClientName like '" + txtClientName.Text + "%' order by InquieryClient.IClientId desc";
               // String sql = "SELECT InquieryClient.IClientId,InquieryClient.ClientName,InquieryClient.EmailAddress,InquieryClient.ContactPersonName,InquieryClient.CellNumber from InquieryClient where InquieryClient.ClientName like '" + txtClientName.Text + "%'order by InquieryClient.ClientName desc";
                cmd = new SqlCommand(sql, con);
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

        private void txtResposible2Person_TextChanged(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(UserId) from Registration where Name='"+txtResposible2Person.Text+"' ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    rpUserId = (rdr.GetString(0));
                }
                 con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView2.SelectedRows[0];
                txt2ClientId.Text = dr.Cells[0].Value.ToString();
                txt2ClientName.Text = dr.Cells[1].Value.ToString();

                label7.Text = labelg.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt2ClientId_TextChanged(object sender, EventArgs e)
        {
            FollowUpGridLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt2ClientId.Enabled = true;
            txt2ClientName.Enabled = true;
            txtClentInquiry.Enabled = true;
            feedback2TextBox.Enabled = true;
            action2MultiTextBox.Enabled = true;
            txtResposible2Person.Enabled = true;
            txtModeOfConduct.Enabled = true;
            dataGridView2.Enabled = true;


        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.CurrentRow;
                txt2ClientId.Text = dr.Cells[0].Value.ToString();
                txt2ClientName.Text = dr.Cells[1].Value.ToString();

                label7.Text = l12.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtModeOfConduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT ModeOfConductId from ModeOfConducts WHERE ModesOfConduct= '" + txtModeOfConduct.Text + "'";
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

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FirstStepOfIClientFeedbackDairy frm2=new FirstStepOfIClientFeedbackDairy();
            frm2.txt1ClientId.Text = txt2ClientId.Text;
            frm2.txt1ClientName.Text = txt2ClientName.Text;
            frm2.feedback1TextBox.Text = feedback2TextBox.Text;
            frm2.txtClientInquiry.Text = txtClentInquiry.Text;
            frm2.feedback1DeadlineDateTime.Value = feedback2DateTime.Value;
            frm2.cmbModeOfConduct.Text = txtModeOfConduct.Text;
            frm2.action1MultiTextBox.Text = action2MultiTextBox.Text;
            frm2.responsible1PersonComboBox.Text = txtResposible2Person.Text;
            frm2.followUp1DeadlineDateTimePicker.Value = followUp2Deadlinedatetime.Value;
            frm2.Show();
        }

        private void InqueiryClientFeedbackDairy_FormClosed(object sender, FormClosedEventArgs e)
        {
                  this.Hide();
       FeedBack frm=new FeedBack();
                  frm.Show();
        }

      
    }
}
