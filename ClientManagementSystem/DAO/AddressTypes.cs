using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementSystem.DAO
{
  public   class AddressTypes
  {
      private int addressTypeId;
      private string addressType;

      public int AddressTypeId
      {
          get { return addressTypeId; }
          set { addressTypeId = value; }
      }

      public string AddressType
      {
          get { return addressType; }
          set { addressType = value; }
      }
  }
}
