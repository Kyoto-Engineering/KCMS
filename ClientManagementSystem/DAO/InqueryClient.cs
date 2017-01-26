using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public class InqueryClient
    {
        private decimal iClientId;
        private string clientName;
        private string clientType;

        private int  clientTypeId;
        private int natureOfClientId;
        public int industrycategoryId;

        private string natureOfClient;
        private string emailAddress;
        private string industryCatagory;
        private string endUser;
        private string userId;
        private DateTime dates;
        private DateTime currentDates;
        private string rmId;

        public string RMId
        {
            set { rmId = value; }
            get { return rmId; }
        }
        public string  UserId
        {
            set { userId = value; }
            get { return userId; }
        }
        public decimal IClientId
        {
            set { iClientId = value; }
            get { return iClientId; }
        }

        public string ClientName
        {
            set { clientName = value; }
            get { return clientName; }
        }

        public int ClientTypeId
        {
            set { clientTypeId = value; }
            get { return clientTypeId; }
        }
        public string ClientType
        {
            get { return clientType; }
            set { clientType = value; }
        }

        public int NatureOfClientId
        {
            set { natureOfClientId = value; }
            get { return natureOfClientId; }
        }

        public int IndustryCategoryId
        {
            set { industrycategoryId = value; }
            get { return industrycategoryId; }
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

        public string EndUser
        {
            set { endUser = value; }
            get { return endUser; }
        }

        public DateTime Dates
        {
            set { dates = value; }
            get { return dates; }
        }

        public DateTime CurrentDates
        {
            set { currentDates = value; }
            get { return currentDates; }
        }

    }
}
