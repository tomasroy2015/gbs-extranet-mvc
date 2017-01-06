using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelCancelPolicyHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelCancelPolicyHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelCancelPolicyHistoryExt> list = new List<TB_HotelCancelPolicyHistoryExt>();

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
                    TB_HotelCancelPolicyHistoryExt model = new TB_HotelCancelPolicyHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelCancelPolicyID = Convert.ToInt32(dr["HotelCancelPolicyID"]);
                    model.Hotel = dr["HotelID"].ToString();
                    model.CancelType = dr["CancelTypeID"].ToString();
                    model.RefundableDayCount = dr["RefundableDayCount"].ToString();
                    model.PenaltyRate = dr["PenaltyRateTypeID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    model.LogUser = dr["LogUserID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_HotelCancelPolicyHistoryExt
    {
        public int ID { get; set; }
        public int HotelCancelPolicyID { get; set; }
        public string Hotel { get; set; }
        public string CancelType { get; set; }
        public string RefundableDayCount { get; set; }
        public string PenaltyRate { get; set; }
        public bool Active { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}