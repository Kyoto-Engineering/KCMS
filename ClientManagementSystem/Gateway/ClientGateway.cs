﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DAO;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.UI;

namespace ClientManagementSystem.Gateway
{
   public  class ClientGateway:ConnectionGateway
   {
       
      
       public void UpdateContactPersonDetails(ContactPersonDetails contact)
       {
          InqueryClient iClientId=new InqueryClient();
          connection.Open();
          string UpdateQuery = "Update ContactPersonDetails set ContactPersonName=@d1,Designation=@d2,EmailId=@d3,CellNumber=@d4 Where IClientId='" + iClientId.IClientId + "'";
          SqlCommand cmd = new SqlCommand(UpdateQuery, connection);
          cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(contact.ContactPersonName) ? (object)DBNull.Value : contact.ContactPersonName));
          cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(contact.Designation) ? (object)DBNull.Value : contact.Designation));
          cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(contact.CellNumber) ? (object)DBNull.Value : contact.CellNumber));
          cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(contact.CPEmailAddress) ? (object)DBNull.Value : contact.CPEmailAddress));                  
          cmd.ExecuteReader();
          connection.Close();
         


       }
       public void UpdateCorporatAddress(CorporateAddress cAdd)
       {
           InqueryClient aClient = new InqueryClient();
           connection.Open();
           string UpdateQuery = "Update CorporateAddresses set Division_ID=@d1,D_ID=@d2,T_ID=@d3,PostOfficeId=@d4,CFlatNo=@d5,CHouseNo=@d6,CRoadNo=@d7,CBlock=@d8,CArea=@d9,CContactNo=@d10  Where  CorporateAddresses.IClientId='" + aClient.IClientId + "'";
           SqlCommand cmd = new SqlCommand(UpdateQuery, connection);           
           cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(cAdd.DivisionId.ToString()) ? (object)DBNull.Value : cAdd.DivisionId));
           cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(cAdd.DistrictId.ToString()) ? (object)DBNull.Value : cAdd.DistrictId));
           cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(cAdd.ThanaId.ToString()) ? (object)DBNull.Value : cAdd.ThanaId));
           cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(cAdd.PostOfficeId.ToString()) ? (object)DBNull.Value : cAdd.PostOfficeId));
           cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(cAdd.CFlatNo) ? (object)DBNull.Value : cAdd.CFlatNo));
           cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(cAdd.CHouseNo) ? (object)DBNull.Value : cAdd.CHouseNo));
           cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(cAdd.CRoadNo) ? (object)DBNull.Value : cAdd.CRoadNo));
           cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(cAdd.CBlock) ? (object)DBNull.Value : cAdd.CBlock));
           cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(cAdd.CARea) ? (object)DBNull.Value : cAdd.CARea));
           cmd.Parameters.Add(new SqlParameter("@d10", string.IsNullOrEmpty(cAdd.CContactNo) ? (object)DBNull.Value : cAdd.CContactNo));
           cmd.ExecuteReader();
           connection.Close();
       }
       public void UpdateTraddingAddress(TraddingAddress tAdd)
       {
           InqueryClient atClient=new InqueryClient();
           connection.Open();
           string UpdateQuery = "Update TraddingAddresses set Division_ID=@d1,D_ID=@d2,T_ID=@d3,PostOfficeId=@d4,TFlatNo=@d5,THouseNo=@d6,TRoadNo=@d7,TBlock=@d8,TArea=@d9,TContactNo=@d10  Where  TraddingAddresses.IClientId='" + atClient.IClientId + "'";
           SqlCommand cmd = new SqlCommand(UpdateQuery, connection);           
           cmd.Parameters.Add(new SqlParameter("@d1", string.IsNullOrEmpty(tAdd.DivisionId.ToString()) ? (object)DBNull.Value : tAdd.DivisionId));
           cmd.Parameters.Add(new SqlParameter("@d2", string.IsNullOrEmpty(tAdd.DistrictId.ToString()) ? (object)DBNull.Value : tAdd.DistrictId));
           cmd.Parameters.Add(new SqlParameter("@d3", string.IsNullOrEmpty(tAdd.ThanaId.ToString()) ? (object)DBNull.Value : tAdd.ThanaId));
           cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(tAdd.PostOfficeId.ToString()) ? (object)DBNull.Value : tAdd.PostOfficeId));
           cmd.Parameters.Add(new SqlParameter("@d5", string.IsNullOrEmpty(tAdd.TFlatNo) ? (object)DBNull.Value : tAdd.TFlatNo));
           cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(tAdd.THouseNo) ? (object)DBNull.Value : tAdd.THouseNo));
           cmd.Parameters.Add(new SqlParameter("@d7", string.IsNullOrEmpty(tAdd.TRoadNo) ? (object)DBNull.Value : tAdd.TRoadNo));
           cmd.Parameters.Add(new SqlParameter("@d8", string.IsNullOrEmpty(tAdd.TBlock) ? (object)DBNull.Value : tAdd.TBlock));
           cmd.Parameters.Add(new SqlParameter("@d9", string.IsNullOrEmpty(tAdd.TARea) ? (object)DBNull.Value : tAdd.TARea));
           cmd.Parameters.Add(new SqlParameter("@d10",string.IsNullOrEmpty(tAdd.TContactNo) ? (object)DBNull.Value : tAdd.TContactNo));
           cmd.ExecuteReader();        
           connection.Close();
       }
       public void UpdateClient(InqueryClient  nClient)
       {         
           connection.Open();
           string UpdateQuery = "Update InquieryClient set ClientName=@d1,ClientTypeId=@d2,NatureOfClientId=@d3,EmailAddress=@d4,IndustryCategoryId=@d5,EndUser=@d6,UserId=@d7,Dates=@d8,SuperviserId=@d9  Where IClientId='" + nClient.IClientId + "'";
           SqlCommand cmd = new SqlCommand(UpdateQuery, connection);
           cmd.Parameters.AddWithValue("@d1", nClient.ClientName);
           cmd.Parameters.AddWithValue("@d2", nClient.ClientTypeId);
           cmd.Parameters.AddWithValue("@d3", nClient.NatureOfClientId);
           cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(nClient.EmailAddress) ? (object)DBNull.Value : nClient.EmailAddress));
           cmd.Parameters.AddWithValue("@d5", nClient.industrycategoryId);
           cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(nClient.EndUser) ? (object)DBNull.Value : nClient.EndUser));          
           cmd.Parameters.AddWithValue("@d7", nClient.UserId);
           cmd.Parameters.AddWithValue("@d8", DateTime.UtcNow.ToLocalTime());
           cmd.Parameters.AddWithValue("@d9", nClient.RMId);
           cmd.ExecuteReader();
           connection.Close();          
       }
       

      public InqueryClient SearchClient(decimal clientId)
       {
          connection.Open();
          string selectQuery = string.Format("Select * from InquieryClient Where IClientId='{0}'", clientId);
          SqlCommand cmd=new SqlCommand(selectQuery,connection);
          SqlDataReader daraReader = cmd.ExecuteReader();
          InqueryClient  aClient=new InqueryClient();
          while (daraReader.Read())
          {
              aClient.IClientId = Convert.ToDecimal(daraReader[0].ToString());
              aClient.ClientName = daraReader[1].ToString();
              aClient.ClientType = daraReader[2].ToString();
              aClient.NatureOfClient = daraReader[3].ToString();
              aClient.EmailAddress = daraReader[4].ToString();
              aClient.IndustryCatagory = daraReader[5].ToString();

              
          }
          daraReader.Close();
          connection.Close();
          return aClient;
          

       }
     
       
      public InqueryClient SearchInquiryClientDetails(decimal clientId)
      {
          connection.Open();
         // string selectQuery = string.Format("SELECT RTRIM(InquieryClient.ClientName),RTRIM(ClientTypes.ClientType),RTRIM(NatureOfClients.ClientNature),RTRIM(InquieryClient.EmailAddress),RTRIM(IndustryCategorys.IndustryCategory),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.Designation),RTRIM(ContactPersonDetails.CellNumber),RTRIM(InquieryClient.EndUser) FROM InquieryClient,ClientTypes,NatureOfClients,IndustryCategorys,ContactPersonDetails  WHERE InquieryClient.ClientTypeId=ClientTypes.ClientTypeId and InquieryClient.NatureOfClientId=NatureOfClients.NatureOfClientId and InquieryClient.IndustryCategoryId=IndustryCategorys.IndustryCategoryId and InquieryClient.IClientId=ContactPersonDetails.IClientId  and InquieryClient.IClientId='{0}'", clientId);
          string selectQuery=string.Format("SELECT RTRIM(InquieryClient.ClientName),RTRIM(ClientTypes.ClientType),RTRIM(NatureOfClients.ClientNature),RTRIM(InquieryClient.EmailAddress),RTRIM(IndustryCategorys.IndustryCategory) FROM InquieryClient,ClientTypes,NatureOfClients,IndustryCategorys  WHERE InquieryClient.ClientTypeId=ClientTypes.ClientTypeId and InquieryClient.NatureOfClientId=NatureOfClients.NatureOfClientId and InquieryClient.IndustryCategoryId=IndustryCategorys.IndustryCategoryId and  InquieryClient.IClientId='{0}'",clientId);

          SqlCommand cmd = new SqlCommand(selectQuery, connection);
          SqlDataReader daraReader = cmd.ExecuteReader();
          InqueryClient aClient = new InqueryClient();
          while (daraReader.Read())
          {              
              aClient.ClientName = daraReader[0].ToString();
              aClient.ClientType = daraReader[1].ToString();
              aClient.NatureOfClient = daraReader[2].ToString();
              aClient.EmailAddress = daraReader[3].ToString();
              aClient.IndustryCatagory = daraReader[4].ToString();

             
          }
          daraReader.Close();
          connection.Close();
          return aClient;


      }

     

    
      public InquiryFollowUp SearchFollowUp(decimal followUpId)
      {
          connection.Open();
          string selectQuery = string.Format("Select RTRIM(FollowUp.IClientId),RTRIM(FollowUp.Actions),RTRIM(FollowUp.DeadLineDateTime),RTRIM(Registration.Name) from FollowUp,Registration Where FollowUp.SBUserId=Registration.UserId and  FollowUp.FollowUpId='{0}'", followUpId);
          SqlCommand cmd = new SqlCommand(selectQuery, connection);
          SqlDataReader daraReader = cmd.ExecuteReader();
          InquiryFollowUp aflopId = new InquiryFollowUp();
          while (daraReader.Read())
          {
              aflopId.IClientId = Convert.ToInt32(daraReader[0].ToString());
              aflopId.IAction = daraReader[1].ToString();
              aflopId.IDeadFLine = Convert.ToDateTime(daraReader[2].ToString());
              aflopId.ISubmittedBy = daraReader[3].ToString();

          }
          daraReader.Close();
          connection.Close();
          return aflopId;


      }

       public InqueryClient SearchClientId(string clientName)
       {
           connection.Open();
           string query =string.Format("Select InquieryClient.IClientId from InquieryClient where InquieryClient.ClientName={0}", clientName);
           SqlCommand cmd=new SqlCommand(query,connection);
           SqlDataReader daraReader2 = cmd.ExecuteReader();
           InqueryClient bClient=new InqueryClient();
           while (daraReader2.Read())
           {
               bClient.IClientId = Convert.ToDecimal(daraReader2[0].ToString());
           }
           daraReader2.Close();
           connection.Close();
           return bClient;
       }
       public InqueryClient SearchClientName(decimal aIClientId)
       {
           connection.Open();
           string query =string.Format("Select InquieryClient.ClientName from InquieryClient where InquieryClient.IClientId={0}",aIClientId);
           SqlCommand cmd=new SqlCommand(query,connection);
           SqlDataReader daraReader = cmd.ExecuteReader();
           InqueryClient aClient=new InqueryClient();
           while (daraReader.Read())
           {
               aClient.ClientName = daraReader[0].ToString();
           }
           daraReader.Close();
           connection.Close();
           return aClient;

       }
      public InqueryClient SearchClients(decimal clientId)
      {
          connection.Open();
          //
          string selectQuery = string.Format("Select RTRIM(InquieryClient.IClientId),RTRIM(InquieryClient.ClientName),RTRIM(ContactPersonDetails.ContactPersonName),RTRIM(ContactPersonDetails.CellNumber) from InquieryClient,ContactPersonDetails  Where  InquieryClient.IClientId=ContactPersonDetails.IClientId  and  InquieryClient.IClientId='{0}'", clientId);
          SqlCommand cmd = new SqlCommand(selectQuery, connection);
          SqlDataReader daraReader = cmd.ExecuteReader();
          InqueryClient aClientId = new InqueryClient();
          while (daraReader.Read())
          {
              aClientId.IClientId = Convert.ToInt32(daraReader[0].ToString());
              aClientId.ClientName = daraReader[1].ToString();
              //aClientId.ContactPersonName = daraReader[2].ToString();
              //aClientId.CellNumber = daraReader[3].ToString();

          }
          daraReader.Close();
          connection.Close();
          return aClientId;


      }
      public SalesClient SearchSalesClients(decimal sdClientId)
      {
          connection.Open();
          //
          string selectQuery = string.Format("SELECT SalesClient.SClientId,SalesClient.ClientName,ContactPersonDetails.ContactPersonName,ContactPersonDetails.CellNumber FROM  SalesClient INNER JOIN  ContactPersonDetails ON SalesClient.SClientId = ContactPersonDetails.SClientId  where SalesClient.SClientId='{0}'", sdClientId);
          SqlCommand cmd = new SqlCommand(selectQuery, connection);
          SqlDataReader daraReader = cmd.ExecuteReader();
          SalesClient sClientId = new SalesClient();
          while (daraReader.Read())
          {
              sClientId.IClientId = Convert.ToInt32(daraReader[0].ToString());
              sClientId.SClientName = daraReader[1].ToString();
              sClientId.SContactPersonName = daraReader[2].ToString();
              sClientId.SCellNumber = daraReader[3].ToString();

          }
          daraReader.Close();
          connection.Close();
          return sClientId;


      }

      public SalesFollowUp SearchSFollowUp(decimal sFollowUpId)
      {
          connection.Open();

          string selectQuery = string.Format("Select RTRIM(FollowUp.SClientId),RTRIM(FollowUp.Actions),RTRIM(FollowUp.DeadLineDateTime),RTRIM(Registration.Name)  from FollowUp,Registration Where FollowUp.SBUserId=Registration.UserId and  FollowUp.FollowUpId='{0}'", sFollowUpId);
          SqlCommand cmd = new SqlCommand(selectQuery, connection);
          SqlDataReader daraReader = cmd.ExecuteReader();
          SalesFollowUp sflopId = new SalesFollowUp();
          while (daraReader.Read())
          {
              sflopId.SClientId = Convert.ToInt32(daraReader[0].ToString());
              sflopId.SAction = daraReader[1].ToString();
              sflopId.SDeadFLine = Convert.ToDateTime(daraReader[2].ToString());
              sflopId.SSubmittedBy = daraReader[3].ToString();

          }
          daraReader.Close();
          connection.Close();
          return sflopId;

      }
   }
}
