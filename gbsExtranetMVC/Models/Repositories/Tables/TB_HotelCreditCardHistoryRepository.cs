using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelCreditCardHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelCreditCardHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelCreditCardHistoryExt> list = new List<TB_HotelCreditCardHistoryExt>();

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
                    TB_HotelCreditCardHistoryExt model = new TB_HotelCreditCardHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelCreditCardID = Convert.ToInt32(dr["HotelCreditCardID"]);
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.CreditCardType = dr["FK_CreditCardTypeID"].ToString();
                    model.OperationDate = dr["OpDateTime"].ToString();
                    model.OperationUser = dr["FK_OpUserID"].ToString();
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    model.LogUser = dr["FK_LogUserID_"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_HotelCreditCardHistoryExt
    {
        public int ID { get; set; }
        public int HotelCreditCardID { get; set; }
        public string Hotel { get; set; }
        public string CreditCardType { get; set; }
        public string OperationDate { get; set; }
        public string OperationUser { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}