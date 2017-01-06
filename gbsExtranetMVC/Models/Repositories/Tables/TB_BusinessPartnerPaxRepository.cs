using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BusinessPartnerPaxRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_BusinessPartnerPaxExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerPaxExt> list = new List<TB_BusinessPartnerPaxExt>();
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
                    TB_BusinessPartnerPaxExt model = new TB_BusinessPartnerPaxExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.MinPeopleCount = Convert.ToInt16(dr["MinPeopleCount"]);
                    model.MaxPeopleCount = Convert.ToInt16(dr["MaxPeopleCount"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_BusinessPartnerPaxExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_BusinessPartnerPax obj = new TB_BusinessPartnerPax();
            
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.Name = model.Name;
            obj.MinPeopleCount = model.MinPeopleCount;
            obj.MaxPeopleCount = model.MaxPeopleCount;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_BusinessPartnerPax.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_BusinessPartnerPaxExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartnerPax.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_BusinessPartnerPax.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_BusinessPartnerPaxExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartnerPax.Where(x => x.ID == model.ID).FirstOrDefault();

            obj.ID = model.ID;
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.Name = model.Name;
            obj.MinPeopleCount = model.MinPeopleCount;
            obj.MaxPeopleCount = model.MaxPeopleCount;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }


    }

    public class TB_BusinessPartnerPaxExt
    {
        public int ID { get; set; }
        public int BusinessPartnerID { get; set; }
        public string BusinessPartner { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public Int16 MinPeopleCount { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public Int16 MaxPeopleCount { get; set; }
        public bool Active { get; set; }
    }
}