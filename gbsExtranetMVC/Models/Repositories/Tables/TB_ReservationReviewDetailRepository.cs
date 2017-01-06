using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_ReservationReviewDetailRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ReservationReviewDetailExt> ReadAll(int TableID)
        {
            List<TB_ReservationReviewDetailExt> list = new List<TB_ReservationReviewDetailExt>();

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
                    TB_ReservationReviewDetailExt PageObj = new TB_ReservationReviewDetailExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.ReservationReviewID = dr["FK_ReservationReviewID_ID"].ToString();
                    PageObj.ReviewType = dr["FK_ReviewTypeID_ID"].ToString();
                    PageObj.ReviewScale = dr["FK_ReviewScaleTypeID_ID"].ToString();
                   
                    list.Add(PageObj);
                }
            }

            return list;
        }
    }
    public class TB_ReservationReviewDetailExt
    {
        public int ID { get; set; }
        public string ReservationReviewID { get; set; }
        public string ReviewType { get; set; }
        public string ReviewScale { get; set; }       
    }
}