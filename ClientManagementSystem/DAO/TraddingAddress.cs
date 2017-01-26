using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
  public  class TraddingAddress
  {
        private int tAddId;
        private int addTypeIdt;
        private string tFlatNo;
        private string tHouseNo;
        private string tRoadNo;
        private string tBlock;
        private string tArea;
        private string tPost;
        private string tPostCode;
        private string tDistrict;
        private string tContactNo;

        private int divisionId;
        private int districtId;
        private int thanaId;
        private int postOfficeId;
      public int TraddingAddId
      {
          set { tAddId = value;}
          get { return tAddId; }
      }
        public string TFlatNo
        {
            get { return tFlatNo; }
            set { tFlatNo = value; }
        }

        public string THouseNo
        {
            get { return tHouseNo; }
            set { tHouseNo = value; }
        }

        public string TRoadNo
        {
            get { return tRoadNo; }
            set { tRoadNo = value; }

        }

        public string TBlock
        {
            get { return tBlock; }
            set { tBlock = value; }
        }

        public string TARea
        {
            get { return tArea; }
            set { tArea = value; }
        }

        public string TPost
        {
            get
            {
                return tPost;
            }
            set { tPost = value; }
        }
        public string TPostCode
        {
            get { return tPostCode; }
            set { tPostCode = value; }
        }

        public string TDistrict
        {
            set { tDistrict = value; }
            get { return tDistrict; }
        }

        public string TContactNo
        {
            set { tContactNo = value; }
            get { return tContactNo; }
        }

        public int DivisionId
        {
            set { divisionId = value; }
            get { return divisionId; }
        }
        public int DistrictId
        {
            set { districtId = value; }
            get { return districtId; }
        }
        public int ThanaId
        {
            get { return thanaId; }
            set { thanaId = value; }
        }
        public int PostOfficeId
        {
            set { postOfficeId = value; }
            get { return postOfficeId; }
        }
        public int AddTypeIdT
        {
            set { addTypeIdt = value; }
            get { return addTypeIdt; }
        }
    }
}
