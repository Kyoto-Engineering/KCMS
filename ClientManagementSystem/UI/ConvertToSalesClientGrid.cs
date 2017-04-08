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
        private SqlDataAdapter sda;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public ConvertToSalesClientGrid()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string a, b;
                this.Hide();
                DataGridViewRow dr = dataGridView1.CurrentRow;
                ClientApprovedFinalForm frm = new ClientApprovedFinalForm();
                frm.Show();

                frm.cmbSuperviserName.Text = dr.Cells[0].Value.ToString();
                frm.txtIClientId.Text = dr.Cells[1].Value.ToString();
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
                //Changed by Runju
                frm.cContactNoTextBox.Text = dr.Cells[17].Value.ToString();
                frm.cAreaTextBox.Text = dr.Cells[16].Value.ToString();

                frm.cDivisionCombo.Text = dr.Cells[18].Value.ToString();
                frm.cDistrictCombo.Text = dr.Cells[19].Value.ToString();
                frm.cThanaCombo.Text = dr.Cells[20].Value.ToString().Trim();
                frm.cPostOfficeCombo.Text = dr.Cells[21].Value.ToString();
               a= frm.cPostCodeTextBox.Text = dr.Cells[22].Value.ToString();


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
               b= frm.tPostCodeTextBox.Text = dr.Cells[33].Value.ToString();
                if (a == b)
                {
                   
                    frm.tASameAsCACheckBox.Checked = true;
                }
                else if (string.IsNullOrEmpty(b))
                {

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select RTRIM(TraddingAddresses.IClientId) from TraddingAddresses where TraddingAddresses.IClientId='" + frm.txtIClientId.Text + "'";
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

                frm.lk.Text = lg.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InquiryClientDetailsGrid()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            sda = new SqlDataAdapter("Select FirstSet.Name RM,FirstSet.IClientId ClientId,FirstSet.ClientName ClientName,FirstSet.ClientType  ClientType,FirstSet.ClientNature NatureOfClient,FirstSet.Email EmailId,FirstSet.IndustryCategory ,FirstSet.EndUser,thirdq.ContactPersonName,thirdq.Designation, thirdq.CellNumber,thirdq.Email,FirstSet.CFlatNo,FirstSet.CHouseNo,FirstSet.CRoadNo,FirstSet.CBlock,FirstSet.CArea,FirstSet.CContactNo,FirstSet.Division CDivition,FirstSet.District CDistrict,FirstSet.Thana CPoliceStation,FirstSet.PostOfficeName CPostOfficeName,FirstSet.PostCode CPostCode,QUERYTWO.TFlatNo,QUERYTWO.THouseNo,QUERYTWO.TRoadNo,QUERYTWO.TBlock,QUERYTWO.TArea,QUERYTWO.TContactNo,QUERYTWO.Division TDivision,QUERYTWO.District TDistrict,QUERYTWO.Thana TThana,QUERYTWO.PostOfficeName TPostOfficeName,QUERYTWO.PostCode TPostCode from (SELECT Registration.Name,InquieryClient.IClientId,InquieryClient.ClientName,ClientTypes.ClientType,NatureOfClients.ClientNature,EmailBank.Email,IndustryCategorys.IndustryCategory,InquieryClient.EndUser,CorporateAddresses.CFlatNo,CorporateAddresses.CHouseNo,CorporateAddresses.CRoadNo,CorporateAddresses.CBlock,CorporateAddresses.CArea,CorporateAddresses.CContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode  FROM  InquieryClient  INNER JOIN  Registration ON InquieryClient.SuperviserId = Registration.UserId INNER JOIN ClientTypes ON InquieryClient.ClientTypeId = ClientTypes.ClientTypeId  INNER JOIN NatureOfClients ON InquieryClient.NatureOfClientId = NatureOfClients.NatureOfClientId  INNER JOIN IndustryCategorys ON InquieryClient.IndustryCategoryId = IndustryCategorys.IndustryCategoryId INNER JOIN CorporateAddresses ON InquieryClient.IClientId = CorporateAddresses.IClientId  INNER JOIN PostOffice ON CorporateAddresses.PostOfficeId = PostOffice.PostOfficeId  INNER JOIN Thanas ON PostOffice.T_ID = Thanas.T_ID  INNER JOIN Districts ON Thanas.D_ID = Districts.D_ID  INNER JOIN  Divisions ON Districts.Division_ID = Divisions.Division_ID  Left Join EmailBank ON InquieryClient.EmailBankId= EmailBank.EmailBankId) AS  FirstSet  lEFT jOIN (SELECT InquieryClient.IClientId,TraddingAddresses.TFlatNo,TraddingAddresses.THouseNo,TraddingAddresses.TRoadNo,TraddingAddresses.TBlock,TraddingAddresses.TArea,TraddingAddresses.TContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode   FROM  InquieryClient  INNER JOIN TraddingAddresses ON InquieryClient.IClientId = TraddingAddresses.IClientId INNER JOIN PostOffice ON TraddingAddresses.PostOfficeId = PostOffice.PostOfficeId  INNER JOIN Thanas ON PostOffice.T_ID = Thanas.T_ID  INNER JOIN Districts ON Thanas.D_ID = Districts.D_ID INNER JOIN  Divisions ON Districts.Division_ID = Divisions.Division_ID) aS QUERYTWO ON FirstSet.IClientId =  QUERYTWO.IClientId left join (SELECT InquieryClient.IClientId,ContactPersonDetails.ContactPersonName,ContactPersonDetails.Designation,ContactPersonDetails.CellNumber,EmailBank.Email  FROM  InquieryClient  INNER JOIN ContactPersonDetails ON InquieryClient.IClientId = ContactPersonDetails.IClientId left join EmailBank on ContactPersonDetails.EmailBankId=EmailBank.EmailBankId) as thirdq on FirstSet.IClientId  = thirdq.IClientId where FirstSet.IClientId not in (select  IClientId  FROM  SalesClient where IClientId is not null)", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void ConvertToSalesClientGrid_Load(object sender, EventArgs e)
        {
           InquiryClientDetailsGrid();
        }

        private void ConvertToSalesClientGrid_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            ForSalseClientMP frm = new ForSalseClientMP();
            frm.Show();
        }
    }
}
