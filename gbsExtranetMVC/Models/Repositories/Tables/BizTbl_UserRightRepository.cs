using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_UserRightRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_UserRightExt> ReadAll(int TableID)
        {
            List<BizTbl_UserRightExt> list = new List<BizTbl_UserRightExt>();
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
                    BizTbl_UserRightExt model = new BizTbl_UserRightExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.User = dr["FK_UserID_ID"].ToString();
                    model.Role = dr["FK_SecurityGroupID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class BizTbl_UserRightExt
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Role { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}