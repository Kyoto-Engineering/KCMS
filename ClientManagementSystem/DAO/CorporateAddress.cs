using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class CorporateAddress
   {
        private int cpAddId;
        private string cFlatNo;
        private string cHouseNo;
        private string cRoadNo;
        private string cBlock;
        private string cArea;
        private string cContactNo;

        private string cDivision;
        private int  divisionId;
        private int districtId;
        private int thanaId;
        private int postOfficeId;
        private string cDistrict;
        
        private string cThana;
       

        private string cPost;
        
        private string cPostCode;
        private int  addTypeid1;
       

       public int CPAddId
       {
           set { cpAddId = value; }
           get { return cpAddId; }
       }
        public string CFlatNo
        {
            get { return cFlatNo; }
            set { cFlatNo = value; }
        }

        public string CHouseNo
        {
            get { return cHouseNo; }
            set { cHouseNo = value; }
        }

        public string CRoadNo
        {
            get { return cRoadNo; }
            set { cRoadNo = value; }

        }

        public string CBlock
        {
            get { return cBlock; }
            set { cBlock = value; }
        }

        public string CARea
        {
            get { return cArea; }
            set { cArea = value; }
        }

        public string CContactNo
        {
            set { cContactNo = value; }
            get { return cContactNo; }
        }

       public string CDivision
       {
           set { cDivision = value; }
           get { return cDivision; }

       }

       

      
        public string CDistrict
        {
            set { cDistrict = value; }
            get { return cDistrict; }
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
        public string CThana
        {
            get
            {
                return cThana;
            }
            set { cPost = value; }
        }

      
        public string CPost
        {
            get
            {
                return cPost;
            }
            set { cPost = value; }
        }

       
        public string CPostCode
        {
            get { return cPostCode; }
            set { cPostCode = value; }
        }

       public int  AddTypeId1
       {
           set { addTypeid1 = value; }
           get { return addTypeid1; }
       }
    }
}
