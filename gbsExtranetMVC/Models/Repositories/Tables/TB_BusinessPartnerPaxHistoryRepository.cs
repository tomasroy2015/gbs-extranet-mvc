using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BusinessPartnerPaxHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_BusinessPartnerPaxHistoryExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerPaxHistoryExt> list = new List<TB_BusinessPartnerPaxHistoryExt>();
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
                    TB_BusinessPartnerPaxHistoryExt model = new TB_BusinessPartnerPaxHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.PaxID = Convert.ToInt32(dr["BusinessPartnerPaxID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.MinPeopleCount = dr["MinPeopleCount"].ToString();
                    model.MaxPeopleCount = dr["MaxPeopleCount"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_BusinessPartnerPaxHistoryExt
    {
        public int ID { get; set; }
        public int PaxID { get; set; }
        public string BusinessPartner { get; set; }
        public string Name { get; set; }
        public string MinPeopleCount { get; set; }
        public string MaxPeopleCount { get; set; }
        public bool Active { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
   
}