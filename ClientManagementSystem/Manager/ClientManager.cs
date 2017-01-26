using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DAO;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.Gateway;

namespace ClientManagementSystem.Manager
{
   public  class ClientManager:ConnectionGateway
   {
       private ClientGateway aClientGateway;

       public int SaveClient(InqueryClient aClient)
       {
           
           aClientGateway=new ClientGateway();
          return aClientGateway.SaveClient(aClient);
       }
       public int SaveClient2(InqueryClient aClient)
       {
           
           aClientGateway = new ClientGateway();
           return aClientGateway.SaveClient2(aClient);
       }

      

     public  int  SaveInstantClient(InstantClient anInstantClient)
      {
          aClientGateway=new ClientGateway();
          return aClientGateway.SaveInstantClient(anInstantClient);
      }
   }
}
