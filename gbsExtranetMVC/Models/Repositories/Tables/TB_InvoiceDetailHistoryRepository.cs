using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_InvoiceDetailHistoryRepository : BaseRepository
    {
        public static string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_InvoiceDetailHistoryExt> ReadAll(int TableID)
        {
            List<TB_InvoiceDetailHistoryExt> list = new List<TB_InvoiceDetailHistoryExt>();

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
                    TB_InvoiceDetailHistoryExt PageObj = new TB_InvoiceDetailHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.InvoiceDetailID = Convert.ToInt32(dr["InvoiceDetailID"]);
                    PageObj.Invoice = dr["FK_InvoiceID_ID"].ToString();
                    PageObj.ReservationID = dr["FK_ReservationID_ID"].ToString();
                    PageObj.LogDateTime = dr["LogDateTime"].ToString();
                    PageObj.Loguser = dr["FK_LogUserID_ID"].ToString();

                    list.Add(PageObj);
                }
            }


            return list;
        }

    }
    public class TB_InvoiceDetailHistoryExt
    {

        public int ID { get; set; }
        public string LogDateTime { get; set; }
        public string Loguser { get; set; }

        public int InvoiceDetailID { get; set; }

        public string Invoice { get; set; }

        public string ReservationID { get; set; }
    }
}