using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRegionRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelRegionExt> ReadAll(int TableID)
        {
            List<TB_HotelRegionExt> list = new List<TB_HotelRegionExt>();

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
                    TB_HotelRegionExt PageObj = new TB_HotelRegionExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.Hotel = dr["FK_HotelID_ID"].ToString();
                    PageObj.Region =dr["FK_RegionID_ID"].ToString();
                    PageObj.RegionID = Convert.ToInt64(dr["RegionID"]);
                    PageObj.HotelID = Convert.ToInt32(dr["HotelID"].ToString());
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(TB_HotelRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelRegion PageObj = new TB_HotelRegion();
            //PageObj.ID = model.ID;
            PageObj.HotelID = model.HotelID;
            PageObj.RegionID = model.RegionID;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.TB_HotelRegion.Add(PageObj);
            insertentity.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelRegion.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelRegion.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelRegion.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.HotelID = model.HotelID;
            PageObj.RegionID = model.RegionID;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

    }
    public class TB_HotelRegionExt
    {
        public int ID { get; set; }
        public string Hotel { get; set; }
        public string Region { get; set; }
        public Int64 RegionID { get; set; }
        public int HotelID { get; set; }
        public bool Active { get; set; }
        
    }
}