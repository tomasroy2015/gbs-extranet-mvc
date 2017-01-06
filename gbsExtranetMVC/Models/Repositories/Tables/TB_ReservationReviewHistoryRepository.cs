using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_ReservationReviewHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ReservationReviewHistoryExt> ReadAll(int TableID)
        {
            List<TB_ReservationReviewHistoryExt> list = new List<TB_ReservationReviewHistoryExt>();

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
                    TB_ReservationReviewHistoryExt PageObj = new TB_ReservationReviewHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.ReservationReviewID = dr["ReservationReviewID"].ToString();
                    PageObj.ReservationID = dr["FK_ReservationID_ID"].ToString();
                    PageObj.ReviewStatus = dr["FK_ReviewStatusID_ID"].ToString();
                    PageObj.TravellerType = dr["FK_TravellerTypeID_ID"].ToString();
                    PageObj.Review = dr["Review"].ToString();
                    PageObj.AveragePoint = dr["AveragePoint"].ToString();
                    PageObj.Anonymous = dr["Anonymous"].ToString();
                    PageObj.Active = Convert.ToBoolean(dr["Active"]);
                    PageObj.IPAddress = dr["IPAddress"].ToString();
                    PageObj.LogDate = Convert.ToDateTime(dr["LogDateTime"]);
                    PageObj.LogUser = dr["FK_LogUserID_ID"].ToString();

                    list.Add(PageObj);
                }
            }

            return list;
        }
    }
    public class TB_ReservationReviewHistoryExt
    {
        public int ID { get; set; }
        public string ReservationReviewID { get; set; }
        public string ReservationID { get; set; }
        public string ReviewStatus { get; set; }
        public string TravellerType { get; set; }
        public string Review { get; set; }
        public string AveragePoint { get; set; }
        public string Anonymous { get; set; }
        public bool Active { get; set; }
        public string IPAddress { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}