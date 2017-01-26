using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagementSystem.DAO;
using ClientManagementSystem.DBGateway;

namespace ClientManagementSystem.Gateway
{
   public  class FeedBackGateway:ConnectionGateway
   {
       public int affectedRows, affectedRows1, affectedRows3;

        public  int SaveFeedBack(FollowUp afUp)
        {
            connection.Open();
            string query = "insert into FollowUp2(IClientId,Actions,DeadLineDateTime,ResponsiblePerson,Designation,Department,SubmittedBy,SDesignation,SDepartment,CurrentDate) values(@clientId,@action,@dtn3,@rPerson,@rpDesig,@rpDept,@sbName,@sbDesig,@sbDept,@dt11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
           SqlCommand cmd=new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@clientId", afUp.IClientId);
            cmd.Parameters.AddWithValue("@action", afUp.ActionDetails);
            cmd.Parameters.AddWithValue("@dtn3", afUp.DeadLineDateTime);
            cmd.Parameters.AddWithValue("@rPerson", afUp.ResponsiblePerson);
            cmd.Parameters.AddWithValue("@rpDesig", afUp.RpDesignation);
            cmd.Parameters.AddWithValue("@rpDept", afUp.RpDepartment);
            cmd.Parameters.AddWithValue("@sbName", afUp.FSName);
            cmd.Parameters.AddWithValue("@sbDesig", afUp.FSDesignation);
            cmd.Parameters.AddWithValue("@sbDept", afUp.FSDepartment);
            cmd.Parameters.AddWithValue("@dt11", afUp.CurrentDate);
            affectedRows = (int)cmd.ExecuteScalar();
            connection.Close();
            return affectedRows;
        }

        public  int  SaveNewFeedBack(FeedBack afeeBack)
        {
            connection.Open();
            string query = "insert into IClientFeedbackDairy(IClientId,DateTimes,Feedback,SubmittedBy,SBDesignation,SBDepartment,CurrentDate,Status) Values(@clientId,@feedBack,@deadLine,@submittedBy,@sbDesignation,@sbDept,@d5,@status)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@clientId", afeeBack.IClientId);
            cmd.Parameters.AddWithValue("@feedBack", afeeBack.FeedBacks);
            cmd.Parameters.AddWithValue("@deadLine", afeeBack.FdeadLineDateTime);
            cmd.Parameters.AddWithValue("@submittedBy", afeeBack.SubMittedBy);
            cmd.Parameters.AddWithValue("@sbDesignation", afeeBack.SBDesignation);
            cmd.Parameters.AddWithValue("@sbDept", afeeBack.SBDepartment);
            cmd.Parameters.AddWithValue("@d5", afeeBack.CurrentDate);
            cmd.Parameters.AddWithValue("@status", afeeBack.Status);
            affectedRows1 = (int) cmd.ExecuteScalar();
            connection.Close();
            return affectedRows1;

        }

       public int SaveFolloUp1(FollowUp afUp)
       {
           connection.Open();
          // string query = "insert into FollowUp(IClientId,Actions,DeadLineDateTime,ResponsiblePerson,Designation,Department,SubmittedBy,SDesignation,SDepartment,CurrentDate) values(@clientId,@action,@dtn3,@rPerson,@rpDesig,@rpDept,@sbName,@sbDesig,@sbDept,@dt11)" + "SELECT CONVERT(int, SCOPE_IDENTITY())";
           string insertQuery2 = "insert into FollowUp(IClientId,IClientFeedbackId,Actions,DeadLineDateTime,ResponsiblePerson,Designation,Department,SubmittedBy,SDesignation,SDepartment,CurrentDate) values(@iClientId,@feedbackId,@actions,@deadLineDateTime,@rPerson,@rpDesignation,@rpDepartment,@sbName,@sbDesignation,@sbDept,@d2) " + "SELECT CONVERT(int, SCOPE_IDENTITY())";
           SqlCommand cmd = new SqlCommand(insertQuery2, connection);
           cmd.Parameters.AddWithValue("@iClientId", afUp.IClientId);
           cmd.Parameters.AddWithValue("@feedbackId", affectedRows1);
           cmd.Parameters.AddWithValue("@actions",afUp.ActionDetails);
           cmd.Parameters.AddWithValue("@deadLineDateTime",afUp.DeadLineDateTime);
           cmd.Parameters.AddWithValue("@rPerson", afUp.ResponsiblePerson);
           cmd.Parameters.AddWithValue("@rpDesignation", afUp.RpDesignation);
           cmd.Parameters.AddWithValue("@rpDepartment", afUp.RpDepartment);
           cmd.Parameters.AddWithValue("@sbName", afUp.FSName);
           cmd.Parameters.AddWithValue("@sbDesignation", afUp.FSDesignation);
           cmd.Parameters.AddWithValue("@sbDept", afUp.FSDepartment);
           cmd.Parameters.AddWithValue("@d2", System.DateTime.Now);
           affectedRows3 = (int)cmd.ExecuteScalar();
           connection.Close();
           return affectedRows3;
       }
   }
}
