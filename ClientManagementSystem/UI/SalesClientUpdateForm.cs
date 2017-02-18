using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DAO;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.Gateway;
using ClientManagementSystem.LoginUI;

namespace ClientManagementSystem.UI
{
    public partial class SalesClientUpdateForm : Form
    {


        ConnectionString cs = new ConnectionString();
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataReader rdr = null;
        private delegate void ChangeFocusDelegate(Control ctl);
        public int affectedRows1, affectedRows2, affectedRows3, affectedRows4, currentSalesClientId, addressTypeId1 = 1, addressTypeId2 = 2, addressTypeId3 = 3;
        public string fullName2, userId, districtIdC, districtIdT, districtIdB, divisionIdC, divisionIdT, divisionIdB, thanaIdC, thanaIdT, thanaIdB, postofficeIdC, postOfficeIdT, postOfficeIdB;
        public ClientGateway clientGateway, clientGateway1, clientGateway2;
        private List<InqueryClient> clients;
        private InqueryClient client;
        private Addresss add1, add2;
        public string natureOfClientId, industryCategoryId, clientTypeId;
        public int superviserId, bankEmailId, bankCPEmailId;
        public SalesClientUpdateForm()
        {
            InitializeComponent();
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
                    cmbSuperviserName.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    tDivitionCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillBDivisionCombo()
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
                    bDivisionCombo.Items.Add(rdr[0]);
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
        private void changeFocus(Control ctl)
        {
            ctl.Focus();
        }
        private void SalesClientUpdateForm_Load(object sender, EventArgs e)
        {
            userId = LoginForm.uId.ToString();
            FillCMBSuperviserName();

            FillClientType();
            FillNatureOfClient();
            FillIndustryCategory();

            FillCDivisionCombo();
            FillTDivisionCombo();
            FillBDivisionCombo();      
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
                cDistrictCombo.Items.Clear();
                cDistrictCombo.Text = "";
                cDistrictCombo.Enabled = true;
                cDistrictCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" + divisionIdC + "' order by Districts.Division_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cDistrictCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cDistrictCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "District"));
                cmd.Parameters["@find"].Value = cDistrictCombo.Text;
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


                cDistrictCombo.Text = cDistrictCombo.Text.Trim();
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
                    postofficeIdC = (rdr.GetString(0));
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
        private void GetClientTypeId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string query = "SELECT RTRIM(ClientTypes.ClientTypeId) from ClientTypes WHERE  ClientTypes.ClientType = '" + cmbClientType.Text + "'";
                cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    clientTypeId = (rdr.GetString(0));
                }

                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetNatureOfClientId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT RTRIM(NatureOfClients.NatureOfClientId) from NatureOfClients WHERE  NatureOfClients.ClientNature = '" + cmbNatureOfClient.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    natureOfClientId = (rdr.GetString(0));
                }

