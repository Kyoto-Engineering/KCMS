using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagementSystem.DAO;
using ClientManagementSystem.DBGateway;

namespace ClientManagementSystem.Gateway
{
   public class UserGateway:ConnectionGateway
    {
       public int SaveUser(User aUser)
       {
           connection.Open();
           string insertquery = " insert into Registration(Username,Usertype,Password,Name,Email,Designation,Department,ContactNo) Values(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";

           SqlCommand cmd =new SqlCommand(insertquery,connection);
           cmd.Parameters.AddWithValue("@d1", aUser.UserName);
           cmd.Parameters.AddWithValue("@d2", aUser.UserType);
           cmd.Parameters.AddWithValue("@d3", aUser.Password);
           cmd.Parameters.AddWithValue("@d4", aUser.Name);
           cmd.Parameters.AddWithValue("@d5", aUser.Email);
           cmd.Parameters.AddWithValue("@d6", aUser.Designation);
           cmd.Parameters.AddWithValue("@d7", aUser.Department);
           cmd.Parameters.AddWithValue("@d8", aUser.ContactNo);
           int affectedrows = cmd.ExecuteNonQuery();
           connection.Close();
           return affectedrows;
       }
    }
}
