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
    public partial class GridForSalesClient : Form
    {
        private SqlConnection con;
        ConnectionString cs=new ConnectionString();
        public GridForSalesClient()
        {
            InitializeComponent();
        }

        private void GridForSalesClient_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string selectQuery = "Select FirstSet.Name RM,FirstSet.SClientId ClientId,FirstSet.ClientName ClientName,FirstSet.ClientType  ClientType,FirstSet.ClientNature NatureOfClient,FirstSet.EmailAddress EmailId,FirstSet.IndustryCategory ,FirstSet.EndUser,thirdq.ContactPersonName,thirdq.Designation, thirdq.CellNumber,thirdq.EmailId,FirstSet.CFlatNo,FirstSet.CHouseNo,FirstSet.CRoadNo,FirstSet.CBlock,FirstSet.CArea,FirstSet.CContactNo,FirstSet.Division CDivition,FirstSet.District CDistrict,FirstSet.Thana CPoliceStation,FirstSet.PostOfficeName CPostOfficeName,FirstSet.PostCode CPostCode,QUERYTWO.TFlatNo,QUERYTWO.THouseNo,QUERYTWO.TRoadNo,QUERYTWO.TBlock,QUERYTWO.TArea,QUERYTWO.TContactNo,QUERYTWO.Division TDivision,QUERYTWO.District TDistrict,QUERYTWO.Thana TThana,QUERYTWO.PostOfficeName TPostOfficeName,QUERYTWO.PostCode TPostCode,QUERY3.BFlatNo,QUERY3.BHouseNo,QUERY3.BRoadNo,QUERY3.BBlock,QUERY3.BArea,QUERY3.BContactNo,QUERY3.Division BDivision,QUERY3.District BDistrict,QUERY3.Thana BThana,QUERY3.PostOfficeName BPostOfficeName,QUERY3.PostCode BPostCode from (SELECT Registration.Name,SalesClient.SClientId,SalesClient.ClientName,ClientTypes.ClientType,NatureOfClients.ClientNature,SalesClient.EmailAddress,IndustryCategorys.IndustryCategory,SalesClient.EndUser,CorporateAddresses.CFlatNo,CorporateAddresses.CHouseNo,CorporateAddresses.CRoadNo,CorporateAddresses.CBlock,CorporateAddresses.CArea,CorporateAddresses.CContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode  FROM  SalesClient  INNER JOIN  Registration ON SalesClient.SuperviserId = Registration.UserId  INNER JOIN ClientTypes ON SalesClient.ClientTypeId = ClientTypes.ClientTypeId  INNER JOIN NatureOfClients ON SalesClient.NatureOfClientId = NatureOfClients.NatureOfClientId  INNER JOIN IndustryCategorys ON SalesClient.IndustryCategoryId = IndustryCategorys.IndustryCategoryId  INNER JOIN CorporateAddresses ON SalesClient.SClientId = CorporateAddresses.SClientId  INNER JOIN  Divisions ON CorporateAddresses.Division_ID = Divisions.Division_ID  INNER JOIN Districts ON CorporateAddresses.D_ID = Districts.D_ID  INNER JOIN Thanas ON CorporateAddresses.T_ID = Thanas.T_ID  INNER JOIN PostOffice ON CorporateAddresses.PostOfficeId = PostOffice.PostOfficeId ) AS FirstSet lEFT jOIN (SELECT SalesClient.SClientId,TraddingAddresses.TFlatNo,TraddingAddresses.THouseNo,TraddingAddresses.TRoadNo,TraddingAddresses.TBlock,TraddingAddresses.TArea,TraddingAddresses.TContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode  FROM  SalesClient  INNER JOIN TraddingAddresses ON SalesClient.SClientId = TraddingAddresses.SClientId  INNER JOIN  Divisions ON TraddingAddresses.Division_ID = Divisions.Division_ID  INNER JOIN Districts ON TraddingAddresses.D_ID = Districts.D_ID  INNER JOIN Thanas ON TraddingAddresses.T_ID = Thanas.T_ID  INNER JOIN PostOffice ON TraddingAddresses.PostOfficeId = PostOffice.PostOfficeId) aS QUERYTWO ON FirstSet.SClientId =  QUERYTWO.SClientId  lEFT jOIN (SELECT SalesClient.SClientId,BA.BFlatNo,BA.BHouseNo,BA.BRoadNo,BA.BBlock,BA.BArea,BA.BContactNo,Divisions.Division,Districts.District,Thanas.Thana,PostOffice.PostOfficeName,PostOffice.PostCode  FROM  SalesClient  INNER JOIN BillingAddresses AS BA ON SalesClient.SClientId = BA.SClientId  INNER JOIN  Divisions ON BA.Division_ID = Divisions.Division_ID  INNER JOIN Districts ON BA.D_ID = Districts.D_ID  INNER JOIN Thanas ON BA.T_ID = Thanas.T_ID  INNER JOIN PostOffice ON BA.PostOfficeId = PostOffice.PostOfficeId) aS QUERY3 ON FirstSet.SClientId =  QUERY3.SClientId left join (SELECT SalesClient.SClientId,ContactPersonDetails.ContactPersonName,ContactPersonDetails.Designation,ContactPersonDetails.CellNumber,ContactPersonDetails.EmailId  FROM  SalesClient  INNER JOIN ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId) as thirdq on FirstSet.SClientId  = thirdq.SClientId";           
            SqlDataAdapter myadapter = new SqlDataAdapter(selectQuery, con);
            DataTable dt = new DataTable();
            myadapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ForSalseClientMP am=new ForSalseClientMP();
            am.ShowDialog();
        }
    }
}
