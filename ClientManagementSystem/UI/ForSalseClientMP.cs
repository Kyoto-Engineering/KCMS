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


        private void InquiryClientDetailsGrid()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            sda = new SqlDataAdapter("Select FirstSet.Name,FirstSet.IClientId,FirstSet.ClientName,FirstSet.ClientType,FirstSet.ClientNature,FirstSet.EmailAddress,FirstSet.IndustryCategory,FirstSet.EndUser,thirdq.ContactPersonName,thirdq.Designation, thirdq.CellNumber,thirdq.EmailId,FirstSet.CFlatNo,FirstSet.CHouseNo,FirstSet.CRoadNo,FirstSet.CBlock,FirstSet.CArea,FirstSet.CContactNo,FirstSet.Division,FirstSet.District,FirstSet.Thana,FirstSet.PostOfficeName,FirstSet.PostCode,QUERYTWO.TFlatNo,QUERYTWO.THouseNo,QUERYTWO.TRoadNo,QUERYTWO.TBlock,QUERYTWO.TArea,QUERYTWO.TContactNo,QUERYTWO.Division,QUERYTWO.District,QUERYTWO.Thana,QUERYTWO.PostOfficeName,QUERYTWO.PostCode from ( SELECT Registration.Name,InquieryClient.IClientId,InquieryClient.ClientName,ClientTypes.ClientType,NatureOfClients.ClientNature,InquieryClient.EmailAddress,IndustryCategorys.IndustryCategory,InquieryClient.EndUser,CorporateAddresses.CFlatNo,CorporateAddresses.CHouseNo,CorporateAddresses.CRoadNo,CorporateAddresses.CBlock,CorporateAddresses.CArea,CorporateAddresses.CContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode FROM  InquieryClient INNER JOIN  Registration ON InquieryClient.UserId = Registration.UserId INNER JOIN ClientTypes ON InquieryClient.ClientTypeId = ClientTypes.ClientTypeId INNER JOIN NatureOfClients ON InquieryClient.NatureOfClientId = NatureOfClients.NatureOfClientId INNER JOIN IndustryCategorys ON InquieryClient.IndustryCategoryId = IndustryCategorys.IndustryCategoryId INNER JOIN CorporateAddresses ON InquieryClient.IClientId = CorporateAddresses.IClientId INNER JOIN  Divisions ON CorporateAddresses.Division_ID = Divisions.Division_ID INNER JOIN Districts ON CorporateAddresses.D_ID = Districts.D_ID INNER JOIN Thanas ON CorporateAddresses.T_ID = Thanas.T_ID INNER JOIN PostOffice ON CorporateAddresses.PostOfficeId = PostOffice.PostOfficeId ) AS FirstSet lEFT jOIN (SELECT InquieryClient.IClientId,TraddingAddresses.TFlatNo,TraddingAddresses.THouseNo,TraddingAddresses.TRoadNo,TraddingAddresses.TBlock,TraddingAddresses.TArea,TraddingAddresses.TContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode FROM  InquieryClient INNER JOIN TraddingAddresses ON InquieryClient.IClientId = TraddingAddresses.IClientId INNER JOIN  Divisions ON TraddingAddresses.Division_ID = Divisions.Division_ID INNER JOIN Districts ON TraddingAddresses.D_ID = Districts.D_ID INNER JOIN Thanas ON TraddingAddresses.T_ID = Thanas.T_ID INNER JOIN PostOffice ON TraddingAddresses.PostOfficeId = PostOffice.PostOfficeId) aS QUERYTWO ON FirstSet.IClientId =  QUERYTWO.IClientId left join (SELECT InquieryClient.IClientId,ContactPersonDetails.ContactPersonName,ContactPersonDetails.Designation, ContactPersonDetails.CellNumber,ContactPersonDetails.EmailId FROM  InquieryClient INNER JOIN ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId) as thirdq on FirstSet.IClientId  = thirdq.IClientId", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void ForSalseClientMP_Load(object sender, EventArgs e)
        {
            InquiryClientDetailsGrid();
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
                frm.emailAddressAPTextBox.Text = dr.Cells[4].Value.ToString();
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
                this.Hide();
                DataGridViewRow dr = dataGridView1.CurrentRow;
                ClientApprovedFinalForm frm = new ClientApprovedFinalForm();
                frm.Show();

                frm.cmbSuperviserName.Text = dr.Cells[0].Value.ToString();
                frm.txtIClientId.Text = dr.Cells[1].Value.ToString();
                frm.clientNameAPTextBox.Text = dr.Cells[2].Value.ToString();
                frm.cmbClientType.Text = dr.Cells[3].Value.ToString();
                frm.cmbNatureOfClient.Text = dr.Cells[4].Value.ToString();
                frm.emailAddressAPTextBox.Text = dr.Cells[5].Value.ToString();
                frm.cmbIndustryCategory.Text = dr.Cells[6].Value.ToString();
                frm.endUserAPTextBox.Text = dr.Cells[7].Value.ToString();

                frm.contactPersonNameAPTextBox.Text = dr.Cells[8].Value.ToString();
                frm.designationAPTextBox.Text = dr.Cells[9].Value.ToString();
                frm.cellNumberAPTextBox.Text = dr.Cells[10].Value.ToString();
                frm.txtCPEmailAddress.Text = dr.Cells[11].Value.ToString();


                frm.cFlatNoTextBox.Text = dr.Cells[12].Value.ToString();
                frm.cHouseNoTextBox.Text = dr.Cells[13].Value.ToString();
                frm.cRoadNoTextBox.Text = dr.Cells[14].Value.ToString();
                frm.cBlockTextBox.Text = dr.Cells[15].Value.ToString();
                frm.cContactNoTextBox.Text = dr.Cells[16].Value.ToString();
                frm.cAreaTextBox.Text = dr.Cells[17].Value.ToString();

                frm.cDivisionCombo.Text = dr.Cells[18].Value.ToString();
                frm.cDistrictCombo.Text = dr.Cells[19].Value.ToString();
                frm.cThanaCombo.Text = dr.Cells[20].Value.ToString();
                frm.cPostOfficeCombo.Text = dr.Cells[21].Value.ToString();
                frm.cPostCodeTextBox.Text = dr.Cells[22].Value.ToString();


                frm.tFlatNoTextBox.Text = dr.Cells[23].Value.ToString();
                frm.tHouseNoTextBox.Text = dr.Cells[24].Value.ToString();
                frm.tRoadNoTextBox.Text = dr.Cells[25].Value.ToString();
                frm.tBlockTextBox.Text = dr.Cells[26].Value.ToString();
                frm.tContactNoTextBox.Text = dr.Cells[27].Value.ToString();
                frm.tAreaTextBox.Text = dr.Cells[28].Value.ToString();

                frm.tDivitionCombo.Text = dr.Cells[29].Value.ToString();
                frm.tDistComboBox.Text = dr.Cells[30].Value.ToString();
                frm.tThanaCombo.Text = dr.Cells[31].Value.ToString();
                frm.tPostOfficeCombo.Text = dr.Cells[32].Value.ToString();
                frm.tPostCodeTextBox.Text = dr.Cells[33].Value.ToString();


                frm.lk.Text = lg.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        
    }
}
