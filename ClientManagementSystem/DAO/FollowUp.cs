using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class FollowUp
   {
       private string actionDetails ;
       private string reponsiblePerson;
       private string deadlineDateTime;
       private string  iClientId;

       private string rPDesignation;
       private string rpDepartment;
       private string fSName;
       private string fSDesig;
       private string fSDepart;

       private DateTime currentDate;
       public string ActionDetails
       {
           set { actionDetails = value; }
           get { return actionDetails; }
       }

       
       public string DeadLineDateTime
       {
           set { deadlineDateTime = value; }
           get { return deadlineDateTime; }
       }

       public string  IClientId
       {
           set { iClientId = value; }
           get { return iClientId; }
       }
       public string ResponsiblePerson
       {
           set { reponsiblePerson = value; }
           get { return reponsiblePerson; }
       }

       public string RpDesignation
       {
           set { rPDesignation = value; }
           get { return rPDesignation; }
       }

       public string RpDepartment
       {
           set { rpDepartment = value; }
           get { return rpDepartment; }
       }
       public string FSName
       {
           set { fSName = value; }
           get { return fSName; }
       }

      
      

       public string FSDesignation
       {
           set { fSDesig = value; }
           get { return fSDesig; }
       }

       public string FSDepartment
       {
           set { fSDepart = value; }
           get { return fSDepart; }
       }

       public DateTime CurrentDate
       {
           set { currentDate = value; }
           get { return currentDate; }
       }
   }
}
