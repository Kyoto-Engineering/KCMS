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
            sda = new SqlDataAdapter("SELECT RTRIM(SalesClient.IClientId) ClientId,RTRIM(SalesClient.ClientName) ClientName,RTRIM(ClientTypes.ClientType) ClientType,RTRIM(NatureOfClients.ClientNature) NatureOfClient,RTRIM(SalesClient.EmailAddress) EmailAddress,RTRIM(IndustryCategorys.IndustryCategory) IndustryCategory,RTRIM(ContactPersonDetails.ContactPersonName) ContactPersonName,RTRIM(ContactPersonDetails.Designation) Designation,RTRIM(ContactPersonDetails.CellNumber) CellNumber,RTRIM(SalesClient.EndUser) EndUser,RTRIM(A.FlatNo) CFlatNo,RTRIM(A.HouseNo) CHouseNo,RTRIM(A.RoadNo) CRoadNo,RTRIM(A.Block) CBlock,RTRIM(A.Area) CArea,RTRIM(Thanas.Thana) CPoliceStation,RTRIM(PostOffice.PostCode) CPSCode,RTRIM(Districts.District) CDistrict,RTRIM(A.ContactNo) CContactNo,RTRIM(B.FlatNo) TFlatNo,RTRIM(B.HouseNo) THouseNo,RTRIM(B.RoadNo) TRoadNo,RTRIM(B.Block) TBlock,RTRIM(B.Area) TArea,RTRIM(Thanas.Thana) TPoliceStation ,RTRIM(PostOffice.PostCode) TpostCode,RTRIM(Districts.District) TDistrict,RTRIM(B.ContactNo) TContactNo  FROM Addresses A INNER JOIN Addresses B on A.IClientId=B.IClientId INNER JOIN Divisions ON A.Division_ID = Divisions.Division_ID  INNER JOIN Districts ON A.D_ID = Districts.D_ID  INNER JOIN Thanas ON A.T_ID = Thanas.T_ID  INNER JOIN PostOffice ON A.PostOfficeId = PostOffice.PostOfficeId INNER JOIN SalesClient ON A.SClientId = SalesClient.SClientId INNER JOIN ClientTypes ON SalesClient.ClientTypeId = ClientTypes.ClientTypeId  INNER JOIN ContactPersonDetails ON SalesClient.IClientId = ContactPersonDetails.IClientId  INNER JOIN IndustryCategorys ON SalesClient.IndustryCategoryId = IndustryCategorys.IndustryCategoryId  INNER JOIN NatureOfClients ON SalesClient.NatureOfClientId = NatureOfClients.NatureOfClientId  where A.ADTypeId = 1  and B.ADTypeId = 2", con);
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
        //    try
        //    {
        //        DataGridViewRow dr = dataGridView1.SelectedRows[0];
        //        this.Hide();
        //        ClientApprovedFinalForm frm = new ClientApprovedFinalForm();
        //        frm.Show();

        //        frm.txtIClientId.Text = dr.Cells[0].Value.ToString();
        //        frm.clientNameAPTextBox.Text = dr.Cells[1].Value.ToString();
        //        frm.clientTypeAPTextBox.Text = dr.Cells[2].Value.ToString();
        //        frm.txtAPNatureOfClient.Text = dr.Cells[3].Value.ToString();
        //        frm.emailAddressAPTextBox.Text = dr.Cells[4].Value.ToString();
        //        frm.txtAPIndustryCategory.Text = dr.Cells[5].Value.ToString();
        //        frm.contactPersonNameAPTextBox.Text = dr.Cells[6].Value.ToString();
        //        frm.designationAPTextBox.Text = dr.Cells[7].Value.ToString();
        //        frm.cellNumberAPTextBox.Text = dr.Cells[8].Value.ToString();
        //        frm.endUserAPTextBox.Text = dr.Cells[9].Value.ToString();

        //        frm.cFlatNoTextBox.Text = dr.Cells[10].Value.ToString();
        //        frm.cHouseNoTextBox.Text = dr.Cells[11].Value.ToString();
        //        frm.cRoadNoTextBox.Text = dr.Cells[12].Value.ToString();
        //        frm.cBlockTextBox.Text = dr.Cells[13].Value.ToString();
        //        frm.cAreaTextBox.Text = dr.Cells[14].Value.ToString();
        //        frm.cThanaCombo.Text = dr.Cells[15].Value.ToString();
        //        frm.cPostCodeTextBox.Text = dr.Cells[16].Value.ToString();
        //        frm.cDistrictCombo.Text = dr.Cells[17].Value.ToString();
        //        frm.cContactNoTextBox.Text = dr.Cells[18].Value.ToString();

        //        frm.tFlatNoTextBox.Text = dr.Cells[19].Value.ToString();
        //        frm.tHouseNoTextBox.Text = dr.Cells[20].Value.ToString();
        //        frm.tRoadNoTextBox.Text = dr.Cells[21].Value.ToString();
        //        frm.tBlockTextBox.Text = dr.Cells[22].Value.ToString();
        //        frm.tAreaTextBox.Text = dr.Cells[23].Value.ToString();
        //        frm.cThanaCombo.Text = dr.Cells[24].Value.ToString();
        //        frm.tPostCodeTextBox.Text = dr.Cells[25].Value.ToString();
        //        frm.tDistComboBox.Text = dr.Cells[26].Value.ToString();
        //        frm.tContactNoTextBox.Text = dr.Cells[27].Value.ToString();
        //        frm.lk.Text = lg.Text;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
             MainUI frm=new MainUI();
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
                frm.clientTypeAPTextBox.Text = dr.Cells[2].Value.ToString();
                frm.txtAPNatureOfClient.Text = dr.Cells[3].Value.ToString();
                frm.emailAddressAPTextBox.Text = dr.Cells[4].Value.ToString();
                frm.txtAPIndustryCategory.Text = dr.Cells[5].Value.ToString();
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
        
    }
}
