using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelPromotionHistoryRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelPromotionHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelPromotionHistoryExt> list = new List<TB_HotelPromotionHistoryExt>();

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
                    TB_HotelPromotionHistoryExt PageObj = new TB_HotelPromotionHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelPromotionID = Convert.ToInt32(dr["HotelPromotionID"].ToString());
                    PageObj.Hotel = dr["FK_HotelID_ID"].ToString();
                    PageObj.Promotion = dr["FK_PromotionID_ID"].ToString();
                    PageObj.StartDate = dr["StartDate"].ToString();
                    PageObj.EndDate = dr["EndDate"].ToString();
                    PageObj.DayCount = Convert.ToInt32(dr["DayCount"]);
                    PageObj.DiscountPercentage = Convert.ToInt32(dr["DiscountPercentage"]);
                    PageObj.Region = dr["Region"].ToString();
                    PageObj.ValidForAllRoomTypes = Convert.ToBoolean(dr["ValidForAllRoomTypes"].ToString());
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    PageObj.LogDateTime = dr["LogDateTime"].ToString();
                    PageObj.Loguser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(PageObj);
                }
            }


            return list;
        }

    }
    public class TB_HotelPromotionHistoryExt
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int DayCount { get; set; }
        public int DiscountPercentage { get; set; }

        public int HotelPromotionID { get; set; }
        public string Hotel { get; set; }
        public string Promotion { get; set; }
        public string Region { get; set; }
        public bool ValidForAllRoomTypes { get; set; }
        public bool Active { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreateDateTime { get; set; }
        public string LogDateTime { get; set; }
        public string Loguser { get; set; }

    }
}