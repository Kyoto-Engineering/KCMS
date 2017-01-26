using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagementSystem.DAO;
using ClientManagementSystem.Gateway;

namespace ClientManagementSystem.Manager
{
   public class FeedBackManager
   {

       public FeedBackGateway agatGateway;
        public  int  SaveFeedBack(FollowUp afUp)
        {
            agatGateway=new FeedBackGateway();
            return agatGateway.SaveFeedBack(afUp);
        }

        public  int SaveNewFeedBack(FeedBack afeeBack)
        {
            agatGateway=new FeedBackGateway();
           return agatGateway.SaveNewFeedBack(afeeBack);

        }

        public int SaveFolloUp1(FollowUp afUp)
       {
           agatGateway=new FeedBackGateway();
            return agatGateway.SaveFolloUp1(afUp);
       }
   }
}
