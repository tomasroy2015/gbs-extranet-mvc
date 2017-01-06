using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRoomBedHistoryRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelRoomBedHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelRoomBedHistoryExt> list = new List<TB_HotelRoomBedHistoryExt>();

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
                    TB_HotelRoomBedHistoryExt PageObj = new TB_HotelRoomBedHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelRoomBedID = Convert.ToInt32(dr["HotelRoomBedID"].ToString());
                    PageObj.OptionNo = Convert.ToInt32(dr["OptionNo"].ToString());
                    PageObj.BedType = dr["FK_BedTypeID_ID"].ToString();
                    PageObj.HotelRoomID = Convert.ToInt32(dr["FK_HotelRoomID_ID"]);
                    PageObj.Count = Convert.ToInt32(dr["Count"].ToString());
                    PageObj.LogDateTime = dr["LogDateTime"].ToString();
                    PageObj.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(PageObj);
                }
            }


            return list;
        }


    }
    public class TB_HotelRoomBedHistoryExt
    {
        public int ID { get; set; }
        public int HotelRoomID { get; set; }
        public int HotelRoomBedID { get; set; }
        public string BedType { get; set; }
        public string Attribute { get; set; }
        public string Charge { get; set; }
        public int Count { get; set; }
        public int OptionNo { get; set; }
        public string LogDateTime { get; set; }
        public string LogUser { get; set; }



    }
}