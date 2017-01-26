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
                cmd = new SqlCommand("SELECT RTRIM(SalesClient.SClientId),RTRIM(SalesClient.ClientName),RTRIM(SalesClient.EmailAddress),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from SalesClient,ContactPersonDetails  where SalesClient.SClientId=ContactPersonDetails.SClientId  order by SalesClient.SClientId desc", con); 
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
                string ct = "select RTRIM(Name) from Registration order by Name";
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
                cmd = new SqlCommand("SELECT RTRIM(FollowUp.DeadLineDateTime),RTRIM(IClientFeedbackDairy.Feedback),RTRIM(FollowUp.Actions),RTRIM(Registration.Name),RTRIM(FollowUp.Statuss) FROM  (FollowUp INNER JOIN IClientFeedbackDairy ON FollowUp.IClientFeedbackId = IClientFeedbackDairy.IClientFeedbackId) LEFT JOIN  Registration ON IClientFeedbackDairy.UserId = Registration.UserId  where FollowUp.Statuss='Pending' order by FollowUp.FollowUpId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
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
            FollowUpGridLoad();
            GetData();
        }

        private void Reset()
        {
            txtSClientId.Clear();
            txtClientName.Clear();
            feedbackSTextBox.Clear();
            actionSMultiTextBox.Clear();
            responsibleSPersonComboBox.SelectedIndex = -1;
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Visible = false;
                dynamic frm3 = new SalesClientFeedbackDairy();
                frm3.txt2SClientId.Text = txtSClientId.Text;
                frm3.txt2SClientName.Text = txtSClientName.Text;
                frm3.txtClientInquiry.Text = txtSClientInquiry.Text;
                frm3.feedback2STextBox.Text = feedbackSTextBox.Text;
                frm3.feedback2SDateTime.Text = feedbackSDeadlineDateTime.Text;
                frm3.action2SMultiTextBox.Text = actionSMultiTextBox.Text;
                frm3.txtResposible2SPerson.Text = responsibleSPersonComboBox.Text;
                frm3.followUpDeadline2STextBox.Text = followUpSDeadlinedate.Text;
                frm3.ShowDialog();
                this.Visible = true;
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txtSClientId.Text = dr.Cells[0].Value.ToString();
                txtSClientName.Text = dr.Cells[1].Value.ToString();

                label7.Text = labelh.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
