using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class BillingAddress
   {
        private int bAddId;
        private string bFlatNo;
        private string bHouseNo;
        private string bRoadNo;
        private string bBlock;
        private string bArea;
        private string bPost;
        private string bPostCode;
        private string bDistrict;
        private string bContactNo;

       public int BillAddId
       {
           set { bAddId = value; }
           get { return bAddId; }
       }
        public string BFlatNo
        {
            get { return bFlatNo; }
            set { bFlatNo = value; }
        }

        public string BHouseNo
        {
            get { return bHouseNo; }
            set { bHouseNo = value; }
        }

        public string BRoadNo
        {
            get { return bRoadNo; }
            set { bRoadNo = value; }

        }

        public string BBlock
        {
            get { return bBlock; }
            set { bBlock = value; }
        }

        public string BARea
        {
            get { return bArea; }
            set { bArea = value; }
        }

        public string BPost
        {
            get
            {
                return bPost;
            }
            set { bPost = value; }
        }
        public string BPostCode
        {
            get { return bPostCode; }
            set { bPostCode = value; }
        }

        public string BDistrict
        {
            set { bDistrict = value; }
            get { return bDistrict; }
        }
        public string BContactNo
        {
            set { bContactNo = value; }
            get { return bContactNo; }
        }

    }
}