                con.Close();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetIndustryCategoryId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT RTRIM(IndustryCategorys.IndustryCategoryId) from IndustryCategorys WHERE  IndustryCategorys.IndustryCategory = '" + cmbIndustryCategory.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    industryCategoryId = (rdr.GetString(0));
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSalesClient()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string query = "Update SalesClient set ClientName=@d2,ClientTypeId=@d3,NatureOfClientId=@d4,EmailBankId=@d5,IndustryCategoryId=@d6,EndUser=@d7,UserId=@d8,Dates=@d9,SuperviserId=@d10  Where SalesClient.SClientId='" + txtSalesClientId.Text + "'";
                cmd = new SqlCommand(query,con);               
                //cmd.Parameters.AddWithValue("@d1", txtIClientId.Text);
                cmd.Parameters.AddWithValue("@d2", clientNameAPTextBox.Text);
                cmd.Parameters.AddWithValue("@d3", clientTypeId);
                cmd.Parameters.AddWithValue("@d4", natureOfClientId);
                cmd.Parameters.AddWithValue("@d5", bankEmailId);
                cmd.Parameters.AddWithValue("@d6", industryCategoryId);
                cmd.Parameters.AddWithValue("@d7", endUserAPTextBox.Text);
                cmd.Parameters.AddWithValue("@d8", userId);
                cmd.Parameters.AddWithValue("@d9", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@d10", superviserId);
                cmd.ExecuteReader();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateSalesClientCorporateORTraddingORBillingAddress(string tablName1)
        {
            string corporatTable = tablName1;
            try
            {
                if (corporatTable == "CorporateAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string query = "Update " + corporatTable + " set PostOfficeId=@d4,CFlatNo=@d5,CHouseNo=@d6,CRoadNo=@d7,CBlock=@d8,CArea=@d9,CContactNo=@d10  Where  CorporateAddresses.SClientId='" + txtSalesClientId.Text + "'";
                    cmd = new SqlCommand(query, con);
                    //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdC) ? (object)DBNull.Value : divisionIdC));
                    //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdC) ? (object)DBNull.Value : districtIdC));
                    //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdC) ? (object)DBNull.Value : thanaIdC));
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
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
                    string query = "Update " + corporatTable + " set PostOfficeId=@d4,TFlatNo=@d5,THouseNo=@d6,TRoadNo=@d7,TBlock=@d8,TArea=@d9,TContactNo=@d10  Where  TraddingAddresses.SClientId='" + txtSalesClientId.Text + "'";
                    cmd = new SqlCommand(query, con);
                    //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdT) ? (object)DBNull.Value : divisionIdT));
                    //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdT) ? (object)DBNull.Value : districtIdT));
                    //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdT) ? (object)DBNull.Value : thanaIdT));
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
                else if (corporatTable == "BillingAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string query = "Update " + corporatTable + " set PostOfficeId=@d4,BFlatNo=@d5,BHouseNo=@d6,BRoadNo=@d7,BBlock=@d8,BArea=@d9,BContactNo=@d10  Where  BillingAddresses.SClientId='" + txtSalesClientId.Text + "'";
                    cmd = new SqlCommand(query, con);
                    //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdB) ? (object)DBNull.Value : divisionIdB));
                    //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdB) ? (object)DBNull.Value : districtIdB));
                    //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdB) ? (object)DBNull.Value : thanaIdB));
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdB) ? (object)DBNull.Value : postOfficeIdB));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(bFlatNoTextBox.Text) ? (object)DBNull.Value : bFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(bHouseNoTextBox.Text) ? (object)DBNull.Value : bHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(bRoadNoTextBox.Text) ? (object)DBNull.Value : bRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(bBlockTextBox.Text) ? (object)DBNull.Value : bBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(bAreaTextBox.Text) ? (object)DBNull.Value : bAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(bContactNoTextBox.Text) ? (object)DBNull.Value : bContactNoTextBox.Text));
                    cmd.ExecuteReader();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetClientIdAndSaveOrUpdateSalesClientAddress(string  tableName)
        {
            string checkTable = tableName;
            if (checkTable == "CorporateAddresses")
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct2 = "select RTRIM(CorporateAddresses.SClientId) from CorporateAddresses where CorporateAddresses.SClientId='" + txtSalesClientId.Text + "'";
                cmd = new SqlCommand(ct2, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    UpdateSalesClientCorporateORTraddingORBillingAddress("CorporateAddresses");
                }
                else
                {
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                }
            }
            else  if (checkTable == "TraddingAddresses")
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct2 = "select RTRIM(TraddingAddresses.SClientId) from TraddingAddresses where TraddingAddresses.SClientId='" + txtSalesClientId.Text + "'";
                cmd = new SqlCommand(ct2, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    UpdateSalesClientCorporateORTraddingORBillingAddress("TraddingAddresses");
                }
                else
                {
                    SaveCorporateORTraddingORBillingAddress("TraddingAddresses");
                }
            }
            else if (checkTable == "BillingAddresses")
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct2 = "select RTRIM(BillingAddresses.SClientId) from BillingAddresses where BillingAddresses.SClientId='" + txtSalesClientId.Text + "'";
                cmd = new SqlCommand(ct2, con);
                rdr = cmd.ExecuteReader();
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    UpdateSalesClientCorporateORTraddingORBillingAddress("BillingAddresses");
                }
                else
                {
                    SaveCorporateORTraddingORBillingAddress("BillingAddresses");
                }
            }

        }
        private void SaveCorporateORTraddingORBillingAddress(string tblName1)
        {
            string sTableName = tblName1;
            try
            {
                if (sTableName == "CorporateAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string insertQ = "insert into " + sTableName + "(PostOfficeId,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CContactNo,SClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(insertQ);
                    cmd.Connection = con;
                    //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdC) ? (object)DBNull.Value : divisionIdC));
                    //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdC) ? (object)DBNull.Value : districtIdC));
                    //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdC) ? (object)DBNull.Value : thanaIdC));
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox.Text) ? (object)DBNull.Value : cBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
                    cmd.Parameters.AddWithValue("@d11", currentSalesClientId);
                    affectedRows1 = (int)cmd.ExecuteScalar();
                    con.Close();
                }
                else if (sTableName == "TraddingAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string Qry = "insert into " + sTableName + "(PostOfficeId,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TContactNo,SClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(Qry);
                    cmd.Connection = con;

                    //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdT) ? (object)DBNull.Value : divisionIdT));
                    //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdT) ? (object)DBNull.Value : districtIdT));
                    //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdT) ? (object)DBNull.Value : thanaIdT));
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));

                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object)DBNull.Value : tFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object)DBNull.Value : tHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object)DBNull.Value : tRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tBlockTextBox.Text) ? (object)DBNull.Value : tBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAreaTextBox.Text) ? (object)DBNull.Value : tAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(tContactNoTextBox.Text) ? (object)DBNull.Value : tContactNoTextBox.Text));
                    cmd.Parameters.AddWithValue("@d11", currentSalesClientId);
                    affectedRows2 = (int)cmd.ExecuteScalar();
                    con.Close();
                }
                else if (sTableName == "BillingAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string Qry = "insert into " + sTableName + "(PostOfficeId,BFlatNo,BHouseNo,BRoadNo,BBlock,BArea,BContactNo,SClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(Qry);
                    cmd.Connection = con;
                    //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdB) ? (object)DBNull.Value : divisionIdB));
                    //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdB) ? (object)DBNull.Value : districtIdB));
                    //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdB) ? (object)DBNull.Value : thanaIdB));
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdB) ? (object)DBNull.Value : postOfficeIdB));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(bFlatNoTextBox.Text) ? (object)DBNull.Value : bFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(bHouseNoTextBox.Text) ? (object)DBNull.Value : bHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(bRoadNoTextBox.Text) ? (object)DBNull.Value : bRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(bBlockTextBox.Text) ? (object)DBNull.Value : bBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(bAreaTextBox.Text) ? (object)DBNull.Value : bAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(bContactNoTextBox.Text) ? (object)DBNull.Value : bContactNoTextBox.Text));
                    cmd.Parameters.AddWithValue("@d11", currentSalesClientId);
                    affectedRows3 = (int)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveContactPersonDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qury = "insert into ContactPersonDetails(ContactPersonName,Designation,CellNumber,EmailBankId,SClientId) Values(@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(qury);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(contactPersonNameAPTextBox.Text) ? (object)DBNull.Value : contactPersonNameAPTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(designationAPTextBox.Text) ? (object)DBNull.Value : designationAPTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(cellNumberAPTextBox.Text) ? (object)DBNull.Value : cellNumberAPTextBox.Text));
            //cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(cmbEmailAddress.Text) ? (object)DBNull.Value : cmbEmailAddress.Text));
            cmd.Parameters.AddWithValue("@d4", bankCPEmailId);
            cmd.Parameters.AddWithValue("@d5", currentSalesClientId);
            affectedRows3 = (int)cmd.ExecuteScalar();
            con.Close();
        }

        private void UpdateContactPersonDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qury = "Update  ContactPersonDetails Set ContactPersonName=@d1,Designation=@d2,CellNumber=@d3,EmailBankId=@d4  where  ContactPersonDetails.SClientId='" + txtSalesClientId.Text + "'";
            cmd = new SqlCommand(qury,con);            
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(contactPersonNameAPTextBox.Text) ? (object)DBNull.Value : contactPersonNameAPTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(designationAPTextBox.Text) ? (object)DBNull.Value : designationAPTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(cellNumberAPTextBox.Text) ? (object)DBNull.Value : cellNumberAPTextBox.Text));
           // cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(cmbCPEmailAddress.Text) ? (object)DBNull.Value : cmbCPEmailAddress.Text));
            cmd.Parameters.AddWithValue("@d4", bankCPEmailId);
            cmd.ExecuteReader();
            con.Close();
        }
        private void GetClientIdAndSaveOrUpdateContactPersondetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string ct2 = "select RTRIM(ContactPersonDetails.SClientId) from ContactPersonDetails where ContactPersonDetails.SClientId='" + txtSalesClientId.Text + "'";
            cmd = new SqlCommand(ct2, con);
            rdr = cmd.ExecuteReader();
            if (rdr.Read() && !rdr.IsDBNull(0))
            {
                UpdateContactPersonDetails();
            }
            else
            {
                SaveContactPersonDetails();
            }

        }

        private void UpdateBankDetails()
        {
            try
            {
                if (bankNameTextBox.Text != "" || accountNoTextBox.Text != "")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string apquery = "Update  BankDetails Set BankName=@d1,BranchName=@d2,AccountNo=@d3  where  BankDetails.SClientId='" + txtSalesClientId.Text + "' ";
                    cmd = new SqlCommand(apquery,con);                  
                    cmd.Parameters.AddWithValue("@d1", bankNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@d2", branchNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@d3", accountNoTextBox.Text);
                    cmd.ExecuteReader();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetClientIdAndSaveOrUpdateBankDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string ct2 = "select RTRIM(BankDetails.SClientId) from BankDetails where BankDetails.SClientId='" + txtSalesClientId.Text + "'";
            cmd = new SqlCommand(ct2, con);
            rdr = cmd.ExecuteReader();
            if (rdr.Read() && !rdr.IsDBNull(0))
            {
                UpdateBankDetails();
            }
            else
            {
                SaveBankDetails();
            }

        }
        private void SaveBankDetails()
        {

            try
            {
                if (bankNameTextBox.Text != "" || accountNoTextBox.Text != "")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string apquery = "insert into BankDetails(SClientId,BankName,BranchName,AccountNo) Values(@d1,@d2,@d3,@d4)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(apquery);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", currentSalesClientId);
                    cmd.Parameters.AddWithValue("@d2", bankNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@d3", branchNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@d4", accountNoTextBox.Text);
                    affectedRows4 = (int)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TASameAsCA(string tableNamek)
        {
            string tableName = tableNamek;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string query = "Update " + tableName + " set PostOfficeId=@d4,TFlatNo=@d5,THouseNo=@d6,TRoadNo=@d7,TBlock=@d8,TArea=@d9,TContactNo=@d10  Where  TraddingAddresses.SClientId='" + txtSalesClientId.Text + "'";
            cmd = new SqlCommand(query, con);
            //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdC) ? (object)DBNull.Value : divisionIdC));
            //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdC) ? (object)DBNull.Value : districtIdC));
            //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdC) ? (object)DBNull.Value : thanaIdC));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox.Text) ? (object)DBNull.Value : cBlockTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
            rdr = cmd.ExecuteReader();
            con.Close();

        }
        private void BASameAsCA(string tableNamek)
        {
            string tableName = tableNamek;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string query = "Update " + tableName + " set PostOfficeId=@d4,BFlatNo=@d5,BHouseNo=@d6,BRoadNo=@d7,BBlock=@d8,BArea=@d9,BContactNo=@d10  Where  BillingAddresses.SClientId='" + txtSalesClientId.Text + "'";
            cmd = new SqlCommand(query, con);
            //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdC) ? (object)DBNull.Value : divisionIdC));
            //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdC) ? (object)DBNull.Value : districtIdC));
            //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdC) ? (object)DBNull.Value : thanaIdC));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox.Text) ? (object)DBNull.Value : cBlockTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
            rdr = cmd.ExecuteReader();
            con.Close();

        }
        private void BASameAsTA(string tableNamek)
        {
            string tableName = tableNamek;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string query = "Update " + tableName + " set PostOfficeId=@d4,BFlatNo=@d5,BHouseNo=@d6,BRoadNo=@d7,BBlock=@d8,BArea=@d9,BContactNo=@d10  Where  BillingAddresses.SClientId='" + txtSalesClientId.Text + "'";
            cmd = new SqlCommand(query, con);
            //cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdT) ? (object)DBNull.Value : divisionIdT));
            //cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdT) ? (object)DBNull.Value : districtIdT));
            //cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdT) ? (object)DBNull.Value : thanaIdT));
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
        private void SaveCauseOfUpDate()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string qry = "insert into UpdateLog(CauseOfUpdate,UpdateByUId,UpdateDateTime,SClientId) Values(@d1,@d2,@d3,@d4)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@d1", txtCauseOfUpdate.Text);
                cmd.Parameters.AddWithValue("@d2", userId);
                cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                cmd.Parameters.AddWithValue("@d4", txtSalesClientId.Text);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (cmbSuperviserName.Text == "")
            {
                MessageBox.Show("Please select supervisor Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((bANotAppCheckBox.Checked == false) && (bASameAsCACheckBox.Checked == false) &&
                (bASameAsTACheckBox.Checked == false))
            {
                if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
                {
                    MessageBox.Show("Please select billing Address division", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bDistrictCombo.Text))
                {
                    MessageBox.Show("Please Select billing Address district", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bThanaCombo.Text))
                {
                    MessageBox.Show("Please select billing Address Thana", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bPostOfficeCombo.Text))
                {
                    MessageBox.Show("Please Select billing Address Post Name", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bPostCodeTextBox.Text))
                {
                    MessageBox.Show("Please select billing Address Post Code", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {


                GetClientTypeId();
                GetNatureOfClientId();
                GetIndustryCategoryId();
                //1.Tradding Address Not Applicable &&  Billing Address Not Applicable
                if (tANotApplicable.Checked && bANotAppCheckBox.Checked)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                }

                //2.Tradding Address Not Applicable &&  Billing Address  same as Corporat Address
                else if (tANotApplicable.Checked && bASameAsCACheckBox.Checked)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    BASameAsCA("BillingAddresses"); //diff Method

                }
                //3.Tradding Address Not Applicable &&  Billing Address  Applicable
                else if (tANotApplicable.Checked && bANotAppCheckBox.Checked == false && bASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked == false)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    GetClientIdAndSaveOrUpdateSalesClientAddress("BillingAddresses");

                }
                //4.Tradding Address same as Corporat Address &&  Billing Address Not Applicable
                else if (tASameAsCACheckBox.Checked && bANotAppCheckBox.Checked)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    TASameAsCA("TraddingAddresses"); //diff method  

                }
                //5.Tradding Address same as Corporat Address &&  Billing Address same as Corporat Address

                else if (tASameAsCACheckBox.Checked && bASameAsCACheckBox.Checked)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    TASameAsCA("TraddingAddresses"); //diff method  
                    BASameAsCA("BillingAddresses"); //diff method  

                }
                //6.Tradding Address same as Corporat Address &&  Billing Address Applicable

                else if (tASameAsCACheckBox.Checked && bANotAppCheckBox.Checked == false && bASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked == false)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    TASameAsCA("TraddingAddresses"); //diff method  
                    GetClientIdAndSaveOrUpdateSalesClientAddress("BillingAddresses");

                }
                //7.Tradding Address Aplicable  &&  Biling Address Not Applicable

                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bANotAppCheckBox.Checked)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    GetClientIdAndSaveOrUpdateSalesClientAddress("TraddingAddresses");

                }
                //8.Tradding Address Aplicable  &&  Biling Address Same As Corporat Address
                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bASameAsCACheckBox.Checked)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    GetClientIdAndSaveOrUpdateSalesClientAddress("TraddingAddresses");
                    BASameAsCA("BillingAddresses"); //diff method  

                }
                //9.Tradding Address Aplicable  &&  Biling Address Same As Tradding Address
                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    GetClientIdAndSaveOrUpdateSalesClientAddress("TraddingAddresses");
                    BASameAsTA("BillingAddresses"); //diff method  

                }
                //10.Tradding Address Aplicable  &&  Biling Address Applicable
                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bANotAppCheckBox.Checked == false && bASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked == false)
                {
                    UpdateSalesClient();
                    GetClientIdAndSaveOrUpdateSalesClientAddress("CorporateAddresses");
                    GetClientIdAndSaveOrUpdateSalesClientAddress("TraddingAddresses");                  
                    GetClientIdAndSaveOrUpdateSalesClientAddress("BillingAddresses");

                }
                if (!string.IsNullOrEmpty(contactPersonNameAPTextBox.Text))
                {
                    GetClientIdAndSaveOrUpdateContactPersondetails();
                }
                if (!string.IsNullOrEmpty(bankNameTextBox.Text))
                {
                    GetClientIdAndSaveOrUpdateBankDetails();
                }
                MessageBox.Show("Successfully Updated .", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveCauseOfUpDate();               
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSuperviserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT UserId from Registration WHERE Name = '" + cmbSuperviserName.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    superviserId = (rdr.GetInt32(0));
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tDivitionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Division"));
                cmd.Parameters["@find"].Value = tDivitionCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    divisionIdT = (rdr.GetString(0).Trim());

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                tDivitionCombo.Text = tDivitionCombo.Text.Trim();
                tDistComboBox.Items.Clear();
                tDistComboBox.Text = "";
                tDistComboBox.Enabled = true;
                tDistComboBox.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" + divisionIdT + "' order by Districts.Division_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tDistComboBox.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tDistComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "District"));
                cmd.Parameters["@find"].Value = tDistComboBox.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    districtIdT = (rdr.GetString(0).Trim());

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                tDistComboBox.Text = tDistComboBox.Text.Trim();
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

        private void bDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division=@find";

                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Division"));
                cmd.Parameters["@find"].Value = bDivisionCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    divisionIdB = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                bDivisionCombo.Text = bDivisionCombo.Text.Trim();
                bDistrictCombo.Items.Clear();
                bDistrictCombo.Text = "";
                bDistrictCombo.Enabled = true;
                bDistrictCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" + divisionIdB + "' order by Districts.Division_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    bDistrictCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bDistrictCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "District"));
                cmd.Parameters["@find"].Value = bDistrictCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    districtIdB = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                bDistrictCombo.Text = bDistrictCombo.Text.Trim();
                bThanaCombo.Items.Clear();
                bThanaCombo.Text = "";
                bThanaCombo.Enabled = true;
                bThanaCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Thanas.Thana) from Thanas  Where Thanas.D_ID = '" + districtIdB + "' order by Thanas.D_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    bThanaCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "Thana"));
                cmd.Parameters["@find"].Value = bThanaCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    thanaIdB = (rdr.GetString(0));

                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                bThanaCombo.Text = bThanaCombo.Text.Trim();
                bPostOfficeCombo.Items.Clear();
                bPostOfficeCombo.Text = "";
                bPostOfficeCombo.Enabled = true;
                bPostOfficeCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(PostOffice.PostOfficeName) from PostOffice  Where PostOffice.T_ID = '" + thanaIdB + "' order by PostOffice.T_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    bPostOfficeCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bPostOfficeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode) from PostOffice WHERE PostOffice.PostOfficeName=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "PostOfficeName"));
                cmd.Parameters["@find"].Value = bPostOfficeCombo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    postOfficeIdB = (rdr.GetString(0));
                    bPostCodeTextBox.Text = (rdr.GetString(1));

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
        private void ResetTraddingAddress()
        {
            tFlatNoTextBox.Clear();
            tHouseNoTextBox.Clear();
            tRoadNoTextBox.Clear();
            tBlockTextBox.Clear();
            tAreaTextBox.Clear();
            tContactNoTextBox.Clear();
            tDivitionCombo.SelectedIndex = -1;
            tDistComboBox.SelectedIndex = -1;
            tThanaCombo.SelectedIndex = -1;
            tPostOfficeCombo.SelectedIndex = -1;
            tPostCodeTextBox.Clear();
        }
        private void ResetBillingAddress()
        {
            bFlatNoTextBox.Clear();
            bHouseNoTextBox.Clear();
            bRoadNoTextBox.Clear();
            bBlockTextBox.Clear();
            bAreaTextBox.Clear();
            bContactNoTextBox.Clear();
            bDivisionCombo.SelectedIndex = -1;
            bDistrictCombo.SelectedIndex = -1;
            bThanaCombo.SelectedIndex = -1;
            bPostOfficeCombo.SelectedIndex = -1;
            bPostCodeTextBox.Clear();
        }
        private void Reset()
        {
            txtSalesClientId.Clear();
            cmbSuperviserName.SelectedIndex = -1;
            //txtIClientId.Clear();
            clientNameAPTextBox.Clear();
            cmbClientType.SelectedIndex = -1;
            cmbNatureOfClient.SelectedIndex = -1;
            cmbEmailAddress.SelectedIndex = -1;
            cmbCPEmailAddress.SelectedIndex = -1;
            cmbIndustryCategory.SelectedIndex = -1;

            cFlatNoTextBox.Clear();
            cHouseNoTextBox.Clear();
            cRoadNoTextBox.Clear();
            cBlockTextBox.Clear();
            cAreaTextBox.Clear();
            cContactNoTextBox.Clear();
            cDivisionCombo.SelectedIndex = -1;
            cDistrictCombo.SelectedIndex = -1;
            cThanaCombo.SelectedIndex = -1;
            cPostOfficeCombo.SelectedIndex = -1;
            cPostCodeTextBox.Clear();

            bANotAppCheckBox.CheckedChanged -= bANotAppCheckBox_CheckedChanged;
            bANotAppCheckBox.Checked = false;
            bANotAppCheckBox.CheckedChanged += bANotAppCheckBox_CheckedChanged;

            bASameAsCACheckBox.CheckedChanged -= bASameAsCACheckBox_CheckedChanged;
            bASameAsCACheckBox.Checked = false;
            bASameAsCACheckBox.CheckedChanged += bASameAsCACheckBox_CheckedChanged;

            bASameAsTACheckBox.CheckedChanged -= bASameAsTACheckBox_CheckedChanged;
            bASameAsTACheckBox.Checked = false;
            bASameAsTACheckBox.CheckedChanged += bASameAsTACheckBox_CheckedChanged;

            tASameAsCACheckBox.CheckedChanged -= tASameAsCACheckBox_CheckedChanged;
            tASameAsCACheckBox.Checked = false;
            tASameAsCACheckBox.CheckedChanged += tASameAsCACheckBox_CheckedChanged;

            tANotApplicable.CheckedChanged -= tANotApplicable_CheckedChanged;
            tANotApplicable.Checked = false;
            tANotApplicable.CheckedChanged += tANotApplicable_CheckedChanged;

            ResetTraddingAddress();

            contactPersonNameAPTextBox.Clear();
            designationAPTextBox.Clear();
            cellNumberAPTextBox.Clear();
            endUserAPTextBox.Clear();

            ResetBillingAddress();

            bankNameTextBox.Clear();
            branchNameTextBox.Clear();
            accountNoTextBox.Clear();

            updateButton.Enabled = true;

        }


        private void tANotApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (tANotApplicable.Checked)
            {

                if (tASameAsCACheckBox.Checked)
                {
                    tASameAsCACheckBox.CheckedChanged -= tASameAsCACheckBox_CheckedChanged;
                    tASameAsCACheckBox.Checked = false;
                    tASameAsCACheckBox.CheckedChanged += tASameAsCACheckBox_CheckedChanged;
                    groupBox8.Enabled = false;
                    ResetTraddingAddress();
                }
                else
                {

                    groupBox8.Enabled = false;
                    ResetTraddingAddress();

                }

            }
            else
            {
                if (tASameAsCACheckBox.Checked)
                {
                    groupBox8.Enabled = false;
                    ResetTraddingAddress();
                }
                else
                {

                    groupBox8.Enabled = true;
                    ResetTraddingAddress();

                }
            }           
        }

        private void tASameAsCACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tASameAsCACheckBox.Checked)
            {
                bASameAsTACheckBox.Visible = false;

                if (tANotApplicable.Checked)
                {
                    tANotApplicable.CheckedChanged -= tANotApplicable_CheckedChanged;
                    tANotApplicable.Checked = false;
                    tANotApplicable.CheckedChanged += tANotApplicable_CheckedChanged;
                    groupBox8.Enabled = false;
                    ResetTraddingAddress();
                }
                else
                {

                    groupBox8.Enabled = false;
                    ResetTraddingAddress();
                }

            }
            else
            {
                bASameAsTACheckBox.Visible = true;
                if (tANotApplicable.Checked)
                {
                    groupBox8.Enabled = false;
                    ResetTraddingAddress();
                }
                else
                {

                    groupBox8.Enabled = true;
                    ResetTraddingAddress();
                }
            }
        }

        private void bANotAppCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bANotAppCheckBox.Checked)
            {

                if (bASameAsCACheckBox.Checked || bASameAsTACheckBox.Checked)
                {
                    bASameAsCACheckBox.CheckedChanged -= bASameAsCACheckBox_CheckedChanged;
                    bASameAsCACheckBox.Checked = false;
                    bASameAsCACheckBox.CheckedChanged += bASameAsCACheckBox_CheckedChanged;

                    bASameAsTACheckBox.CheckedChanged -= bASameAsTACheckBox_CheckedChanged;
                    bASameAsTACheckBox.Checked = false;
                    bASameAsTACheckBox.CheckedChanged += bASameAsTACheckBox_CheckedChanged;

                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }

            }
            else
            {
                if (tASameAsCACheckBox.Checked || bASameAsTACheckBox.Checked)
                {
                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox7.Enabled = true;
                    ResetBillingAddress();
                }
            }           
        }

        private void bASameAsCACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bASameAsCACheckBox.Checked)
            {

                if (bANotAppCheckBox.Checked || bASameAsTACheckBox.Checked)
                {
                    bANotAppCheckBox.CheckedChanged -= bANotAppCheckBox_CheckedChanged;
                    bANotAppCheckBox.Checked = false;
                    bANotAppCheckBox.CheckedChanged += bANotAppCheckBox_CheckedChanged;

                    bASameAsTACheckBox.CheckedChanged -= bASameAsTACheckBox_CheckedChanged;
                    bASameAsTACheckBox.Checked = false;
                    bASameAsTACheckBox.CheckedChanged += bASameAsTACheckBox_CheckedChanged;

                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }

            }
            else
            {
                if (tANotApplicable.Checked || bASameAsTACheckBox.Checked)
                {
                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox7.Enabled = true;
                }
            } 
        }

        private void bASameAsTACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bASameAsTACheckBox.Checked)
            {

                if (bANotAppCheckBox.Checked || bASameAsCACheckBox.Checked)
                {
                    bANotAppCheckBox.CheckedChanged -= bANotAppCheckBox_CheckedChanged;
                    bANotAppCheckBox.Checked = false;
                    bANotAppCheckBox.CheckedChanged += bANotAppCheckBox_CheckedChanged;

                    bASameAsCACheckBox.CheckedChanged -= bASameAsCACheckBox_CheckedChanged;
                    bASameAsCACheckBox.Checked = false;
                    bASameAsCACheckBox.CheckedChanged += bASameAsCACheckBox_CheckedChanged;

                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }

            }
            else
            {
                if (tANotApplicable.Checked || bASameAsCACheckBox.Checked)
                {
                    groupBox7.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox7.Enabled = true;
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForSalseClientMP frm=new ForSalseClientMP();
               frm.Show();
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

        private void cmbNatureOfClient_SelectedIndexChanged(object sender, EventArgs e)
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
                            cmd.Parameters.AddWithValue("@d2", userId);
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
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        string emailId = input.Trim();
                        Regex mRegxExpression;

                        mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                        if (!mRegxExpression.IsMatch(emailId))
                        {

                            MessageBox.Show("Please type a valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;

                        }
                    }

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
                            cmd.Parameters.AddWithValue("@d2", userId);
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

        private void cmbEmailAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbEmailAddress.Text) && !cmbEmailAddress.Items.Contains(cmbEmailAddress.Text))
            {
                MessageBox.Show("Please Select A Valid Email Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEmailAddress.ResetText();
                this.BeginInvoke(new ChangeFocusDelegate(changeFocus), cmbEmailAddress);
            }
        }

        private void cPostOfficeCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cDivisionCombo.Focus();
            }



            else if (string.IsNullOrWhiteSpace(cDistrictCombo.Text))
            {
                MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cDistrictCombo.Focus();
            }
            else if (string.IsNullOrWhiteSpace(cThanaCombo.Text))
            {
                MessageBox.Show("Please  select thana name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cThanaCombo.Focus();
            }
        }

        private void cThanaCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cDivisionCombo.Focus();
            }



            else if (string.IsNullOrWhiteSpace(cDistrictCombo.Text))
            {
                MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cDistrictCombo.Focus();
            }
        }

        private void cDistrictCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cDivisionCombo.Focus();
            }

        }

        private void tPostOfficeCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tDivitionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tDivitionCombo.Focus();
            }



            else if (string.IsNullOrWhiteSpace(tDistComboBox.Text))
            {
                MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tDistComboBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(tThanaCombo.Text))
            {
                MessageBox.Show("Please  select thana name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tThanaCombo.Focus();
            }
        }

        private void tThanaCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tDivitionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tDivitionCombo.Focus();
            }



            else if (string.IsNullOrWhiteSpace(tDistComboBox.Text))
            {
                MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tDistComboBox.Focus();
            }
        }

        private void tDistComboBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tDivitionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tDivitionCombo.Focus();
            }

        }

        private void bPostOfficeCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bDivisionCombo.Focus();
            }



            else if (string.IsNullOrWhiteSpace(bDistrictCombo.Text))
            {
                MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bDistrictCombo.Focus();
            }
            else if (string.IsNullOrWhiteSpace(bThanaCombo.Text))
            {
                MessageBox.Show("Please  select thana name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bThanaCombo.Focus();
            }
        }

        private void bThanaCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bDivisionCombo.Focus();
            }

            else if (string.IsNullOrWhiteSpace(bDistrictCombo.Text))
            {
                MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bDistrictCombo.Focus();
            }
        }

        private void bDistrictCombo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
            {
                MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bDivisionCombo.Focus();
            }

        }

        private void SalesClientUpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            ForSalseClientMP frm = new ForSalseClientMP();
            frm.Show();
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

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        string emailId = input.Trim();
                        Regex mRegxExpression;

                        mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                        if (!mRegxExpression.IsMatch(emailId))
                        {

                            MessageBox.Show("Please type a valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;

                        }
                    }

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
                            cmd.Parameters.AddWithValue("@d2", userId);
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
    }
}
