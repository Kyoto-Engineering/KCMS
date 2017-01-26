using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class Addresss
   {
        private  int aDId;
       private int iClientId;
        

        
       public int ADId
       {
           get { return aDId; }
           set { aDId = value; }
       }
       public int  IClientId
       {
           set { iClientId = value; }
           get { return iClientId; }
       }

        

       

    }
}
