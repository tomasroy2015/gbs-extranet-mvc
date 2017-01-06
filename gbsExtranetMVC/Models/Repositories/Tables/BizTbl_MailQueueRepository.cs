using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_MailQueueRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_MailQueueExt> ReadAll(int TableID)
        {
            List<BizTbl_MailQueueExt> list = new List<BizTbl_MailQueueExt>();

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
                    BizTbl_MailQueueExt EmailObj = new BizTbl_MailQueueExt();
                    EmailObj.ID = Convert.ToInt32(dr["ID"]);
                    EmailObj.Template = dr["MailTemplateID"].ToString();
                    EmailObj.MailFrom = dr["MailFrom"].ToString();
                    EmailObj.MailTo = dr["MailTo"].ToString();
                    EmailObj.MailCC = dr["MailCC"].ToString();
                    EmailObj.Subject = dr["Subject"].ToString();
                    EmailObj.Content = dr["Body"].ToString();
                    EmailObj.SentStatus = dr["IsSent"].ToString();
                    EmailObj.ResentCount = dr["ResentCount"].ToString();
                    EmailObj.SendingDate = Convert.ToDateTime(dr["SendingDateTime"]);
                    EmailObj.Record = dr["RecordID"].ToString();
                    //EmailObj.Operation = dr["Operation"].ToString();
                    list.Add(EmailObj);
                }
            }
           

            return list;
        }

        public bool Create(BizTbl_MailQueueExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            BizTbl_MailQueue MsgObj = new BizTbl_MailQueue();
            MsgObj.ID = model.ID;
            //MsgObj.MailTemplateID = model.MailTemplateID;
            MsgObj.MailFrom = model.MailFrom;
            MsgObj.MailTo = model.MailTo;
            MsgObj.MailCC = model.MailCC;
            MsgObj.Subject = model.Subject;
            MsgObj.Body = model.Content;
            MsgObj.IsSent = Convert.ToBoolean(model.SentStatus);
            MsgObj.ResentCount = Convert.ToInt32(model.ResentCount);
            MsgObj.SendingDateTime = model.SendingDate;
            MsgObj.RecordID = Convert.ToInt64(model.Record);
            MsgObj.OpDateTime = DateTime.Now;
            MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.BizTbl_MailQueue.Add(MsgObj);
            insertentity.SaveChanges();
            return status;
        }

        //public bool Delete(BizTbl_MailQueueExt model, ref string Msg, Controller ctrl)
        //{
        //    bool status = true;

        //    var obj = db.BizTbl_Culture.Where(x => x.ID == model.ID).FirstOrDefault();
        //    db.BizTbl_Culture.Remove(obj);
        //    db.SaveChanges();

        //    return status;
        //}

        public bool Update(BizTbl_MailQueueExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var MailTable = db.BizTbl_MailQueue.Where(x => x.ID == model.ID).FirstOrDefault();
           // MailTable.MailTemplateID =model.MailTemplateID;
            MailTable.MailFrom = model.MailFrom;
            MailTable.MailTo = model.MailTo;
            MailTable.MailCC = model.MailCC;
            MailTable.MailCC = model.MailCC;
            MailTable.Subject = model.Subject;
            MailTable.Body = model.Content;
            MailTable.IsSent = Convert.ToBoolean(model.SentStatus);
            MailTable.ResentCount = Convert.ToInt32(model.ResentCount);
            MailTable.SendingDateTime = model.SendingDate;
            MailTable.RecordID = Convert.ToInt64(model.Record);
            MailTable.OpDateTime = DateTime.Now;
            MailTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
    }

    public class BizTbl_MailQueueExt
    {
        public int ID { get; set; }

        public string Template { get; set; }

        public string Name { get; set; }
        public string MailFrom { get; set; }

        public string MailTo { get; set; }

        public string MailCC { get; set; }

        public string Subject { get; set; }



        public string Content { get; set; }

        public string SentStatus { get; set; }

        public string ResentCount { get; set; }

        public DateTime SendingDate { get; set; }

        public string Record { get; set; }

        public string Operation { get; set; }

        public int MailTemplateID { get; set; }
    }
}