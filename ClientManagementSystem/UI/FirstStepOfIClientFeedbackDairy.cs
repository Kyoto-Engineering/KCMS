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
    public partial class FirstStepOfIClientFeedbackDairy : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;

        ConnectionString cs =new ConnectionString();
        public FirstStepOfIClientFeedbackDairy()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.Visible = false;
                dynamic frm2 = new InqueiryClientFeedbackDairy();                
                frm2.txt2ClientId.Text = txt1ClientId.Text;
                frm2.txt2ClientName.Text = txt1ClientName.Text;
                frm2.feedback2TextBox.Text = feedback1TextBox.Text;
                frm2.txtClentInquiry.Text = txtClientInquiry.Text;
                frm2.feedback2DateTime.Value = feedback1DeadlineDateTime.Value;
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
                cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending' order by FollowUp.FollowUpId desc", con);
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
                FollowUpGridLoad2();
                FillCombo2();
                GetData();
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
    }
}
