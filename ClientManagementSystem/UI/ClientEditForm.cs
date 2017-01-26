using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DAO;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.Gateway;
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class ClientEditForm : Form
    {
        private SqlConnection con;
        ConnectionString cs =new ConnectionString();
        private SqlDataReader rdr;
        private SqlCommand cmd;
        private ClientGateway clientGateway;
        private List<InqueryClient> clients;
        private InqueryClient client;
        public ClientEditForm()
        {
            InitializeComponent();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
           //clientGateway=new ClientGateway();
           // decimal clientId = Convert.ToInt64(clientIdTextBox.Text);
           //client= clientGateway.SearchClient(clientId);
           // if (client.IClientId != Convert.ToInt64(clientIdTextBox.Text))
           // {
           //     MessageBox.Show("Client Id is not found.Please enter correct Id");
           // }
           // else
           // {
           //     clientIdTextBox.Text = Convert.ToString(client.IClientId);
           //     clientNameEditTextBox.Text = client.ClientName;
           //     txtClientEditComboBox.Text = client.ClientType;
           //     txtNatureOfClientEditComboBox.Text = client.NatureOfClient;
           //     emailAddressEditTextBox.Text = client.EmailAddress;
           //     txtIndustryCatagoryComboBox.Text = client.IndustryCatagory;
           //     cFlatNoTextBox.Text = client.CFlatNo;
           //     cHouseNoTextBox.Text = client.CHouseNo;
           //     cRoadNoTextBox.Text = client.CRoadNo;
           //     cBlockTextBox.Text = client.CBlock;
           //     cAreaTextBox.Text = client.CARea;
           //     cPostTextBox.Text = client.CPost;
           //     cPostCodeTextBox.Text = client.CPostCode;
           //     cDistCombo.Text = client.CDistrict;
           //     cContactNoTextBox.Text = client.CContactNo;
           //     tFlatNoTextBox.Text = client.TFlatNo;
           //     tHouseNoTextBox.Text = client.THouseNo;
           //     tRoadNoTextBox.Text = client.TRoadNo;
           //     tBlockTextBox.Text = client.TBlock;
           //     tAreaTextBox.Text = client.TARea;
           //     tPostTextBox.Text = client.TPost;
           //     tPostCodeTextBox.Text = client.TPostCode;
           //     tDistComboBox.Text = client.TDistrict;
           //     tContactNoTextBox.Text = client.TContactNo;
           //     contactPersonNameEditTextBox.Text = client.ContactPersonName;
           //     designationTextBox.Text = client.Designation;
           //     cellNumberTextBox.Text = client.CellNumber;
           //     endUserTextBox.Text = client.EndUser;

           // }
        }

        private void Autocomplete()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT distinct ClientName FROM Client", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Client");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["clientName"].ToString());

                }
                clientNameEditTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                clientNameEditTextBox.AutoCompleteCustomSource = col;
                clientNameEditTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reset()
        {
            clientNameEditTextBox.Text = "";
            txtClientEditComboBox.SelectedIndex = -1;
            txtNatureOfClientEditComboBox.SelectedIndex = -1;
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            ClientGateway aClientGateway = new ClientGateway();
            try
            {
                InqueryClient aClient = new InqueryClient
                {
                    IClientId = Convert.ToInt64(cmbIClientId.Text),
                    ClientName = clientNameEditTextBox.Text,
                    ClientType = txtClientEditComboBox.Text,
                    NatureOfClient = txtNatureOfClientEditComboBox.Text,
                    EmailAddress = emailAddressEditTextBox.Text,
                    IndustryCatagory = txtIndustryCatagoryComboBox.Text,


                    //CFlatNo = cFlatNoTextBox.Text,
                    //CHouseNo = cHouseNoTextBox.Text,
                    //CRoadNo = cRoadNoTextBox.Text,
                    //CBlock = cBlockTextBox.Text,
                    //CARea = cAreaTextBox.Text,
                    //CPost = cPostTextBox.Text,
                    //CPostCode = cPostCodeTextBox.Text,
                    //CDistrict = cDistCombo.Text,
                    //CContactNo = cContactNoTextBox.Text,

                    //TFlatNo = tFlatNoTextBox.Text,
                    //THouseNo = tHouseNoTextBox.Text,
                    //TRoadNo = tRoadNoTextBox.Text,
                    //TBlock = tBlockTextBox.Text,
                    //TARea = tAreaTextBox.Text,
                    //TPost = tPostTextBox.Text,
                    //TPostCode = tPostCodeTextBox.Text,
                    //TDistrict = tDistComboBox.Text,
                    //TContactNo = tContactNoTextBox.Text,

                    //ContactPersonName = contactPersonNameEditTextBox.Text,
                    //Designation = designationTextBox.Text,
                    //CellNumber = cellNumberTextBox.Text,
                    //EndUser = endUserTextBox.Text
                };
                aClientGateway.UpdateClient(aClient);
                Report2();
                Reset();
               


            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Please Enter Input in Correct Format", formatException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainUIInquieryClient aform = new MainUIInquieryClient();
            aform.Show();
        }

        private void clientIdTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cellNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                         (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

           
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cPostCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tPostCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cPostCodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        public void FillCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(IClientId) from InquieryClient order by IClientId";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbIClientId.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClientEditForm_Load(object sender, EventArgs e)
        {
            FillCombo();
        }
        private void Report2()
        {
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = "id2";
            paramDiscreteValue.Value = cmbIClientId.Text;
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
            InquiryClientRegistrationCrystalReport cr = new InquiryClientRegistrationCrystalReport();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientGateway = new ClientGateway();
            decimal clientId = Convert.ToInt64(cmbIClientId.Text);
            client = clientGateway.SearchClient(clientId);
            if (client.IClientId != Convert.ToInt64(cmbIClientId.Text))
            {
                MessageBox.Show("Client Id is not found.Please enter correct Id");
            }
            else
            {
                cmbIClientId.Text = Convert.ToString(client.IClientId);
                clientNameEditTextBox.Text = client.ClientName;
                txtClientEditComboBox.Text = client.ClientType;
                txtNatureOfClientEditComboBox.Text = client.NatureOfClient;
                emailAddressEditTextBox.Text = client.EmailAddress;
                txtIndustryCatagoryComboBox.Text = client.IndustryCatagory;
                //cFlatNoTextBox.Text = client.CFlatNo;
                //cHouseNoTextBox.Text = client.CHouseNo;
                //cRoadNoTextBox.Text = client.CRoadNo;
                //cBlockTextBox.Text = client.CBlock;
                //cAreaTextBox.Text = client.CARea;
                //cPostTextBox.Text = client.CPost;
                //cPostCodeTextBox.Text = client.CPostCode;
                //cDistCombo.Text = client.CDistrict;
                //cContactNoTextBox.Text = client.CContactNo;
                //tFlatNoTextBox.Text = client.TFlatNo;
                //tHouseNoTextBox.Text = client.THouseNo;
                //tRoadNoTextBox.Text = client.TRoadNo;
                //tBlockTextBox.Text = client.TBlock;
                //tAreaTextBox.Text = client.TARea;
                //tPostTextBox.Text = client.TPost;
                //tPostCodeTextBox.Text = client.TPostCode;
                //tDistComboBox.Text = client.TDistrict;
                //tContactNoTextBox.Text = client.TContactNo;
                //contactPersonNameEditTextBox.Text = client.ContactPersonName;
                //designationTextBox.Text = client.Designation;
                //cellNumberTextBox.Text = client.CellNumber;
                //endUserTextBox.Text = client.EndUser;

            }
        }
    }
}
