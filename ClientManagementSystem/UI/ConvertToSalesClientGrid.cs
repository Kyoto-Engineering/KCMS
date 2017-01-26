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

namespace ClientManagementSystem.UI
{
    public partial class ConvertToSalesClientGrid : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        private SqlDataAdapter sda;
        ConnectionString cs = new ConnectionString();
        public ConvertToSalesClientGrid()
        {
            InitializeComponent();
        }
        private void InquiryClientDetailsGrid()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            //string query = "SELECT RTRIM(InquieryClient.ClientName),RTRIM(ClientTypes.ClientType),RTRIM(InquieryClient.EmailAddress),RTRIM(IndustryCategorys.IndustryCategory),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.Designation),RTRIM(ContactPersonDetails.CellNumber),RTRIM(InquieryClient.EndUser) FROM InquieryClient,ClientTypes,NatureOfClients,IndustryCategorys,ContactPersonDetails WHERE InquieryClient.ClientTypeId=ClientTypes.ClientTypeId and InquieryClient.NatureOfClientId=NatureOfClients.NatureOfClientId  and InquieryClient.IndustryCategoryId=IndustryCategorys.IndustryCategoryId and InquieryClient.IClientId=ContactPersonDetails.IClientId and InquieryClient.IClientId=";
            sda = new SqlDataAdapter("SELECT RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName) ClientName,RTRIM(ClientTypes.ClientType) ClientType,RTRIM(NatureOfClients.ClientNature) NatureOfClient,RTRIM(InquieryClient.EmailAddress) EmailAddress,RTRIM(IndustryCategorys.IndustryCategory) IndustryCategory,RTRIM(ContactPersonDetails.ContactPersonName) ContactPersonName,RTRIM(ContactPersonDetails.Designation) Designation,RTRIM(ContactPersonDetails.CellNumber) CellNumber,RTRIM(InquieryClient.EndUser) EndUser,RTRIM(A.FlatNo) CFlatNo,RTRIM(A.HouseNo) CHouseNo,RTRIM(A.RoadNo) CRoadNo,RTRIM(A.Block) CBlock,RTRIM(A.Area) CArea,RTRIM(Thanas.Thana) CPoliceStation,RTRIM(PostOffice.PostCode) CPSCode,RTRIM(Districts.District) CDistrict,RTRIM(A.ContactNo) CContactNo,RTRIM(B.FlatNo) TFlatNo,RTRIM(B.HouseNo) THouseNo,RTRIM(B.RoadNo) TRoadNo,RTRIM(B.Block) TBlock,RTRIM(B.Area) TArea,RTRIM(Thanas.Thana) TPoliceStation ,RTRIM(PostOffice.PostCode) TpostCode,RTRIM(Districts.District) TDistrict,RTRIM(B.ContactNo) TContactNo  FROM Addresses A INNER JOIN Addresses B on A.IClientId=B.IClientId  INNER JOIN InquieryClient ON A.IClientId = InquieryClient.IClientId INNER JOIN Divisions ON A.Division_ID = Divisions.Division_ID INNER JOIN Districts ON A.D_ID = Districts.D_ID INNER JOIN Thanas ON A.T_ID = Thanas.T_ID INNER JOIN PostOffice ON A.PostOfficeId = PostOffice.PostOfficeId INNER JOIN ClientTypes ON InquieryClient.ClientTypeId = ClientTypes.ClientTypeId INNER JOIN ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId INNER JOIN IndustryCategorys ON InquieryClient.IndustryCategoryId = IndustryCategorys.IndustryCategoryId INNER JOIN NatureOfClients ON InquieryClient.NatureOfClientId = NatureOfClients.NatureOfClientId where (A.ADTypeId = 1)  and (B.ADTypeId = 2)", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void ConvertToSalesClientGrid_Load(object sender, EventArgs e)
        {
            InquiryClientDetailsGrid();
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainUIInquieryClient frm=new MainUIInquieryClient();
            frm.Show();
        }
    }
}
