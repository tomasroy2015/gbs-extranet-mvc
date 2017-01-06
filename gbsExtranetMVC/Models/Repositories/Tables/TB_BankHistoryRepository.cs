using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BankHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_BankHistoryExt> ReadAll(int TableID)
        {
            List<TB_BankHistoryExt> list = new List<TB_BankHistoryExt>();
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
                    TB_BankHistoryExt model = new TB_BankHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BankID = Convert.ToInt32(dr["BankID"]);
                    model.Country = dr["FK_CountryID_ID"].ToString();
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    model.BankName = dr["BankName"].ToString();
                    model.BankBranchName = dr["BankBranchName"].ToString();
                    model.BankAccountNumber = dr["BankAccountNumber"].ToString();
                    model.IBAN = dr["IBAN"].ToString();
                    model.SWIFT = dr["SWIFT"].ToString();
                    model.OtherInfo = dr["OtherInfo"].ToString();
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
    public class TB_BankHistoryExt
    {
        public int ID { get; set; }
        public int BankID { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankAccountNumber { get; set; }
        public string IBAN { get; set; }
        public string SWIFT { get; set; }
        public string OtherInfo { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogUserID { get; set; }
        public DateTime OpDateTime { get; set; }
        public string OpUserID { get; set; }
    }
}