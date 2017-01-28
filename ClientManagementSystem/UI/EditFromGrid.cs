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
        public int clientTypeId1, natureOfClientId, industryCategoryId;
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
        private void EditFromGrid_Load(object sender, EventArgs e)
        {
            nUserId = LoginForm.uId.ToString();
            FillCDivisionCombo();
            FillTDivisionCombo();
        }
        private void Reset()
        {
            txtClientId.Text = "";
            txtClientName.Text="";
            cmbClientType.Text ="";
            cmbNatureOfClient.Text = "";
            txtEmailAddress.Text = "";
            cmbIndustryCategory.Text = "";

            cFlatNoTextBox.Text = "";
            cHouseNoTextBox.Text = "";
            cRoadNoTextBox.Text = "";
            cBlockTextBox.Text = "";
            cAreaTextBox.Text = "";
            cPostCodeTextBox.Text = "";
            cThanaCombo.Text = "";
            cDistCombo.Text = "";
            cContactNoTextBox.Text = "";

            tFlatNoTextBox.Text = "";
            tRoadNoTextBox.Text = "";
            tHouseNoTextBox.Text = "";
            tBlockTextBox.Text = "";
            tAreaTextBox.Text = "";
            tPostCodeTextBox.Text = "";
            tThanaCombo.Text = "";
            tDistCombo.Text = "";
            tContactNoTextBox.Text = "";

            txtCPName.Text = "";
            txtDesignation.Text = "";
            cellPhoneTextBox.Text = "";
            txtEndUser.Text = "";

        }
        private void SaveContactPersonDetails()
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
                    CPEmailAddress = txtCPEmailAddress.Text,
                    EndUser = txtEndUser.Text
                    
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
                   AddTypeId1  = addTypeId,
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
                    ClientTypeId = clientTypeId1,
                    NatureOfClientId = natureOfClientId,
                    EmailAddress = txtEmailAddress.Text,
                    IndustryCategoryId = industryCategoryId,
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
        private void saveButton_Click(object sender, EventArgs e)
        {
         
            try
            {
                //1.Corporate Address Applicable  & Tradding Address not Applicable
                if (notApplicableCheckBox.Checked)
                {
                    UpdateClient();
                    UpdateCorporatAddress(1);                       
                    SaveContactPersonDetails();
                    
                }
                //2.Corporate Address Applicable  & Tradding Address Same as  Corporate Address                                        
                if (sameAsCorporatAddCheckBox.Checked)
                {
                    UpdateClient();
                    UpdateCorporatAddress(1);
                    UpdateCorporatAddress(2);
                    SaveContactPersonDetails();
                    
                }
                //3.Corporate Address Applicable  & Tradding Address  Applicable
                if (sameAsCorporatAddCheckBox.Checked == false && notApplicableCheckBox.Checked == false)
                {
                    UpdateClient();
                    UpdateCorporatAddress(1);
                    UpdateTraddingAddress(2);
                    SaveContactPersonDetails();
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division = '" + cDivisionCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District = '" + cDistCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana= '" + cThanaCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode)  from PostOffice WHERE PostOffice.PostOfficeName= '" + cPostOfficeCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District = '" + tDistCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana= '" + tThanaCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode)  from PostOffice WHERE PostOffice.PostOfficeName= '" + tPostOfficeCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division = '" + tDivisionCombo.Text + "'";
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
                    groupBox3.Enabled = false;
                }
                else
                {

                    groupBox3.Enabled = false;
                }

            }
            else
            {
                if (sameAsCorporatAddCheckBox.Checked)
                {
                    groupBox3.Enabled = false;
                }
                else
                {

                    groupBox3.Enabled = true;
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
                    groupBox3.Enabled = false;
                }
                else
                {

                    groupBox3.Enabled = false;
                }

            }
            else
            {
                if (notApplicableCheckBox.Checked)
                {
                    groupBox3.Enabled = false;
                }
                else
                {

                    groupBox3.Enabled = true;
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
                    clientTypeId1 = (rdr.GetInt32(0));

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
                    natureOfClientId = (rdr.GetInt32(0));

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
                    industryCategoryId = (rdr.GetInt32(0));

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
    }
}
