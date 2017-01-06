using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories.Tables
{
    public class TB_TourRegionHistoryRepository:BaseRepository
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TourRegionHistoryExt> ReadAll(int TableID)
        {
            List<TB_TourRegionHistoryExt> list = new List<TB_TourRegionHistoryExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTableNew_BizTbl_Table_Sp", SQLCon);
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
                    TB_TourRegionHistoryExt model = new TB_TourRegionHistoryExt();
                    model.ID = Convert.ToInt64(dr["ID"]);
                    model.TourRegionID = Convert.ToInt32(dr["TourRegionID"].ToString());
                    model.TourID = dr["FK_TourID_ID"].ToString();
                    model.RegionID = dr["FK_RegionID_ID"].ToString();                    
                    model.LogDateTime = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    model.LogUserID = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
    public class TB_TourRegionHistoryExt
    {
        public Int64 ID { get; set; }
        public int TourRegionID { get; set; }
        public string TourID { get; set; }
        public string RegionID { get; set; }
        public DateTime OpDateTime { get; set; }
        public Int64 OpUserID { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogUserID { get; set; }
    }
}