using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BusinessPartnerRegionHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_BusinessPartnerRegionHistoryExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerRegionHistoryExt> list = new List<TB_BusinessPartnerRegionHistoryExt>();
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
                    TB_BusinessPartnerRegionHistoryExt model = new TB_BusinessPartnerRegionHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerRegionID = Convert.ToInt32(dr["BusinessPartnerRegionID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.Region = dr["FK_RegionID_ID"].ToString();
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_BusinessPartnerRegionHistoryExt
    {
        public int ID { get; set; }
        public int BusinessPartnerRegionID { get; set; }
        public string BusinessPartner { get; set; }
        public string Region { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}