using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_MailTemplateRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_MailTemplateExt> ReadAll(int TableID)
        {
            List<BizTbl_MailTemplateExt> list = new List<BizTbl_MailTemplateExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTable_BizTbl_Table_Sp", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TableID", TableID);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            //return dt;

            //string MailTemplateID = "";
            //string Description = "";
            //  CountryTble = objcountry.GetCountriestble(CultureCode);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_MailTemplateExt model = new BizTbl_MailTemplateExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();
                    model.MailParameters = dr["MailParameters"].ToString();
                    model.MailFrom = dr["MailFrom"].ToString();
                    model.MailTo = dr["MailTo"].ToString();
                    model.MailCC = dr["MailCC"].ToString();
                    model.Description_en = dr["Desctiption"].ToString();
                    model.Subject_en = dr["Subject"].ToString();
                    model.Body_en = dr["Body"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    //model.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    //model.OpUserID = Convert.ToInt64(dr["OpUserID"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(BizTbl_MailTemplateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_MailTemplate obj = new BizTbl_MailTemplate();
            obj.Code = model.Code;
            obj.Description_en = model.Description_en;
            obj.MailParameters = model.MailParameters;
            obj.MailFrom = model.MailFrom;
            obj.MailTo = model.MailTo;
            obj.Subject_en = model.Subject_en;
            obj.Body_en = model.Body_en;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.BizTbl_MailTemplate.Add(obj);
            db.SaveChanges();

            int id = obj.ID;

            return status;
        }

        public bool Delete(BizTbl_MailTemplateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_MailTemplate.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_MailTemplate.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_MailTemplateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_MailTemplate.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.Code;
            obj.Description_en = model.Description_en;
            obj.MailParameters = model.MailParameters;
            obj.MailFrom = model.MailFrom;
            obj.MailTo = model.MailTo;
            obj.Subject_en = model.Subject_en;
            obj.Body_en = model.Body_en;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }
    }
    public class BizTbl_MailTemplateExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Code { get; set; }
        public string MailParameters { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailCC { get; set; }
        public string Description_en { get; set; }
        public string Subject_en { get; set; }
        public string Body_en { get; set; }
        public bool Active { get; set; }
       // public DateTime OpDateTime { get; set; }
        //public long OpUserID { get; set; }
    }
}