using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_MessageRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_MessageExt> ReadAll(int TableID)
        {
            List<BizTbl_MessageExt> list = new List<BizTbl_MessageExt>();

            //DataTable dt = new DataTable();
            //SQLCon.Open();
            //SqlCommand cmd = new SqlCommand("B_DisplayTable_BizTbl_Table_Sp", SQLCon);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@TableID", TableID);
            //cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //SQLCon.Close();


            DBEntities entity = new DBEntities();
            var TableIDparam = new SqlParameter("@TableID", TableID);
            var Cultureparam = new SqlParameter("@CultureCode", CultureCode);
            var result = entity.Database.SqlQuery<BizTbl_Message>("B_DisplayTable_BizTbl_Table_Sp @TableID,@CultureCode", TableIDparam, Cultureparam).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Code");
            dt.Columns.Add("Description_ar");
            dt.Columns.Add("Description_de");
            dt.Columns.Add("Description_en");
            dt.Columns.Add("Description_es");
            dt.Columns.Add("Description_fr");
            dt.Columns.Add("Description_it");
            dt.Columns.Add("Description_ja");
            dt.Columns.Add("Description_pt");
            dt.Columns.Add("Description_ru");
            dt.Columns.Add("Description_tr");
            dt.Columns.Add("Description_zh");
            dt.Columns.Add("IsCommon");
            dt.Columns.Add("OpDateTime");
            dt.Columns.Add("OpUserID");
            foreach (BizTbl_Message dr in result)
            {
                dt.Rows.Add(dr.ID, dr.Code, dr.Description_ar, dr.Description_de, dr.Description_en, dr.Description_es, dr.Description_fr,dr.Description_it,dr.Description_ja,
                    dr.Description_pt,dr.Description_ru,dr.Description_tr,dr.Description_zh,dr.IsCommon,dr.OpDateTime,dr.OpUserID);
            }
            //return dt;

            //string MailTemplateID = "";
            //string Description = "";
            //  CountryTble = objcountry.GetCountriestble(CultureCode);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_MessageExt model = new BizTbl_MessageExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();
                    model.Description_en = dr["Description_en"].ToString();
                    model.Description_de = dr["Description_de"].ToString();
                    model.Description_tr = dr["Description_tr"].ToString();
                    model.Description_es = dr["Description_es"].ToString();
                    model.Description_fr = dr["Description_fr"].ToString();
                    model.Description_ru = dr["Description_ru"].ToString();
                    model.Description_it = dr["Description_it"].ToString();
                    model.Description_ar = dr["Description_ar"].ToString();
                    model.Description_ja = dr["Description_ja"].ToString();
                    model.Description_pt = dr["Description_pt"].ToString();
                    model.Description_zh = dr["Description_zh"].ToString();
                    model.IsCommon = Convert.ToBoolean( dr["IsCommon"]);
                    
                    //model.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    //model.OpUserID = Convert.ToInt64(dr["OpUserID"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(BizTbl_MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            BizTbl_Message MsgObj = new BizTbl_Message();
            MsgObj.Code = model.Code;
            MsgObj.Description_tr = model.Description_tr;
            MsgObj.Description_en = model.Description_en;
            MsgObj.Description_de = model.Description_de;
            MsgObj.Description_es = model.Description_es;
            MsgObj.Description_fr = model.Description_fr;
            MsgObj.Description_ru = model.Description_ru;
            MsgObj.Description_it = model.Description_it;
            MsgObj.Description_ar = model.Description_ar;
            MsgObj.Description_ja = model.Description_ja;
            MsgObj.Description_pt = model.Description_pt;
            MsgObj.Description_zh = model.Description_zh;
            MsgObj.Description_pt = model.Description_pt;
            MsgObj.IsCommon = true;
            MsgObj.OpDateTime = DateTime.Now;
            MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.BizTbl_Message.Add(MsgObj);
            insertentity.SaveChanges();
            int id = MsgObj.ID;
            return status;
        }

        public bool Delete(BizTbl_MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Message.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_Message.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Message.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.Code;
            obj.Description_en = model.Description_en;
            obj.Description_de = model.Description_de;
            obj.Description_tr = model.Description_tr;
            obj.Description_es = model.Description_es;
            obj.Description_fr = model.Description_fr;
            obj.Description_ru = model.Description_ru;
            obj.Description_it = model.Description_it;
            obj.Description_ar = model.Description_ar;
            obj.Description_ja = model.Description_ja;
            obj.Description_pt = model.Description_pt;
            obj.Description_zh = model.Description_zh;
            obj.IsCommon = model.IsCommon;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = 0;
            db.SaveChanges();

            return status;
        }
    }
    public class BizTbl_MessageExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Code { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_tr { get; set; }
        public string Description_es { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_it { get; set; }
        public string Description_ar { get; set; }
        public string Description_ja { get; set; }
        public string Description_pt { get; set; }
        public string Description_zh { get; set; }
        public bool IsCommon { get; set; }
        //public DateTime OpDateTime { get; set; }
        //public long OpUserID { get; set; }
    }
}