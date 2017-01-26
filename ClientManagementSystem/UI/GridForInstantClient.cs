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
    public partial class GridForInstantClient : Form
    {
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        ConnectionString cs =new ConnectionString();
        public GridForInstantClient()
        {
            InitializeComponent();
        }

        private void GridForInstantClient_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string selectQuery = "Select top 1000 ClientName,InstantClientId as ClientId,NatureOfClient,EmailAddress,IndustryCatagory,ContactPersonName,Designation,CellNumber,EndUser from InstantsClient order by InstantClientId desc";
            //string selectQuery = "Select * from Salary";
            //string selectQuery = "Select  Months, Year,EmployeeId, EmployeeName,Designation,TotalworkingDay,DayPayable,LeaveWithPay,UnpaidLeave,Basic, TransportAllowance,MedicalAllowance,HouseRent,OtherAllowance,GrossSalary,Tax,DeductionForLeave,Fine,DeductionFromAnnualLeave,Adjustment,AdvancePaid,TotalDeduction,PreviousDues,TotalAddition,NetPayable from Salary";
            SqlDataAdapter myadapter = new SqlDataAdapter(selectQuery, con);
            DataTable dt = new DataTable();
            myadapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForSalseClientMP mp=new ForSalseClientMP();
            mp.Show();
        }
    }
}
