using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DBGateway;

namespace ClientManagementSystem.UI
{
    public partial class GridForClientDetails : Form
    {
         //ClientApprovedForm frm =new ClientApprovedForm();
        ConnectionString cs =new ConnectionString();
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        private DataTable dt;
       
        public GridForClientDetails()
        {
            InitializeComponent();
        }
      private SqlConnection Connection
        {
        get
         {
              SqlConnection ConnectionToFetch = new SqlConnection(cs.DBConn);
              ConnectionToFetch.Open();
                return ConnectionToFetch;
          }
       }

        //private SqlConnection Connection
        //{
        //    get
        //    {
        //        SqlConnection ConnectionToFetch=new SqlConnection(cs.DBConn);
        //        ConnectionToFetch.Open();
        //        return ConnectionToFetch;
        //    }
        //}

        private void GridForClientDetails_Load(object sender, EventArgs e)
        {

            //GetData();
            //con=new SqlConnection(cs.DBConn);
            //string query = "Select * from  Clients";
            //SqlDataAdapter myDataAdapter=new SqlDataAdapter(query,con);
            //dt=new DataTable();
            //myDataAdapter.Fill(dt);
            //dataGridView14.DataSource = dt;

            con = new SqlConnection(cs.DBConn);
            con.Open();
            string selectQuery = "Select top 5000 IClientId as InquieyClientId,ClientName,NatureOfClient,EmailAddress,ContactPersonName,Designation,CellNumber,EndUser from InquieryClient order by IClientId desc";
            //string selectQuery = "Select * from Salary";
            //string selectQuery = "Select  Months, Year,EmployeeId, EmployeeName,Designation,TotalworkingDay,DayPayable,LeaveWithPay,UnpaidLeave,Basic, TransportAllowance,MedicalAllowance,HouseRent,OtherAllowance,GrossSalary,Tax,DeductionForLeave,Fine,DeductionFromAnnualLeave,Adjustment,AdvancePaid,TotalDeduction,PreviousDues,TotalAddition,NetPayable from Salary";
            //string selectQuery = String.Format("SELECT RTRIM(InqueryClient.IClientId),RTRIM(InqueryClient.ClientName),RTRIM(InqueryClient.ClientType),RTRIM(InqueryClient.NatureOfClient),RTRIM(InqueryClient.EmailAddress),RTRIM(InqueryClient.CorporateAddress),RTRIM(InqueryClient.ContactNo),RTRIM(InqueryClient.TradingAddress),RTRIM(InqueryClient.ContactPersonName),RTRIM(InqueryClient.Designation),RTRIM(InqueryClient.CellNumber),RTRIM(InqueryClient.EndUser) from InqueryClient orderBy IClientId desc");
            SqlDataAdapter myadapter = new SqlDataAdapter(selectQuery, con);
            DataTable dt = new DataTable();
            myadapter.Fill(dt);
            dataGridView14.DataSource = dt;

        }
        //public void GetData()
        //{
        //    try
        //    {
        //        con = new SqlConnection(cs.DBConn);
        //        con.Open();
        //        cmd = new SqlCommand("SELECT RTRIM(InqueryClient.IClientId),RTRIM(InqueryClient.ClientName),RTRIM(InqueryClient.ClientType),RTRIM(InqueryClient.NatureOfClient),RTRIM(InqueryClient.EmailAddress),RTRIM(InqueryClient.CorporateAddress),RTRIM(InqueryClient.ContactNo),RTRIM(InqueryClient.TradingAddress),RTRIM(InqueryClient.ContactPersonName),RTRIM(InqueryClient.Designation),RTRIM(InqueryClient.CellNumber),RTRIM(InqueryClient.EndUser) from InqueryClient", con);
        //        rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        dataGridView14.Rows.Clear();
        //        while (rdr.Read() == true)
        //        {
        //            dataGridView14.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8],rdr[9],rdr[10],rdr[11]);
        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void backToHomeButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //ClientRegistrationForm  crm =new ClientRegistrationForm();
            //   crm.Show();
        }

        private void dataGridView14_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //this.Hide();
           // ClientApprovedForm aform=new ClientApprovedForm();
           // aform.Show();
          //aform.SetForApproved();
            //try
            //{
            //    DataGridViewRow dr = dataGridView14.SelectedRows[0];
            //    this.Hide();
            //  frm = new ClientApprovedForm();
            //    frm.Show();
            //    frm.clientIdATextBox.Text = dr.Cells[0].Value.ToString();
            //    frm.clientNameATextBox.Text = dr.Cells[1].Value.ToString();
            //    frm.txtClientAComboBox.Text = dr.Cells[2].Value.ToString();
            //    frm.emailAddressATextBox.Text = dr.Cells[3].Value.ToString();
            //    frm.corporatAddressATextBox.Text = dr.Cells[4].Value.ToString();
            //    frm.billingAddressATextBox.Text = dr.Cells[5].Value.ToString();
            //    frm.traddingAddressATextBox.Text = dr.Cells[6].Value.ToString();
            //    frm.txtNatureOfClientAComboBox.Text = dr.Cells[7].Value.ToString();

            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

       

        private void dataGridView14_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView14_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           // MessageBox.Show("Call Row Post Paint");
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView14.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView14.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void addNewButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
           dynamic afForm=new ClientRegistrationForm();
            afForm.ShowDialog();
            this.Visible = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MasterPageOnlyforClient af = new MasterPageOnlyforClient();
            af.Show();
        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    con = new SqlConnection(cs.DBConn);
            //    con.Open();
            //    cmd = new SqlCommand("SELECT RTRIM(IClientId),RTRIM(ClientName),RTRIM(ClientType),RTRIM(NatureOfClient),RTRIM(EmailAddress),RTRIM(CorporateAddress),RTRIM(ContactNo),RTRIM(TradingAddress),RTRIM(ContactPersonName),RTRIM(Designation),RTRIM(CellNumber),RTRIM(EndUser) from InqueryClient where ClientName like '" + txtClientName.Text + "%' order by ClientName", con);
            //    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //    dataGridView14.Rows.Clear();
            //    while (rdr.Read() == true)
            //    {
            //        dataGridView14.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8]);
            //    }
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
