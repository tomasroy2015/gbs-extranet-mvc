using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_InvoiceDetailRepository:BaseRepository
    {
        public static string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_InvoiceDetailExt> ReadAll(int TableID)
        {
            List<TB_InvoiceDetailExt> list = new List<TB_InvoiceDetailExt>();

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
                    TB_InvoiceDetailExt PageObj = new TB_InvoiceDetailExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.InvoiceID = Convert.ToInt32(dr["FK_InvoiceID_ID"]);
                    PageObj.ReservationID = Convert.ToInt32(dr["FK_ReservationID_ID"]);
                    list.Add(PageObj);
                }
            }


            return list;
        }


    }
    public class TB_InvoiceDetailExt
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int ReservationID { get; set; }
     
    }

}