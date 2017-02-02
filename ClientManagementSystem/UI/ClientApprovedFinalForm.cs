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
using ClientManagementSystem.Manager;
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class ClientApprovedFinalForm : Form
    {
        ConnectionString cs=new ConnectionString();
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataReader rdr = null;
        public int affectedRows1, affectedRows2, affectedRows3, affectedRows4, currentSalesClientId, addressTypeId1 = 1, addressTypeId2 = 2, addressTypeId3 = 3;
        public string fullName2, userId, districtIdC, districtIdT, districtIdB, divisionIdC, divisionIdT, divisionIdB, thanaIdC, thanaIdT, thanaIdB, postofficeIdC, postOfficeIdT, postOfficeIdB;
        public ClientGateway clientGateway, clientGateway1, clientGateway2;
        private List<InqueryClient> clients;
        private InqueryClient client;
        private Addresss add1,add2;
        public string natureOfClientId, industryCategoryId, clientTypeId;
        public int superviserId;
        public ClientApprovedFinalForm()
        {
            InitializeComponent();
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

        private void SaveCorporateAddress(string tblName1)
        {
            string cAAddtbl = tblName1;
            try
            {
               
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string insertQ = "insert into "+cAAddtbl+"(Division_ID,D_ID,T_ID,PostOfficeId,FlatNo,HouseNo,RoadNo,Block,Area,ContactNo,IClientId,SClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)" + "SELECT CONVERT(int, SCOPE_IDENTITY())"; 
                    cmd = new SqlCommand(insertQ);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1",string.IsNullOrEmpty(divisionIdC) ? (object)DBNull.Value : divisionIdC));
                    cmd.Parameters.Add(new SqlParameter("@d2",string.IsNullOrEmpty(districtIdC) ? (object)DBNull.Value : districtIdC));
                    cmd.Parameters.Add(new SqlParameter("@d3",string.IsNullOrEmpty(thanaIdC) ? (object)DBNull.Value : thanaIdC));
                    cmd.Parameters.Add(new SqlParameter("@d4",string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
                    cmd.Parameters.Add(new SqlParameter("@d5",string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object) DBNull.Value : cFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6",string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object) DBNull.Value : cHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7",string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object) DBNull.Value : cRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8",string.IsNullOrEmpty(cBlockTextBox.Text) ? (object) DBNull.Value : cBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9",string.IsNullOrEmpty(cAreaTextBox.Text) ? (object) DBNull.Value : cAreaTextBox.Text));                   
                    cmd.Parameters.Add(new SqlParameter("@d10",string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object) DBNull.Value : cContactNoTextBox.Text));                  
                    cmd.Parameters.Add(new SqlParameter("@d11", string.IsNullOrEmpty(txtIClientId.Text) ? (object)DBNull.Value : txtIClientId.Text));
                    cmd.Parameters.AddWithValue("@d12", currentSalesClientId);
                    affectedRows1 = (int) cmd.ExecuteScalar();
                    con.Close();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveTraddingAddress(string tblName2)
        {
            string traddingAdd = tblName2;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string Qry = "insert into " + traddingAdd + "(Division_ID,D_ID,T_ID,PostOfficeId,FlatNo,HouseNo,RoadNo,Block,Area,ContactNo,IClientId,SClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(Qry);
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdT) ? (object)DBNull.Value : divisionIdT));
                cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdT) ? (object)DBNull.Value : districtIdT));
                cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdT) ? (object)DBNull.Value : thanaIdT));
                cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));

                cmd.Parameters.Add(new SqlParameter("@d5",string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object) DBNull.Value : tFlatNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d6",string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object) DBNull.Value : tHouseNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d7",string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object) DBNull.Value : tRoadNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d8",string.IsNullOrEmpty(tBlockTextBox.Text) ? (object) DBNull.Value : tBlockTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d9",string.IsNullOrEmpty(tAreaTextBox.Text) ? (object) DBNull.Value : tAreaTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d10",string.IsNullOrEmpty(tContactNoTextBox.Text) ? (object) DBNull.Value : tContactNoTextBox.Text));              
                cmd.Parameters.Add(new SqlParameter("@d11", string.IsNullOrEmpty(txtIClientId.Text) ? (object)DBNull.Value : txtIClientId.Text));
                cmd.Parameters.AddWithValue("@d12", currentSalesClientId); 
                affectedRows2 = (int) cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void SaveBillingAddress(string  tbleName3)
        {
            string billingAdd = tbleName3;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string Qry = "insert into " + billingAdd + "(Division_ID,D_ID,T_ID,PostOfficeId,FlatNo,HouseNo,RoadNo,Block,Area,ContactNo,IClientId,SClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(Qry);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1",string.IsNullOrEmpty(divisionIdB) ? (object)DBNull.Value : divisionIdB));
                cmd.Parameters.Add(new SqlParameter("@d2",string.IsNullOrEmpty(districtIdB) ? (object)DBNull.Value : districtIdB));
                cmd.Parameters.Add(new SqlParameter("@d3",string.IsNullOrEmpty(thanaIdB) ? (object)DBNull.Value : thanaIdB));
                cmd.Parameters.Add(new SqlParameter("@d4",string.IsNullOrEmpty(postOfficeIdB) ? (object)DBNull.Value : postOfficeIdB));
                cmd.Parameters.Add(new SqlParameter("@d5",string.IsNullOrEmpty(bFlatNoTextBox.Text) ? (object) DBNull.Value : bFlatNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d6",string.IsNullOrEmpty(bHouseNoTextBox.Text) ? (object) DBNull.Value : bHouseNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d7",string.IsNullOrEmpty(bRoadNoTextBox.Text) ? (object) DBNull.Value : bRoadNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d8",string.IsNullOrEmpty(bBlockTextBox.Text) ? (object) DBNull.Value : bBlockTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d9",string.IsNullOrEmpty(bAreaTextBox.Text) ? (object) DBNull.Value : bAreaTextBox.Text));               
                cmd.Parameters.Add(new SqlParameter("@d10",string.IsNullOrEmpty(bContactNoTextBox.Text) ? (object)DBNull.Value : bContactNoTextBox.Text));                
                cmd.Parameters.Add(new SqlParameter("@d11", string.IsNullOrEmpty(txtIClientId.Text) ? (object)DBNull.Value : txtIClientId.Text));
                cmd.Parameters.AddWithValue("@d12", currentSalesClientId);
                affectedRows3 = (int) cmd.ExecuteScalar();
                con.Close();
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
                cmd = new SqlCommand(query,con);
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

        private void CreateSalesClient()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string apquery = "insert into SalesClient(IClientId,ClientName,ClientTypeId,NatureOfClientId,EmailAddress,IndustryCategoryId,EndUser,UserId,Dates,SuperviserId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(apquery);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@d1", txtIClientId.Text);
            //cmd.Parameters.AddWithValue("@d2", cmbSuperviserName.Text);
            cmd.Parameters.AddWithValue("@d2", clientNameAPTextBox.Text);
            cmd.Parameters.AddWithValue("@d3", clientTypeId);
            cmd.Parameters.AddWithValue("@d4", natureOfClientId);
            cmd.Parameters.AddWithValue("@d5", emailAddressAPTextBox.Text);
            cmd.Parameters.AddWithValue("@d6", industryCategoryId);
            cmd.Parameters.AddWithValue("@d7", endUserAPTextBox.Text);
            cmd.Parameters.AddWithValue("@d8", userId);
            cmd.Parameters.AddWithValue("@d9", DateTime.UtcNow.ToLocalTime());
            cmd.Parameters.AddWithValue("@d10", superviserId);
            currentSalesClientId = (int)cmd.ExecuteScalar();
            con.Close();

        }
        private void SaveContactPersonDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qury = "insert into ContactPersonDetails(ContactPersonName,Designation,CellNumber,EmailId,SClientId) Values(@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(qury);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(contactPersonNameAPTextBox.Text) ? (object)DBNull.Value : contactPersonNameAPTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(designationAPTextBox.Text) ? (object)DBNull.Value : designationAPTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(cellNumberAPTextBox.Text) ? (object)DBNull.Value : cellNumberAPTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(emailAddressAPTextBox.Text) ? (object)DBNull.Value : emailAddressAPTextBox.Text));
            cmd.Parameters.AddWithValue("@d5", currentSalesClientId);
            affectedRows3 = (int)cmd.ExecuteScalar();
            con.Close();
        }
       
        private void approvedButton_Click(object sender, EventArgs e)
        {
            if (cmbSuperviserName.Text == "")
            {
                MessageBox.Show("Please select supervisor Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((bANotAppCheckBox.Checked == false) && (bASameAsCACheckBox.Checked == false) && (bASameAsTACheckBox.Checked == false))
            {
                if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
                {
                    MessageBox.Show("Please select billing Address division", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bDistrictCombo.Text))
                {
                    MessageBox.Show("Please Select billing Address district", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bThanaCombo.Text))
                {
                    MessageBox.Show("Please select billing Address Thana", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bPostOfficeCombo.Text ))
                {
                    MessageBox.Show("Please Select billing Address Post Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bPostCodeTextBox.Text))
                {
                    MessageBox.Show("Please select billing Address Post Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "select ClientName from SalesClient where ClientName='" + clientNameAPTextBox.Text + "'";

                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("This Sales Client Already Exists. You can not create it again.Please Select another Client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Hide();
                    ForSalseClientMP frm = new ForSalseClientMP();
                    frm.Show();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }

                GetClientTypeId();
                GetNatureOfClientId();
                GetIndustryCategoryId();
                
                //1.Tradding Address Not Applicable &&  Billing Address Not Applicable
                if (tANotApplicable.Checked && bANotAppCheckBox.Checked)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                  
                }

                //2.Tradding Address Not Applicable &&  Billing Address  same as Corporat Address
                else if (tANotApplicable.Checked && bASameAsCACheckBox.Checked)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveCorporateAddress("BillingAddresses"); //diff Method
                   
                }
                //3.Tradding Address Not Applicable &&  Billing Address  Applicable
                else if (tANotApplicable.Checked && bANotAppCheckBox.Checked == false && bASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked == false)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveBillingAddress("BillingAddresses");
                    
                }
                //4.Tradding Address same as Corporat Address &&  Billing Address Not Applicable
                else if (tASameAsCACheckBox.Checked && bANotAppCheckBox.Checked)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveCorporateAddress("TraddingAddresses"); //diff method  
                    
                }
                //5.Tradding Address same as Corporat Address &&  Billing Address same as Corporat Address

                else if (tASameAsCACheckBox.Checked && bASameAsCACheckBox.Checked)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveCorporateAddress("TraddingAddresses"); //diff method  
                    SaveCorporateAddress("BillingAddresses"); //diff method  
                    
                }
                //6.Tradding Address same as Corporat Address &&  Billing Address Applicable

                else if (tASameAsCACheckBox.Checked && bANotAppCheckBox.Checked == false && bASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked == false)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveCorporateAddress("TraddingAddresses"); //diff method  
                    SaveBillingAddress("BillingAddresses");
                    
                }
                //7.Tradding Address Aplicable  &&  Biling Address Not Applicable

                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bANotAppCheckBox.Checked)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveTraddingAddress("TraddingAddresses");
                   
                }
                //8.Tradding Address Aplicable  &&  Biling Address Same As Corporat Address

                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bASameAsCACheckBox.Checked)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveTraddingAddress("TraddingAddresses");
                    SaveCorporateAddress("BillingAddresses"); //diff method  
                    
                }
                //9.Tradding Address Aplicable  &&  Biling Address Same As Tradding Address

                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveTraddingAddress("TraddingAddresses");
                    SaveTraddingAddress("BillingAddresses"); //diff method  
                    
                }
                //10.Tradding Address Aplicable  &&  Biling Address Applicable

                else if (tANotApplicable.Checked == false && tASameAsCACheckBox.Checked == false && bANotAppCheckBox.Checked == false && bASameAsCACheckBox.Checked == false && bASameAsTACheckBox.Checked == false)
                {
                    CreateSalesClient();
                    SaveCorporateAddress("CorporateAddresses");
                    SaveTraddingAddress("TraddingAddresses");
                    SaveBillingAddress("BillingAddresses");
                    
                }
                if (!string.IsNullOrEmpty(contactPersonNameAPTextBox.Text))
                {
                    SaveContactPersonDetails();
                }
                if (!string.IsNullOrEmpty(bankNameTextBox.Text))
                {
                    SaveBankDetails();
                }
                MessageBox.Show("Registration Completed Successfully,Current Id is:" + currentSalesClientId, "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);              
                approvedButton.Enabled = false;     
                txtIClientId.Clear();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            cmbSuperviserName.SelectedIndex = -1;
            txtIClientId.Clear();
            clientNameAPTextBox.Clear();
            cmbClientType.SelectedIndex=-1;
            cmbNatureOfClient.SelectedIndex = -1;
            emailAddressAPTextBox.Clear();
            txtCPEmailAddress.Clear();
            cmbIndustryCategory.SelectedIndex=-1;

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

            contactPersonNameAPTextBox.Clear();
            designationAPTextBox.Clear();
            cellNumberAPTextBox.Clear();
            endUserAPTextBox.Clear();

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

            bankNameTextBox.Clear();
            branchNameTextBox.Clear();
            accountNoTextBox.Clear();

            approvedButton.Enabled = true;
            
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForSalseClientMP aMainUi=new ForSalseClientMP();
            aMainUi.ShowDialog();
        }

        private void cContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
       //     if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       //(e.KeyChar != '.'))
       //     {
       //         e.Handled = true;
       //     }

          
       //     if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
       //     {
       //         e.Handled = true;
       //     }
        }

        private void cPostCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
       //     if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       //(e.KeyChar != '.'))
       //     {
       //         e.Handled = true;
       //     }

           
       //     if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
       //     {
       //         e.Handled = true;
       //     }
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

        private void bContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void bPostCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void accountNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cellNumberAPTextBox_KeyPress(object sender, KeyPressEventArgs e)
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
        public void FillCDistrictCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  order by Districts.D_ID desc ";
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
        public void FillTDistrictCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  order by Districts.D_ID desc";
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
        public void FillBDistrictCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  order by Districts.D_ID desc";
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
       
        private void ClientApprovedFinalForm_Load(object sender, EventArgs e)
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
        private void Report2()
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
            SalesClientInputReport cr = new SalesClientInputReport();
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
       
        private void GetTraddingAddress()
        {
            try
            {
                clientGateway2 = new ClientGateway();
                int iClientId2 = Convert.ToInt32(txtIClientId.Text);
                add2 = clientGateway2.SearchTraddingAddress(iClientId2);
                
                    //tFlatNoTextBox.Text = add2.TFlatNo;
                    //tHouseNoTextBox.Text = add2.THouseNo;
                    //tRoadNoTextBox.Text = add2.TRoadNo;
                    //tBlockTextBox.Text = add2.TBlock;
                    //tAreaTextBox.Text = add2.TARea;
                    //tThanaCombo.Text = add2.TPost;
                    //tPostCodeTextBox.Text = add2.TPostCode;
                    //tDistComboBox.Text = add2.TDistrict;
                    //tContactNoTextBox.Text = add2.TContactNo;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetCorporateAddress()
        {

            try
            {
                clientGateway1 = new ClientGateway();
                int iClientId1 = Convert.ToInt32(txtIClientId.Text);
                add1 = clientGateway1.SearchCorporateAddress(iClientId1);

                    //cFlatNoTextBox.Text = add1.CFlatNo;
                    //cHouseNoTextBox.Text = add1.CHouseNo;
                    //cRoadNoTextBox.Text = add1.CRoadNo;
                    //cBlockTextBox.Text = add1.CBlock;
                    //cAreaTextBox.Text = add1.CARea;
                    //cThanaCombo.Text = add1.CPost;
                    //cPostCodeTextBox.Text = add1.CPostCode;
                    //cDistrictCombo.Text = add1.CDistrict;
                    //cContactNoTextBox.Text = add1.CContactNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iClientIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
   
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string query = "SELECT RTRIM(InquieryClient.ClientName),RTRIM(ClientTypes.ClientType),RTRIM(NatureOfClients.ClientNature),RTRIM(InquieryClient.EmailAddress),RTRIM(IndustryCategorys.IndustryCategory),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.Designation),RTRIM(ContactPersonDetails.CellNumber),RTRIM(InquieryClient.EndUser) FROM InquieryClient,ClientTypes,NatureOfClients,IndustryCategorys,ContactPersonDetails WHERE InquieryClient.ClientTypeId=ClientTypes.ClientTypeId and InquieryClient.NatureOfClientId=NatureOfClients.NatureOfClientId  and InquieryClient.IndustryCategoryId=IndustryCategorys.IndustryCategoryId and InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.IClientId='" + txtIClientId.Text + "'";
                cmd=new SqlCommand(query,con);
                rdr = cmd.ExecuteReader();
                if (txtIClientId.Text == null)
                {

                    MessageBox.Show("This Client Id is not Live,Please register as Inquiry Client.");
                }
                else
                {
                    if (rdr.Read())
                    {
                        clientNameAPTextBox.Text = (rdr.GetString(0));
                        cmbClientType.Text = (rdr.GetString(1));
                        cmbNatureOfClient.Text = (rdr.GetString(2));
                        emailAddressAPTextBox.Text = (rdr.GetString(3));
                        cmbIndustryCategory.Text = (rdr.GetString(4));
                        contactPersonNameAPTextBox.Text = (rdr.GetString(5));
                        designationAPTextBox.Text = (rdr.GetString(6));
                        cellNumberAPTextBox.Text = (rdr.GetString(7));
                        endUserAPTextBox.Text = (rdr.GetString(8));
                    }
                }

                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                GetCorporateAddress();
                GetTraddingAddress();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SaveStatus()
        {

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb2 = "Update InquieryClient Set Status='Done', SClientId='" + affectedRows4 + "' Where IClientId = '" + txtIClientId.Text + "'";
                cmd = new SqlCommand(cb2);
                cmd.Connection = con;
                cmd.ExecuteReader();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clientTypeAPTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtIndustryCategoryCombo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAPNatureOfClient_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForSalseClientMP frm=new ForSalseClientMP();
             frm.Show();
        }

        private void emailAddressAPTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(emailAddressAPTextBox.Text))
            {
                string emailId = emailAddressAPTextBox.Text.Trim();
                Regex mRegxExpression;

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(emailId))
                {

                    MessageBox.Show("Please type a valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    emailAddressAPTextBox.Clear();
                   

                }
            }
        }

        private void cDistrictCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District = '" + cDistrictCombo.Text + "'";
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

        private void tDistComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void bDistrictCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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

        private void tDistComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District = '" + tDistComboBox.Text + "'";
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

        private void bDistrictCombo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District = '" + bDistrictCombo.Text + "'";
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

        private void tDivitionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division = '" + tDivitionCombo.Text + "'";
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

        private void bDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Divisions.Division_ID)  from Divisions WHERE Divisions.Division = '" + bDivisionCombo.Text + "'";
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

        private void bThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana= '" + bThanaCombo.Text + "'";
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode)  from PostOffice WHERE PostOffice.PostOfficeName= '" + tPostOfficeCombo.Text + "'";
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

        private void tANotApplicable_CheckedChanged(object sender, EventArgs e)
        {

            if (tANotApplicable.Checked)
            {

                if (tASameAsCACheckBox.Checked )
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

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void txtCPEmailAddress_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCPEmailAddress.Text))
            {
                string emailId2 = txtCPEmailAddress.Text.Trim();
                Regex mRegxExpression;

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(emailId2))
                {

                    MessageBox.Show("Please type a valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCPEmailAddress.Clear();
                   

                }
            }
        }

        private void cellNumberAPTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void tContactNoTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void bContactNoTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void designationAPTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contactPersonNameAPTextBox.Text))
            {
                MessageBox.Show("Please  enter Before Contact Person Name", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cellNumberAPTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contactPersonNameAPTextBox.Text))
            {
                MessageBox.Show("Please  enter Contact Person Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void branchNameTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bankNameTextBox.Text))
            {
                MessageBox.Show("Please  enter  Bank Name first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void accountNoTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bankNameTextBox.Text))
            {
                MessageBox.Show("Please  enter  Bank Name first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtCPEmailAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contactPersonNameAPTextBox.Text))
            {
                MessageBox.Show("Please  enter  Contact Person Name first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void ClearContactPersonDetails()
        {
            designationAPTextBox.Clear();
            cellNumberAPTextBox.Clear();
            txtCPEmailAddress.Clear();
        }
        private void contactPersonNameAPTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(contactPersonNameAPTextBox.Text))
            {
                ClearContactPersonDetails();
            }
        }

        private void bankNameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bankNameTextBox.Text))
            {
                branchNameTextBox.Clear();
                accountNoTextBox.Clear();
            }
        }
    }
}
