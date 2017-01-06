using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories.Tables
{    
    public class TB_MessengerRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_MessengerExt> ReadAll(int TableID)
        {
            List<TB_MessengerExt> list = new List<TB_MessengerExt>();

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
                    TB_MessengerExt model = new TB_MessengerExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.From = dr["From"].ToString();
                    model.To = dr["To"].ToString();
                    model.Message = dr["Message"].ToString();
                    model.Sent = Convert.ToDateTime(dr["Sent"].ToString());
                    model.Recd = Convert.ToInt32(dr["Recd"].ToString());                   
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_MessengerExt
    {
        public int ID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public DateTime Sent { get; set; }
        public int Recd { get; set; }
    }
}