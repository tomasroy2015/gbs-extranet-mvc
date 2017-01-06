using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BusinessPartnerRegionRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_BusinessPartnerRegionExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerRegionExt> list = new List<TB_BusinessPartnerRegionExt>();
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
                    TB_BusinessPartnerRegionExt model = new TB_BusinessPartnerRegionExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.Region = dr["FK_RegionID_ID"].ToString();
                    model.RegionID = Convert.ToInt32(dr["RegionID"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_BusinessPartnerRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_BusinessPartnerRegion obj = new TB_BusinessPartnerRegion();
            
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.RegionID = Convert.ToInt32(model.RegionID);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_BusinessPartnerRegion.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_BusinessPartnerRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartnerRegion.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_BusinessPartnerRegion.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_BusinessPartnerRegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartnerRegion.Where(x => x.ID == model.ID).FirstOrDefault();

            obj.ID = model.ID;
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.RegionID = Convert.ToInt32(model.RegionID);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }


    }

    public class TB_BusinessPartnerRegionExt
    {
        public int BusinessPartnerID { get; set; }
        public int ID { get; set; }
        public int RegionID { get; set; }
        
        public string Region { get; set; }
        public string BusinessPartner { get; set; }
        
    }
}