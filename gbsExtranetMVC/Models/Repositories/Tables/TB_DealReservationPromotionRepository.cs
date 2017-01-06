using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_DealReservationPromotionRepository :BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_DealReservationPromotionExt> ReadAll(int TableID)
        {
            List<TB_DealReservationPromotionExt> list = new List<TB_DealReservationPromotionExt>();
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
                    TB_DealReservationPromotionExt model = new TB_DealReservationPromotionExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.ReservationID = Convert.ToInt32(dr["FK_ReservationID_ID"]);
                    model.DealReservationID = dr["FK_DealReservationID_ID"].ToString();
                    model.Promotion = dr["FK_PromotionID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_DealReservationPromotionExt
    {
        public int ID { get; set; }
        public int ReservationID { get; set; }
        public string DealReservationID { get; set; }
        public string Promotion { get; set; }
    }
}