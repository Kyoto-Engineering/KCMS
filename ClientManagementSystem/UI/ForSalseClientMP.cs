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
using ClientManagementSystem.LoginUI;
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class ForSalseClientMP : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        private SqlDataAdapter sda;
        ConnectionString cs = new ConnectionString();
        public string userTypeK;
        public ForSalseClientMP()
        {
            InitializeComponent();
        }

        private void salseClientButton_Click(object sender, EventArgs e)
        {
           
        }

        private void instanInquiryClientButton_Click(object sender, EventArgs e)
        {
            dynamic af = new InstantClientEntryForm();
            this.Visible = false;
             af.ShowDialog();
            this.Visible = true;

        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MainUI am=new MainUI();
            am.ShowDialog();
        }

        private void salesClientGridButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic af=new GridForSalesClient();
            af.ShowDialog();
            this.Visible = true;
        }

        private void instantbutton_Click(object sender, EventArgs e)
        {
            
           dynamic fg=new GridForInstantClient();
           this.Visible = false;
            fg.ShowDialog();
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //creating an object of ParameterField class
            ParameterField paramField = new ParameterField();

            //creating an object of ParameterFields class
            ParameterFields paramFields = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

            //set the parameter field name
            paramField.Name = "User Id ";

            //set the parameter value
            paramDiscreteValue.Value = LoginForm.uId;

            //add the parameter value in the ParameterField object
            paramField.CurrentValues.Add(paramDiscreteValue);

            //add the parameter in the ParameterFields object
            paramFields.Add(paramField);

            //set the parameterfield information in the crystal report



            ReportViewer f2 = new ReportViewer();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            //	Table table = default(Table);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "NewProductList";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            ListofSalesClientContactPoints cr = new ListofSalesClientContactPoints();
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields;
            //set the parameterfield information in the crystal report
            f2.crystalReportViewer1.ReportSource = cr;
            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           //creating an object of ParameterField class
            ParameterField paramField = new ParameterField();

            //creating an object of ParameterFields class
            ParameterFields paramFields = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

            //set the parameter field name
            paramField.Name = "User Id ";

            //set the parameter value
            paramDiscreteValue.Value = LoginForm.uId;

            //add the parameter value in the ParameterField object
            paramField.CurrentValues.Add(paramDiscreteValue);

            //add the parameter in the ParameterFields object
            paramFields.Add(paramField);

            //set the parameterfield information in the crystal report



            ReportViewer f2 = new ReportViewer();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            //	Table table = default(Table);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "NewProductList";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            SalesClientIndustryCataegory cr = new SalesClientIndustryCataegory();
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields;
            //set the parameterfield information in the crystal report
            f2.crystalReportViewer1.ReportSource = cr;
            this.Visible = false;

            f2.ShowDialog();
            this.Visible= true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParameterField paramField = new ParameterField();

            //creating an object of ParameterFields class
            ParameterFields paramFields = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

            //set the parameter field name
            paramField.Name = "User Id ";

            //set the parameter value
            paramDiscreteValue.Value = LoginForm.uId;

            //add the parameter value in the ParameterField object
            paramField.CurrentValues.Add(paramDiscreteValue);

            //add the parameter in the ParameterFields object
            paramFields.Add(paramField);

            //set the parameterfield information in the crystal report



            ReportViewer f2 = new ReportViewer();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            //	Table table = default(Table);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "NewProductList";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            SalesClientIProfile cr = new SalesClientIProfile();
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields;
            //set the parameterfield information in the crystal report
            f2.crystalReportViewer1.ReportSource = cr;
            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
        }

        private void SalesClientDetailsGrid()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string selectQuery = "Select FirstSet.SClientId,FirstSet.Name RM,FirstSet.ClientName ClientName,FirstSet.ClientType  ClientType,FirstSet.ClientNature NatureOfClient,FirstSet.Email EmailId,FirstSet.IndustryCategory ,FirstSet.EndUser,thirdq.ContactPersonName,thirdq.Designation, thirdq.CellNumber,thirdq.Email,FirstSet.CFlatNo,FirstSet.CHouseNo,FirstSet.CRoadNo,FirstSet.CBlock,FirstSet.CArea,FirstSet.CContactNo,FirstSet.Division CDivition,FirstSet.District CDistrict,FirstSet.Thana CPoliceStation,FirstSet.PostOfficeName CPostOfficeName,FirstSet.PostCode CPostCode,QUERYTWO.TFlatNo,QUERYTWO.THouseNo,QUERYTWO.TRoadNo,QUERYTWO.TBlock,QUERYTWO.TArea,QUERYTWO.TContactNo,QUERYTWO.Division TDivision,QUERYTWO.District TDistrict,QUERYTWO.Thana TThana,QUERYTWO.PostOfficeName TPostOfficeName,QUERYTWO.PostCode TPostCode,QUERY3.BFlatNo,QUERY3.BHouseNo,QUERY3.BRoadNo,QUERY3.BBlock,QUERY3.BArea,QUERY3.BContactNo,QUERY3.Division BDivision,QUERY3.District BDistrict,QUERY3.Thana BThana,QUERY3.PostOfficeName BPostOfficeName,QUERY3.PostCode BPostCode,q4.BankName,q4.BranchName,q4.AccountNo from (SELECT Registration.Name,SalesClient.SClientId,SalesClient.ClientName,ClientTypes.ClientType,NatureOfClients.ClientNature,EmailBank.Email,IndustryCategorys.IndustryCategory,SalesClient.EndUser,CorporateAddresses.CFlatNo,CorporateAddresses.CHouseNo,CorporateAddresses.CRoadNo,CorporateAddresses.CBlock,CorporateAddresses.CArea,CorporateAddresses.CContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode  FROM  SalesClient  INNER JOIN  Registration ON SalesClient.SuperviserId = Registration.UserId  INNER JOIN ClientTypes ON SalesClient.ClientTypeId = ClientTypes.ClientTypeId  INNER JOIN NatureOfClients ON SalesClient.NatureOfClientId = NatureOfClients.NatureOfClientId  INNER JOIN IndustryCategorys ON SalesClient.IndustryCategoryId = IndustryCategorys.IndustryCategoryId  INNER JOIN CorporateAddresses ON SalesClient.SClientId = CorporateAddresses.SClientId  INNER JOIN PostOffice ON CorporateAddresses.PostOfficeId = PostOffice.PostOfficeId  INNER JOIN Thanas ON PostOffice.T_ID = Thanas.T_ID INNER JOIN Districts ON Thanas.D_ID = Districts.D_ID  INNER JOIN  Divisions ON Districts.Division_ID = Divisions.Division_ID  Left Join EmailBank ON SalesClient.EmailBankId= EmailBank.EmailBankId )  AS FirstSet  lEFT jOIN (SELECT SalesClient.SClientId,TraddingAddresses.TFlatNo,TraddingAddresses.THouseNo,TraddingAddresses.TRoadNo,TraddingAddresses.TBlock,TraddingAddresses.TArea,TraddingAddresses.TContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode  FROM  SalesClient   INNER JOIN TraddingAddresses ON SalesClient.SClientId = TraddingAddresses.SClientId INNER JOIN PostOffice ON TraddingAddresses.PostOfficeId = PostOffice.PostOfficeId  INNER JOIN Thanas ON PostOffice.T_ID = Thanas.T_ID  INNER JOIN Districts ON Thanas.D_ID = Districts.D_ID  INNER JOIN  Divisions ON Districts.Division_ID = Divisions.Division_ID) aS QUERYTWO ON FirstSet.SClientId =  QUERYTWO.SClientId  lEFT jOIN (SELECT SalesClient.SClientId,BA.BFlatNo,BA.BHouseNo,BA.BRoadNo,BA.BBlock,BA.BArea,BA.BContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode  FROM  SalesClient  INNER JOIN BillingAddresses AS BA ON SalesClient.SClientId = BA.SClientId  INNER JOIN PostOffice ON BA.PostOfficeId = PostOffice.PostOfficeId  INNER JOIN Thanas ON PostOffice.T_ID = Thanas.T_ID  INNER JOIN Districts ON Thanas.D_ID = Districts.D_ID  INNER JOIN  Divisions ON Districts.Division_ID = Divisions.Division_ID) aS QUERY3 ON FirstSet.SClientId =  QUERY3.SClientId left join (SELECT SalesClient.SClientId,ContactPersonDetails.ContactPersonName,ContactPersonDetails.Designation,ContactPersonDetails.CellNumber,EmailBank.Email  FROM  SalesClient  INNER JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId left join EmailBank on ContactPersonDetails.EmailBankId=EmailBank.EmailBankId) as thirdq on FirstSet.SClientId  = thirdq.SClientId  left Join (Select  SalesClient.SClientId,BankDetails.BankName,BankDetails.BranchName,BankDetails.AccountNo  from SalesClient INNER JOIN  BankDetails ON SalesClient.SClientId =BankDetails.SClientId) as q4 on FirstSet.SClientId  = q4.SClientId";
            SqlDataAdapter myadapter = new SqlDataAdapter(selectQuery, con);
            DataTable dt = new DataTable();
            myadapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

       
        private void ForSalseClientMP_Load(object sender, EventArgs e)
        {
            SalesClientDetailsGrid();
            userTypeK = LoginForm.userType;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainUIInquieryClient frm = new MainUIInquieryClient();
             frm.Show();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.Hide();
                DataGridViewRow dr = dataGridView1.CurrentRow;               
                ClientApprovedFinalForm frm = new ClientApprovedFinalForm();
                frm.Show();
                frm.txtIClientId.Text = dr.Cells[0].Value.ToString();
                frm.clientNameAPTextBox.Text = dr.Cells[1].Value.ToString();
                frm.cmbClientType.Text = dr.Cells[2].Value.ToString();
                frm.cmbNatureOfClient.Text = dr.Cells[3].Value.ToString();
                frm.cmbEmailAddress.Text = dr.Cells[4].Value.ToString();
                frm.cmbIndustryCategory.Text = dr.Cells[5].Value.ToString();
                frm.contactPersonNameAPTextBox.Text = dr.Cells[6].Value.ToString();
                frm.designationAPTextBox.Text = dr.Cells[7].Value.ToString();
                frm.cellNumberAPTextBox.Text = dr.Cells[8].Value.ToString();
                frm.endUserAPTextBox.Text = dr.Cells[9].Value.ToString();

                frm.cFlatNoTextBox.Text = dr.Cells[10].Value.ToString();
                frm.cHouseNoTextBox.Text = dr.Cells[11].Value.ToString();
                frm.cRoadNoTextBox.Text = dr.Cells[12].Value.ToString();
                frm.cBlockTextBox.Text = dr.Cells[13].Value.ToString();
                frm.cAreaTextBox.Text = dr.Cells[14].Value.ToString();
                frm.cThanaCombo.Text = dr.Cells[15].Value.ToString();
                frm.cPostCodeTextBox.Text = dr.Cells[16].Value.ToString();
                frm.cDistrictCombo.Text = dr.Cells[17].Value.ToString();
                frm.cContactNoTextBox.Text = dr.Cells[18].Value.ToString();

                frm.tFlatNoTextBox.Text = dr.Cells[19].Value.ToString();
                frm.tHouseNoTextBox.Text = dr.Cells[20].Value.ToString();
                frm.tRoadNoTextBox.Text = dr.Cells[21].Value.ToString();
                frm.tBlockTextBox.Text = dr.Cells[22].Value.ToString();
                frm.tAreaTextBox.Text = dr.Cells[23].Value.ToString();
                frm.cThanaCombo.Text = dr.Cells[24].Value.ToString();
                frm.tPostCodeTextBox.Text = dr.Cells[25].Value.ToString();
                frm.tDistComboBox.Text = dr.Cells[26].Value.ToString();
                frm.tContactNoTextBox.Text = dr.Cells[27].Value.ToString();
                frm.lk.Text = lg.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string x, y,z;
                this.Hide();
                DataGridViewRow dr = dataGridView1.CurrentRow;
                SalesClientUpdateForm frm = new SalesClientUpdateForm();
                frm.Show();
                frm.txtSalesClientId.Text = dr.Cells[0].Value.ToString();
                frm.cmbSuperviserName.Text = dr.Cells[1].Value.ToString();
                //frm.txtIClientId.Text = dr.Cells[2].Value.ToString();
                frm.clientNameAPTextBox.Text = dr.Cells[2].Value.ToString();
                frm.cmbClientType.Text = dr.Cells[3].Value.ToString();
                frm.cmbNatureOfClient.Text = dr.Cells[4].Value.ToString();
                frm.cmbEmailAddress.Text = dr.Cells[5].Value.ToString();
                frm.cmbIndustryCategory.Text = dr.Cells[6].Value.ToString();
                frm.endUserAPTextBox.Text = dr.Cells[7].Value.ToString();

                frm.contactPersonNameAPTextBox.Text = dr.Cells[8].Value.ToString();
                frm.designationAPTextBox.Text = dr.Cells[9].Value.ToString();
                frm.cellNumberAPTextBox.Text = dr.Cells[10].Value.ToString();
                frm.cmbCPEmailAddress.Text = dr.Cells[11].Value.ToString();


                frm.cFlatNoTextBox.Text = dr.Cells[12].Value.ToString();
                frm.cHouseNoTextBox.Text = dr.Cells[13].Value.ToString();
                frm.cRoadNoTextBox.Text = dr.Cells[14].Value.ToString();
                frm.cBlockTextBox.Text = dr.Cells[15].Value.ToString();
                frm.cContactNoTextBox.Text = dr.Cells[16].Value.ToString();
                frm.cAreaTextBox.Text = dr.Cells[17].Value.ToString();

                frm.cDivisionCombo.Text = dr.Cells[18].Value.ToString();
                frm.cDistrictCombo.Text = dr.Cells[19].Value.ToString();
                frm.cThanaCombo.Text = dr.Cells[20].Value.ToString().Trim();
                frm.cPostOfficeCombo.Text = dr.Cells[21].Value.ToString();
              x=frm.cPostCodeTextBox.Text = dr.Cells[22].Value.ToString();


                frm.tFlatNoTextBox.Text = dr.Cells[23].Value.ToString();
                frm.tHouseNoTextBox.Text = dr.Cells[24].Value.ToString();
                frm.tRoadNoTextBox.Text = dr.Cells[25].Value.ToString();
                frm.tBlockTextBox.Text = dr.Cells[26].Value.ToString();
                frm.tContactNoTextBox.Text = dr.Cells[27].Value.ToString();
                frm.tAreaTextBox.Text = dr.Cells[28].Value.ToString();

                frm.tDivitionCombo.Text = dr.Cells[29].Value.ToString();
                frm.tDistComboBox.Text = dr.Cells[30].Value.ToString();
                frm.tThanaCombo.Text = dr.Cells[31].Value.ToString().Trim();
                frm.tPostOfficeCombo.Text = dr.Cells[32].Value.ToString();
               y=frm.tPostCodeTextBox.Text = dr.Cells[33].Value.ToString();

                frm.bFlatNoTextBox.Text = dr.Cells[34].Value.ToString();
                frm.bHouseNoTextBox.Text = dr.Cells[35].Value.ToString();
                frm.bRoadNoTextBox.Text = dr.Cells[36].Value.ToString();
                frm.bBlockTextBox.Text = dr.Cells[37].Value.ToString();
                frm.bAreaTextBox.Text = dr.Cells[38].Value.ToString();
                frm.bContactNoTextBox.Text = dr.Cells[39].Value.ToString();

                frm.bDivisionCombo.Text = dr.Cells[40].Value.ToString();
                frm.bDistrictCombo.Text = dr.Cells[41].Value.ToString();
                frm.bThanaCombo.Text = dr.Cells[42].Value.ToString();
                frm.bPostOfficeCombo.Text = dr.Cells[43].Value.ToString();
               z= frm.bPostCodeTextBox.Text = dr.Cells[44].Value.ToString();

                frm.bankNameTextBox.Text = dr.Cells[45].Value.ToString();
                frm.branchNameTextBox.Text = dr.Cells[46].Value.ToString();
                frm.accountNoTextBox.Text = dr.Cells[47].Value.ToString();

                if (x == y)
                {
                    frm.tASameAsCACheckBox.Checked = true;
                }
                else if (string.IsNullOrEmpty(y))
                {

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select RTRIM(TraddingAddresses.SClientId) from TraddingAddresses where TraddingAddresses.SClientId='" + frm.txtSalesClientId.Text + "'";
                    cmd = new SqlCommand(ct2, con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read() && !rdr.IsDBNull(0))
                    {

                        frm.tANotApplicable.Checked = false;
                    }
                    else
                    {

                        frm.tANotApplicable.Checked = true;
                    }
                }

                if (y == z)
                {
                    frm.bASameAsTACheckBox.Checked = true;
                }


            if (x == z)
             {
                frm.bASameAsCACheckBox.Checked = true;
            }
            else if (string.IsNullOrEmpty(z))
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select RTRIM(BillingAddresses.SClientId) from BillingAddresses where BillingAddresses.SClientId='" + frm.txtSalesClientId.Text + "'";
                    cmd = new SqlCommand(ct2, con);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read() && !rdr.IsDBNull(0))
                    {

                        frm.bANotAppCheckBox.Checked = false;
                    }
                    else
                    {

                        frm.bANotAppCheckBox.Checked = true;
                    }
                }
               
               

                frm.lk.Text = lg.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
           this.Hide();
           ConvertToSalesClientGrid af = new ConvertToSalesClientGrid();
           af.Show();
        }

        private void ForSalseClientMP_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (userTypeK == "Admin")
            {
                this.Hide();
                MainUI frm=new MainUI();
                frm.Show();
            }
            else if (userTypeK == "User")
            {
                this.Hide();
                MainUIForUser frm=new MainUIForUser();
                frm.Show();
            }
        } 
        
    }
}
