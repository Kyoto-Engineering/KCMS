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
    public partial class EditFromGrid : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public string divisionIdC, divisionIdT, districtIdC, districtIdT, thanaIdC, thanaIdT, postOfficeIdC, postOfficeIdT;
        public string rMId;
        public string nUserId;
        public string clientTypeId1, natureOfClientId, industryCategoryId;
        public int affectedRows2, affectedRows3, bankEmailId, bankCPEmailId;
        public EditFromGrid()
        {
            InitializeComponent();
        }
        public void FillCDivisionCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Divisions.Division) from Divisions  order by Divisions.Division_ID desc ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cDivisionCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillTDivisionCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Divisions.Division) from Divisions  order by Divisions.Division_ID desc ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tDivisionCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillClientType()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(ClientType) from ClientTypes order by ClientTypeId desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbClientType.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillIndustryCategory()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(IndustryCategory) from IndustryCategorys order by IndustryCategoryId desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbIndustryCategory.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillNatureOfClient()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(ClientNature) from NatureOfClients order by NatureOfClientId desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbNatureOfClient.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillCMBSuperviserName()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Name) from Registration order by UserId desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbRM.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditFromGrid_Load(object sender, EventArgs e)
        {
            nUserId = LoginForm.uId.ToString();
            FillCMBSuperviserName();
            EmailAddress();
            EmailCPAddress();

            FillClientType();
            FillNatureOfClient();
            FillIndustryCategory();
            
            FillCDivisionCombo();
            FillTDivisionCombo();
        }

        public void ClearTraddingAddress()
        {
            tFlatNoTextBox.Clear();
            tRoadNoTextBox.Clear();
            tHouseNoTextBox.Clear();
            tBlockTextBox.Clear();
            tContactNoTextBox.Clear();
            tAreaTextBox.Clear();

            tDivisionCombo.SelectedIndex = -1;
            tDistCombo.SelectedIndex = -1;
            tThanaCombo.SelectedIndex = -1;
            tPostOfficeCombo.SelectedIndex = -1;
            tPostCodeTextBox.Clear();
           
        }
        private void Reset()
        {
            cmbRM.SelectedIndex = -1;
            txtClientId.Clear();
            txtClientName.Clear();
            cmbClientType.SelectedIndex=-1;
            cmbNatureOfClient.SelectedIndex = -1;
            cmbEmailAddress.SelectedIndex = -1;
            cmbIndustryCategory.SelectedIndex = -1;
            txtEndUser.Clear();


            cFlatNoTextBox.Clear();
            cHouseNoTextBox.Clear();
            cRoadNoTextBox.Clear();
            cBlockTextBox.Clear();
            cContactNoTextBox.Clear();
            cAreaTextBox.Clear();

            cDivisionCombo.SelectedIndex = -1;
            cDistCombo.SelectedIndex=-1;
            cThanaCombo.SelectedIndex = -1;
            cPostOfficeCombo.SelectedIndex = -1;
            cPostCodeTextBox.Clear();

            notApplicableCheckBox.CheckedChanged -= ifApplicableCheckBox_CheckedChanged;
            notApplicableCheckBox.Checked = false;
            notApplicableCheckBox.CheckedChanged += ifApplicableCheckBox_CheckedChanged;
            sameAsCorporatAddCheckBox.CheckedChanged -= sameAsCorporatAddCheckBox_CheckedChanged;
            sameAsCorporatAddCheckBox.Checked = false;
            sameAsCorporatAddCheckBox.CheckedChanged += sameAsCorporatAddCheckBox_CheckedChanged;
            ClearTraddingAddress();

            txtCPName.Clear();
            txtDesignation.Clear();
            cellPhoneTextBox.Clear();
            cmbCPEmailAddress.SelectedIndex = -1;

        }
        private void UpdateContactPersonDetails()
        {
            ClientGateway aClientGateway4 = new ClientGateway();
            try
            {
                ContactPersonDetails aContact = new ContactPersonDetails
                {
                    ICClientId = Convert.ToInt32(txtClientId.Text),
                    ContactPersonName = txtCPName.Text,
                    Designation = txtDesignation.Text,
                    CellNumber = cellPhoneTextBox.Text,
                    CPEmailAddress = cmbCPEmailAddress.Text
 
                };
                aClientGateway4.UpdateContactPersonDetails(aContact);
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
        private void UpdateTraddingAddress(int addTypeId)
        {
            ClientGateway aClientGateway3 = new ClientGateway();
            try
            {
                TraddingAddress tAdd = new TraddingAddress
                {
                    DivisionId = Convert.ToInt32(divisionIdT),
                    DistrictId = Convert.ToInt32(districtIdT),
                    ThanaId = Convert.ToInt32(thanaIdT),
                    PostOfficeId = Convert.ToInt32(postOfficeIdT),
                    TFlatNo = tFlatNoTextBox.Text,
                    THouseNo = tHouseNoTextBox.Text,
                    TRoadNo = tRoadNoTextBox.Text,
                    TBlock = tBlockTextBox.Text,
                    TARea = tAreaTextBox.Text,
                    TContactNo = tContactNoTextBox.Text,
                    AddTypeIdT = addTypeId
                    
                };
                aClientGateway3.UpdateTraddingAddress(tAdd);
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
        private void UpdateCorporatAddress(int addTypeId)
        {
            ClientGateway aClientGateway2 = new ClientGateway();
            try
            {
                CorporateAddress add1 = new CorporateAddress
                {
                   DivisionId = Convert.ToInt32(divisionIdC),
                   DistrictId = Convert.ToInt32(districtIdC),
                   ThanaId = Convert.ToInt32(thanaIdC),
                   PostOfficeId = Convert.ToInt32(postOfficeIdC),
                   
                   CFlatNo = cFlatNoTextBox.Text,
                   CHouseNo = cHouseNoTextBox.Text,
                   CRoadNo = cRoadNoTextBox.Text,
                   CBlock = cBlockTextBox.Text,
                   CARea = cAreaTextBox.Text,
                   CContactNo = cContactNoTextBox.Text,                  
                   //AddTypeId1  = addTypeId,
                };
                aClientGateway2.UpdateCorporatAddress(add1);
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
        private void UpdateClient()
        {
            ClientGateway aClientGateway = new ClientGateway();
            try
            {
                InqueryClient aClient = new InqueryClient
                {
                    IClientId = Convert.ToInt64(txtClientId.Text),
                    ClientName = txtClientName.Text,
                    ClientTypeId = Convert.ToInt32(clientTypeId1),
                    NatureOfClientId = Convert.ToInt32(natureOfClientId),
                   // EmailAddress = txtEmailAddress.Text,
                    IndustryCategoryId = Convert.ToInt32(industryCategoryId),
                    EndUser = txtEndUser.Text,
                    CurrentDates = DateTime.UtcNow,
                    UserId = nUserId,
                    Dates = System.DateTime.Now,
                    RMId = rMId
                };
                aClientGateway.UpdateClient(aClient);
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

        private void UpdateInquiryClient()
        {
            try
            {
                con=new SqlConnection(cs.DBConn);
                con.Open();
                string query = "Update InquieryClient set ClientName=@d1,ClientTypeId=@d2,NatureOfClientId=@d3,EmailBankId=@d4,IndustryCategoryId=@d5,EndUser=@d6,UserId=@d7,Dates=@d8,SuperviserId=@d9  Where InquieryClient.IClientId='" + txtClientId.Text + "'";
                cmd=new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@d1", txtClientName.Text);
                cmd.Parameters.AddWithValue("@d2", clientTypeId1);
                cmd.Parameters.AddWithValue("@d3", natureOfClientId);
                cmd.Parameters.AddWithValue("@d4", bankEmailId);
               // cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(txtEmailAddress.Text) ? (object)DBNull.Value : txtEmailAddress.Text));
                cmd.Parameters.AddWithValue("@d5", industryCategoryId);
                cmd.Parameters.AddWithValue("@d6", txtEndUser.Text);
                cmd.Parameters.AddWithValue("@d7", nUserId);
                cmd.Parameters.AddWithValue("@d8", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@d9", rMId);
                rdr = cmd.ExecuteReader();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateClientAddress(string tablName1)
        {
            string corporatTable = tablName1;
            try
            {
                if (corporatTable == "CorporateAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string query = "Update " + corporatTable + " set Division_ID=@d1,D_ID=@d2,T_ID=@d3,PostOfficeId=@d4,CFlatNo=@d5,CHouseNo=@d6,CRoadNo=@d7,CBlock=@d8,CArea=@d9,CContactNo=@d10  Where  CorporateAddresses.IClientId='" + txtClientId.Text + "'";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdC) ? (object)DBNull.Value : divisionIdC));
                    cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdC) ? (object)DBNull.Value : districtIdC));
                    cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdC) ? (object)DBNull.Value : thanaIdC));
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdC) ? (object)DBNull.Value : postOfficeIdC));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox.Text) ? (object)DBNull.Value : cBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }

                else if (corporatTable == "TraddingAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string query = "Update " + corporatTable + " set Division_ID=@d1,D_ID=@d2,T_ID=@d3,PostOfficeId=@d4,TFlatNo=@d5,THouseNo=@d6,TRoadNo=@d7,TBlock=@d8,TArea=@d9,TContactNo=@d10  Where  TraddingAddresses.IClientId='" + txtClientId.Text+ "'";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdT) ? (object)DBNull.Value : divisionIdT));
                    cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdT) ? (object)DBNull.Value : districtIdT));
                    cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdT) ? (object)DBNull.Value : thanaIdT));
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object)DBNull.Value : tFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object)DBNull.Value : tHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object)DBNull.Value : tRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tBlockTextBox.Text) ? (object)DBNull.Value : tBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAreaTextBox.Text) ? (object)DBNull.Value : tAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : tContactNoTextBox.Text));
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateContactPersondetails()
        {
            try
            {
                con=new SqlConnection(cs.DBConn);
                con.Open();
                string qry = "Update ContactPersonDetails set ContactPersonName=@d1,Designation=@d2,EmailBankId=@d3,CellNumber=@d4 Where IClientId='" + txtClientId.Text + "'";
                cmd=new SqlCommand(qry,con);
                cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(txtCPName.Text) ? (object)DBNull.Value : txtCPName.Text));
                cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(txtDesignation.Text) ? (object)DBNull.Value : txtDesignation.Text));
                cmd.Parameters.AddWithValue("@d3", bankCPEmailId);
               // cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(txtCPEmailAddress.Text) ? (object)DBNull.Value : txtCPEmailAddress.Text));
                cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(cellPhoneTextBox.Text) ? (object)DBNull.Value : cellPhoneTextBox.Text));                  
                cmd.ExecuteReader();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void SaveContactPersonDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qury = "insert into ContactPersonDetails(ContactPersonName,Designation,CellNumber,EmailBankId,IClientId) Values(@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(qury);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(txtCPName.Text) ? (object)DBNull.Value : txtCPName.Text));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(txtDesignation.Text) ? (object)DBNull.Value : txtDesignation.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(cellPhoneTextBox.Text) ? (object)DBNull.Value : cellPhoneTextBox.Text));
            //cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(txtCPEmailAddress.Text) ? (object)DBNull.Value : txtCPEmailAddress.Text));
            cmd.Parameters.AddWithValue("@d4", bankCPEmailId);
            cmd.Parameters.AddWithValue("@d5", txtClientId.Text);
            affectedRows3 = (int)cmd.ExecuteScalar();
            con.Close();
        }
        private void AddClientAddress(string tblName2)
        {
            string traddingAdd = tblName2;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string Qry = "insert into " + traddingAdd + "(Division_ID,D_ID,T_ID,PostOfficeId,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TContactNo,IClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(Qry);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdT) ? (object)DBNull.Value : divisionIdT));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdT) ? (object)DBNull.Value : districtIdT));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdT) ? (object)DBNull.Value : thanaIdT));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object)DBNull.Value : tFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object)DBNull.Value : tHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object)DBNull.Value : tRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tBlockTextBox.Text) ? (object)DBNull.Value : tBlockTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAreaTextBox.Text) ? (object)DBNull.Value : tAreaTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(tContactNoTextBox.Text) ? (object)DBNull.Value : tContactNoTextBox.Text));
            cmd.Parameters.AddWithValue("@d11", txtClientId.Text);
            affectedRows2 = (int)cmd.ExecuteScalar();
            con.Close();              
        }
        private void GetClientIdAndSaveOrUpdateContactPersonDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string ct2 = "select RTRIM(ContactPersonDetails.IClientId) from ContactPersonDetails where ContactPersonDetails.IClientId='" + txtClientId.Text + "'";
            cmd = new SqlCommand(ct2, con);
            rdr = cmd.ExecuteReader();
            if (rdr.Read() && !rdr.IsDBNull(0))
            {
                UpdateContactPersondetails();
            }
            else
            {
                SaveContactPersonDetails();
            }

        }
        private void GetClientIdAndSaveOrUpdateTraddingAddress()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string ct2 = "select RTRIM(TraddingAddresses.IClientId) from TraddingAddresses where TraddingAddresses.IClientId='" + txtClientId.Text + "'";
            cmd = new SqlCommand(ct2, con);
            rdr = cmd.ExecuteReader();
            if (rdr.Read() && !rdr.IsDBNull(0))
            {
                UpdateClientAddress("TraddingAddresses");
            }
            else
            {
                AddClientAddress("TraddingAddresses");
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
         
            try
            {
                //1.Corporate Address Applicable  & Tradding Address not Applicable
                if (notApplicableCheckBox.Checked)
                {
                    UpdateInquiryClient();
                    UpdateClientAddress("CorporateAddresses");                                     
                }
                //2.Corporate Address Applicable  & Tradding Address Same as  Corporate Address                                        
                if (sameAsCorporatAddCheckBox.Checked)
                {
                    UpdateInquiryClient();
                    UpdateClientAddress("CorporateAddresses");                   
                    UpdateClientAddress("TraddingAddresses");                                      
                }
                //3.Corporate Address Applicable  & Tradding Address  Applicable
                if (sameAsCorporatAddCheckBox.Checked == false && notApplicableCheckBox.Checked == false)
                {
                    UpdateInquiryClient();
                    UpdateClientAddress("CorporateAddresses");
                    GetClientIdAndSaveOrUpdateTraddingAddress();


                }
                if (!string.IsNullOrEmpty(txtCPName.Text))
                {
                    GetClientIdAndSaveOrUpdateContactPersonDetails();
                    
                }
                MessageBox.Show("Successfully Updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
                 this.Hide();
            MainUIInquieryClient frm=new MainUIInquieryClient();
                  frm.Show();
        }

        private void cDistTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division=@find";

                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Division"));
                cmd.Parameters["@find"].Value = cDivisionCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    divisionIdC = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                cDivisionCombo.Text = cDivisionCombo.Text.Trim();
                cDistCombo.Items.Clear();
                cDistCombo.Text = "";
                cDistCombo.Enabled = true;
                cDistCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" + divisionIdC + "' order by Districts.Division_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cDistCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cDistCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "District"));
                cmd.Parameters["@find"].Value = cDistCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    districtIdC = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                cDistCombo.Text = cDistCombo.Text.Trim();
                cThanaCombo.Items.Clear();
                cThanaCombo.Text = "";
                cThanaCombo.Enabled = true;
                cThanaCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Thanas.Thana) from Thanas  Where Thanas.D_ID = '" + districtIdC + "' order by Thanas.D_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cThanaCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Thana"));
                cmd.Parameters["@find"].Value = cThanaCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    thanaIdC = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                cThanaCombo.Text = cThanaCombo.Text.Trim();
                cPostOfficeCombo.Items.Clear();
                cPostOfficeCombo.Text = "";
                cPostOfficeCombo.Enabled = true;
                cPostOfficeCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(PostOffice.PostOfficeName) from PostOffice  Where PostOffice.T_ID = '" + thanaIdC + "' order by PostOffice.T_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cPostOfficeCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cPostOfficeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode) from PostOffice WHERE PostOffice.PostOfficeName=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "PostOfficeName"));
                cmd.Parameters["@find"].Value = cPostOfficeCombo.Text;
                rdr = cmd.ExecuteReader();                
                if (rdr.Read())
                {
                    postOfficeIdC = (rdr.GetString(0));
                    cPostCodeTextBox.Text = (rdr.GetString(1));
                    

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

        private void tDistCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "District"));
                cmd.Parameters["@find"].Value = tDistCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    districtIdT = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                tDistCombo.Text = tDistCombo.Text.Trim();
                tThanaCombo.Items.Clear();
                tThanaCombo.Text = "";
                tThanaCombo.Enabled = true;
                tThanaCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Thanas.Thana) from Thanas  Where Thanas.D_ID = '" + districtIdT + "' order by Thanas.D_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tThanaCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Thana"));
                cmd.Parameters["@find"].Value = tThanaCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    thanaIdT = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                tThanaCombo.Text = tThanaCombo.Text.Trim();
                tPostOfficeCombo.Items.Clear();
                tPostOfficeCombo.Text = "";
                tPostOfficeCombo.Enabled = true;
                tPostOfficeCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(PostOffice.PostOfficeName) from PostOffice  Where PostOffice.T_ID = '" + thanaIdT + "' order by PostOffice.T_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tPostOfficeCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tPostOfficeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode) from PostOffice WHERE PostOffice.PostOfficeName=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "PostOfficeName"));
                cmd.Parameters["@find"].Value = tPostOfficeCombo.Text;
                rdr = cmd.ExecuteReader();                
                if (rdr.Read())
                {
                    postOfficeIdT = (rdr.GetString(0));
                    tPostCodeTextBox.Text = (rdr.GetString(1));

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

        private void tDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division=@find";

                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Division"));
                cmd.Parameters["@find"].Value = tDivisionCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    divisionIdT = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                tDivisionCombo.Text = tDivisionCombo.Text.Trim();
                tDistCombo.Items.Clear();
                tDistCombo.Text = "";
                tDistCombo.Enabled = true;
                tDistCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" + divisionIdT + "' order by Districts.Division_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tDistCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ifApplicableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (notApplicableCheckBox.Checked)
            {

                if (sameAsCorporatAddCheckBox.Checked)
                {
                    sameAsCorporatAddCheckBox.CheckedChanged -= sameAsCorporatAddCheckBox_CheckedChanged;
                    sameAsCorporatAddCheckBox.Checked = false;
                    sameAsCorporatAddCheckBox.CheckedChanged += sameAsCorporatAddCheckBox_CheckedChanged;
                    groupBox4.Enabled = false;
                    ClearTraddingAddress();
                }
                else
                {

                    groupBox4.Enabled = false;
                    ClearTraddingAddress();
                }

            }
            else
            {
                if (sameAsCorporatAddCheckBox.Checked)
                {
                    groupBox4.Enabled = false;
                    ClearTraddingAddress();
                }
                else
                {

                    groupBox4.Enabled = true;
                    ClearTraddingAddress();
                }
            }
        }

        private void sameAsCorporatAddCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sameAsCorporatAddCheckBox.Checked)
            {

                if (notApplicableCheckBox.Checked)
                {
                    notApplicableCheckBox.CheckedChanged -= ifApplicableCheckBox_CheckedChanged;
                    notApplicableCheckBox.Checked = false;
                    notApplicableCheckBox.CheckedChanged += ifApplicableCheckBox_CheckedChanged;
                    groupBox4.Enabled = false;
                    ClearTraddingAddress();
                }
                else
                {

                    groupBox4.Enabled = false;
                    ClearTraddingAddress();
                }

            }
            else
            {
                if (notApplicableCheckBox.Checked)
                {
                    groupBox4.Enabled = false;
                    ClearTraddingAddress();
                }
                else
                {

                    groupBox4.Enabled = true;
                    ClearTraddingAddress();
                }
            }
        }

        private void cmbClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(ClientTypes.ClientTypeId) from ClientTypes WHERE ClientTypes.ClientType = '" + cmbClientType.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    clientTypeId1 = (rdr.GetString(0));

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

        private void cmbNatureOfClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(NatureOfClients.NatureOfClientId) from NatureOfClients WHERE NatureOfClients.ClientNature = '" + cmbNatureOfClient.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    natureOfClientId = (rdr.GetString(0));

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

        private void cmbIndustryCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(IndustryCategorys.IndustryCategoryId) from IndustryCategorys WHERE IndustryCategorys.IndustryCategory = '" + cmbIndustryCategory.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    industryCategoryId = (rdr.GetString(0));

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

        private void cmbRM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Registration.UserId) from Registration WHERE Registration.Name = '" + cmbRM.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    rMId = (rdr.GetString(0));

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

        private void cellPhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void cContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void txtEmailAddress_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtCPName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCPName.Text))
            {
                txtDesignation.Clear();
                cellPhoneTextBox.Clear();
                cmbCPEmailAddress.SelectedIndex = -1;
            }
        }

        private void txtDesignation_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCPName.Text))
            {
                MessageBox.Show("Please  enter Before Contact Person Name", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCPName.Focus();
            }
        }

        private void cellPhoneTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCPName.Text))
            {
                MessageBox.Show("Please  enter Before Contact Person Name", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCPName.Focus();
            }
        }

        private void txtCPEmailAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCPName.Text))
            {
                MessageBox.Show("Please  enter Before Contact Person Name", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCPName.Focus();
            }
        }

        private void cPostOfficeCombo_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void tContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }
        private void EmailAddress()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "select Email from EmailBank";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbEmailAddress.Items.Add(rdr.GetValue(0).ToString());
                }
                cmbEmailAddress.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbEmailAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmailAddress.Text == "Not In The List")
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Please Input Mode Of Conduct  Here", "Input Here", "", -1, -1);
                if (string.IsNullOrWhiteSpace(input))
                {
                    cmbEmailAddress.SelectedIndex = -1;
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select Email from EmailBank where Email='" + input + "'";
                    cmd = new SqlCommand(ct2, con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read() && !rdr.IsDBNull(0))
                    {
                        MessageBox.Show("This Email  Already Exists,Please Select From List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        cmbEmailAddress.SelectedIndex = -1;
                    }
                    else
                    {
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string query1 = "insert into EmailBank (Email, UserId,DateAndTime) values (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                            cmd = new SqlCommand(query1, con);
                            cmd.Parameters.AddWithValue("@d1", input);
                            cmd.Parameters.AddWithValue("@d2", nUserId);
                            cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                            cmd.ExecuteNonQuery();

                            con.Close();
                            cmbEmailAddress.Items.Clear();
                            EmailAddress();
                            cmbEmailAddress.SelectedText = input;

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
                    cmd.CommandText = "SELECT EmailBankId from EmailBank WHERE Email= '" + cmbEmailAddress.Text + "'";

                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        bankEmailId = rdr.GetInt32(0);
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
        private void EmailCPAddress()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctt = "select Email from EmailBank";
                cmd = new SqlCommand(ctt);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbCPEmailAddress.Items.Add(rdr.GetValue(0).ToString());
                }
                cmbCPEmailAddress.Items.Add("Not In The List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbCPEmailAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCPEmailAddress.Text == "Not In The List")
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Please Input Mode Of Conduct  Here", "Input Here", "", -1, -1);
                if (string.IsNullOrWhiteSpace(input))
                {
                    cmbCPEmailAddress.SelectedIndex = -1;
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select Email from EmailBank where Email='" + input + "'";
                    cmd = new SqlCommand(ct2, con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read() && !rdr.IsDBNull(0))
                    {
                        MessageBox.Show("This Email  Already Exists,Please Select From List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        cmbCPEmailAddress.SelectedIndex = -1;
                    }
                    else
                    {
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string query1 = "insert into EmailBank (Email, UserId,DateAndTime) values (@d1,@d2,@d3)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                            cmd = new SqlCommand(query1, con);
                            cmd.Parameters.AddWithValue("@d1", input);
                            cmd.Parameters.AddWithValue("@d2", nUserId);
                            cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                            cmd.ExecuteNonQuery();

                            con.Close();
                            cmbCPEmailAddress.Items.Clear();
                            EmailAddress();
                            cmbCPEmailAddress.SelectedText = input;

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
                    cmd.CommandText = "SELECT EmailBankId from EmailBank WHERE Email= '" + cmbCPEmailAddress.Text + "'";

                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        bankCPEmailId = rdr.GetInt32(0);
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
