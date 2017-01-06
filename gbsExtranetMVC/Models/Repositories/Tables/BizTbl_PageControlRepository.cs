using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_PageControlRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_PageControlExt> ReadAll(int TableID)
        {
            List<BizTbl_PageControlExt> list = new List<BizTbl_PageControlExt>();

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
                    BizTbl_PageControlExt PageObj = new BizTbl_PageControlExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.PageID = dr["PageID"].ToString();
                    PageObj.PageName = dr["Page"].ToString();
                    PageObj.Code = dr["Code"].ToString();
                    PageObj.Text = dr["Text"].ToString();
                    PageObj.Description = dr["Description"].ToString();
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(BizTbl_PageControlExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            BizTbl_PageControl MsgObj = new BizTbl_PageControl();
            MsgObj.ID = model.ID;
            //MsgObj.MailTemplateID = model.MailTemplateID;
            MsgObj.Code = model.Code;
            MsgObj.Description_en = model.Description;
            MsgObj.PageID = Convert.ToInt32(model.PageID);
            MsgObj.Text_en = model.Text;
            MsgObj.OpDateTime = DateTime.Now;
            MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.BizTbl_PageControl.Add(MsgObj);
            insertentity.SaveChanges();
            return status;
        }


        public bool Delete(BizTbl_PageControlExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_PageControl.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_PageControl.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_PageControlExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.BizTbl_PageControl.Where(x => x.ID == model.ID).FirstOrDefault();
            // MailTable.MailTemplateID =model.MailTemplateID;
            PageObj.Code = model.Code;
            PageObj.Description_en = model.Description;
            PageObj.PageID = Convert.ToInt32(model.PageID);
            PageObj.Text_en = model.Text;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
    }
    public class BizTbl_PageControlExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string PageID { get; set; } 
        public string PageName { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Code { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}