using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HitCountRepository : BaseRepository
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_HitCountExt> ReadAll(int TableID)
        {
            List<TB_HitCountExt> list = new List<TB_HitCountExt>();

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
                    TB_HitCountExt model = new TB_HitCountExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Part = dr["FK_PartID"].ToString();
                    model.Record = dr["RecordID"].ToString();
                    model.Date = dr["Date"].ToString();
                    model.HitCount = dr["HitCount"].ToString();
                    model.Description = dr["Description"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

    }

    public class TB_HitCountExt
    {
        public int ID { get; set; }
        public string Part { get; set; }
        public string Record { get; set; }
        public string Date { get; set; }
        public string HitCount { get; set; }
        public string Description { get; set; }
    }
}