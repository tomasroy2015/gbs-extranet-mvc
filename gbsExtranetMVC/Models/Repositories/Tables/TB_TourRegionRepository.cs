using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories.Tables
{
    public class TB_TourRegionRepository: BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TourRegionExt> ReadAll(int TableID)
        {
            List<TB_TourRegionExt> list = new List<TB_TourRegionExt>();

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
                    TB_TourRegionExt model = new TB_TourRegionExt();
                    model.ID = Convert.ToInt64(dr["ID"]);                   
                    model.Tour = dr["FK_TourID_ID"].ToString();
                    model.TourID = Convert.ToInt32(dr["TourID"].ToString());
                    model.RegionName = dr["FK_RegionID_ID"].ToString();
                    model.RegionID = Convert.ToInt32(dr["RegionID"].ToString());
                    list.Add(model);
                }
            }
            return list;
        }
        public bool Create(TB_TourRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            TB_TourRegion obj = new TB_TourRegion();            
            obj.TourID = model.TourID;
            obj.RegionID = model.RegionID;  
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_TourRegion.Add(obj);
            db.SaveChanges();
            Int64 id = obj.ID;
            return status;
        }
        public bool Update(TB_TourRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_TourRegion.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.TourID = model.TourID;
            obj.RegionID = model.RegionID;            
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = 0;
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_TourRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_TourRegion.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_TourRegion.Remove(obj);
            db.SaveChanges();
            return status;
        }
    }
    public class TB_TourRegionExt
    {
        public Int64 ID { get; set; }
        public int TourRegionID { get; set; }
        public int TourID { get; set; }
        public int RegionID { get; set; }
        public string Tour { get; set; }
        public string RegionName { get; set; }      
    }
}