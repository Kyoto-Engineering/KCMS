using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DBGateway
{
  public   class ConnectionString
    {
      public string DBConn = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=NewProductList66;User=sa;Password=SystemAdministrator;Persist Security Info=True";
     // public string DBConn = @"Data Source=DESKTOP-TQ74LPH\SQLSERVER2018;Initial Catalog=NewProductList;User=sa;Password=SystemAdministrator;Persist Security Info=True";
      public string DBConn2 = @"Data Source=DESKTOP-TQ74LPH\SQLSERVER2018;Initial Catalog=POSDb;User=sa;Password=SystemAdministrator;Persist Security Info=True";
    }
}
