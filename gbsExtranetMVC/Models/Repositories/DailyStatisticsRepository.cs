using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{

    public class HitcountExt
    {
        public int ID { get; set; }
        public string Part { get; set; }
        public string RecordID { get; set; }
        public DateTime Date { get; set; }
        public int HitCount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }


     
    }
    public class DailyStatisticsRepository
    {

        public List<HitcountExt> GetHitCountTableValue()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Part");
            dt.Columns.Add("RecordID");
            dt.Columns.Add("HitCount");
            dt.Columns.Add("Description");
            dt.Columns.Add("Date");
            var result = entity.Database.SqlQuery<GetHitCountDetails_Result>("B_Ex_GetHitTableValue_TB_HitCount_SP").ToList();

            List<HitcountExt> ListOfModel = new List<HitcountExt>();
            foreach (GetHitCountDetails_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.PartIDName, Val.RecordID, Val.HitCount, Val.Description, Val.Date);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HitcountExt HitObj = new HitcountExt();
                    HitObj.ID = Convert.ToInt32(dr["ID"]);
                    HitObj.Part = dr["Part"].ToString();
                    HitObj.RecordID = dr["RecordID"].ToString();
                    HitObj.HitCount = Convert.ToInt32(dr["HitCount"]);
                    HitObj.Description = dr["Description"].ToString();
                    HitObj.Date = Convert.ToDateTime(dr["Date"]);
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }

        public List<HitcountExt> GetPartTableValue()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            List<HitcountExt> ListOfModel = new List<HitcountExt>();
            var result = entity.Database.SqlQuery<GetPart_Result>("B_Ex_GetPart_TB_Part_SP").ToList();


            foreach (GetPart_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    HitcountExt HitObj = new HitcountExt();
                    HitObj.ID = Convert.ToInt32(dr["ID"]);
                    HitObj.Name = dr["Name"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }
      
    }
}