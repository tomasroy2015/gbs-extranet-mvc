using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class Biztbl_PageRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<Biztbl_PageExt> ReadAll(int TableID)
        {
            List<Biztbl_PageExt> list = new List<Biztbl_PageExt>();

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
                    Biztbl_PageExt PageObj = new Biztbl_PageExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.Code = dr["Code"].ToString();
                    PageObj.Title = dr["Title"].ToString();
                    PageObj.Description = dr["Description"].ToString();
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(Biztbl_PageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            BizTbl_Page MsgObj = new BizTbl_Page();
            MsgObj.ID = model.ID;
            //MsgObj.MailTemplateID = model.MailTemplateID;
            MsgObj.Code = model.Code;
            MsgObj.Description_en = model.Description;
            MsgObj.Title_en = model.Title;
            MsgObj.OpDateTime = DateTime.Now;
            MsgObj.OpUserID = 0;
            insertentity.BizTbl_Page.Add(MsgObj);
            insertentity.SaveChanges();
            return status;
        }


        public bool Delete(Biztbl_PageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Page.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_Page.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(Biztbl_PageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.BizTbl_Page.Where(x => x.ID == model.ID).FirstOrDefault();
            // MailTable.MailTemplateID =model.MailTemplateID;
            PageObj.Code = model.Code;
            PageObj.Description_en = model.Description;
            PageObj.Title_en = model.Title;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = 0;
            db.SaveChanges();
            return status;
        }
    }
    public  class Biztbl_PageExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}