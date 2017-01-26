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
    public partial class MainUIInquieryClient : Form
    {
        private SqlConnection con;
        ConnectionString cs =new ConnectionString();
        private SqlCommand cmd;
        private SqlDataReader rdr;
        private SqlDataAdapter sda;
        public MainUIInquieryClient()
        {
            InitializeComponent();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientRegistrationForm frm =new ClientRegistrationForm();
            frm.Show();
            
        }
        private void InquiryClientDetailsGrid()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            //string query = "SELECT RTRIM(InquieryClient.ClientName),RTRIM(ClientTypes.ClientType),RTRIM(InquieryClient.EmailAddress),RTRIM(IndustryCategorys.IndustryCategory),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.Designation),RTRIM(ContactPersonDetails.CellNumber),RTRIM(InquieryClient.EndUser) FROM InquieryClient,ClientTypes,NatureOfClients,IndustryCategorys,ContactPersonDetails WHERE InquieryClient.ClientTypeId=ClientTypes.ClientTypeId and InquieryClient.NatureOfClientId=NatureOfClients.NatureOfClientId  and InquieryClient.IndustryCategoryId=IndustryCategorys.IndustryCategoryId and InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.IClientId=";
            sda = new SqlDataAdapter("SELECT RTRIM(InquieryClient.IClientId) ClientId,RTRIM(InquieryClient.ClientName) ClientName,RTRIM(ClientTypes.ClientType) ClientType,RTRIM(NatureOfClients.ClientNature) NatureOfClient,RTRIM(InquieryClient.EmailAddress) EmailAddress,RTRIM(IndustryCategorys.IndustryCategory) IndustryCategory,RTRIM(ContactPersonDetails.ContactPersonName) ContactPersonName,RTRIM(ContactPersonDetails.Designation) Designation,RTRIM(ContactPersonDetails.CellNumber) CellNumber,RTRIM(InquieryClient.EndUser) EndUser,RTRIM(A.FlatNo) CFlatNo,RTRIM(A.HouseNo) CHouseNo,RTRIM(A.RoadNo) CRoadNo,RTRIM(A.Block) CBlock,RTRIM(A.Area) CArea,RTRIM(Thanas.Thana) CPoliceStation,RTRIM(PostOffice.PostOfficeName) CPostCode,RTRIM(Districts.District) CDistrict,RTRIM(A.ContactNo) CContactNo,RTRIM(B.FlatNo) TFlatNo,RTRIM(B.HouseNo) THouseNo,RTRIM(B.RoadNo) TRoadNo,RTRIM(B.Block) TBlock,RTRIM(B.Area) TArea,RTRIM(Thanas.Thana) TPoliceStation ,RTRIM(PostOffice.PostCode) TpostCode,RTRIM(Districts.District) TDistrict,RTRIM(B.ContactNo) TContactNo FROM Addresses A INNER JOIN Addresses B on A.IClientId=B.IClientId  INNER JOIN Divisions ON A.Division_ID = Divisions.Division_ID INNER JOIN Districts ON A.D_ID = Districts.D_ID INNER JOIN Thanas ON A.T_ID = Thanas.D_ID INNER JOIN PostOffice ON A.PostOfficeId = PostOffice.PostOfficeId  INNER JOIN InquieryClient ON A.IClientId = InquieryClient.IClientId  INNER JOIN ClientTypes ON InquieryClient.ClientTypeId = ClientTypes.ClientTypeId INNER JOIN ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId  INNER JOIN IndustryCategorys ON InquieryClient.IndustryCategoryId = IndustryCategorys.IndustryCategoryId  INNER JOIN NatureOfClients ON InquieryClient.NatureOfClientId = NatureOfClients.NatureOfClientId  where A.ADTypeId = 1  and B.ADTypeId = 2", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void MainUIInquieryClient_Load(object sender, EventArgs e)
        {
            InquiryClientDetailsGrid();


        }

        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(InquieryClient.ClientType),RTRIM(InquieryClient.NatureOfClient),RTRIM(InquieryClient.EmailAddress),RTRIM(InquieryClient.IndustryCatagory),RTRIM(InquieryClient.CFlatNo),RTRIM(InquieryClient.CHouseNo),RTRIM(InquieryClient.CRoadNo),RTRIM(InquieryClient.CBlock),RTRIM(InquieryClient.CArea),RTRIM(InquieryClient.CPS),RTRIM(InquieryClient.CPSCode),RTRIM(InquieryClient.CDistrict),RTRIM(InquieryClient.CContactNo),RTRIM(InquieryClient.TFlatNo),RTRIM(InquieryClient.THouseNo),RTRIM(InquieryClient.TRoadNo),RTRIM(InquieryClient.TBlock),RTRIM(InquieryClient.TArea),RTRIM(InquieryClient.TPS),RTRIM(InquieryClient.TPSCode),RTRIM(InquieryClient.TDistrict),RTRIM(InquieryClient.TContactNo),RTRIM(InquieryClient.ContactPersonName),RTRIM(InquieryClient.Designation),RTRIM(InquieryClient.CellNumber),RTRIM(InquieryClient.EndUser) from InquieryClient order by InquieryClient.IClientId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read()==true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4], rdr[5], rdr[6], rdr[7], rdr[8], rdr[9], rdr[10], rdr[11],rdr[12],rdr[13],rdr[14],rdr[15],rdr[16],rdr[17],rdr[18],rdr[19],rdr[20],rdr[21],rdr[22],rdr[23],rdr[24],rdr[25],rdr[26],rdr[27]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            MainUI aMainUi=new MainUI();
            aMainUi.ShowDialog();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            ClientEditForm afForm = new ClientEditForm();
            this.Visible = false;

            afForm.ShowDialog();
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
            ListofClientContactPoints cr = new ListofClientContactPoints();
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
            IndustryCategory cr = new IndustryCategory();
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

        private void button3_Click(object sender, EventArgs e)
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
            InquiryClientProfile cr = new InquiryClientProfile();
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15,
                e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.CurrentRow;
                this.Hide();
                EditFromGrid frm = new EditFromGrid();
                frm.Show();
                frm.txtClientId.Text = dr.Cells[0].Value.ToString();
                frm.txtClientName.Text = dr.Cells[1].Value.ToString();
                //frm.txtClientType.Text = dr.Cells[2].Value.ToString();
                //frm.txtNatureOfClient.Text = dr.Cells[3].Value.ToString();
                //frm.txtEmailAddress.Text = dr.Cells[4].Value.ToString();
                //frm.txtInduxtryCategory.Text = dr.Cells[5].Value.ToString();

                frm.cFlatNoTextBox.Text = dr.Cells[6].Value.ToString();
                frm.cHouseNoTextBox.Text = dr.Cells[7].Value.ToString();
                frm.cRoadNoTextBox.Text = dr.Cells[8].Value.ToString();
                frm.cBlockTextBox.Text = dr.Cells[9].Value.ToString();
                frm.cAreaTextBox.Text = dr.Cells[10].Value.ToString();
                frm.cPostCodeTextBox.Text = dr.Cells[11].Value.ToString();
                frm.cThanaCombo.Text = dr.Cells[12].Value.ToString();
                frm.cDistCombo.Text = dr.Cells[13].Value.ToString();
                frm.cContactNoTextBox.Text = dr.Cells[14].Value.ToString();

                frm.tFlatNoTextBox.Text = dr.Cells[15].Value.ToString();
                frm.tHouseNoTextBox.Text = dr.Cells[16].Value.ToString();
                frm.tRoadNoTextBox.Text = dr.Cells[17].Value.ToString();
                frm.tBlockTextBox.Text = dr.Cells[18].Value.ToString();
                frm.tAreaTextBox.Text = dr.Cells[19].Value.ToString();
                frm.tPostCodeTextBox.Text = dr.Cells[20].Value.ToString();
                frm.tThanaCombo.Text = dr.Cells[21].Value.ToString();
                frm.tDistCombo.Text = dr.Cells[22].Value.ToString();
                frm.tContactNoTextBox.Text = dr.Cells[23].Value.ToString();

                frm.txtCPName.Text = dr.Cells[24].Value.ToString();
                frm.txtDesignation.Text = dr.Cells[25].Value.ToString();
                frm.cellPhoneTextBox.Text = dr.Cells[26].Value.ToString();
                frm.txtEndUser.Text = dr.Cells[27].Value.ToString();



                frm.labeld.Text = labelh.Text;


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainUI frm=new MainUI();
             frm.Show();
        }

        private void convertToSalesClientButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConvertToSalesClientGrid frm=new ConvertToSalesClientGrid();
            frm.Show();
        }

    }
}
