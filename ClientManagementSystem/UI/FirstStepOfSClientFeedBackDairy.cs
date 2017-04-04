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

namespace ClientManagementSystem.UI
{
    public partial class FirstStepOfSClientFeedBackDairy : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public int modeOfConductId;
        public string userId;
        public FirstStepOfSClientFeedBackDairy()
        {
            InitializeComponent();
        }
       
       
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT SalesClient.SClientId, SalesClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM  SalesClient INNER JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId INNER JOIN EmailBank ON SalesClient.EmailBankId = EmailBank.EmailBankId  order by SalesClient.SClientId desc", con); 
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
        public void FollowUpGridLoad33()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(SClientFeedbackDairy.DateTimes),RTRIM(SClientFeedbackDairy.Feedback),RTRIM(SFollowUp.Actions),RTRIM(SFollowUp.ResponsiblePerson),RTRIM(SFollowUp.Status)from SClientFeedbackDairy,SFollowUp where SClientFeedbackDairy.IClientId=SFollowUp.IClientId and SFollowUp.Status='Pending' order by DateTimes desc", con);
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
        public void RPFillCombo()
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
                    responsibleSPersonComboBox.Items.Add(rdr[0]);
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
                cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending'  and FollowUp.SClientId='"+txtSClientId.Text+"' order by FollowUp.FollowUpId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView2.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView2.Rows.Add(rdr[0],rdr[1],rdr[2],rdr[3],rdr[4]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FirstStepOfSClientFeedBackDairy_Load(object sender, EventArgs e)
        {
            RPFillCombo();
            ModeOfConduct();
            GetData();
            feedbackSDeadlineDateTime.MaxDate = DateTime.Now;
            followUpSDeadlinedate.MinDate = DateTime.Today;
            followUpSDeadlinedate.MaxDate = DateTime.Today.AddMonths(1); 
        }

        public  void ResetOfSClientFeedbackDairy()
        {
            txtSClientId.Clear();
            txtClientName.Clear();
            feedbackSTextBox.Clear();
            actionSMultiTextBox.Clear();
            responsibleSPersonComboBox.SelectedIndex = -1;
            cmbModeOfConduct.SelectedIndex = -1;
            feedbackSDeadlineDateTime.Value=DateTime.Today;
            followUpSDeadlinedate.Value=DateTime.Today;
            
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSClientId.Text))
            {
                MessageBox.Show("Please select Client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSClientInquiry.Text))
            {
                MessageBox.Show("Please write Client Inquiry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(feedbackSTextBox.Text))
            {
                MessageBox.Show("Please select type your Feedback", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbModeOfConduct.Text))
            {
                MessageBox.Show("Please select Mode Of Conduct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(actionSMultiTextBox.Text))
            {
                MessageBox.Show("Please write your probable Action", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(responsibleSPersonComboBox.Text))
            {
                MessageBox.Show("Please select  this Job Responsible Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Hide();
                SalesClientFeedbackDairy frm3 = new SalesClientFeedbackDairy();
                frm3.txt2SClientId.Text = txtSClientId.Text;
                frm3.txt2SClientName.Text = txtSClientName.Text;
                frm3.txtClientInquiry.Text = txtSClientInquiry.Text;
                frm3.feedback2STextBox.Text = feedbackSTextBox.Text;
                frm3.txtModeOfConduct.Text = cmbModeOfConduct.Text;
                frm3.feedback2SDateTime.Value = feedbackSDeadlineDateTime.Value;
                frm3.action2SMultiTextBox.Text = actionSMultiTextBox.Text;
                frm3.txtResposible2SPerson.Text = responsibleSPersonComboBox.Text;
                frm3.followUpDeadline2STextBox.Value = followUpSDeadlinedate.Value;
                frm3.Show();                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

           
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void txtSClientId_TextChanged(object sender, EventArgs e)
        {
            FollowUpGridLoad();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.CurrentRow;
                txtSClientId.Text = dr.Cells[0].Value.ToString();
                txtSClientName.Text = dr.Cells[1].Value.ToString();

                label7.Text = labelh.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                            cmd.Parameters.AddWithValue("@d2", userId);
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

        private void FirstStepOfSClientFeedBackDairy_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            FeedBack frm = new FeedBack();
            frm.Show();
        }

        private void searchBySClientIDTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                //String sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  where InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.ClientName like '" + txtClientName.Text + "%' order by InquieryClient.IClientId desc";
                String sql = "SELECT SalesClient.SClientId,SalesClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM SalesClient INNER JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId INNER JOIN EmailBank ON SalesClient.EmailBankId = EmailBank.EmailBankId  WHERE (SalesClient.SClientId LIKE '" + searchBySClientIDTextBox.Text + "%') order by SalesClient.ClientName desc";
                cmd = new SqlCommand(sql, con);
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

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                //String sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  where InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.ClientName like '" + txtClientName.Text + "%' order by InquieryClient.IClientId desc";
                String sql = "SELECT SalesClient.SClientId,SalesClient.ClientName, EmailBank.Email, ContactPersonDetails.ContactPersonName, ContactPersonDetails.CellNumber FROM SalesClient INNER JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId INNER JOIN EmailBank ON SalesClient.EmailBankId = EmailBank.EmailBankId  WHERE (SalesClient.ClientName LIKE '" + txtClientName.Text + "%') order by SalesClient.ClientName desc";
                cmd = new SqlCommand(sql, con);
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
    }
}
