using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class InquiryFollowUp
    {
        private decimal iclientId;
        private string iAction;
        private string iRPerson;
        private string iSubmittedBy;
        private DateTime iDeadline;

        public decimal IClientId
        {
            set { iclientId = value; }
            get { return iclientId; }
        }

        public string IAction
        {
            set { iAction = value; }
            get { return iAction; }
        }

        public string ISubmittedBy
        {
            set { iSubmittedBy = value; }
            get { return iSubmittedBy; }
        }
        public string IRPerson
        {
            set { iRPerson = value; }
            get { return iRPerson; }
        }

        public DateTime IDeadFLine
        {
            set { iDeadline = value; }
            get { return iDeadline; }
        }
    }
}
