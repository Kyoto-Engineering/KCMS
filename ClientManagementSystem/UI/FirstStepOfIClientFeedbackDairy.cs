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
    public partial class FirstStepOfIClientFeedbackDairy : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;

        ConnectionString cs =new ConnectionString();
        public string userId;
        public int modeOfConductId;
        public FirstStepOfIClientFeedbackDairy()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt1ClientId.Text))
            {
                MessageBox.Show("Please select Client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtClientInquiry.Text))
            {
                MessageBox.Show("Please write Client Inquiry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(feedback1TextBox.Text))
            {
                MessageBox.Show("Please select type your Feedback", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(action1MultiTextBox.Text))
            {
                MessageBox.Show("Please write your probable Action", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(responsible1PersonComboBox.Text))
            {
                MessageBox.Show("Please select  this Job Responsible Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                this.Visible = false;
                dynamic frm2 = new InqueiryClientFeedbackDairy();                
                frm2.txt2ClientId.Text = txt1ClientId.Text;
                frm2.txt2ClientName.Text = txt1ClientName.Text;
                frm2.feedback2TextBox.Text = feedback1TextBox.Text;
                frm2.txtClentInquiry.Text = txtClientInquiry.Text;
                frm2.feedback2DateTime.Value = feedback1DeadlineDateTime.Value;
                frm2.txtModeOfConduct.Text = cmbModeOfConduct.Text;
                frm2.action2MultiTextBox.Text = action1MultiTextBox.Text;
                frm2.txtResposible2Person.Text = responsible1PersonComboBox.Text;
                frm2.followUp2Deadlinedatetime.Value = followUp1DeadlineDateTimePicker.Value;
                frm2.ShowDialog();
                this.Visible = true;
               
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  where InquieryClient.IClientId=ContactPersonDetails.IClientId  order by InquieryClient.IClientId desc", con);
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
        public void FollowUpGridLoad2()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending' and IClientFeedbackDairy.IClientId='" + txt1ClientId.Text+ "' order by FollowUp.FollowUpId desc", con);
               // cmd = new SqlCommand("SELECT RTRIM(FollowUp.IClientId),RTRIM(FollowUp.DeadLineDateTime),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Status) FROM  FollowUp,InquieryClient,Registration  where  Registration.UserId=FollowUp.RPUserId and FollowUp.Status='Pending' order by FollowUp.FollowUpId desc", con);
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

        public void FillCombo2()
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
                    responsible1PersonComboBox.Items.Add(rdr[0]);
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
            txt1ClientId.Clear();
            txt1ClientName.Clear();
            feedback1TextBox.Clear();
            action1MultiTextBox.Clear();
            txtClientInquiry.Clear();
            responsible1PersonComboBox.SelectedIndex = -1;


        }
        private void FirstStepOfIClientFeedbackDairy_Load (object sender, EventArgs e)
        {
            ModeOfConduct();
                FillCombo2();
                GetData();
                userId = LoginForm.uId.ToString();
            }

            private void dataGridView1_RowPostPaint (object sender, DataGridViewRowPostPaintEventArgs e)
            {
                string strRowNumber = (e.RowIndex + 1).ToString();
                SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
                if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
                {
                    dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
                }
                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15,
                    e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height)/2));
            }

            private void dataGridView1_RowHeaderMouseClick  (object sender, DataGridViewCellMouseEventArgs e)
            {
                //try
                //{
                //    DataGridViewRow dr = dataGridView1.SelectedRows[0];
                //    txt1ClientId.Text = dr.Cells[0].Value.ToString();
                //    txt1ClientName.Text = dr.Cells[1].Value.ToString();

                //    label7.Text = labelh.Text;
                //}

                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }

            private void textBox6_TextChanged  (object sender, EventArgs e)
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  where InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.IClientId like '" + textBox6.Text + "%' order by InquieryClient.IClientId desc";
                    //String sql ="SELECT InquieryClient.IClientId,InquieryClient.ClientName,InquieryClient.EmailAddress,InquieryClient.ContactPersonName,InquieryClient.CellNumber from InquieryClient where InquieryClient.IClientId like '" +textBox6.Text + "%'order by InquieryClient.IClientId desc";
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

            private void txtClientName_TextChanged (object sender, EventArgs e)
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String sql = "SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  where InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.ClientName like '" + txtClientName.Text + "%' order by InquieryClient.IClientId desc";
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
          private void groupBox1_Enter(object sender, EventArgs e)
       {
        
        }

          private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
          {
              try
              {
                  DataGridViewRow dr = dataGridView1.CurrentRow;
                  txt1ClientId.Text = dr.Cells[0].Value.ToString();
                  txt1ClientName.Text = dr.Cells[1].Value.ToString();

                  label7.Text = labelh.Text;
              }

              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }

          }

          private void txt1ClientId_TextChanged(object sender, EventArgs e)
          {
              FollowUpGridLoad2();
          }

          private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                              cmd.Parameters.AddWithValue("@d2",userId );
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
