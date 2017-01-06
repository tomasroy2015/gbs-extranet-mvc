using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_ReservationReviewDetailHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ReservationReviewDetailHistoryExt> ReadAll(int TableID)
        {
            List<TB_ReservationReviewDetailHistoryExt> list = new List<TB_ReservationReviewDetailHistoryExt>();

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
                    TB_ReservationReviewDetailHistoryExt PageObj = new TB_ReservationReviewDetailHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.ReservationReviewDetailID = dr["ReservationReviewDetailID"].ToString();
                    PageObj.ReservationReviewID = dr["FK_ReservationReviewID_ID"].ToString();
                    PageObj.ReviewType = dr["FK_ReviewTypeID_ID"].ToString();
                    PageObj.ReviewScale = dr["FK_ReviewScaleTypeID_ID"].ToString();
                    PageObj.LogDate = Convert.ToDateTime(dr["LogDateTime"]);
                    PageObj.LogUser = dr["FK_LogUserID_ID"].ToString();

                    list.Add(PageObj);
                }
            }

            return list;
        }
    }
    public class TB_ReservationReviewDetailHistoryExt
    {
        public int ID { get; set; }
        public string ReservationReviewDetailID { get; set; }
        public string ReservationReviewID { get; set; }
        public string ReviewType { get; set; }
        public string ReviewScale { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}