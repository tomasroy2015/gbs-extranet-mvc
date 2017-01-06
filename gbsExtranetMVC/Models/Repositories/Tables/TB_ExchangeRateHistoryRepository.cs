using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_ExchangeRateHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ExchangeRateHistoryExt> ReadAll(int TableID)
        {
            List<TB_ExchangeRateHistoryExt> list = new List<TB_ExchangeRateHistoryExt>();
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
                    TB_ExchangeRateHistoryExt model = new TB_ExchangeRateHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.ExchangeRateID = Convert.ToInt32(dr["ExchangeRateID"]);
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    model.Date = Convert.ToDateTime(dr["FK_DateID_ID"]);
                    model.Rate = dr["Rate"].ToString();
                    model.LogDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    model.LogUserID = dr["FK_OpUserID_ID"].ToString();
                    model.OpDateTime = Convert.ToDateTime(dr["LogDateTime"]);
                    model.OpUserID = dr["FK_LogUserID_ID"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_ExchangeRateHistoryExt
    {
        public int ID { get; set; }
        public int ExchangeRateID { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Rate { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogUserID { get; set; }
        public DateTime OpDateTime { get; set; }
        public string OpUserID { get; set; }
    }
}