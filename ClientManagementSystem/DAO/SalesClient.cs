using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
  public   class SalesClient
  {
      private decimal sClientId;
      private string sClientName;
      private string clientType;
      private string natureOfClient;
      private string emailAddress;
      private string industryCatagory;

      


     





      private string sContactPersonName;
      private string designation;
      private string sCellNumber;
      private string endUser;
      private decimal iClientId;

     

      private string bankName;
      private string branchName;
      private string accountNumber;



      public decimal SClientId
      {
          get { return sClientId; }
          set { sClientId = value; }
      }
      public string SClientName
      {
          set { sClientName = value; }
          get { return sClientName; }
      }

      public string ClientType
      {
          get { return clientType; }
          set { clientType = value; }
      }
      public string NatureOfClient
      {
          get { return natureOfClient; }
          set { natureOfClient = value; }
      }

      public string EmailAddress
      {
          get { return emailAddress; }
          set { emailAddress = value; }
      }

      public string IndustryCatagory
      {
          get { return industryCatagory; }
          set { industryCatagory = value; }
      }

     

      public string SContactPersonName
      {
          set { sContactPersonName = value; }
          get { return sContactPersonName; }
      }

      public string Designation
      {
          set { designation = value; }
          get { return designation; }
      }

      public string SCellNumber
      {
          set { sCellNumber = value; }
          get { return sCellNumber; }
      }

      public string EndUser
      {
          get { return endUser; }
          set { endUser = value; }
      }
      public decimal IClientId
      {
          set { iClientId = value; }
          get { return iClientId; }
      }

     
     

     

      public string BankName
      {
          set { bankName = value; } 
          get { return bankName; }
      }

      public string BranchName
      {
          set { branchName = value; }
          get { return branchName; }
      }

      public string AccountNumber
      {
          get { return accountNumber; }
          set { accountNumber = value; }
      }


  }
}
