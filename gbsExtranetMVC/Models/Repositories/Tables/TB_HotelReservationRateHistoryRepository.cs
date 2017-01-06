using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelReservationRateHistoryRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelReservationRateHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelReservationRateHistoryExt> list = new List<TB_HotelReservationRateHistoryExt>();

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
                    TB_HotelReservationRateHistoryExt PageObj = new TB_HotelReservationRateHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelReservationRateID = Convert.ToInt32(dr["HotelReservationRateID"]);
                    PageObj.HotelReservationID = Convert.ToInt32(dr["FK_HotelReservationID_ID"]);
                    PageObj.Date = dr["FK_DateID_ID"].ToString();
                    PageObj.RoomPrice = Convert.ToDouble(dr["RoomPrice"].ToString());
                    PageObj.Currency = dr["FK_CurrencyID_ID"].ToString();
                    PageObj.CancelDateTime = dr["CancelDateTime"].ToString();
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    PageObj.LogDateTime = dr["LogDateTime"].ToString();
                    PageObj.LogUser = dr["FK_LogUserID_ID"].ToString();
                  
                    list.Add(PageObj);
                }
            }


            return list;
        }

    }
    public class TB_HotelReservationRateHistoryExt
    {
        public string LogDateTime { get; set; }
        public string LogUser { get; set; }
        public int ID { get; set; }
        public string Currency { get; set; }
        public string Date { get; set; }
        public string CancelDateTime { get; set; }
        public double RoomPrice { get; set; }
        public int HotelReservationID { get; set; }
        public int HotelReservationRateID { get; set; }
        public bool Active { get; set; }


    }
}