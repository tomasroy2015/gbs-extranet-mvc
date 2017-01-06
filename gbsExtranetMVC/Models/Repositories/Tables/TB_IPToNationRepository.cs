using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class B_IPToNationRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_IPToNationExt> ReadAll(int TableID)
        {
            List<TB_IPToNationExt> list = new List<TB_IPToNationExt>();

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
                    TB_IPToNationExt PageObj = new TB_IPToNationExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.Country = dr["FK_CountryID_ID"].ToString();
                    PageObj.BeginningIpAddress = dr["BeginningIpAddress"].ToString();
                    PageObj.EndingIpAddress = dr["EndingIpAddress"].ToString();
                    PageObj.BeginningIpNumber = dr["BeginningIpNumber"].ToString();
                    PageObj.EndingIpNumber = dr["EndingIpNumber"].ToString();
                    PageObj.OpDateTime = dr["OpDateTime"].ToString();
                    PageObj.OpLoguser = dr["FK_OpUserID_ID"].ToString();
                   
                    list.Add(PageObj);
                }
            }


            return list;
        }

    }
    public class TB_IPToNationExt
    {

        public int ID { get; set; }
        public string Country { get; set; }
        public string BeginningIpAddress { get; set; }
        public string EndingIpAddress { get; set; }
        public string BeginningIpNumber { get; set; }
        public string EndingIpNumber { get; set; }
        public string OpDateTime { get; set; }
        public string OpLoguser { get; set; }
    }
}