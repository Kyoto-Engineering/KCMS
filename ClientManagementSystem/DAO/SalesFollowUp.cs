using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
 public  class SalesFollowUp
    {
        private decimal sclientId;
        private string sAction;
        private string sRPerson;
        private string sSubmittedBy;
        private DateTime sDeadline;

        public decimal SClientId
        {
            set { sclientId = value; }
            get { return sclientId; }
        }

        public string SAction
        {
            set { sAction = value; }
            get { return sAction; }
        }

        public string SSubmittedBy
        {
            set { sSubmittedBy = value; }
            get { return sSubmittedBy; }
        }
        public string SResponsiblePerson
        {
            set { sRPerson = value; }
            get { return sRPerson; }
        }

        public DateTime SDeadFLine
        {
            set { sDeadline = value; }
            get { return sDeadline; }
        }
    }
}
