using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelAvailabilityHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelAvailabilityHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelAvailabilityHistoryExt> list = new List<TB_HotelAvailabilityHistoryExt>();

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
                    TB_HotelAvailabilityHistoryExt model = new TB_HotelAvailabilityHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelAvailabilityID = Convert.ToInt32(dr["HotelAvailabilityID"]);
                    model.Room = Convert.ToInt32(dr["FK_HotelRoomID"]);
                    model.Date = dr["FK_DateID"].ToString();
                    model.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    model.CTA = Convert.ToInt32(dr["CloseToArrival"]);
                    model.CTD = Convert.ToInt32(dr["CloseToDeparture"]);
                    model.MinimumStay = Convert.ToInt32(dr["MinimumStay"]);
                    model.LogDate = dr["LogDateTime"].ToString();
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_HotelAvailabilityHistoryExt
    {
        public int ID { get; set; }
        public int HotelAvailabilityID { get; set; }
        public int Room { get; set; }
        public string Date { get; set; }     
        public int RoomCount { get; set; }
        public int CTA { get; set; }
        public int CTD { get; set; }
        public int MinimumStay { get; set; }
        public string LogDate { get; set; }
        public string LogUser { get; set; }
    }
}