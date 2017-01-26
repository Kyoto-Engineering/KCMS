using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
  public   class FeedBack
  {
      private string iClientId;
      private string feedBacks;
      private string sBName;
      private string sBDesignation;
      private string sBDepartment;
      private DateTime fDeadLineDateTime;
      private DateTime currentDate;
      private string status;


      public string IClientId
      {
          set { iClientId = value; }
          get { return iClientId; }
      }
      public string FeedBacks
      {
          set { feedBacks = value; }
          get { return feedBacks; }
      }
            
      public string SubMittedBy
      {
          set { sBName = value; }
          get { return sBName; }
      }

      public string SBDesignation
      {
          set { sBDesignation = value; }
          get { return sBDesignation; }
      }
      public string SBDepartment
      {
          set { sBDepartment = value; }
          get { return sBDepartment; }
      }
      public DateTime FdeadLineDateTime
      {
          set { fDeadLineDateTime = value; }
          get { return fDeadLineDateTime; }
      }

      public DateTime CurrentDate
      {
          set { currentDate = value; }
          get { return currentDate; }
      }

      public string Status
      {
          set { status = value; }
          get { return status; }
      }
  }
}
