using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
   public  class InstantClient
    {
        private decimal intantClientId;
        private string clientName;
        private string clientType;
        private string natureOfClient;

        private string emailAddress;
        private string industryCatagory;

        private string cFlatNo;
        private string cHouseNo;
        private string cRoadNo;
        private string cBlock;
        private string cArea;
        private string cPost;
        private string cPostCode;
        private string cDistrict;
        private string cContactNo;

        private string tFlatNo;
        private string tHouseNo;
        private string tRoadNo;
        private string tBlock;
        private string tArea;
        private string tPost;
        private string tPostCode;
        private string tDistrict;
        private string tContactNo;


        private string contactPersonName;
        private string designation;
        private string cellNumber;
        private string endUser;



        private string bFlatNo;
        private string bHouseNo;
        private string bRoadNo;
        private string bBlock;
        private string bArea;
        private string bPost;
        private string bPostCode;
        private string bDistrict;
        private string bContactNo;


        private string bankName;
        private string branchName;
        private string accountNumber;


        public decimal InstantClientId
        {
            set { intantClientId = value; }
            get { return intantClientId; }
        }

        public string ClientName
        {
            set { clientName = value; }
            get { return clientName; }
        }

        public string ClientType
        {
            get { return clientType; }
            set { clientType = value; }
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

        public string CDistrict
        {
            set { cDistrict = value; }
            get { return cDistrict; }
        }

        public string CContactNo
        {
            set { cContactNo = value; }
            get { return cContactNo; }
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

        public string ContactPersonName
        {
            set { contactPersonName = value; }
            get { return contactPersonName; }
        }

        public string Designation
        {
            set { designation = value; }
            get { return designation; }
        }

        public string CellNumber
        {
            set { cellNumber = value; }
            get { return cellNumber; }
        }

        public string EndUser
        {
            get { return endUser; }
            set { endUser = value; }
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


        public string BankName
        {
            set { bankName = value; }
            get { return bankName; }
        }

        public string BranchName
        {
            set { branchName = value; }
            get { return branchName; }
        }

        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }
    }
}
