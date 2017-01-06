using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Business;
using System.Collections;
using gbsExtranetMVC.Models;
using Extension;

namespace gbsExtranetMVC.Models.Repositories
{
    //public class MessageRepository
    //{

    //}
    public class MessageExt
    {
        public int ID { get; set; }

         [Required(ErrorMessage = "This field is required")]
        public string Code { get; set; }
       
        public string Description_en { get; set; }

        public string Description_tr { get; set; }

        public string Description_de { get; set; }

        public string Description_es { get; set; }

        public string Description_fr { get; set; }

        public string Description_ru { get; set; }

        public string Description_it { get; set; }

        public string Description_ar { get; set; }

        public string Description_ja { get; set; }

        public string Description_pt { get; set; }

        public string Description_zh { get; set; }
        public bool IsCommon { get; set; } /// used for add & edit checkbox
    }

    public class MessageRepository 
    {

        public List<MessageExt> GetAllMessages()
        {
            DBEntities entity = new DBEntities();
            var result = entity.Database.SqlQuery<BizTbl_Message>("B_EX_GetAllMessage_BizTbl_Message_SP").ToList();
            List<MessageExt> ListOfModel = new List<MessageExt>();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Code");
            dt.Columns.Add("Description_en");
            dt.Columns.Add("Description_tr");
            dt.Columns.Add("Description_de");
            dt.Columns.Add("Description_es");
            dt.Columns.Add("Description_fr");
            dt.Columns.Add("Description_ru");
            dt.Columns.Add("Description_it");
            dt.Columns.Add("Description_ar");
            dt.Columns.Add("Description_ja");
            dt.Columns.Add("Description_pt");
            dt.Columns.Add("Description_zh");
            dt.Columns.Add("IsCommon");
            foreach (BizTbl_Message dr in result)
            {
                dt.Rows.Add(dr.ID, dr.Code, dr.Description_en, dr.Description_tr, dr.Description_de, dr.Description_es, dr.Description_fr,
                    dr.Description_ru, dr.Description_it, dr.Description_ar, dr.Description_ja, dr.Description_pt, dr.Description_zh, dr.IsCommon);
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MessageExt MsgObj = new MessageExt();
                    MsgObj.ID = Convert.ToInt32(dr["ID"]);
                    MsgObj.Code = dr["Code"].ToString();
                    MsgObj.Description_en = dr["Description_en"].ToString();
                    MsgObj.Description_tr = dr["Description_tr"].ToString();
                    MsgObj.Description_de = dr["Description_de"].ToString();
                    MsgObj.Description_es = dr["Description_es"].ToString();
                    MsgObj.Description_fr = dr["Description_fr"].ToString();
                    MsgObj.Description_ru = dr["Description_ru"].ToString();
                    MsgObj.Description_it = dr["Description_it"].ToString();
                    MsgObj.Description_ar = dr["Description_ar"].ToString();
                    MsgObj.Description_ja = dr["Description_ja"].ToString();
                    MsgObj.Description_pt = dr["Description_pt"].ToString();
                    MsgObj.Description_zh = dr["Description_zh"].ToString();
                    MsgObj.IsCommon = Convert.ToBoolean(dr["IsCommon"].ToString());
                    ListOfModel.Add(MsgObj);
                }
            }

            return ListOfModel;
        }


        public bool Create(MessageExt model, ref string Msg, Controller ctrl)
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

        public bool Update(MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.BizTbl_Message.Where(x => x.ID == model.ID).FirstOrDefault();
                MessageTable.Code = model.Code;
                MessageTable.Description_en = model.Description_en;
                MessageTable.Description_tr = model.Description_tr;
                MessageTable.Description_de = model.Description_de;
                MessageTable.Description_es = model.Description_es;
                MessageTable.Description_fr = model.Description_fr;
                MessageTable.Description_ru = model.Description_ru;
                MessageTable.Description_it = model.Description_it;
                MessageTable.Description_ar = model.Description_ar;
                MessageTable.Description_ja = model.Description_ja;
                MessageTable.Description_pt = model.Description_pt;
                MessageTable.Description_zh = model.Description_zh;
                MessageTable.IsCommon = model.IsCommon;
                MessageTable.OpDateTime = DateTime.Now;
                MessageTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                DE.SaveChanges();
            }

            //var IDParam = new SqlParameter("@ID", model.ID);
            //var CodeParam = new SqlParameter("@Code", model.Code);
            //var Description_trparam = new SqlParameter("@Description_tr", model.Description_tr);
            //var Description_enparam = new SqlParameter("@Description_en", model.Description_en);
            //var Description_deparam = new SqlParameter("@Description_de", model.Description_de);
            //var Description_esparam = new SqlParameter("@Description_es", model.Description_es);
            //var Description_frParam = new SqlParameter("@Description_fr", model.Description_fr);
            //var Description_ruparam = new SqlParameter("@Description_ru", model.Description_ru);
            //var Description_itparam = new SqlParameter("@Description_it", model.Description_it);
            //var Description_arparam = new SqlParameter("@Description_ar", model.Description_ar);
            //var Description_jaParam = new SqlParameter("@Description_ja", model.Description_ja);
            //var Description_ptParam = new SqlParameter("@Description_pt", model.Description_pt);
            //var Description_zhparam = new SqlParameter("@Description_zh", model.Description_zh);
            //var IsCommonparam = new SqlParameter("@IsCommon", model.IsCommon);
            //var i = updateentity.Database.ExecuteSqlCommand("B_Ex_Update_BizTbl_Message_SP @ID,@Code,@Description_tr,@Description_en,@Description_de,@Description_es,@Description_fr,@Description_ru,@Description_it,@Description_ar,@Description_ja,@Description_pt,@Description_zh,@IsCommon", IDParam, CodeParam, Description_trparam, Description_enparam, Description_deparam, Description_esparam, Description_frParam, Description_ruparam, Description_itparam, Description_itparam, Description_arparam, Description_jaParam, Description_ptParam, Description_zhparam, IsCommonparam);
            return status;
        }

        public bool Delete(MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            //DBEntities insertentity = new DBEntities();
            //var IDParam = new SqlParameter("@ID", model.ID);
            //int i = insertentity.Database.ExecuteSqlCommand("B_Ex_Delete_BizTbl_Message_SP @ID", IDParam);

            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.BizTbl_Message.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.BizTbl_Message.Remove(MessageTable);
                DE.SaveChanges();
            }
            return status;

        }

    }
}