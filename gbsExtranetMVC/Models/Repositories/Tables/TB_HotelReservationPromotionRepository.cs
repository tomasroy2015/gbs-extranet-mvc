using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelReservationPromotionRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelReservationPromotionExt> ReadAll(int TableID)
        {
            List<TB_HotelReservationPromotionExt> list = new List<TB_HotelReservationPromotionExt>();

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
                    TB_HotelReservationPromotionExt PageObj = new TB_HotelReservationPromotionExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.ReservationID = Convert.ToInt64(dr["FK_ReservationID_ID"]);
                    PageObj.HotelReservationID = Convert.ToInt32(dr["FK_HotelReservationID_ID"].ToString());
                    PageObj.Promotion = dr["FK_PromotionID_ID"].ToString();
                    PageObj.HotelPromotionID = Convert.ToInt32(dr["FK_HotelPromotionID_ID"]);
                
                    list.Add(PageObj);
                }
            }


            return list;
        }

    }
    public class TB_HotelReservationPromotionExt
    {
        public int ID { get; set; }
        public Int64 ReservationID { get; set; }
        public int HotelPromotionID { get; set; }
        public int HotelReservationID { get; set; }
        public string Promotion { get; set; }
       

    }
}