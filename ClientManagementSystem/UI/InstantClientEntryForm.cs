﻿using System;
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
using ClientManagementSystem.LoginUI;
using ClientManagementSystem.Manager;
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class InstantClientEntryForm : Form
    {
         SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        private delegate void ChangeFocusDelegate(Control ctl);
        public int affectedRowsI,affectedRows1,affectedRows2,affectedRows3, currentClientId, clientTypeId, natureOfClientId, industryCategoryId,addTypeId1,addTypeId2,addTypeId3;
        public string fullName3, submittedBy3, districtIdC, districtIdT, districtIdB, divisionIdC, divisionIdT, divisionIdB, thanaIdC, thanaIdT, thanaIdB, iClientId, postOfficeIdC, postOfficeIdB, postOfficeIdT;
        public int superviserId, bankEmailId, bankCPEmailId, cdistrict_id, tdistrict_id, bdistrict_id;
        public InstantClientEntryForm()
        {
            InitializeComponent();
        }

        public enum Mask
        {
            None, DateOnly, PhoneWithArea, IpAddress,
            SSN, Decimal, Digit
        };
        private Mask m_mask;
        public Mask Maked
        {
            get { return m_mask; }
            set
            {
                m_mask = value;
                this.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ForSalseClientMP af2=new ForSalseClientMP();
            af2.ShowDialog();
        }

        private void SaveInstantSalesClient()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string apquery = "insert into SalesClient(ClientName,ClientTypeId,NatureOfClientId,EmailBankId,IndustryCategoryId,EndUser,UserId,Dates,SuperviserId) values(@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(apquery, con);           
           // cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(iClientId) ? (object)DBNull.Value : iClientId));            
            cmd.Parameters.AddWithValue("@d2", clientNameInsTextBox.Text);
            cmd.Parameters.AddWithValue("@d3", clientTypeId);
            cmd.Parameters.AddWithValue("@d4", natureOfClientId);
            cmd.Parameters.AddWithValue("@d5", bankEmailId);          
            cmd.Parameters.AddWithValue("@d6", industryCategoryId);
            cmd.Parameters.AddWithValue("@d7", endUserInsTextBox.Text);
            cmd.Parameters.AddWithValue("@d8", submittedBy3);
            cmd.Parameters.AddWithValue("@d9", DateTime.UtcNow.ToLocalTime());
            cmd.Parameters.AddWithValue("@d10", superviserId);
            currentClientId = (int)cmd.ExecuteScalar();
            con.Close();

        }

        private void TraddingAddressSameAsCorporateAddress(string tableName)
        {
            string tableName1 = tableName;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string insertQ = "insert into " + tableName1 + "(PostOfficeId,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TContactNo,SClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(insertQ, con);            
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdC) ? (object)DBNull.Value : postOfficeIdC));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox.Text) ? (object)DBNull.Value : cBlockTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
            //cmd.Parameters.AddWithValue("@d11", addTypeId1);
            cmd.Parameters.AddWithValue("@d11", currentClientId);
            affectedRows1 = (int)cmd.ExecuteScalar();
            con.Close();

        }

        private void BillingAddressSameAsCorporateAddress(string tableName)
        {
            string tableName3 = tableName;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string insertQ = "insert into " + tableName3 + "(PostOfficeId,BFlatNo,BHouseNo,BRoadNo,BBlock,BArea,BContactNo,SClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(insertQ, con);           
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdC) ? (object)DBNull.Value : postOfficeIdC));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox.Text) ? (object)DBNull.Value : cBlockTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
           // cmd.Parameters.AddWithValue("@d11", addTypeId1);
            cmd.Parameters.AddWithValue("@d11", currentClientId);
            affectedRows1 = (int)cmd.ExecuteScalar();
            con.Close();
        }
        private void BillingAddressSameAsTraddingAddress(string tableName)
        {
            string tablename32 = tableName;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string Qry = "insert into " + tablename32 + "(PostOfficeId,BFlatNo,BHouseNo,BRoadNo,BBlock,BArea,BContactNo,SClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(Qry,con);                     
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object)DBNull.Value : tFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object)DBNull.Value : tHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object)DBNull.Value : tRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tBlockTextBox.Text) ? (object)DBNull.Value : tBlockTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAreaTextBox.Text) ? (object)DBNull.Value : tAreaTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(tContactNoTextBox.Text) ? (object)DBNull.Value : tContactNoTextBox.Text));
            cmd.Parameters.AddWithValue("@d11", currentClientId);
            affectedRows2 = (int)cmd.ExecuteScalar();
            con.Close(); 
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
                    string insertQ = "insert into " + sTableName + "(PostOfficeId,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CContactNo,SClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" +"SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(insertQ, con);                   
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdC) ? (object) DBNull.Value : postOfficeIdC));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object) DBNull.Value : cFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6",string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object) DBNull.Value : cHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object) DBNull.Value : cRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox.Text) ? (object) DBNull.Value : cBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9",string.IsNullOrEmpty(cAreaTextBox.Text) ? (object) DBNull.Value : cAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object) DBNull.Value : cContactNoTextBox.Text));
                   // cmd.Parameters.AddWithValue("@d11", addTypeId1);
                    cmd.Parameters.AddWithValue("@d11", currentClientId);
                    affectedRows1 = (int) cmd.ExecuteScalar();
                    con.Close();
                }
                else if (sTableName == "TraddingAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string Qry = "insert into " + sTableName + "(PostOfficeId,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TContactNo,SClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(Qry,con);                                        
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object)DBNull.Value : tFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object)DBNull.Value : tHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object)DBNull.Value : tRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tBlockTextBox.Text) ? (object)DBNull.Value : tBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAreaTextBox.Text) ? (object)DBNull.Value : tAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(tContactNoTextBox.Text) ? (object)DBNull.Value : tContactNoTextBox.Text));
                    cmd.Parameters.AddWithValue("@d11", currentClientId);
                    affectedRows2 = (int)cmd.ExecuteScalar();
                    con.Close(); 
                }
                else if (sTableName == "BillingAddresses")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string Qry3 = "insert into " + sTableName + "(PostOfficeId,BFlatNo,BHouseNo,BRoadNo,BBlock,BArea,BContactNo,SClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                    cmd = new SqlCommand(Qry3,con);                            
                    cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdB) ? (object)DBNull.Value : postOfficeIdB));
                    cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(bFlatNoTextBox.Text) ? (object)DBNull.Value : bFlatNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(bHouseNoTextBox.Text) ? (object)DBNull.Value : bHouseNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(bRoadNoTextBox.Text) ? (object)DBNull.Value : bRoadNoTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(bBlockTextBox.Text) ? (object)DBNull.Value : bBlockTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(bAreaTextBox.Text) ? (object)DBNull.Value : bAreaTextBox.Text));
                    cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(bContactNoTextBox.Text) ? (object)DBNull.Value : bContactNoTextBox.Text));
                   // cmd.Parameters.AddWithValue("@d11", addTypeId3);
                    cmd.Parameters.AddWithValue("@d11", currentClientId);
                    affectedRows2 = (int)cmd.ExecuteScalar();
                    con.Close();
                }
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
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(contactPersonNameInsTextBox.Text) ? (object)DBNull.Value : contactPersonNameInsTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(designationInsTextBox.Text) ? (object)DBNull.Value : designationInsTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(cellNumberInsTextBox.Text) ? (object)DBNull.Value : cellNumberInsTextBox.Text));
            //cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(emailAddressInsTextBox.Text) ? (object)DBNull.Value : emailAddressInsTextBox.Text));
            cmd.Parameters.AddWithValue("@d4", bankCPEmailId);
            cmd.Parameters.AddWithValue("@d5", currentClientId);
            affectedRows3 = (int)cmd.ExecuteScalar();
            con.Close();
        }

        private void SaveBankDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qury5 = "insert into BankDetails(SClientId,BankName,BranchName,AccountNo) Values(@d1,@d2,@d3,@d4)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(qury5);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@d1", currentClientId);
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(bankNameInsTextBox.Text) ? (object)DBNull.Value : bankNameInsTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(branchNameInsTextBox.Text) ? (object)DBNull.Value : branchNameInsTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(accountNoInsTextBox.Text) ? (object)DBNull.Value : accountNoInsTextBox.Text));
            affectedRows3 = (int)cmd.ExecuteScalar();
            con.Close();   
        }

        
        private void instantApprovalButton_Click(object sender, EventArgs e)
        {
            if (clientNameInsTextBox.Text == "")
            {
                MessageBox.Show("Please Enter Valid Client Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameInsTextBox.Focus();
                return;
            }
            if (cmbClientType.Text == "")
            {
                MessageBox.Show("Please Select Client Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClientType.Focus();
                return;
            }
            if (cmbNatureOfClient.Text == "")
            {
                MessageBox.Show("Please select Nature of Client", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbNatureOfClient.Focus();
                return;
            }
            if (cmbIndustryCategory.Text == "")
            {
                MessageBox.Show("Please select Industry Category", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbIndustryCategory.Focus();
               
                return;
            }
            if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            {
                MessageBox.Show("Please select Corporate Address division", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cDistrictCombo.Text))
            {
                MessageBox.Show("Please Select Corporate Address district", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cThanaCombo.Text))
            {
                MessageBox.Show("Please select Corporate Address Thana", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cPostOfficeCombo.Text))
            {
                MessageBox.Show("Please Select Corporate Address Post Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cPostCodeTextBox.Text))
            {
                MessageBox.Show("Please enter  Corporate Address Post Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((notApplicableTACheckBox.Checked == false) && (TASameAsCA.Checked == false))
            {
                if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
                {
                    MessageBox.Show("Please select Tradding Address division", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tDistCombo.Text))
                {
                    MessageBox.Show("Please Select Tradding Address district", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tThanaCombo.Text))
                {
                    MessageBox.Show("Please select Tradding Address Thana", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tPostOfficeCombo.Text))
                {
                    MessageBox.Show("Please Select Tradding Address Post Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tPostCodeTextBox.Text))
                {
                    MessageBox.Show("Please enter Tradding Address Post Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if ((notApplicableBA.Checked == false) && (bASameASCA.Checked == false) && (bASameAsTA.Checked == false))
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
                if (string.IsNullOrWhiteSpace(bPostOfficeCombo.Text))
                {
                    MessageBox.Show("Please Select billing Address Post Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(bPostCodeTextBox.Text))
                {
                    MessageBox.Show("Please enter billing Address Post Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }         
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "select ClientName from SalesClient where ClientName='" + clientNameInsTextBox.Text + "'";

                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("This Client Name Already Exists in Sales List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clientNameInsTextBox.Text = "";
                    clientNameInsTextBox.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }

               
                //1.Tradding Address Not Applicable &&  Billing Address Not Applicable
                if (notApplicableTACheckBox.Checked && notApplicableBA.Checked)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    
                }

                //2.Tradding Address Not Applicable &&  Billing Address  same as Corporat Address
                else if (notApplicableTACheckBox.Checked && bASameASCA.Checked)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    BillingAddressSameAsCorporateAddress("BillingAddresses");//diff Method
                    
                }
                //3.Tradding Address Not Applicable &&  Billing Address  Applicable
                else if (notApplicableTACheckBox.Checked && notApplicableBA.Checked == false && bASameASCA.Checked == false && bASameAsTA.Checked == false)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    SaveCorporateORTraddingORBillingAddress("BillingAddresses");
                    
                }
                //4.Tradding Address same as Corporat Address &&  Billing Address Not Applicable
                else if (TASameAsCA.Checked && notApplicableBA.Checked)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    TraddingAddressSameAsCorporateAddress("TraddingAddresses"); //diff method  
                    
                }
                //5.Tradding Address same as Corporat Address &&  Billing Address same as Corporat Address

                else if (TASameAsCA.Checked && bASameASCA.Checked)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    TraddingAddressSameAsCorporateAddress("TraddingAddresses"); //diff method  
                    BillingAddressSameAsCorporateAddress("BillingAddresses"); //diff method  
                    
                }
                //6.Tradding Address same as Corporat Address &&  Billing Address Applicable

                else if (TASameAsCA.Checked && notApplicableBA.Checked == false && bASameASCA.Checked == false && bASameASCA.Checked == false)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    TraddingAddressSameAsCorporateAddress("TraddingAddresses"); //diff method  
                    SaveCorporateORTraddingORBillingAddress("BillingAddresses");
                   
                }
                //7.Tradding Address Aplicable  &&  Biling Address Not Applicable

                else if (notApplicableTACheckBox.Checked == false && TASameAsCA.Checked == false && notApplicableBA.Checked)
                {
                   
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    SaveCorporateORTraddingORBillingAddress("TraddingAddresses");
                   
                }
                //8.Tradding Address Aplicable  &&  Biling Address Same As Corporat Address

                else if (notApplicableTACheckBox.Checked == false && TASameAsCA.Checked == false && bASameASCA.Checked)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    SaveCorporateORTraddingORBillingAddress("TraddingAddresses");
                    BillingAddressSameAsCorporateAddress("BillingAddresses"); //diff method  
                    
                }
                //9.Tradding Address Aplicable  &&  Biling Address Same As Tradding Address

                else if (notApplicableTACheckBox.Checked == false && TASameAsCA.Checked == false && bASameAsTA.Checked)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    SaveCorporateORTraddingORBillingAddress("TraddingAddresses");
                    BillingAddressSameAsTraddingAddress("BillingAddresses"); //diff method
                    
                }
                //10.Tradding Address Aplicable  &&  Biling Address Applicable

                else if (notApplicableTACheckBox.Checked == false && TASameAsCA.Checked == false && notApplicableBA.Checked == false && bASameASCA.Checked == false && bASameAsTA.Checked == false)
                {
                    SaveInstantSalesClient();
                    SaveCorporateORTraddingORBillingAddress("CorporateAddresses");
                    SaveCorporateORTraddingORBillingAddress("TraddingAddresses");
                    SaveCorporateORTraddingORBillingAddress("BillingAddresses");
                    
                }
                if (!string.IsNullOrEmpty(contactPersonNameInsTextBox.Text))
                {
                    SaveContactPersonDetails();
                }
                if (!string.IsNullOrEmpty(bankNameInsTextBox.Text))
                {
                    SaveBankDetails();
                }

                MessageBox.Show("Registration Completed Successfully,Current Id is:" + currentClientId, "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();                             
            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Please Enter Input in Correct Format", formatException.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                     
           
            
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
            bPostCodeTextBox.Text = "";
        }
        private void ResetTraddingAddress()
        {
            tFlatNoTextBox.Clear();
            tHouseNoTextBox.Clear();
            tRoadNoTextBox.Clear();
            tBlockTextBox.Clear();
            tAreaTextBox.Clear();
            tContactNoTextBox.Clear();

            tDivisionCombo.SelectedIndex = -1;
            tDistCombo.SelectedIndex = -1;
            tThanaCombo.SelectedIndex = -1;
            tPostOfficeCombo.SelectedIndex = -1;
            tPostCodeTextBox.Clear();
        }

        private void Reset()
        {
           
            cmbSuperviserName.SelectedIndex = -1;
            clientNameInsTextBox.Clear();
            cmbClientType.SelectedIndex = -1;
            cmbNatureOfClient.SelectedIndex = -1;
            cmbEmailAddress.SelectedIndex = -1;
            cmbIndustryCategory.SelectedIndex = -1;
            endUserInsTextBox.Clear();

            notApplicableTACheckBox.CheckedChanged -= notApplicableTACheckBox_CheckedChanged;
            notApplicableTACheckBox.Checked = false;
            notApplicableTACheckBox.CheckedChanged += notApplicableTACheckBox_CheckedChanged;


            TASameAsCA.CheckedChanged -= TASameAsCA_CheckedChanged;
            TASameAsCA.Checked = false;
            TASameAsCA.CheckedChanged += TASameAsCA_CheckedChanged;

            notApplicableBA.CheckedChanged -= notApplicableBA_CheckedChanged;
            notApplicableBA.Checked = false;
            notApplicableBA.CheckedChanged += notApplicableBA_CheckedChanged;

            bASameASCA.CheckedChanged -= bASameASCA_CheckedChanged;
            bASameASCA.Checked = false;
            bASameASCA.CheckedChanged += bASameASCA_CheckedChanged;

            bASameAsTA.CheckedChanged -= bANotApplicableTA_CheckedChanged;
            bASameAsTA.Checked = false;
            bASameAsTA.CheckedChanged += bANotApplicableTA_CheckedChanged;

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

            ResetTraddingAddress();   
         
            contactPersonNameInsTextBox.Clear();
            designationInsTextBox.Clear();
            cellNumberInsTextBox.Clear();
            cmbCPEmailAddress.SelectedIndex = -1;
 
            ResetBillingAddress();
                       
            bankNameInsTextBox.Text = "";
            branchNameInsTextBox.Text = "";
            accountNoInsTextBox.Text = "";

        }

        private void tContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
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

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void bContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&(e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void bPostCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cContactNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9') return;
            if (e.KeyChar == '+' || e.KeyChar == '-') return;
            if (e.KeyChar == 8) return;
            e.Handled = true;

        }

        private void cPostCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cellNumberInsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void accountNoInsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        public void FillCMBSuperviserName()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Name) from Registration where Statuss!='InActive' order by UserId desc";
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
        private void changeFocus(Control ctl)
        {
            ctl.Focus();
        }
        
        private void InstantClientEntryForm_Load(object sender, EventArgs e)
        {
             submittedBy3=LoginForm.uId.ToString();
            FillCMBSuperviserName();
            EmailAddress();       
            EmailCPAddress();

            FillClientType();
            FillNatureOfClient();
            FillIndustryCategory();

            FillCDivisionCombo();
            FillTDivisionCombo();
            FillBDivisionCombo();
            
            FillBDistrictCombo();

            cDistrictCombo.Enabled = false;
            cThanaCombo.Enabled = false;
            cPostOfficeCombo.Enabled = false;

            tDistCombo.Enabled = false;
            tThanaCombo.Enabled = false;
            tPostOfficeCombo.Enabled = false;

            bDistrictCombo.Enabled = false;
            bThanaCombo.Enabled = false;
            bPostOfficeCombo.Enabled = false;


        }
        private void Report2()
        {

            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = "id";
            paramDiscreteValue.Value = affectedRowsI;
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
            InstantSalesClientInputReport cr = new InstantSalesClientInputReport();
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

        private void emailAddressInsTextBox_Validating(object sender, CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(emailAddressInsTextBox.Text))
            //{
            //    string emailId = emailAddressInsTextBox.Text.Trim();
            //    Regex mRegxExpression;

            //    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            //    if (!mRegxExpression.IsMatch(emailId))
            //    {

            //        MessageBox.Show("Please type a valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        emailAddressInsTextBox.Clear();
            //        emailAddressInsTextBox.Focus();

            //    }
            //}
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
                cThanaCombo.ResetText();
                cPostOfficeCombo.Items.Clear();
                cPostOfficeCombo.ResetText();
                cPostOfficeCombo.SelectedIndex = -1;
                cPostOfficeCombo.Enabled = false;
                cPostCodeTextBox.Clear();
                cThanaCombo.Enabled = true;
                cThanaCombo.Focus();


                //cDistrictCombo.Text = cDistrictCombo.Text.Trim();
                //cThanaCombo.Items.Clear();
                //cThanaCombo.Text = "";
                //cPostOfficeCombo.SelectedIndex = -1;
                //cPostCodeTextBox.Clear();
                //cThanaCombo.Enabled = true;
                //cThanaCombo.Focus();

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

        private void tDistCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void bDistrictCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void GetClientTypeId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT ClientTypeId from ClientTypes WHERE ClientType = '" + cmbClientType.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    clientTypeId = (rdr.GetInt32(0));
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmbClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetClientTypeId();
        }

        private void cmbNatureOfClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT NatureOfClientId from NatureOfClients WHERE ClientNature = '" + cmbNatureOfClient.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    natureOfClientId = (rdr.GetInt32(0));
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
                cmd.CommandText = "SELECT IndustryCategoryId from IndustryCategorys WHERE IndustryCategory = '" + cmbIndustryCategory.Text + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    industryCategoryId = (rdr.GetInt32(0));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cContactNoTextBox_TextChanged(object sender, EventArgs e)
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

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void tDistCombo_SelectedIndexChanged_1(object sender, EventArgs e)
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
                tThanaCombo.ResetText();
                tPostOfficeCombo.Items.Clear();
                tPostOfficeCombo.ResetText();
                tPostOfficeCombo.SelectedIndex = -1;
                tPostOfficeCombo.Enabled = false;
                tPostCodeTextBox.Clear();
                tThanaCombo.Enabled = true;
                tThanaCombo.Focus();


                //tDistCombo.Text = tDistCombo.Text.Trim();
                //tThanaCombo.Items.Clear();
                //tThanaCombo.Text = "";
                //tThanaCombo.Enabled = true;
                //tThanaCombo.Focus();

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
                bThanaCombo.ResetText();
                bPostOfficeCombo.Items.Clear();
                bPostOfficeCombo.ResetText();
                bPostOfficeCombo.SelectedIndex = -1;
                bPostOfficeCombo.Enabled = false;
                bPostCodeTextBox.Clear();
                bThanaCombo.Enabled = true;
                bThanaCombo.Focus();


                //bDistrictCombo.Text = bDistrictCombo.Text.Trim();
                //bThanaCombo.Items.Clear();
                //bThanaCombo.Text = "";
                //bThanaCombo.Enabled = true;
                //bThanaCombo.Focus();

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
            cDistrictCombo.Items.Clear();
            cDistrictCombo.ResetText();
            cThanaCombo.Items.Clear();
            cThanaCombo.ResetText();
            cPostOfficeCombo.Items.Clear();
            cPostOfficeCombo.ResetText();


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
                cDistrictCombo.ResetText();
                cThanaCombo.Items.Clear();
                cThanaCombo.ResetText();
                cThanaCombo.SelectedIndex = -1;
                cPostOfficeCombo.Items.Clear();
                cPostOfficeCombo.ResetText();
                cPostOfficeCombo.SelectedIndex = -1;
                cPostCodeTextBox.Clear();
                cDistrictCombo.Enabled = true;
                cDistrictCombo.Focus();

                //cDivisionCombo.Text = cDivisionCombo.Text.Trim();
                //cDistrictCombo.Items.Clear();
                //cDistrictCombo.Text = "";
                //cThanaCombo.SelectedIndex = -1;
                //cPostOfficeCombo.SelectedIndex = -1;
                //cPostCodeTextBox.Clear();
                //cDistrictCombo.Enabled = true;
                //cDistrictCombo.Focus();

               

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
            cThanaCombo.Enabled = false;
            cPostOfficeCombo.Enabled = false;
        }

        private void cThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "select D_ID from Districts WHERE District= '" + cDistrictCombo.Text + "'";

            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                cdistrict_id = rdr.GetInt32(0);
            }
            if ((rdr != null))
            {
                rdr.Close();
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana=@find AND Thanas.D_ID='" + cdistrict_id + "'";
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
                cPostOfficeCombo.ResetText();
                // cPostOfficeCombo.Text = "";
                cPostCodeTextBox.Clear();
                cPostOfficeCombo.Enabled = true;
                cPostOfficeCombo.Focus();

                //cThanaCombo.Text = cThanaCombo.Text.Trim();
                //cPostOfficeCombo.Items.Clear();
                //cPostOfficeCombo.Text = "";
                //cPostOfficeCombo.Enabled = true;
                //cPostOfficeCombo.Focus();

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

        private void tDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tDistCombo.Items.Clear();
            tDistCombo.ResetText();
            tThanaCombo.Items.Clear();
            tThanaCombo.ResetText();
            tPostOfficeCombo.Items.Clear();
            tPostOfficeCombo.ResetText();



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
                tDistCombo.ResetText();
                tThanaCombo.Items.Clear();
                tThanaCombo.ResetText();
                tThanaCombo.SelectedIndex = -1;
                tPostOfficeCombo.Items.Clear();
                tPostOfficeCombo.ResetText();
                tPostOfficeCombo.SelectedIndex = -1;
                tPostCodeTextBox.Clear();
                tDistCombo.Enabled = true;
                tDistCombo.Focus();

                //tDivisionCombo.Text = tDivisionCombo.Text.Trim();
                //tDistCombo.Items.Clear();
                //tDistCombo.Text = "";
                //tDistCombo.Enabled = true;
                //tDistCombo.Focus();

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

            tThanaCombo.Enabled = false;
            tPostOfficeCombo.Enabled = false;
        }

        private void tThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "select D_ID from Districts WHERE District= '" + tDistCombo.Text + "'";

            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                tdistrict_id = rdr.GetInt32(0);
            }
            if ((rdr != null))
            {
                rdr.Close();
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana=@find AND Thanas.D_ID='" + tdistrict_id + "'";
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
                tPostOfficeCombo.ResetText();
                // cPostOfficeCombo.Text = "";
                tPostCodeTextBox.Clear();
                tPostOfficeCombo.Enabled = true;
                tPostOfficeCombo.Focus();



                //tThanaCombo.Text = tThanaCombo.Text.Trim();
                //tPostOfficeCombo.Items.Clear();
                //tPostOfficeCombo.Text = "";
                //tPostOfficeCombo.Enabled = true;
                //tPostOfficeCombo.Focus();

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
            bDistrictCombo.Items.Clear();
            bDistrictCombo.ResetText();
            bThanaCombo.Items.Clear();
            bThanaCombo.ResetText();
            bPostOfficeCombo.Items.Clear();
            bPostOfficeCombo.ResetText();


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
                bDistrictCombo.ResetText();
                bThanaCombo.Items.Clear();
                bThanaCombo.ResetText();
                bThanaCombo.SelectedIndex = -1;
                bPostOfficeCombo.Items.Clear();
                bPostOfficeCombo.ResetText();
                bPostOfficeCombo.SelectedIndex = -1;
                bPostCodeTextBox.Clear();
                bDistrictCombo.Enabled = true;
                bDistrictCombo.Focus();

                //bDivisionCombo.Text = bDivisionCombo.Text.Trim();
                //bDistrictCombo.Items.Clear();
                //bDistrictCombo.Text = "";
                //bDistrictCombo.Enabled = true;
                //bDistrictCombo.Focus();

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

            bThanaCombo.Enabled = false;
            bPostOfficeCombo.Enabled = false;

        }

        private void bThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "select D_ID from Districts WHERE District= '" + bDistrictCombo.Text + "'";

            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                bdistrict_id = rdr.GetInt32(0);
            }
            if ((rdr != null))
            {
                rdr.Close();
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana=@find AND Thanas.D_ID='"+bdistrict_id+"'";
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
                bPostOfficeCombo.ResetText();
                // cPostOfficeCombo.Text = "";
                bPostCodeTextBox.Clear();
                bPostOfficeCombo.Enabled = true;
                bPostOfficeCombo.Focus(); 

                //bThanaCombo.Text = bThanaCombo.Text.Trim();
                //bPostOfficeCombo.Items.Clear();
                //bPostOfficeCombo.Text = "";
                //bPostOfficeCombo.Enabled = true;
                //bPostOfficeCombo.Focus();

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

        private void ifApplicableCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void notApplicableTACheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (notApplicableTACheckBox.Checked)
            {

                if (TASameAsCA.Checked)
                {
                    TASameAsCA.CheckedChanged -= TASameAsCA_CheckedChanged;
                    TASameAsCA.Checked = false;
                    TASameAsCA.CheckedChanged += TASameAsCA_CheckedChanged;
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
                if (TASameAsCA.Checked)
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

        private void TASameAsCA_CheckedChanged(object sender, EventArgs e)
        {
            if (TASameAsCA.Checked)
            {
                bASameAsTA.Visible = false;

                if (notApplicableTACheckBox.Checked)
                {
                    notApplicableTACheckBox.CheckedChanged -= notApplicableTACheckBox_CheckedChanged;
                    notApplicableTACheckBox.Checked = false;
                    notApplicableTACheckBox.CheckedChanged += notApplicableTACheckBox_CheckedChanged;
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
                bASameAsTA.Visible = true;
                if (notApplicableTACheckBox.Checked)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void notApplicableBA_CheckedChanged(object sender, EventArgs e)
        {
            if (notApplicableBA.Checked)
            {

                if (bASameASCA.Checked || bASameAsTA.Checked)
                {
                    bASameASCA.CheckedChanged -= bASameASCA_CheckedChanged;
                    bASameASCA.Checked = false;
                    bASameASCA.CheckedChanged += bASameASCA_CheckedChanged;

                    bASameAsTA.CheckedChanged -= bANotApplicableTA_CheckedChanged;
                    bASameAsTA.Checked = false;
                    bASameAsTA.CheckedChanged += bANotApplicableTA_CheckedChanged;

                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }

            }
            else
            {
                if (bASameASCA.Checked || bASameAsTA.Checked)
                {
                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox9.Enabled = true;
                    ResetBillingAddress();
                }
            }           
        }

        private void bASameASCA_CheckedChanged(object sender, EventArgs e)
        {
            if (bASameASCA.Checked)
            {

                if (notApplicableBA.Checked || bASameAsTA.Checked)
                {
                    notApplicableBA.CheckedChanged -= notApplicableBA_CheckedChanged;
                    notApplicableBA.Checked = false;
                    notApplicableBA.CheckedChanged += notApplicableBA_CheckedChanged;

                    bASameAsTA.CheckedChanged -= bANotApplicableTA_CheckedChanged;
                    bASameAsTA.Checked = false;
                    bASameAsTA.CheckedChanged += bANotApplicableTA_CheckedChanged;

                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }

            }
            else
            {
                if (notApplicableBA.Checked || bASameAsTA.Checked)
                {
                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox9.Enabled = true;
                    ResetBillingAddress();
                }
            } 
        }

        private void bANotApplicableTA_CheckedChanged(object sender, EventArgs e)
        {
            if (bASameAsTA.Checked)
            {

                if (notApplicableBA.Checked || bASameASCA.Checked)
                {
                    notApplicableBA.CheckedChanged -= notApplicableBA_CheckedChanged;
                    notApplicableBA.Checked = false;
                    notApplicableBA.CheckedChanged += notApplicableBA_CheckedChanged;

                    bASameASCA.CheckedChanged -= bASameASCA_CheckedChanged;
                    bASameASCA.Checked = false;
                    bASameASCA.CheckedChanged += bASameASCA_CheckedChanged;

                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }

            }
            else
            {
                if (notApplicableBA.Checked || bASameASCA.Checked)
                {
                    groupBox9.Enabled = false;
                    ResetBillingAddress();
                }
                else
                {

                    groupBox9.Enabled = true;
                }
            } 
        }

        private void tContactNoTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void bContactNoTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tPostCodeTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cellNumberInsTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCPEmailAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtCPEmailAddress_Validating(object sender, CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtCPEmailAddress.Text))
            //{
            //    string emailId2 = txtCPEmailAddress.Text.Trim();
            //    Regex mRegxExpression;

            //    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            //    if (!mRegxExpression.IsMatch(emailId2))
            //    {

            //        MessageBox.Show("Please type a valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        txtCPEmailAddress.Clear();
                    

            //    }
            //}
           
        }

        private void designationInsTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contactPersonNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter  Contact Person Name first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contactPersonNameInsTextBox.Focus();
            }
            
        }

        private void cellNumberInsTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contactPersonNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter  Contact Person Name first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contactPersonNameInsTextBox.Focus();
            }
            
        }

        private void txtCPEmailAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contactPersonNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter  Contact Person Name first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contactPersonNameInsTextBox.Focus();
            }
        }

        private void branchNameInsTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bankNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter  Bank Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bankNameInsTextBox.Focus();
            }
        }

        private void accountNoInsTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bankNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter  Bank Name  first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bankNameInsTextBox.Focus();
            }
        }

        private void contactPersonNameInsTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(contactPersonNameInsTextBox.Text))
            {
                cmbCPEmailAddress.SelectedIndex = -1;
                 designationInsTextBox.Clear();
                 cellNumberInsTextBox.Clear();
            }
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
                string emailName = Microsoft.VisualBasic.Interaction.InputBox("Please Input Mode Of Conduct  Here", "Input Here", "", -1, -1);
                if (string.IsNullOrWhiteSpace(emailName))
                {
                    cmbEmailAddress.SelectedIndex = -1;
                }
                else
                {

                    if (!string.IsNullOrWhiteSpace(emailName))
                    {
                        string emailId = emailName.Trim();
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
                    string ct2 = "select Email from EmailBank where Email='" + emailName + "'";
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
                            cmd.Parameters.AddWithValue("@d1", emailName);
                            cmd.Parameters.AddWithValue("@d2", submittedBy3);
                            cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                            cmd.ExecuteNonQuery();
                            con.Close();

                            cmbEmailAddress.Items.Clear();
                            EmailAddress();
                            cmbEmailAddress.SelectedText = emailName;
                            EmailCPAddress();

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
                string emailCPWindow = Microsoft.VisualBasic.Interaction.InputBox("Please Input Mode Of Conduct  Here", "Input Here", "", -1, -1);
                if (string.IsNullOrWhiteSpace(emailCPWindow))
                {
                    cmbCPEmailAddress.SelectedIndex = -1;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(emailCPWindow))
                    {
                        string emailId = emailCPWindow.Trim();
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
                    string ct2 = "select Email from EmailBank where Email='" + emailCPWindow + "'";
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
                            cmd.Parameters.AddWithValue("@d1", emailCPWindow);
                            cmd.Parameters.AddWithValue("@d2", submittedBy3);
                            cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                            cmd.ExecuteNonQuery();

                            con.Close();
                            cmbCPEmailAddress.Items.Clear();
                            EmailCPAddress();
                            cmbCPEmailAddress.SelectedText = emailCPWindow;
                            

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
            //if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(cDistrictCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cDistrictCombo.Focus();
            //}
            //else if (string.IsNullOrWhiteSpace(cThanaCombo.Text))
            //{
            //    MessageBox.Show("Please  select thana name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cThanaCombo.Focus();
            //}
        }

        private void cThanaCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(cDistrictCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cDistrictCombo.Focus();
            //}
        }

        private void cDistrictCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cDivisionCombo.Focus();
            //}
        }

        private void tPostOfficeCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(tDistCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDistCombo.Focus();
            //}
            //else if (string.IsNullOrWhiteSpace(tThanaCombo.Text))
            //{
            //    MessageBox.Show("Please  select thana name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tThanaCombo.Focus();
            //}
        }

        private void tThanaCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(tDistCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDistCombo.Focus();
            //}
        }

        private void tDistCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDivisionCombo.Focus();
            //}
        }

        private void bPostOfficeCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    bDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(bDistrictCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    bDistrictCombo.Focus();
            //}
            //else if (string.IsNullOrWhiteSpace(bThanaCombo.Text))
            //{
            //    MessageBox.Show("Please  select thana name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    bThanaCombo.Focus();
            //}
        }

        private void bThanaCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    bDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(bDistrictCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    bDistrictCombo.Focus();
            //}
        }

        private void bDistrictCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(bDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    bDivisionCombo.Focus();
            //}
        }

        private void cmbCPEmailAddress_Enter(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(contactPersonNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter Contact Person Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contactPersonNameInsTextBox.Focus();
            }
        }

        private void cmbClientType_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviserName.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviserName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(clientNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameInsTextBox.Focus();
            }
        }

        private void cmbNatureOfClient_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviserName.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviserName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(clientNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameInsTextBox.Focus();
            }
        }

        private void cmbEmailAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviserName.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviserName.Focus();
            }
            else  if (string.IsNullOrWhiteSpace(clientNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameInsTextBox.Focus();
            }
        }

        private void cmbIndustryCategory_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviserName.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviserName.Focus();
            }
            else  if (string.IsNullOrWhiteSpace(clientNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameInsTextBox.Focus();
            }
        }

        private void endUserInsTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviserName.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviserName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(clientNameInsTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameInsTextBox.Focus();
            }
        }

        private void clientNameInsTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviserName.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviserName.Focus();
            }
        }
        }
    
}
