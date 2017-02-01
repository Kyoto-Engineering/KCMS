using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class ContactPersonDetails
   {
        private int iCClientId;
        private string contactPersonName;
        private string designation;
        private string cellNumber;
        private string cPEmailAddress;
        

       public int ICClientId
       {
           set { iCClientId = value; }
           get { return iCClientId; }
       }
        public string ContactPersonName
        {
            set { contactPersonName = value; }
            get { return contactPersonName; }
        }

        public string Designation
        {
            set { designation = value; }
            get { return designation; }
        }

        public string CellNumber
        {
            set { cellNumber = value; }
            get { return cellNumber; }
        }

        public string CPEmailAddress
        {
            set { cPEmailAddress = value; }
            get { return cPEmailAddress; }
        }
       
    }
}
