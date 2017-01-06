using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_UserOperationRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_UserOperationExt> ReadAll(int TableID)
        {
            List<BizTbl_UserOperationExt> list = new List<BizTbl_UserOperationExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTable_BizTbl_Table_Sp", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TableID", TableID);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_UserOperationExt model = new BizTbl_UserOperationExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.UserID = dr["FK_UserID_ID"].ToString();
                    model.UserIDs = dr["UserIDs"].ToString();
                    model.Date = Convert.ToDateTime(dr["Date"].ToString());
                    model.OperationType = dr["FK_OperationTypeID_ID"].ToString();
                    model.Part = dr["FK_PartID_ID"].ToString();
                    model.RecordID = dr["RecordID"].ToString();
                    model.UserSessionID = dr["FK_UserSessionID_ID"].ToString();
                    model.IPAddress = dr["IPAddress"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public List<BizTbl_UserOperationExt> GetOperationType()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            List<BizTbl_UserOperationExt> ListOfModel = new List<BizTbl_UserOperationExt>();
            var result = entity.Database.SqlQuery<GetOperationType_Result>("B_Ex_GetOperationType_TB_TypeOperation_SP @CultureCode", Culture).ToList();


            foreach (GetOperationType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_UserOperationExt HitObj = new BizTbl_UserOperationExt();
                    HitObj.ID = Convert.ToInt32(dr["ID"]);
                    HitObj.Name = dr["Name"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }

        public List<BizTbl_UserOperationExt> GetPart()
        {

            {

                DBEntities entity = new DBEntities();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Name");
                List<BizTbl_UserOperationExt> ListOfModel = new List<BizTbl_UserOperationExt>();
                var result = entity.Database.SqlQuery<GetPart_Result>("B_Ex_GetPart_TB_Part_SP").ToList();


                foreach (GetPart_Result Val in result)
                {
                    dt.Rows.Add(Val.ID, Val.Name);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        BizTbl_UserOperationExt HitObj = new BizTbl_UserOperationExt();
                        HitObj.ID = Convert.ToInt32(dr["ID"]);
                        HitObj.Name = dr["Name"].ToString();
                        ListOfModel.Add(HitObj);
                    }
                }
                return ListOfModel;
            }
        }
    }
    public class BizTbl_UserOperationExt
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }
        public string OperationType { get; set; }
        public string Part { get; set; }
        public string RecordID { get; set; }
        public string UserSessionID { get; set; }
        public string IPAddress { get; set; }
        public string Name { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }

        public string UserIDs { get; set; }
    }
}