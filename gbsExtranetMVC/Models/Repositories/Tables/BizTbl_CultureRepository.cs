using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_CultureRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_CultureExt> ReadAll(int TableID)
        {
            List<BizTbl_CultureExt> list = new List<BizTbl_CultureExt>();
            DBEntities entity = new DBEntities();
            var TableIDparam = new SqlParameter("@TableID", TableID);
            var Cultureparam = new SqlParameter("@CultureCode", CultureCode);
            var result = entity.Database.SqlQuery<BizTbl_Culture>("B_DisplayTable_BizTbl_Table_Sp @TableID,@CultureCode", TableIDparam, Cultureparam).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Code");
            dt.Columns.Add("SystemCode");
            dt.Columns.Add("Description");
            dt.Columns.Add("Active");
            dt.Columns.Add("OpDateTime");
            dt.Columns.Add("OpUserID");
            foreach (BizTbl_Culture dr in result)
            {
                dt.Rows.Add(dr.ID, dr.Code, dr.SystemCode, dr.Description, dr.Active,dr.OpDateTime,dr.OpUserID);
            }
           
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_CultureExt model = new BizTbl_CultureExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();
                    model.SystemCode = dr["SystemCode"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    model.OpUserID = Convert.ToInt64(dr["OpUserID"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(BizTbl_CultureExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_Culture obj = new BizTbl_Culture();
            obj.ID = model.ID;
            obj.Code = model.Code;
            obj.SystemCode = model.SystemCode;
            obj.Description = model.Description;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.BizTbl_Culture.Add(obj);
            db.SaveChanges();

            int id = obj.ID;

            return status;
        }

        public bool Delete(BizTbl_CultureExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Culture.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_Culture.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_CultureExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Culture.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.Code;
            obj.SystemCode = model.SystemCode;
            obj.Description = model.Description;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }
    }

    public class BizTbl_CultureExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int ID { get; set; }
         [Required(ErrorMessage = "This field is required!")]
         [StringLength(3, ErrorMessage = "This field cannot be longer than 3 characters.")]
        public string Code { get; set; }
         [Required(ErrorMessage = "This field is required!")]
        public string SystemCode { get; set; }
         [Required(ErrorMessage = "This field is required!")]
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}