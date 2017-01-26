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
using ClientManagementSystem.Manager;
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class ActionFollowUpProceedForm : Form
    {
       public  int affectedRows4;
         SqlConnection con;
         SqlCommand cmd;
        SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        private ClientGateway aClientGateway;
        private InqueryClient aIClient;
        public string sbUserId,iClientFeedBackId;
        public int rpUserId;
        public ActionFollowUpProceedForm()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            txt3ClientId.Text = "";
            txt3ClientName.Text = "";
            action2MultiTextBox.Text = "";
            responsiblePerson2ComboBox.SelectedIndex = -1;
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (txt3ClientId.Text == "")
            {
                MessageBox.Show("Please Select  a Client Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt3ClientId.Focus();
                return;

            }
            if (action2MultiTextBox.Text == "")
            {
                MessageBox.Show("You must write your Action Before Submit","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                action2MultiTextBox.Focus();
                return;

            }
            if (responsiblePerson2ComboBox.Text == "")
            {
                MessageBox.Show("You must write your Action Before Submit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                responsiblePerson2ComboBox.Focus();
                return;

            }
           
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "insert into FollowUp(IClientId,IClientFeedbackId,Actions,DeadLineDateTime,RPUserId,SBUserId,CurrentDate,Statuss) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txt3ClientId.Text);
                cmd.Parameters.AddWithValue("@d2", iClientFeedBackId);
                cmd.Parameters.AddWithValue("@d3", action2MultiTextBox.Text);
                cmd.Parameters.AddWithValue("@d4", Convert.ToDateTime(followUpDeadlineDateTimePicker.Value, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
                cmd.Parameters.AddWithValue("@d5", rpUserId);
                cmd.Parameters.AddWithValue("@d6", sbUserId);
                cmd.Parameters.AddWithValue("@d7", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@d8", "Pending");
                affectedRows4 = (int)cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("Saccesfully Submitted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    responsiblePerson2ComboBox.Items.Add(rdr[0]);
                }
                con.Close();

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
                con = new SqlConnection(cs.DBConn2);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(ProductID),RTRIM(ProductName),RTRIM(Category.ID),RTRIM(CategoryName),RTRIM(SubCategory.ID),RTRIM(SubCategoryName),RTRIM(Features),RTRIM(Price),Image from Product,Category,SubCategory where Product.CategoryID=Category.ID and Product.SubCategoryID=SubCategory.ID order by Productname", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8]);
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
                cmd = new SqlCommand("Select * from FollowUp2");
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        private void MyFollowUpList()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();

                cmd = new SqlCommand("SELECT RTRIM(FollowUp.IClientFeedbackId), RTRIM(FollowUp.DeadLineDateTime),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  FollowUp,Registration where FollowUp.SBUserId=Registration.UserId and FollowUp.Statuss='Pending' order by FollowUp.FollowUpId desc", con);
                //cmd = new SqlCommand("SELECT RTRIM(FollowUp.IClientId),RTRIM(FollowUp.DeadLineDateTime),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  FollowUp,InquieryClient,Registration  where  Registration.UserId=FollowUp.SBUserId and FollowUp.Statuss='Pending' order by FollowUp.FollowUpId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3],rdr[4]);
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
                cmd = new SqlCommand("SELECT RTRIM(InquieryClient.IClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails,FollowUp  where InquieryClient.IClientId=ContactPersonDetails.IClientId  and  InquieryClient.IClientId=FollowUp.IClientId order by InquieryClient.IClientId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView2.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3],rdr[4],rdr[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActionFollowUpProceedForm_Load(object sender, EventArgs e)
        {
            LoadClientgGrid();
            PopulateResponsiblePerson();
            sbUserId = LoginForm.uId.ToString();
            followUpDeadlineDateTimePicker.MinDate=DateTime.Now;
            MyFollowUpList();

        }

        private void responsiblePerson2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                
                    con=new SqlConnection(cs.DBConn);
                    con.Open();
                    string cst = "Select UserId from Registration Where Name='" + responsiblePerson2ComboBox.Text + "'";
                    cmd=new SqlCommand(cst);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void feedBackButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
           dynamic aform=new InqueiryClientFeedbackDairy();
            aform.ShowDialog();
            this.Visible = true;
        }
        private void Report()
        {
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = "id";
            paramDiscreteValue.Value = affectedRows4;
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

      private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails,FollowUp  where InquieryClient.IClientId=ContactPersonDetails.IClientId  and  InquieryClient.IClientId=FollowUp.IClientId and InquieryClient.IClientId like '" + textBox6.Text + "%' order by InquieryClient.IClientId desc";
               // String sql = "SELECT RTRIM(IClientId),RTRIM(ClientName),RTRIM(EmailAddress),RTRIM(ContactPersonName),RTRIM(CellNumber) from InquieryClient where InquieryClient.IClientId like '" + textBox6.Text + "%'order by InquieryClient.IClientId desc";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView2.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4],rdr[5]);
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
                string sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(FollowUp.IClientFeedbackId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails,FollowUp  where InquieryClient.IClientId=ContactPersonDetails.IClientId  and  InquieryClient.IClientId=FollowUp.IClientId and InquieryClient.ClientName like '" + txtClientName.Text + "%' order by InquieryClient.IClientId desc";
                //String sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(InquieryClient.ContactPersonName),RTRIM(InquieryClient.CellNumber) from InquieryClient where InquieryClient.ClientName like '" + txtClientName.Text + "%'order by InquieryClient.IClientId desc";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView2.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4],rdr[5]);
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

                txt3ClientId.Text = dr.Cells[0].Value.ToString();
                iClientFeedBackId = dr.Cells[1].Value.ToString();
                txt3ClientName.Text = dr.Cells[2].Value.ToString(); 
                k.Text = labelh.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15,
                e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }
    }
}
