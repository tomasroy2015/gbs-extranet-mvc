using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_ReservationStatusHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ReservationStatusHistoryExt> ReadAll(int TableID)
        {
            List<TB_ReservationStatusHistoryExt> list = new List<TB_ReservationStatusHistoryExt>();

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
                    TB_ReservationStatusHistoryExt PageObj = new TB_ReservationStatusHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.ReservationID = dr["FK_ReservationID_ID"].ToString();
                    PageObj.Status = dr["FK_StatusID_ID"].ToString();
                 
                    list.Add(PageObj);
                }
            }

            return list;
        }
    }
    public class TB_ReservationStatusHistoryExt
    {
        public int ID { get; set; }
        public string ReservationID { get; set; }
        public string Status { get; set; }

    }
}