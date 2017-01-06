using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class EmailRepository : BaseRepository
    {
        //public DataTable GetParameters(string CultureCode)
        //{
        //    DataTable dt = new DataTable();
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("B_GetEmails_BizTbl_MailQueue_SP", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(dt);
        //    SQLCon.Close();
        //    return dt;
        //}
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<EmailExt> ReadAll(Controller ctrl)
        {
            List<EmailExt> list = new List<EmailExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetEmails_BizTbl_MailQueue_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
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
                    EmailExt EmailObj = new EmailExt();
                    EmailObj.ID = Convert.ToInt32(dr["ID"]);
                    EmailObj.MailTemplateID = Convert.ToInt32(dr["MailTemplateID"]);
                    EmailObj.Template = dr["MailTemplate"].ToString();
                    EmailObj.MailFrom = dr["MailFrom"].ToString();
                    EmailObj.MailTo = dr["MailTo"].ToString();
                    EmailObj.MailCC = dr["MailCC"].ToString();
                    EmailObj.Subject = dr["Subject"].ToString();
                    EmailObj.Content = ctrl.Server.HtmlDecode(dr["Body"].ToString());
                    EmailObj.SentStatus = Convert.ToBoolean(dr["IsSent"]);
                    EmailObj.ResentCount = Convert.ToInt32(dr["ResentCount"]);
                    EmailObj.SendingDate = Convert.ToDateTime(dr["SendingDateTime"]);
                    EmailObj.Record = dr["RecordID"].ToString();
                    //EmailObj.Operation = dr["Operation"].ToString();
                    list.Add(EmailObj);
                }
            }

            return list;
        }

        public EmailExt GetEmailByID(long MailQueueID)
        {
            EmailExt EmailObj = new EmailExt();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetEmailByID_BizTbl_MailQueue_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@MailQueueID", MailQueueID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EmailObj.ID = Convert.ToInt32(dr["ID"]);
                    EmailObj.MailTemplateID = Convert.ToInt32(dr["MailTemplateID"]);
                    EmailObj.Template = dr["MailTemplate"].ToString();
                    EmailObj.MailFrom = dr["MailFrom"].ToString();
                    EmailObj.MailTo = dr["MailTo"].ToString();
                    EmailObj.MailCC = dr["MailCC"].ToString();
                    EmailObj.Subject = dr["Subject"].ToString();
                    EmailObj.Content = dr["Body"].ToString();
                    EmailObj.SentStatus = Convert.ToBoolean(dr["IsSent"]);
                    EmailObj.ResentCount = Convert.ToInt32(dr["ResentCount"]);
                    EmailObj.SendingDate = Convert.ToDateTime(dr["SendingDateTime"]);
                    EmailObj.Record = dr["RecordID"].ToString();
                    //EmailObj.Operation = dr["Operation"].ToString();
                }
            }

            return EmailObj;
        }

        public bool Create(EmailExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            BizTbl_MailQueue MsgObj = new BizTbl_MailQueue();
            MsgObj.ID = model.ID;
            MsgObj.MailTemplateID = model.MailTemplateID;
            MsgObj.MailFrom = model.MailFrom;
            MsgObj.MailTo = model.MailTo;
            MsgObj.MailCC = model.MailCC;
            MsgObj.Subject = model.Subject;
            MsgObj.Body = model.Content;
            MsgObj.IsSent = Convert.ToBoolean(model.SentStatus);
            MsgObj.ResentCount = model.ResentCount;
            MsgObj.SendingDateTime = DateTime.Now;
            MsgObj.RecordID = Convert.ToInt64(model.Record);
            MsgObj.OpDateTime = DateTime.Now;
            MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.BizTbl_MailQueue.Add(MsgObj);
            insertentity.SaveChanges();
            return status;
        }

        public string InsertEmail(string Template, string MailFrom, string MailTo, string ResentCount, string SendingDate, string Subject, string Content, string MailCC, string SentStatus, string Record,Controller Ctrl)
        {
            string status = "Success";
            DBEntities insertentity = new DBEntities();
            BizTbl_MailQueue MsgObj = new BizTbl_MailQueue();
            MsgObj.MailTemplateID = Convert.ToInt32(Template);
            MsgObj.MailFrom = MailFrom;
            MsgObj.MailTo = MailTo;
            MsgObj.MailCC = MailCC;
            MsgObj.Subject = Subject;
            MsgObj.Body = Content;
            MsgObj.IsSent = Convert.ToBoolean(SentStatus);
            if (ResentCount == "")
            {
                MsgObj.ResentCount = 0;
            }
            else
            {
                MsgObj.ResentCount = Convert.ToInt32(ResentCount);
            }
            MsgObj.SendingDateTime = DateTime.Now;
            if (Record == "")
            {
                MsgObj.RecordID = null;
            }
            else
            {
                MsgObj.RecordID = Convert.ToInt64(Record);
            }
            MsgObj.OpDateTime = DateTime.Now;
            MsgObj.OpUserID = Convert.ToInt64(Ctrl.Session["UserID"]);
            insertentity.BizTbl_MailQueue.Add(MsgObj);
            insertentity.SaveChanges();
            return status;
        }

        public bool Update(EmailExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var MailTable = DE.BizTbl_MailQueue.Where(x => x.ID == model.ID).FirstOrDefault();
                MailTable.MailTemplateID = model.MailTemplateID;
                MailTable.MailFrom = model.MailFrom;
                MailTable.MailTo = model.MailTo;
                MailTable.MailCC = model.MailCC;
                MailTable.MailCC = model.MailCC;
                MailTable.Subject = model.Subject;
                MailTable.Body = model.Content;
                MailTable.IsSent = Convert.ToBoolean(model.SentStatus);
                MailTable.ResentCount = model.ResentCount;
                MailTable.SendingDateTime = DateTime.Now;
                MailTable.RecordID = Convert.ToInt64(model.Record);
                DE.SaveChanges();
            }
            return status;
        } 

        public string UpdateEmail(string ID, string Template, string MailFrom, string MailTo, string ResentCount, string SendingDate, string Subject, string Content, string MailCC, string SentStatus, string Record, Controller ctrl)
        {
            string status ="Success";
            using (DBEntities DE = new DBEntities())
            {
                long Id = Convert.ToInt64(ID);
                var MailTable = DE.BizTbl_MailQueue.Where(x => x.ID == Id).FirstOrDefault();
                MailTable.MailTemplateID = Convert.ToInt32(Template);
                MailTable.MailFrom = MailFrom;
                MailTable.MailTo = MailTo;
                MailTable.MailCC = MailCC;             
                MailTable.Subject = Subject;
                MailTable.Body = Content;               
                MailTable.IsSent = Convert.ToBoolean(SentStatus);
                if (ResentCount == "")
                {
                    MailTable.ResentCount = 0;
                }
                else
                {
                    MailTable.ResentCount = Convert.ToInt32(ResentCount);
                }
                MailTable.SendingDateTime = DateTime.Now;
                if (Record == "")
                {
                    MailTable.RecordID = null;
                }
                else
                {
                    MailTable.RecordID = Convert.ToInt64(Record);
                }
                DE.SaveChanges();
            }
            return status;
        }

       

        public bool Delete(EmailExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.BizTbl_MailQueue.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.BizTbl_MailQueue.Remove(MessageTable);
                DE.SaveChanges();
            }
            return status;
        }

        public int DeleteEMail(int ID)
        {
            int status = 1;

            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.BizTbl_MailQueue.Where(x => x.ID == ID).FirstOrDefault();
                DE.BizTbl_MailQueue.Remove(MessageTable);
                DE.SaveChanges();
            }
            return status;

        }




        public List<EmailExt> GetTemplate()
        {
            List<EmailExt> list = new List<EmailExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetTemplate_BizTbl_MailQueue", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EmailExt HitObj = new EmailExt();
                    HitObj.ID = Convert.ToInt32(dr["ID"]);
                    HitObj.Name = dr["MailTemplateID"].ToString();
                    list.Add(HitObj);
                }
            }
            return list;
        }

        //public DataTable GetTemplate(string CultureCode)
        //{

        //    List<EmailExt> list = new List<EmailExt>();
        //    DataTable dt = new DataTable();
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("B_GetTemplate_BizTbl_MailQueue", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in Table.Rows)
        //        {
        //            EmailExt HitObj = new EmailExt();
        //            HitObj.ID = Convert.ToInt32(dr["ID"]);
        //            HitObj.Name = dr["MailTemplateID"].ToString();
        //            ListOfModel.Add(HitObj);
        //        }
        //    }
        //    return list;
           
        //    SQLCon.Close();
        //    return dt;
        //}
    }

    public class EmailExt
    {
        public int ID { get; set; }
        
        public string Template { get; set; }

        public string Name { get; set; }
          [Required(ErrorMessage = "Please Enter Mail From")]
        public string MailFrom { get; set; }

           [Required(ErrorMessage = "Please Enter Mail To")]
        public string MailTo { get; set; }

        public string MailCC { get; set; }

        [Required(ErrorMessage = "Please Enter Subject")]
        public string Subject { get; set; }

         [Required(ErrorMessage = "Please Enter Email Body")]
        public string Content { get; set; }

        public bool SentStatus { get; set; }
         [Required(ErrorMessage = "Please Enter Resent Count")]
        public int ResentCount { get; set; }
         [Required(ErrorMessage = "Please Select Sending Date")]
        public DateTime SendingDate { get; set; }

        public string Record { get; set; }

        public string Operation { get; set; }
        [Required(ErrorMessage="Please Select Email Template")]
        public int MailTemplateID { get; set; }
    }
}