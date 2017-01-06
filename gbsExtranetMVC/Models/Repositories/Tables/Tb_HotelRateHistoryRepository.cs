using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class Tb_HotelRateHistoryRepository : BaseRepository 
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<Tb_HotelRateHistoryExt> ReadAll(int TableID)
        {
            List<Tb_HotelRateHistoryExt> list = new List<Tb_HotelRateHistoryExt>();

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
                    Tb_HotelRateHistoryExt model = new Tb_HotelRateHistoryExt();
                    model.ID = Convert.ToInt64(dr["ID"]);
                    model.HotelRateID = Convert.ToInt32(dr["HotelRateID"].ToString());
                    model.HotelRoomId = Convert.ToInt32(dr["FK_HotelRoomID_ID"].ToString());
                    model.DateID = dr["FK_DateID_ID"].ToString();
                    model.PricePolicyTypeID = dr["FK_PricePolicyTypeID_ID"].ToString();
                    model.HotelAccommodationTypeID = dr["FK_HotelAccommodationTypeID_ID"].ToString();
                    model.SinglePrice = Convert.ToDouble(dr["SinglePrice"]);
                    model.DoublePrice = Convert.ToDouble(dr["DoublePrice"]);
                    model.RoomPrice = Convert.ToDouble(dr["RoomPrice"]);
                    model.CurrencyID = dr["FK_CurrencyID_ID"].ToString();                                                     
                    model.LogDateTime = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    model.LogUserID = dr["FK_LogUserID_ID"].ToString();    
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class Tb_HotelRateHistoryExt
    {
        public Int64 ID { get; set; }
        public int HotelRateID { get; set; }
        public int HotelRoomId { get; set; }
        public string DateID { get; set; }
        public string PricePolicyTypeID { get; set; }
        public string HotelAccommodationTypeID { get; set; }
        public double SinglePrice { get; set; }
        public double DoublePrice { get; set; }
        public double RoomPrice { get; set; }
        public string CurrencyID { get; set; }
        //public DateTime OpDateTime { get; set; }
        //public Int64 OpUserID { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogUserID { get; set; }
        
    }
}