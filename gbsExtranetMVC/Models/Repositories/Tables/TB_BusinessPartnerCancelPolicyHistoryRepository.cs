using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BusinessPartnerCancelPolicyHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_BusinessPartnerCancelPolicyHistoryExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerCancelPolicyHistoryExt> list = new List<TB_BusinessPartnerCancelPolicyHistoryExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTableNew_BizTbl_Table_Sp", SQLCon);
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
                    TB_BusinessPartnerCancelPolicyHistoryExt model = new TB_BusinessPartnerCancelPolicyHistoryExt();
                    model.ID = Convert.ToInt32(dr["FK_ID_ID"]);
                    model.BusinessPartnerCancelPolicyID = Convert.ToInt32(dr["BusinessPartnerCancelPolicyID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.Part = dr["PartID"].ToString();
                    model.CancelType = dr["FK_CancelTypeID_ID"].ToString();
                    model.RefundableDayCount = dr["RefundableDayCount"].ToString();
                    model.PenaltyRateType = dr["FK_PenaltyRateTypeID_ID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_BusinessPartnerCancelPolicyHistoryExt
    {
        public int ID { get; set; }
        public int BusinessPartnerCancelPolicyID { get; set; }
        public string BusinessPartner { get; set; }
        public string Part { get; set; }
        public string CancelType { get; set; }
        public string RefundableDayCount { get; set; }
        public string PenaltyRateType { get; set; }
        public bool Active { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}