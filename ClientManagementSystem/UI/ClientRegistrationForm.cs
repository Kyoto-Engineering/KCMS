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
        private delegate void ChangeFocusDelegate(Control ctl);
        private int affectedRows1, currentClientId, affectedRows2, affectedRows3, affectedRows12;
        private SqlConnection con;
        ConnectionString cs=new ConnectionString();
        private SqlDataReader rdr;
        private  SqlCommand cmd;
        public string fullName, submittedBy, districtIdC, districtIdT, divisionIdC, divisionIdT, thanaIdC,thanaIdC2, thanaIdT,postofficeIdC,postOfficeIdT,userType1;
        public int clientTypeId, natureOfClientId, industryCategoryId, addressTypeId1, addressTypeId2, superviserId, bankEmailId, bankCPEmailId;
        public int cdistrict_id, tdistrict_id;
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
            string insertQuery = "insert into InquieryClient(ClientName,ClientTypeId,NatureOfClientId,EmailBankId,IndustryCategoryId,EndUser,UserId,Dates,SuperviserId) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(insertQuery);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@d1", clientNameTextBox.Text);
            cmd.Parameters.AddWithValue("@d2", clientTypeId);
            cmd.Parameters.AddWithValue("@d3", natureOfClientId);
           // cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(bankEmailId) ? (object)DBNull.Value : cmbEmailAddress.Text));
            cmd.Parameters.AddWithValue("@d4", bankEmailId);
            cmd.Parameters.AddWithValue("@d5", industryCategoryId);
            cmd.Parameters.AddWithValue("@d6", endUserTextBox.Text);            
            cmd.Parameters.AddWithValue("@d7", submittedBy);
            cmd.Parameters.AddWithValue("@d8", DateTime.UtcNow.ToLocalTime());
            cmd.Parameters.AddWithValue("@d9", superviserId);
            currentClientId = (int)cmd.ExecuteScalar();
            con.Close();
        }

        private void TASameAsCA(string  tblName1)
        {
            string tableName = tblName1;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string Qry = "insert into " + tableName + "(PostOfficeId,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TContactNo,IClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(Qry);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
            cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox1.Text) ? (object)DBNull.Value : cBlockTextBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
            cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));
            cmd.Parameters.AddWithValue("@d11", currentClientId);
            affectedRows2 = (int)cmd.ExecuteScalar();
            con.Close();              
        }
        private void SaveCorporateORTraddingAddress(string tblName1)
        {
           string tableName = tblName1;
           if (tableName == "CorporateAddresses")
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string insertQ = "insert into " + tableName + "(PostOfficeId,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CContactNo,IClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
                cmd = new SqlCommand(insertQ);
                cmd.Connection = con;               
                cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postofficeIdC) ? (object)DBNull.Value : postofficeIdC));
                cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cFlatNoTextBox.Text) ? (object)DBNull.Value : cFlatNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cHouseNoTextBox.Text) ? (object)DBNull.Value : cHouseNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cRoadNoTextBox.Text) ? (object)DBNull.Value : cRoadNoTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cBlockTextBox1.Text) ? (object)DBNull.Value : cBlockTextBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAreaTextBox.Text) ? (object)DBNull.Value : cAreaTextBox.Text));
                cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cContactNoTextBox.Text) ? (object)DBNull.Value : cContactNoTextBox.Text));

                cmd.Parameters.AddWithValue("@d11", currentClientId);
                affectedRows1 = (int)cmd.ExecuteScalar();
                con.Close();
            }
           else if (tableName == "TraddingAddresses")
           {
               con = new SqlConnection(cs.DBConn);
               con.Open();
               string Qry = "insert into " + tableName + "(PostOfficeId,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TContactNo,IClientId) Values(@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
               cmd = new SqlCommand(Qry);
               cmd.Connection = con;              
               cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(postOfficeIdT) ? (object)DBNull.Value : postOfficeIdT));
               cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tFlatNoTextBox.Text) ? (object)DBNull.Value : tFlatNoTextBox.Text));
               cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tHouseNoTextBox.Text) ? (object)DBNull.Value : tHouseNoTextBox.Text));
               cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tRoadNoTextBox.Text) ? (object)DBNull.Value : tRoadNoTextBox.Text));
               cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tBlockTextBox2.Text) ? (object)DBNull.Value : tBlockTextBox2.Text));
               cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAreaTextBox.Text) ? (object)DBNull.Value : tAreaTextBox.Text));
               cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(tContactNoTextBox.Text) ? (object)DBNull.Value : tContactNoTextBox.Text));
               cmd.Parameters.AddWithValue("@d11", currentClientId);
               affectedRows2 = (int)cmd.ExecuteScalar();
               con.Close();              
           }
        }       

        private void SaveContactPersonDetails()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string qury = "insert into ContactPersonDetails(ContactPersonName,Designation,CellNumber,EmailBankId,IClientId) Values(@d1,@d2,@d3,@d4,@d5)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            cmd = new SqlCommand(qury);
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(txtContactPerson.Text) ? (object)DBNull.Value : txtContactPerson.Text));
            cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(txtDesignation.Text) ? (object)DBNull.Value : txtDesignation.Text));
            cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(txtCellNumber.Text) ? (object)DBNull.Value : txtCellNumber.Text));           
            cmd.Parameters.AddWithValue("@d4", bankCPEmailId);
            cmd.Parameters.AddWithValue("@d5", currentClientId);
            affectedRows3 = (int)cmd.ExecuteScalar();
            con.Close();
        }  

        private void ClearContactPersonDetails()
        {
            txtDesignation.Clear();
            txtCellNumber.Clear();
            cmbCPEmailAddress.SelectedIndex = -1;
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
            if (cDivisionCombo.Text == "")
            {
                MessageBox.Show("Please Select Corporate Division", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cDivisionCombo.Focus();
                return;
            }
            if (cDistCombo.Text == "")
            {
                MessageBox.Show("Please Select Corporate District", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cDistCombo.Focus();
                return;
            }
            if (cThanaCombo.Text == "")
            {
                MessageBox.Show("Please Enter Corporate Thana", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cThanaCombo.Focus();
                return;
            }
            if (cPostOfficeCombo.Text == "")
            {
                MessageBox.Show("Please Enter Corporate PostOffice", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cPostOfficeCombo.Focus();
                return;
            }
            if (cPostCodeTextBox.Text == "")
            {
                MessageBox.Show("Please Enter Corporate PostCode", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cPostCodeTextBox.Focus();
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
                    SaveCorporateORTraddingAddress("CorporateAddresses");                                       
                }
               //2.Corporate Address Applicable  & Tradding Address Same as  Corporate Address                                        
                if (sameAsCorporatAddCheckBox.Checked)
                {
                    SaveInquiryClient();
                    SaveCorporateORTraddingAddress("CorporateAddresses");
                    TASameAsCA("TraddingAddresses");
                                               
                }
                //3.Corporate Address Applicable  & Tradding Address  Applicable
                if (sameAsCorporatAddCheckBox.Checked == false && notApplicableCheckBox.Checked == false)
                {
                    SaveInquiryClient();
                    SaveCorporateORTraddingAddress("CorporateAddresses");
                    SaveCorporateORTraddingAddress("TraddingAddresses");                   
                }
                if (!string.IsNullOrEmpty(txtContactPerson.Text))
                {
                    SaveContactPersonDetails();
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

            tPostCodeTextBox.Clear();
            tPostCombo.SelectedIndex = -1;
            tThenaCombo.SelectedIndex = -1;
            tDistrictCombo.SelectedIndex = -1;
            tDivisionCombo.SelectedIndex = -1;
           
           
            
            
        }
        private void FilStar()
        {
            label47.Visible = true;
            label36.Visible = true;
            label40.Visible = true;
            label35.Visible = true;
            label45.Visible = true;
        }

        private void ResetStar()
        {
            label47.Visible = false;
            label36.Visible = false;
            label40.Visible = false;
            label35.Visible = false;
            label45.Visible = false;
        }

        private void Reset()
        {
            
            cmbClientType.SelectedIndex=-1;
            cmbNatureOfClient.SelectedIndex = -1;
            cmbEmailAddress.SelectedIndex = -1;
            cmbIndustryCategory.SelectedIndex=-1;
            cmbCPEmailAddress.SelectedIndex = -1;
            endUserTextBox.Clear();  
            clientNameTextBox.Clear();
            cmbSuperviser.SelectedIndex = -1;

            
            cFlatNoTextBox.Clear();
            cHouseNoTextBox.Clear();
            cRoadNoTextBox.Clear();
            cBlockTextBox1.Clear();
            cAreaTextBox.Clear();
            cContactNoTextBox.Clear();
           
            
           
            cPostCodeTextBox.Clear();
            cPostOfficeCombo.SelectedIndex = -1;
            cThanaCombo.SelectedIndex = -1;
            cDistCombo.SelectedIndex = -1;
            cDivisionCombo.SelectedIndex = -1;
            

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
      
            
            
            txtCellNumber.Clear();
            txtDesignation.Clear();
            txtContactPerson.Clear();
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
            //ToolTip toolTip1 = new ToolTip();
            //toolTip1.AutoPopDelay = 0;
            //toolTip1.InitialDelay = 0;
            //toolTip1.ReshowDelay = 0;
            //toolTip1.ShowAlways = true;
            //toolTip1.SetToolTip(this.txtClientComboBox, txtClientComboBox.Items[txtClientComboBox.SelectedIndex].ToString());

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
                string ct = "select RTRIM(Name) from Registration where Statuss!='InActive'  order by UserId desc";
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
            userType1 = LoginForm.userType;
            submittedBy = LoginForm.uId.ToString();            
            FillCMBSuperviserName();
            EmailAddress();
            EmailCPAddress();

            FillClientType();
            FillNatureOfClient();
            FillIndustryCategory();

            FillCDivisionCombo();
            FillTDivisionCombo();

            cDistCombo.Enabled = false;
            cThanaCombo.Enabled = false;
            cPostOfficeCombo.Enabled = false;

            tDistrictCombo.Enabled = false;
            tThenaCombo.Enabled = false;
            tPostCombo.Enabled = false;

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
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 0;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.cmbNatureOfClient, cmbNatureOfClient.Items[cmbNatureOfClient.SelectedIndex].ToString());
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

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbIndustryCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 0;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.cmbIndustryCategory, cmbIndustryCategory.Items[cmbIndustryCategory.SelectedIndex].ToString());

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
                    cThanaCombo.ResetText();
                    cPostOfficeCombo.Items.Clear();
                    cPostOfficeCombo.ResetText();
                    cPostOfficeCombo.SelectedIndex = -1;
                    cPostOfficeCombo.Enabled = false;
                    cPostCodeTextBox.Clear();
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
                string ctk = "SELECT  RTRIM(Districts.D_ID)  from Districts WHERE Districts.District=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "District"));
                cmd.Parameters["@find"].Value = tDistrictCombo.Text;
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

                tDistrictCombo.Text = cDistCombo.Text.Trim();
                tThenaCombo.Items.Clear();
                tThenaCombo.ResetText();
                tPostCombo.Items.Clear();
                tPostCombo.ResetText();
                tPostCombo.SelectedIndex = -1;
                tPostCombo.Enabled = false;
                tPostCodeTextBox.Clear();
                tThenaCombo.Enabled = true;
                tPostCombo.Focus();

                //tDistrictCombo.Text = tDistrictCombo.Text.Trim();
                //tThenaCombo.Items.Clear();
                //tThenaCombo.Text = "";
                //tPostCombo.SelectedIndex = -1;
                //tPostCodeTextBox.Clear();
                //tThenaCombo.Enabled = true;
                //tThenaCombo.Focus();

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
            //if (!string.IsNullOrEmpty(emailAddressTextBox.Text))
            //{
            //    string emailId = emailAddressTextBox.Text.Trim();
            //    Regex mRegxExpression;

            //    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            //    if (!mRegxExpression.IsMatch(emailId))
            //    {

            //        MessageBox.Show("Please type your  valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        emailAddressTextBox.Clear();
            //        emailAddressTextBox.Focus();

            //    }
            //}
        }

        private void tctCPEmailAddress_Validating(object sender, CancelEventArgs e)
        {
            //string emailId = txtCPEmailAddress.Text.Trim();
            //Regex mRegxExpression;

            //mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            //if (!mRegxExpression.IsMatch(emailId))
            //{

            //    MessageBox.Show("User Id must be your email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtCPEmailAddress.Clear();
            //    txtCPEmailAddress.Focus();

            //}
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
                    ResetStar();
                }
                else
                {

                    groupBox3.Enabled = false;
                    ResetTradingAddress();
                    ResetStar();
                }

            }
            else
            {
                if (notApplicableCheckBox.Checked)
                {
                    groupBox3.Enabled = false;
                    ResetTradingAddress();
                    ResetStar();
                }
                else
                {

                    groupBox3.Enabled = true;
                    ResetTradingAddress();
                    FilStar();
                }
            }
            
        }

        private void ifApplicableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
           
            
        }

        private void cDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
              
            cDistCombo.Items.Clear();
            cDistCombo.ResetText();
            cThanaCombo.Items.Clear();
            cThanaCombo.ResetText();
            cPostOfficeCombo.Items.Clear();
            cPostOfficeCombo.ResetText();
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ctk = "SELECT RTRIM(Divisions.Division_ID) from Divisions WHERE Divisions.Division=@find";

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
                cDistCombo.ResetText();
                cThanaCombo.Items.Clear();
                cThanaCombo.ResetText();
                cThanaCombo.SelectedIndex = -1;
                cPostOfficeCombo.Items.Clear();
                cPostOfficeCombo.ResetText();
                cPostOfficeCombo.SelectedIndex = -1;
                cPostCodeTextBox.Clear();
                cDistCombo.Enabled = true;               
                cDistCombo.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(Districts.District) from Districts  Where Districts.Division_ID = '" + divisionIdC + "'  order by Districts.Division_ID desc";
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

            cThanaCombo.Enabled = false;
            cPostOfficeCombo.Enabled = false;
        }

        private void cThanaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "select D_ID from Districts WHERE District= '" + cDistCombo.Text + "'";

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

                con = new SqlConnection(cs.DBConn);
                con.Open();
                //string ct = "select RTRIM(PostOffice.PostOfficeName) from PostOffice  Where PostOffice.T_ID = '" + thanaIdC + "' order by PostOffice.T_ID desc";
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
            cPostOfficeCombo.SelectedIndex = -1;
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

        private void tDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tDistrictCombo.Items.Clear();
            tDistrictCombo.ResetText();
            tThenaCombo.Items.Clear();
            tThenaCombo.ResetText();
            tPostCombo.Items.Clear();
            tPostCombo.ResetText();
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
                tDistrictCombo.Items.Clear();
                tDistrictCombo.ResetText();
                tThenaCombo.Items.Clear();
                tThenaCombo.ResetText();
                tThenaCombo.SelectedIndex = -1;
                tPostCombo.Items.Clear();
                tPostCombo.ResetText();
                tPostCombo.SelectedIndex = -1;
                tPostCodeTextBox.Clear();
                tDistrictCombo.Enabled = true;
                tDistrictCombo.Focus();

                //tDivisionCombo.Text = tDivisionCombo.Text.Trim();
                //tDistrictCombo.Items.Clear();
                //tDistrictCombo.Text = "";
                //tThenaCombo.SelectedIndex = -1;
                //tPostCombo.SelectedIndex = -1;
                //tPostCodeTextBox.Clear();
                //tDistrictCombo.Enabled = true;
                //tDistrictCombo.Focus();

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
            tThenaCombo.Enabled = false;
            tPostCombo.Enabled = false;
        }

        private void tThenaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = con.CreateCommand();

            cmd.CommandText = "select D_ID from Districts WHERE District= '" + tDistrictCombo.Text + "'";

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
                cmd.Parameters["@find"].Value = tThenaCombo.Text;
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

                tThenaCombo.Text = cThanaCombo.Text.Trim();
                tPostCombo.Items.Clear();
                tPostCombo.ResetText();              
                tPostCodeTextBox.Clear();
                tPostCombo.Enabled = true;
                tPostCombo.Focus();

                //tThenaCombo.Text = tThenaCombo.Text.Trim();
                //tPostCombo.Items.Clear();
                //tPostCombo.Text = "";
                //tPostCodeTextBox.Clear();
                //tPostCombo.Enabled = true;
                //tPostCombo.Focus();

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
                string ctk = "SELECT  RTRIM(PostOffice.PostOfficeId),RTRIM(PostOffice.PostCode) from PostOffice WHERE PostOffice.PostOfficeName=@find";
                cmd = new SqlCommand(ctk);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NVarChar, 50, "PostOfficeName"));
                cmd.Parameters["@find"].Value = tPostCombo.Text;
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
            //if (!String.IsNullOrEmpty(txtCPEmailAddress.Text))
            //{
            //    string emailId2 = txtCPEmailAddress.Text.Trim();
            //    Regex mRegxExpression;

            //    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            //    if (!mRegxExpression.IsMatch(emailId2))
            //    {

            //        MessageBox.Show("Please type valid email Address.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        txtCPEmailAddress.Clear();
            //        txtContactPerson.Focus();

            //    }
            //}
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
                txtContactPerson.Focus();
            }
            
        }

        private void txtCellNumber_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Please  enter Contact Person Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactPerson.Focus();
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
                    ResetTradingAddress();
                    ResetStar();
                }
                else
                {

                    groupBox3.Enabled = false;
                    ResetTradingAddress();
                    ResetStar();
                }

            }
            else
            {
                if (sameAsCorporatAddCheckBox.Checked)
                {
                    groupBox3.Enabled = false;
                    ResetTradingAddress();
                    ResetStar();
                }
                else
                {

                    groupBox3.Enabled = true;
                    ResetTradingAddress();
                    FilStar();
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
                txtContactPerson.Focus();
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
            if (!(cmbCPEmailAddress.SelectedIndex==-1))
            {
                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 0;
                toolTip1.InitialDelay = 0;
                toolTip1.ReshowDelay = 0;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(this.cmbEmailAddress, cmbEmailAddress.Items[cmbEmailAddress.SelectedIndex].ToString());  
            }
            


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
                                cmd.Parameters.AddWithValue("@d2", submittedBy);
                                cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                                cmd.ExecuteNonQuery();

                                con.Close();
                                cmbEmailAddress.Items.Clear();
                                EmailAddress();
                                cmbEmailAddress.SelectedText = input;
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
            if (!(cmbCPEmailAddress.SelectedIndex==-1))
            {
                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 0;
                toolTip1.InitialDelay = 0;
                toolTip1.ReshowDelay = 0;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(this.cmbCPEmailAddress, cmbCPEmailAddress.Items[cmbCPEmailAddress.SelectedIndex].ToString()); 
            }
            

            if (cmbCPEmailAddress.Text == "Not In The List")
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Please Input Email Here", "Input Here", "", -1, -1);
                if (string.IsNullOrWhiteSpace(input))
                {
                    cmbCPEmailAddress.SelectedIndex = -1;
                    //cmbCPEmailAddress.ResetText();
                    
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
                            cmd.Parameters.AddWithValue("@d2", submittedBy);
                            cmd.Parameters.AddWithValue("@d3", DateTime.UtcNow.ToLocalTime());
                            cmd.ExecuteNonQuery();
                            con.Close();

                            cmbCPEmailAddress.Items.Clear();
                            EmailCPAddress();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ClientRegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            MainUIInquieryClient frm = new MainUIInquieryClient();
            frm.Show();
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
        private void changeFocus(Control ctl)
        {
            ctl.Focus();
        }

        private void cmbCPEmailAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbCPEmailAddress.Text) && !cmbCPEmailAddress.Items.Contains(cmbCPEmailAddress.Text))
            {
                MessageBox.Show("Please Select A Valid Email Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCPEmailAddress.ResetText();
                this.BeginInvoke(new ChangeFocusDelegate(changeFocus), cmbCPEmailAddress);
            }
        }

        private void cmbCPEmailAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Please  enter Contact Person Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactPerson.Focus();
            }
        }

        private void cPostOfficeCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(cDistCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cDistCombo.Focus();
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
            //    return;
            //}

            //else if (string.IsNullOrWhiteSpace(cDistCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //   return;
            //}
        }

        private void cDistCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(cDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
        }

        private void cmbClientType_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviser.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviser.Focus();
            }
            else if (string.IsNullOrWhiteSpace(clientNameTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameTextBox.Focus();
            }
        }

        private void cmbNatureOfClient_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviser.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviser.Focus();
            }
            else  if (string.IsNullOrWhiteSpace(clientNameTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameTextBox.Focus();
            }
        }

        private void cmbEmailAddress_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviser.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviser.Focus();
            }
            else  if (string.IsNullOrWhiteSpace(clientNameTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameTextBox.Focus();
            }
        }

        private void cmbIndustryCategory_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviser.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviser.Focus();
            }
            else if (string.IsNullOrWhiteSpace(clientNameTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameTextBox.Focus();
            }
        }

        private void endUserTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSuperviser.Text))
            {
                MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSuperviser.Focus();
            }
            else if (string.IsNullOrWhiteSpace(clientNameTextBox.Text))
            {
                MessageBox.Show("Please  enter Client Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientNameTextBox.Focus();
            }
        }

        private void tPostCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(tDistrictCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDistrictCombo.Focus();
            //}
            //else if (string.IsNullOrWhiteSpace(tThenaCombo.Text))
            //{
            //    MessageBox.Show("Please  select thana name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tThenaCombo.Focus();
            //}
        }

        private void tThenaCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDivisionCombo.Focus();
            //}



            //else if (string.IsNullOrWhiteSpace(tDistrictCombo.Text))
            //{
            //    MessageBox.Show("Please  select District first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDistrictCombo.Focus();
            //}
        }

        private void tDistrictCombo_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tDivisionCombo.Text))
            //{
            //    MessageBox.Show("Please  select Division  first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tDivisionCombo.Focus();
            //}
        }

        private void clientNameTextBox_Enter(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(cmbSuperviser.Text))
            //{
            //    MessageBox.Show("Please  Select Supervisor  Name first.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cmbSuperviser.Focus();
            //}
        }

        private void txtCellNumber_Leave(object sender, EventArgs e)
        {
            

        }

        private void txtCellNumber_Validating(object sender, CancelEventArgs e)
        {        

            //int sum = 0;
            //foreach (Control ctrl in this.Controls)
            //{
            //    if ((ctrl as TextBox) != null)
            //    {
            //        TextBox txt = ctrl as TextBox;
            //        int val = Convert.ToInt32(txt.Text);
            //        sum += val;
            //    }
            //}



            decimal sum = 0;
            decimal num = Convert.ToDecimal(txtCellNumber.Text);
            while (num > 0)
            {
                sum = sum + (num / 10);
                num = num / 10;
            }

            if (sum == 0)
            {
                txtCellNumber.Clear();
            }        
        }

        private void cContactNoTextBox_Validating(object sender, CancelEventArgs e)
        {
            decimal sum = 0;
            decimal num = Convert.ToDecimal(cContactNoTextBox.Text);
            while (num > 0)
            {
                sum = sum + (num / 10);
                num = num / 10;
            }

            if (sum == 0)
            {
                cContactNoTextBox.Clear();
            } 
        }

        private void tContactNoTextBox_Validating(object sender, CancelEventArgs e)
        {
            int sum = 0;
            int num = Convert.ToInt32(tContactNoTextBox.Text);
            while (num > 0)
            {
                sum = sum + (num / 10);
                num = num / 10;
            }

            if (sum == 0)
            {
                tContactNoTextBox.Clear();
            } 
        }

        private void cmbEmailAddress_MouseHover(object sender, EventArgs e)
        {
            if (!(cmbCPEmailAddress.SelectedIndex == -1))
            {
                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 0;
                toolTip1.InitialDelay = 0;
                toolTip1.ReshowDelay = 0;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(this.cmbEmailAddress, cmbEmailAddress.Items[cmbEmailAddress.SelectedIndex].ToString());
            }
        }       
    }
}
