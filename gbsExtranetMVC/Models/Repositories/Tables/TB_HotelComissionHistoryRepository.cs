using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelComissionHistoryRepository :BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelComissionHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelComissionHistoryExt> list = new List<TB_HotelComissionHistoryExt>();

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
                    TB_HotelComissionHistoryExt model = new TB_HotelComissionHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelCommisionID = Convert.ToInt32(dr["HotelComissionID"]);
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.StartDate = dr["StartDate"].ToString();
                    model.EndDate = dr["EndDate"].ToString();
                    model.Commision = dr["Comission"].ToString();
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_HotelComissionHistoryExt
    {
        public int ID { get; set; }
        public int HotelCommisionID { get; set; }
        public string Hotel { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Commision { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}