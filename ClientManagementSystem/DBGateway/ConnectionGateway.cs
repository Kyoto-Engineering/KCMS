using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DBGateway
{
   public  class ConnectionGateway
   {
       protected SqlConnection connection;

       public ConnectionGateway()
       {
           string connectionString = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=NewProductList66;User=sa;Password=SystemAdministrator;Persist Security Info=True;";
           // string connectionString = @"server=KYOTO-PC1\SQLSERVER2014; Integrated Security = SSPI; database =EmployeeMSDb";
           // string connectionString = @"KYOTOPC-7\SQLSERVER1416;database =EmployeeMSDb;Integrated Security = true;";
           //string connectionString = @"server=KYOTO-PC06\SQLSERVER2014; Integrated Security = SSPI; database =NewProductList;Connect Timeout=30";
           // string connectionString = @"server=DESKTOP-TQ74LPH\SQLSERVER2018;Initial Catalog=NewProductList;User=sa;Password=SystemAdministrator;Persist Security Info=True;";

           connection = new SqlConnection(connectionString);
       }
    }
}
