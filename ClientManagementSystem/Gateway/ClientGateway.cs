using System;
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
        int currentClientId,currentClientId2;       
        int affectedRowId;
        int instantRowsId;
       public int SaveClient2(InqueryClient aClient)
       {
           connection.Open();
           string insertQuery2 = "insert into InquieryClient(ClientName,ClientType,NatureOfClient,EmailAddress,IndustryCatagory,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CPS,CPSCode,CDistrict,CContactNo,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TPS,TPSCode,TDistrict,TContactNo,ContactPersonName,Designation,CellNumber,EndUser,CurrentDate) Values(@clientName,@clientType,@natureOfClient,@emailAddress,@industryCategory,@cFlateNo,@cHouseNo,@cRoadNo,@cBlock,@cArea,@cPS,@cPostCode,@tDistrict,@cContactNo,@tFlatNo,@tHouseNo,@tRoadNo,@tBlock,@tArea,@tPost,@tPostCode,@tDistrict,@tContactNo,@contactPersonName,@designation,@cellNumber,@endUser,@d1)" + "SELECT CONVERT(int, SCOPE_IDENTITY())"; 
           SqlCommand  cmd=new SqlCommand(insertQuery2,connection);
           cmd.Parameters.AddWithValue("@clientName", aClient.ClientName);
           cmd.Parameters.AddWithValue("@clientType", aClient.ClientType);
           cmd.Parameters.AddWithValue("@natureOfClient", aClient.NatureOfClient);
           cmd.Parameters.AddWithValue("@emailAddress", aClient.EmailAddress);
           cmd.Parameters.AddWithValue("@industryCategory", aClient.IndustryCatagory);           
           //cmd.Parameters.AddWithValue("@contactPersonName", aClient.ContactPersonName);
           //cmd.Parameters.AddWithValue("@designation", aClient.Designation);
           //cmd.Parameters.AddWithValue("@cellNumber", aClient.CellNumber);
           //cmd.Parameters.AddWithValue("@endUser", aClient.EndUser);
           cmd.Parameters.AddWithValue("@d1", System.DateTime.Now);
           currentClientId2 = (int)cmd.ExecuteScalar();
           connection.Close();
           return currentClientId2;
         
          

       }
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
          cmd.Parameters.AddWithValue("@d5", iClientId.IClientId);
          cmd.ExecuteReader();
          connection.Close();
         


       }
       public void UpdateCorporatAddress(CorporateAddress cAdd)
       {
           InqueryClient aClient = new InqueryClient();
           connection.Open();
           string UpdateQuery = "Update Addresses set Division_ID=@d1,D_ID=@d2,T_ID=@d3,PostOfficeId=@d4,FlatNo=@d5,HouseNo=@d6,RoadNo=@d7,Block=@d8,Area=@d9,ContactNo=@d10,ADTypeId=@d11 Where ADTypeId='" + cAdd.AddTypeId1 + "' and IClientId='" + aClient.IClientId + "'";
           SqlCommand cmd = new SqlCommand(UpdateQuery, connection);
           cmd.ExecuteReader();
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
           cmd.Parameters.Add(new SqlParameter("@d11", string.IsNullOrEmpty(cAdd.AddTypeId1.ToString()) ? (object)DBNull.Value : cAdd.AddTypeId1));
           connection.Close();
       }
       public void UpdateTraddingAddress(TraddingAddress tAdd)
       {
           InqueryClient atClient=new InqueryClient();
           connection.Open();
           string UpdateQuery = "Update Addresses set Division_ID=@d1,D_ID=@d2,T_ID=@d3,PostOfficeId=@d4,FlatNo=@d5,HouseNo=@d6,RoadNo=@d7,Block=@d8,Area=@d9,ContactNo=@d10,ADTypeId=@d11 Where ADTypeId='" + tAdd.AddTypeIdT + "' and IClientId='" + atClient.IClientId + "'";
           SqlCommand cmd = new SqlCommand(UpdateQuery, connection);
           cmd.ExecuteReader();
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
           cmd.Parameters.Add(new SqlParameter("@d11",string.IsNullOrEmpty(tAdd.AddTypeIdT.ToString()) ? (object)DBNull.Value : tAdd.AddTypeIdT));          
           connection.Close();
       }
       public void UpdateClient(InqueryClient  nClient)
       {         
           connection.Open();
           string UpdateQuery = "Update InquieryClient set ClientName=@d1,ClientTypeId=@d2,NatureOfClientId=@d3,EmailAddress=@d4,IndustryCategoryId=@d5,EndUser=@d6,CurrentDate=@d7,UserId=@d8,Dates=@d9,SupervisorName=@d10,  Where IClientId='" + nClient.IClientId + "'";
           SqlCommand cmd = new SqlCommand(UpdateQuery, connection);
           cmd.Parameters.AddWithValue("@d1", nClient.ClientName);
           cmd.Parameters.AddWithValue("@d2", nClient.ClientTypeId);
           cmd.Parameters.AddWithValue("@d3", nClient.NatureOfClientId);
           cmd.Parameters.Add(new SqlParameter("@d4", string.IsNullOrEmpty(nClient.EmailAddress) ? (object)DBNull.Value : nClient.EmailAddress));
           cmd.Parameters.AddWithValue("@d5", nClient.industrycategoryId);
           cmd.Parameters.Add(new SqlParameter("@d6", string.IsNullOrEmpty(nClient.EndUser) ? (object)DBNull.Value : nClient.EndUser));
           cmd.Parameters.AddWithValue("@d7", System.DateTime.Now);
           cmd.Parameters.AddWithValue("@d8", nClient.UserId);
           cmd.Parameters.AddWithValue("@d9", DateTime.UtcNow.ToLocalTime());
           cmd.Parameters.AddWithValue("@d10", nClient.RMId);
           cmd.ExecuteReader();
           connection.Close();          
       }
       public int SaveClient(InqueryClient aClient)
       {
           connection.Open();
           string insertQuery = "insert into InquieryClient(ClientName,ClientType,NatureOfClient,EmailAddress,IndustryCatagory,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CPS,CPSCode,CDistrict,CContactNo,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TPS,TPSCode,TDistrict,TContactNo,ContactPersonName,Designation,CellNumber,EndUser,CurrentDate) Values(@clientName,@clientType,@natureOfClient,@emailAddress,@industryCategory,@cFlateNo,@cHouseNo,@cRoadNo,@cBlock,@cArea,@cPS,@cPostCode,@tDistrict,@cContactNo,@tFlatNo,@tHouseNo,@tRoadNo,@tBlock,@tArea,@tPost,@tPostCode,@tDistrict,@tContactNo,@contactPersonName,@designation,@cellNumber,@endUser,@d1)"+ "SELECT CONVERT(int, SCOPE_IDENTITY())";
           SqlCommand cmd = new SqlCommand(insertQuery, connection);
           cmd.Parameters.AddWithValue("@clientName", aClient.ClientName);
           cmd.Parameters.AddWithValue("@clientType", aClient.ClientType);
           cmd.Parameters.AddWithValue("@natureOfClient", aClient.NatureOfClient);
           cmd.Parameters.AddWithValue("@emailAddress", aClient.EmailAddress);
           cmd.Parameters.AddWithValue("@industryCategory", aClient.IndustryCatagory);
           
           //cmd.Parameters.AddWithValue("@tFlatNo", aClient.TFlatNo);
           //cmd.Parameters.AddWithValue("@tHouseNo", aClient.THouseNo);
           //cmd.Parameters.AddWithValue("@tRoadNo", aClient.TRoadNo);
           //cmd.Parameters.AddWithValue("@tBlock", aClient.TBlock);
           //cmd.Parameters.AddWithValue("@tArea", aClient.TARea);
           //cmd.Parameters.AddWithValue("@tPost", aClient.TPost);
           //cmd.Parameters.AddWithValue("@tPostCode", aClient.TPostCode);
           //cmd.Parameters.AddWithValue("@tDistrict", aClient.TDistrict);
           //cmd.Parameters.AddWithValue("@tContactNo", aClient.TContactNo);
           //cmd.Parameters.AddWithValue("@contactPersonName", aClient.ContactPersonName);
           //cmd.Parameters.AddWithValue("@designation", aClient.Designation);
           //cmd.Parameters.AddWithValue("@cellNumber", aClient.CellNumber);
           //cmd.Parameters.AddWithValue("@endUser", aClient.EndUser);
           cmd.Parameters.AddWithValue("@d1", System.DateTime.Now);
           currentClientId = (int)cmd.ExecuteScalar();
           connection.Close();
           return currentClientId;

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

              //aClient.CFlatNo = daraReader[6].ToString();
              //aClient.CHouseNo = daraReader[7].ToString();
              //aClient.CRoadNo = daraReader[8].ToString();
              //aClient.CBlock = daraReader[9].ToString();
              //aClient.CARea = daraReader[10].ToString();
              //aClient.CPost = daraReader[11].ToString();
              //aClient.CPostCode = daraReader[12].ToString();
              //aClient.CDistrict = daraReader[13].ToString();
              //aClient.CContactNo = daraReader[14].ToString();

              //aClient.TFlatNo = daraReader[15].ToString();
              //aClient.THouseNo = daraReader[16].ToString();
              //aClient.TRoadNo = daraReader[17].ToString();
              //aClient.TBlock = daraReader[18].ToString();
              //aClient.TARea = daraReader[19].ToString();
              //aClient.TPost = daraReader[20].ToString();
              //aClient.TPostCode = daraReader[21].ToString();
              //aClient.TDistrict=daraReader[22].ToString();
              //aClient.TContactNo = daraReader[23].ToString();

              //aClient.ContactPersonName = daraReader[24].ToString();
              //aClient.Designation = daraReader[25].ToString();
              //aClient.CellNumber = daraReader[26].ToString();
              //aClient.EndUser = daraReader[27].ToString();
          }
          daraReader.Close();
          connection.Close();
          return aClient;
          

       }
      public Addresss SearchTraddingAddress(int iClientId2)
      {
          connection.Open();
          string selectQuery = string.Format("SELECT RTRIM(Addresses.FlatNo),RTRIM(Addresses.HouseNo),RTRIM(Addresses.RoadNo),RTRIM(Addresses.Block),RTRIM(Addresses.Area),RTRIM(Addresses.PS),RTRIM(Addresses.PSCode),RTRIM(Addresses.District),RTRIM(Addresses.ContactNo) from Addresses,InquieryClient,AddressTypes where InquieryClient.IClientId=Addresses.IClientId and AddressTypes.ADTypeId=2 and InquieryClient.IClientId='{0}'", iClientId2);
          SqlCommand cmd = new SqlCommand(selectQuery, connection);
          SqlDataReader drReader = cmd.ExecuteReader();
          Addresss add2 = new Addresss();
          while (drReader.Read())
          {
              //add2.TFlatNo = drReader[0].ToString();
              //add2.THouseNo = drReader[1].ToString();
              //add2.TRoadNo = drReader[2].ToString();
              //add2.TBlock = drReader[3].ToString();
              //add2.TARea = drReader[4].ToString();
              //add2.TPost = drReader[5].ToString();
              //add2.TPostCode = drReader[6].ToString();
              //add2.TDistrict = drReader[7].ToString();
              //add2.TContactNo = drReader[8].ToString();
          }
          drReader.Close();
          connection.Close();
          return add2;

      }
       public Addresss SearchCorporateAddress(int iClientId1)
       {
           connection.Open();
           string selectQuery = string.Format("SELECT RTRIM(Addresses.FlatNo),RTRIM(Addresses.HouseNo),RTRIM(Addresses.RoadNo),RTRIM(Addresses.Block),RTRIM(Addresses.Area),RTRIM(Addresses.PS),RTRIM(Addresses.PSCode),RTRIM(Addresses.District),RTRIM(Addresses.ContactNo) from Addresses,InquieryClient,AddressTypes where InquieryClient.IClientId=Addresses.IClientId and AddressTypes.ADTypeId=1 and InquieryClient.IClientId='{0}'",iClientId1);
           SqlCommand cmd = new SqlCommand(selectQuery, connection);
           SqlDataReader drReader = cmd.ExecuteReader();
           Addresss add1=new Addresss();
           while (drReader.Read())
           {
               //add1.CFlatNo = drReader[0].ToString();
               //add1.CHouseNo = drReader[1].ToString();
               //add1.CRoadNo = drReader[2].ToString();
               //add1.CBlock = drReader[3].ToString();
               //add1.CARea = drReader[4].ToString();
               //add1.CPost = drReader[5].ToString();
               //add1.CPostCode = drReader[6].ToString();
               //add1.CDistrict = drReader[7].ToString();
               //add1.CContactNo = drReader[8].ToString();
           }
           drReader.Close();
           connection.Close();
           return add1;

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

              //aClient.CFlatNo = daraReader[6].ToString();
              //aClient.CHouseNo = daraReader[7].ToString();
              //aClient.CRoadNo = daraReader[8].ToString();
              //aClient.CBlock = daraReader[9].ToString();
              //aClient.CARea = daraReader[10].ToString();
              //aClient.CPost = daraReader[11].ToString();
              //aClient.CPostCode = daraReader[12].ToString();
              //aClient.CDistrict = daraReader[13].ToString();
              //aClient.CContactNo = daraReader[14].ToString();

              //aClient.TFlatNo = daraReader[15].ToString();
              //aClient.THouseNo = daraReader[16].ToString();
              //aClient.TRoadNo = daraReader[17].ToString();
              //aClient.TBlock = daraReader[18].ToString();
              //aClient.TARea = daraReader[19].ToString();
              //aClient.TPost = daraReader[20].ToString();
              //aClient.TPostCode = daraReader[21].ToString();
              //aClient.TDistrict = daraReader[22].ToString();
              //aClient.TContactNo = daraReader[23].ToString();

              //aClient.ContactPersonName = daraReader[5].ToString();
              //aClient.Designation = daraReader[6].ToString();
              //aClient.CellNumber = daraReader[7].ToString();
              //aClient.EndUser = daraReader[8].ToString();
          }
          daraReader.Close();
          connection.Close();
          return aClient;


      }

     

      public  int SaveInstantClient(InstantClient aClient)
      {
         connection.Open();
         string apquery = "insert into InstantsClient(ClientName,ClientType,NatureOfClient,EmailAddress,IndustryCatagory,CFlatNo,CHouseNo,CRoadNo,CBlock,CArea,CPS,CPSCode,CDistrict,CContactNo,TFlatNo,THouseNo,TRoadNo,TBlock,TArea,TPS,TPSCode,TDistrict,TContactNo,ContactPersonName,Designation,CellNumber,EndUser,BFlatNo,BHouseNo,BRoadNo,BBlock,BArea,BPS,BPSCode,BDistrict,BContactNo,BankName,BranchName,AccountNo,CurrentDate) values(@clientName,@clientType,@natureOfClient,@emailAddress,@industryCategory,@cFlateNo,@cHouseNo,@cRoadNo,@cBlock,@cArea,@cPS,@cPostCode,@cDistrict,@cContactNo,@tFlatNo,@tHouseNo,@tRoadNo,@tBlock,@cArea,@tPost,@tPostCode,@tDistrict,@tContactNo,@contactPersonName,@designation,@cellNumber,@endUser,@bFlatNo,@bHouseNo,@bRoadNo,@bBlock,@bArea,@bPost,@bPostCode,@bDistrict,@bContactNo,@bankName,@branchName,@accountNo,@d1)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
          SqlCommand cmd = new SqlCommand(apquery, connection);
          cmd.Parameters.AddWithValue("@clientName", aClient.ClientName);
          cmd.Parameters.AddWithValue("@clientType", aClient.ClientType);
          cmd.Parameters.AddWithValue("@natureOfClient", aClient.NatureOfClient);
          cmd.Parameters.AddWithValue("@emailAddress", aClient.EmailAddress);
          cmd.Parameters.AddWithValue("@industryCategory", aClient.IndustryCatagory);
          cmd.Parameters.AddWithValue("@cFlateNo", aClient.CFlatNo);
          cmd.Parameters.AddWithValue("@cHouseNo", aClient.CHouseNo);
          cmd.Parameters.AddWithValue("@cRoadNo", aClient.CRoadNo);
          cmd.Parameters.AddWithValue("@cBlock", aClient.CBlock);
          cmd.Parameters.AddWithValue("@cArea", aClient.CARea);
          cmd.Parameters.AddWithValue("@cPS", aClient.CPost);
          cmd.Parameters.AddWithValue("@cPostCode", aClient.CPostCode);
          cmd.Parameters.AddWithValue("@cDistrict", aClient.CDistrict);
          cmd.Parameters.AddWithValue("@cContactNo", aClient.CContactNo);
          cmd.Parameters.AddWithValue("@tFlatNo", aClient.TFlatNo);
          cmd.Parameters.AddWithValue("@tHouseNo", aClient.THouseNo);
          cmd.Parameters.AddWithValue("@tRoadNo", aClient.TRoadNo);
          cmd.Parameters.AddWithValue("@tBlock", aClient.TBlock);
          cmd.Parameters.AddWithValue("@tArea", aClient.TARea);
          cmd.Parameters.AddWithValue("@tPost", aClient.TPost);
          cmd.Parameters.AddWithValue("@tPostCode", aClient.TPostCode);
          cmd.Parameters.AddWithValue("@tDistrict", aClient.TDistrict);
          cmd.Parameters.AddWithValue("@tContactNo", aClient.TContactNo);
          cmd.Parameters.AddWithValue("@contactPersonName", aClient.ContactPersonName);
          cmd.Parameters.AddWithValue("@designation", aClient.Designation);
          cmd.Parameters.AddWithValue("@cellNumber", aClient.CellNumber);
          cmd.Parameters.AddWithValue("@endUser", aClient.EndUser);
          cmd.Parameters.AddWithValue("@bFlatNo", aClient.BFlatNo);
          cmd.Parameters.AddWithValue("@bHouseNo", aClient.BHouseNo);
          cmd.Parameters.AddWithValue("@bRoadNo", aClient.BRoadNo);
          cmd.Parameters.AddWithValue("@bBlock", aClient.BBlock);
          cmd.Parameters.AddWithValue("@bArea", aClient.BARea);
          cmd.Parameters.AddWithValue("@bPost", aClient.BPost);
          cmd.Parameters.AddWithValue("@bPostCode", aClient.BPostCode);
          cmd.Parameters.AddWithValue("@bDistrict", aClient.BDistrict);
          cmd.Parameters.AddWithValue("@bContactNo", aClient.BContactNo);
          cmd.Parameters.AddWithValue("@bankName", aClient.BankName);
          cmd.Parameters.AddWithValue("@branchName", aClient.BranchName);
          cmd.Parameters.AddWithValue("@accountNo", aClient.AccountNumber);
          cmd.Parameters.AddWithValue("@d1", System.DateTime.Now);
          instantRowsId = (int)cmd.ExecuteScalar();
          connection.Close();
          return instantRowsId;
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
          string selectQuery = string.Format("Select SalesClient.SClientId,SalesClient.ClientName,SalesClient.ContactPersonName,SalesClient.CellNumber from SalesClient Where SClientId='{0}'", sdClientId);
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

          string selectQuery = string.Format("Select SFollowUpId,Actions,DeadLineDateTime,SubmittedBy from SFollowUp Where SFollowUpId='{0}'", sFollowUpId);
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
