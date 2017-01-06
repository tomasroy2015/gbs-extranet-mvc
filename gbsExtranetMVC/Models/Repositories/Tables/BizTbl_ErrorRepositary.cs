using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_ErrorRepositary : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_ErrorExt> ReadAll(int TableID)
        {
            List<BizTbl_ErrorExt> list = new List<BizTbl_ErrorExt>();
            DBEntities entity = new DBEntities();
            var TableIDparam = new SqlParameter("@TableID", TableID);
            var Cultureparam = new SqlParameter("@CultureCode", CultureCode);
            var result = entity.Database.SqlQuery<BizTbl_Error>("B_DisplayTable_BizTbl_Table_Sp @TableID,@CultureCode", TableIDparam, Cultureparam).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Page");
            dt.Columns.Add("Message");
            dt.Columns.Add("Detail");
            dt.Columns.Add("Date");
            dt.Columns.Add("IPAddress");
            foreach (BizTbl_Error dr in result)
            {
                dt.Rows.Add(dr.ID, dr.Page, dr.Message, dr.Detail,  dr.Date, dr.IPAddress);
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_ErrorExt model = new BizTbl_ErrorExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Page = dr["Page"].ToString();
                    model.Message = dr["Message"].ToString();
                    model.Detail = dr["Detail"].ToString();
                    model.Date = Convert.ToDateTime(dr["Date"]);
                    model.IPAddress = dr["IPAddress"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class BizTbl_ErrorExt
    {
        public int ID { get; set; }
        public string Page { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string IPAddress { get; set; }
    }
}