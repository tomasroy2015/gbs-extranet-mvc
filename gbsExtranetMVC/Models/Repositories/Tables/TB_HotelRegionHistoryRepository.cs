using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRegionHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelRegionHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelRegionHistoryExt> list = new List<TB_HotelRegionHistoryExt>();

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
                    TB_HotelRegionHistoryExt PageObj = new TB_HotelRegionHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    PageObj.HotelRegionID = Convert.ToInt32(dr["HotelRegionID"].ToString());
                    PageObj.Hotel = dr["FK_HotelID_ID"].ToString();
                    PageObj.Region = dr["FK_RegionID_ID"].ToString();
                    PageObj.LogDateTime = dr["LogDateTime"].ToString();
                    PageObj.Loguser = dr["FK_LogUserID_ID"].ToString();

                    list.Add(PageObj);
                }
            }


            return list;
        }

    }
    public class TB_HotelRegionHistoryExt
    {

        public int ID { get; set; }
        public int HotelID { get; set; }

        public int HotelRegionID { get; set; }
        public string Hotel { get; set; }

        public string Region { get; set; }

        public bool Active { get; set; }

        public string LogDateTime { get; set; }
        public string Loguser { get; set; }

    }
}