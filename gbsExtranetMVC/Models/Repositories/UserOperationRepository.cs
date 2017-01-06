using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class UserOperationRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<UserOperationExt> GetUserOperationDetails()
        {
            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("User");
            dt.Columns.Add("Date");
            dt.Columns.Add("OperationType");
            dt.Columns.Add("Part");
            dt.Columns.Add("RecordID");
            dt.Columns.Add("UserSessionID");
            dt.Columns.Add("IPAddress");
            dt.Columns.Add("UserID");
            var result = entity.Database.SqlQuery<GetUserOperationDetails_Result1>("B_Ex_GetUserOperationDetails_BizTbl_UserOperation_SP").ToList();
            List<UserOperationExt> ListOfModel = new List<UserOperationExt>();
            foreach (GetUserOperationDetails_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.FK_UserID_ID, Val.Date, Val.FK_OperationTypeID_ID, Val.FK_PartID_ID, Val.RecordID, Val.FK_UserSessionID_ID, Val.IPAddress, Val.UserID);
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    UserOperationExt UserObj = new UserOperationExt();
                    UserObj.ID = Convert.ToInt32(dr["ID"]);
                    UserObj.User = dr["User"].ToString();
                    UserObj.Part = dr["Part"].ToString();
                    UserObj.OperationType = dr["OperationType"].ToString();
                    UserObj.Date = Convert.ToDateTime(dr["Date"]);
                    UserObj.RecordID = dr["RecordID"].ToString();
                    UserObj.UserSessionID = dr["UserSessionID"].ToString();
                    UserObj.IPAddress = dr["IPAddress"].ToString();
                    UserObj.UserID = dr["UserID"].ToString();
                    ListOfModel.Add(UserObj);
                }
            }
            return ListOfModel;
        }

        public List<HitcountExt> GetOperationTypeTableValue()
        {

            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureValue);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");

            var result = entity.Database.SqlQuery<GetOperationType_Result>("B_Ex_GetOperationType_TB_TypeOperation_SP @CultureCode", Culture).ToList();


            foreach (GetOperationType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            List<HitcountExt> ListOfModel = new List<HitcountExt>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HitcountExt HitObj = new HitcountExt();
                    HitObj.ID = Convert.ToInt32(dr["ID"]);
                    HitObj.Name = dr["Name"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }

       
   
    }
       


    public class UserOperationExt
    {
        public int ID { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string OperationType { get; set; }
        public string Part { get; set; }
        public string RecordID { get; set; }
        public string UserSessionID { get; set; }
        public string IPAddress { get; set; }

        public string UserID { get; set; }
    }

}