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
using ClientManagementSystem.LoginUI;
using ClientManagementSystem.Manager;
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class ClientRegistrationForm : Form
    {
        private int affectedRows1, currentClientId, affectedRows2, affectedRows3, affectedRows12;
        private SqlConnection con;
        ConnectionString cs=new ConnectionString();
        private SqlDataReader rdr;
        private  SqlCommand cmd;
        public string fullName, submittedBy, districtIdC, districtIdT, divisionIdC, divisionIdT, thanaIdC,thanaIdC2, thanaIdT,postofficeIdC,postOfficeIdT;
        public int clientTypeId, natureOfClientId, industryCategoryId, addressTypeId1, addressTypeId2,superviserId;
        public ClientRegistrationForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void SaveInquiryClient()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string insertQuery = "insert into InquieryClient(ClientName,ClientTypeId,NatureOfClientId,EmailAddress,IndustryCategoryId,EndUser,UserId,Dates,SuperviserId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(insertQuery);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@d1", clientNameTextBox.Text);
            cmd.Parameters.AddWithValue("@d2", clientTypeId);
            cmd.Parameters.AddWithValue("@d3", natureOfClientId);
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(emailAddressTextBox.Text) ? (object)DBNull.Value : emailAddressTextBox.Text));
            cmd.Parameters.AddWithValue("@d5", industryCategoryId);
            cmd.Parameters.AddWithValue("@d6", endUserTextBox.Text);            
            cmd.Parameters.AddWithValue("@d7", submittedBy);
            cmd.Parameters.AddWithValue("@d8", DateTime.UtcNow.ToLocalTime());
            cmd.Parameters.AddWithValue("@d9", superviserId);
            currentClientId = (int)cmd.ExecuteScalar();
            con.Close();
        }

        private void SaveCorporateAddress(int addressTypeId)
        {
            addressTypeId1 = addressTypeId;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string insertQ = "insert into Addresses(Division_ID,D_ID,T_ID,PostOfficeId,FlatNo,HouseNo,RoadNo,Block,Area,ContactNo,ADTypeId,IClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(insertQ);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdC) ? (object)DBNull.Value : divisionIdC));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdC) ? (object)DBNull.Value : districtIdC));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdC) ? (object)DBNull.Value : thanaIdC));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox1.Text) ? (object)DBNull.Value : cBlockTextBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));            
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
            cmd.Parameters.AddWithValue("@d11", addressTypeId1);
            cmd.Parameters.AddWithValue("@d12", currentClientId);
            affectedRows1 = (int)cmd.ExecuteScalar();
            con.Close();
        }

        public void SaveTraddingAddress(int addressTypeId)
        {
            addressTypeId2 = addressTypeId;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string Qry = "insert into Addresses(Division_ID,D_ID,T_ID,PostOfficeId,FlatNo,HouseNo,RoadNo,Block,Area,ContactNo,ADTypeId,IClientId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(Qry);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(divisionIdT) ? (object)DBNull.Value : divisionIdT));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(districtIdT) ? (object)DBNull.Value : districtIdT));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(thanaIdT) ? (object)DBNull.Value : thanaIdT));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object)DBNull.Value : tFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object)DBNull.Value : tHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object)DBNull.Value : tRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tBlockTextBox2.Text) ? (object)DBNull.Value : tBlockTextBox2.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAreaTextBox.Text) ? (object)DBNull.Value : tAreaTextBox.Text));          
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(tContactNoTextBox.Text) ? (object)DBNull.Value : tContactNoTextBox.Text));
            cmd.Parameters.AddWithValue("@d11", addressTypeId2);
            cmd.Parameters.AddWithValue("@d12", currentClientId);
            affectedRows2 = (int)cmd.ExecuteScalar();
            con.Close();              
        }

        private void SaveContactPersonDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qury = "insert into ContactPersonDetails(ContactPersonName,Designation,CellNumber,EmailId,IClientId) Values(@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(qury);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(txtContactPerson.Text) ? (object)DBNull.Value : txtContactPerson.Text));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(txtDesignation.Text) ? (object)DBNull.Value : txtDesignation.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(txtCellNumber.Text) ? (object)DBNull.Value : txtCellNumber.Text));
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(txtCPEmailAddress.Text) ? (object)DBNull.Value : txtCPEmailAddress.Text));
            cmd.Parameters.AddWithValue("@d5", currentClientId);
            affectedRows3 = (int)cmd.ExecuteScalar();
            con.Close();
        }

        private void CheckFactoryAddress()
        {
            
           
        }

        private void ClearContactPersonDetails()
        {
            txtDesignation.Clear();
            txtCellNumber.Clear();
            txtCPEmailAddress.Clear();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(clientNameTextBox.Text))
            {
                MessageBox.Show("Please Enter Valid Client name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
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
            if (cThanaCombo.Text == "")
            {
                MessageBox.Show("Please Enter Corporate Thana", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cThanaCombo.Focus();
                return;
            }
            if (cDistCombo.Text == "")
            {
                MessageBox.Show("Please Select Corporate District", "Input Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                cDistCombo.Focus();
                return;
            }

            if ((notApplicableCheckBox.Checked==false) && (sameAsCorporatAddCheckBox.Checked == false))
            {
                if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
                {
                    MessageBox.Show("Please select factory division", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                    return;
                }
                if (string.IsNullOrWhiteSpace(tDistrictCombo.Text))
                {
                    MessageBox.Show("Please Select factory district", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    return;
                }
                if (string.IsNullOrWhiteSpace(tThenaCombo.Text))
                {
                    MessageBox.Show("Please select factory Thana", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    return;
                }
                if (string.IsNullOrWhiteSpace(tPostCombo.Text))
                {
                    MessageBox.Show("Please Select factory Post Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    return;
                }
                if (string.IsNullOrWhiteSpace(tPostCodeTextBox.Text))
                {
                    MessageBox.Show("Please select factory Post Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                    return;
                }
            }
           
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select ClientName from InquieryClient where ClientName='" + clientNameTextBox.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("This Client Name Already Exists in Inquiry List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clientNameTextBox.Text = "";
                    clientNameTextBox.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
               


               //1.Corporate Address Applicable  & Tradding Address not Applicable
                if (notApplicableCheckBox.Checked)
                {
                    SaveInquiryClient();
                    SaveCorporateAddress(1);
                    if (!string.IsNullOrEmpty(txtContactPerson.Text))
                    {
                        SaveContactPersonDetails();
                    }
                   
                }
               //2.Corporate Address Applicable  & Tradding Address Same as  Corporate Address                                        
                if (sameAsCorporatAddCheckBox.Checked)
                {
                    SaveInquiryClient();
                    SaveCorporateAddress(1);
                    SaveCorporateAddress(2);
                    if (!string.IsNullOrEmpty(txtContactPerson.Text))
                    {
                        SaveContactPersonDetails();
                    }                                      
                }
                //3.Corporate Address Applicable  & Tradding Address  Applicable
                if (sameAsCorporatAddCheckBox.Checked == false && notApplicableCheckBox.Checked == false)
                {
                    SaveInquiryClient();
                    SaveCorporateAddress(1);
                    SaveTraddingAddress(2);
                    if (!string.IsNullOrEmpty(txtContactPerson.Text))
                    {
                        SaveContactPersonDetails();
                    }
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
        

        private void editButton_Click(object sender, EventArgs e)
        {
        
        }

        private void getDataButton_Click(object sender, EventArgs e)
        {
            
        }

        private void ResetTradingAddress()
        {
           
            tFlatNoTextBox.Clear();
            tHouseNoTextBox.Clear();
            tRoadNoTextBox.Clear();
            tAreaTextBox.Clear();
            tBlockTextBox2.Clear();
            tContactNoTextBox.Text = "";

            tDivisionCombo.SelectedIndex = -1;
            tDistrictCombo.SelectedIndex = -1;
            tThenaCombo.SelectedIndex = -1;
            tPostCombo.SelectedIndex = -1;
            tPostCodeTextBox.Clear();
        }

        private void Reset()
        {
            clientNameTextBox.Clear();
            cmbClientType.SelectedIndex=-1;
            cmbNatureOfClient.SelectedIndex = -1;
            emailAddressTextBox.Clear();
            cmbIndustryCategory.SelectedIndex=-1;
            cmbSuperviser.SelectedIndex = -1;
            txtCPEmailAddress.Clear();

            
            cFlatNoTextBox.Clear();
            cHouseNoTextBox.Clear();
            cRoadNoTextBox.Clear();
            cBlockTextBox1.Clear();
            cAreaTextBox.Clear();
            cContactNoTextBox.Clear();
            cDivisionCombo.SelectedIndex = -1;
            cDistCombo.SelectedIndex = -1;
            cThanaCombo.SelectedIndex = -1;
            cPostOfficeCombo.SelectedIndex = -1;
            cPostCodeTextBox.Clear();

            notApplicableCheckBox.CheckedChanged -= NotApplicableCheckBox_CheckedChanged;
            notApplicableCheckBox.Checked = false;
            notApplicableCheckBox.CheckedChanged += NotApplicableCheckBox_CheckedChanged;

            sameAsCorporatAddCheckBox.CheckedChanged -= sameAsCorporatAddCheckBox_CheckedChanged;
            sameAsCorporatAddCheckBox.Checked = false;
            sameAsCorporatAddCheckBox.CheckedChanged += sameAsCorporatAddCheckBox_CheckedChanged;
            if ((notApplicableCheckBox.Checked== false) && (sameAsCorporatAddCheckBox.Checked == false))
            {
                ResetTradingAddress(); 
            }   
      
            txtContactPerson.Clear();
            txtDesignation.Clear();
            txtCellNumber.Clear();
            endUserTextBox.Clear();            
            saveButton.Enabled = true;

        }

        private void newButton_Click(object sender, EventArgs e)
        {
            Reset();
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {  
            this.Hide();
          MainUIInquieryClient aMainUi=new MainUIInquieryClient();
            aMainUi.ShowDialog();

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

        private void txtClientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetClientTypeId();

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
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
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
            
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
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
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
            
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            // (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
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
                    cmbSuperviser.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClientRegistrationForm_Load(object sender, EventArgs e)
        {
            submittedBy = LoginForm.uId.ToString();
            FillCMBSuperviserName();

            FillClientType();
            FillNatureOfClient();
            FillIndustryCategory();

            FillCDivisionCombo();
            FillTDivisionCombo();
        }
        private void Report2()
        {
            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue(); 
            paramField.Name = "id2";
            paramDiscreteValue.Value = affectedRows1;
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

        private void cmbAddressType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbAddressType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        private void tDistrictCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District = '" + tDistrictCombo.Text + "'";
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


                tDistrictCombo.Text = tDistrictCombo.Text.Trim();
                tThenaCombo.Items.Clear();
                tThenaCombo.Text = "";
                tThenaCombo.Enabled = true;
                tThenaCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Thanas.Thana) from Thanas  Where Thanas.D_ID = '" + districtIdT + "' order by Thanas.D_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tThenaCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void emailAddressTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(emailAddressTextBox.Text))
            {
                string emailId = emailAddressTextBox.Text.Trim();
                Regex mRegxExpression;

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(emailId))
                {

                    MessageBox.Show("Please type your  valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    emailAddressTextBox.Clear();
                    emailAddressTextBox.Focus();

                }
            }
        }

        private void tctCPEmailAddress_Validating(object sender, CancelEventArgs e)
        {
            string emailId = txtCPEmailAddress.Text.Trim();
            Regex mRegxExpression;

            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            if (!mRegxExpression.IsMatch(emailId))
            {

                MessageBox.Show("User Id must be your email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCPEmailAddress.Clear();
                txtCPEmailAddress.Focus();

            }
        }

        private void cmbSuperviser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT UserId from Registration WHERE Name = '" + cmbSuperviser.Text + "'";
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

        private void closeButton_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MainUIInquieryClient frm=new MainUIInquieryClient();
            frm.Show();
        }

        private void sameAsCorporatAddCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sameAsCorporatAddCheckBox.Checked)
            {
               
                if (notApplicableCheckBox.Checked)
                {
                    notApplicableCheckBox.CheckedChanged -= NotApplicableCheckBox_CheckedChanged;
                    notApplicableCheckBox.Checked = false;
                    notApplicableCheckBox.CheckedChanged += NotApplicableCheckBox_CheckedChanged;
                    groupBox3.Enabled = false;
                    ResetTradingAddress();
                }
                else
                {

                    groupBox3.Enabled = false;
                    ResetTradingAddress();
                }

            }
            else
            {
                if (notApplicableCheckBox.Checked)
                {
                    groupBox3.Enabled = false;
                    ResetTradingAddress();
                }
                else
                {

                    groupBox3.Enabled = true;
                    ResetTradingAddress();
                }
            }
            
        }

        private void ifApplicableCheckBox_CheckedChanged(object sender, EventArgs e)
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
                tDistrictCombo.Items.Clear();
                tDistrictCombo.Text = "";
                tDistrictCombo.Enabled = true;
                tDistrictCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" + divisionIdT + "' order by Districts.Division_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tDistrictCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tThenaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(Thanas.T_ID)  from Thanas WHERE Thanas.Thana= '" + tThenaCombo.Text + "'";
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


                tThenaCombo.Text = tThenaCombo.Text.Trim();
                tPostCombo.Items.Clear();
                tPostCombo.Text = "";
                tPostCombo.Enabled = true;
                tPostCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(PostOffice.PostOfficeName) from PostOffice  Where PostOffice.T_ID = '" + thanaIdT + "' order by PostOffice.T_ID desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tPostCombo.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tPostCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode) from PostOffice WHERE PostOffice.PostOfficeName= '" + tPostCombo.Text + "'";
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

        private void NotApplicableCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tctCPEmailAddress_Validating_1(object sender, CancelEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCPEmailAddress.Text))
            {
                string emailId2 = txtCPEmailAddress.Text.Trim();
                Regex mRegxExpression;

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(emailId2))
                {

                    MessageBox.Show("Please type valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCPEmailAddress.Clear();
                    txtCPEmailAddress.Focus();

                }
            }
        }

        private void cellNumberTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
            
           
        }

        private void designationTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Please  enter Contact Person Name first", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void txtCellNumber_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Please  enter Contact Person Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void notApplicableCheckBox_CheckedChanged(object sender, EventArgs e)
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

        private void txtContactPerson_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void txtContactPerson_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContactPerson.Text))
            {
                ClearContactPersonDetails();
            }
        }

        private void txtCPEmailAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Please  enter Contact Person Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
