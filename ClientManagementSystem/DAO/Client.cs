using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class Client
   {
       private decimal clientId;
       private string clientName;
       private string clientType;
       private string emailAddress;
       private string corporateAddress;
       private string billingAddress;
       private string traddingAddress;
       private string natureOfClient;
       private string bankName;
       private string branchName;
       private string accountNumber;
       private string contactPersonName;
       private string designation;
       private string cellNumber;
       private string  endUser;


       public decimal ClientId
       {
           set { clientId = value; }
           get { return clientId; }
       }

       public string ClientName
       {
           set { clientName = value; }
           get { return clientName; }
       }

       public string ClientType
       {
           get { return clientType; }
           set { clientType = value; }
       }

       public string EmailAddress
       {
           get { return emailAddress; }
           set { emailAddress = value; }
       }

       public string CorporateAddress
       {
           get { return corporateAddress; }
           set { corporateAddress = value; }
       }

       public string BillingAddress
       {
           get { return billingAddress; }
           set { billingAddress = value; }
       }

       public string TraddingAddress
       {
           set { traddingAddress = value; }
           get { return traddingAddress; }
       }

       public string NatureOfClient
       {
           get { return natureOfClient; }
           set { natureOfClient = value; }
       }

       public string BankName
       {
           get { return bankName; }
           set { bankName = value; }
       }

       public string BranchName
       {
           get { return branchName; }
           set { branchName = value; }
       }

       public string AccountNumber
       {
           get { return accountNumber; }
           set { accountNumber = value; }
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

       public string EndUser
       {
           get { return endUser; }
           set { endUser = value; }
       }
   }
}
