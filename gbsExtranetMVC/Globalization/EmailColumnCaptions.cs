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
using System.Globalization;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
namespace mailColumnCaptions
{
    public class EmailCaption
    {
        public string Name { get; set; }
        public string Caption { get; set; }
    }
    public class mailColumnCaptions
    {

        static BaseRepository bR = new BaseRepository();
        public static Hashtable TableColumns = new Hashtable();
        public static string GetMEssageTableCaptions(string value,string TableName1)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {
                DBEntities entity = new DBEntities();
                var TableName = new SqlParameter("@TableName", TableName1);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var MessageCode = new SqlParameter("@MessageCode", value);
                var result = entity.Database.SqlQuery<GetTableCaptionValue_Result>("B_Ex_GetCaptionValues_BizTbl_TableColumn_SP @TableName,@Culture,@MessageCode", TableName, Culture, MessageCode).ToList();
                DataTable dt = new DataTable();
                EmailCaption objN = new EmailCaption();
                foreach (GetTableCaptionValue_Result Val in result)
                {
                    string name = Val.Name;
                    Caption = Val.Caption;
                    if (Caption == "")
                    {
                        Caption = Val.Name;
                    }
                }
            }
            catch
            {

            }


            return Caption;
        }

        public static string Template
        {
            get
            {
                return (string)GetMEssageTableCaptions("MailTemplateID", "BizTbl_MailQueue");
            }
        }
        public static string MailFrom
        {
            get
            {
                return (string)GetMEssageTableCaptions("MailFrom", "BizTbl_MailQueue");
            }
        }
        public static string MailTo
        {
            get
            {
                return (string)GetMEssageTableCaptions("MailTo", "BizTbl_MailQueue");
            }
        }
        public static string MailCC
        {
            get
            {
                return (string)GetMEssageTableCaptions("MailCC", "BizTbl_MailQueue");
            }
        }
        public static string Subject
        {
            get
            {
                return (string)GetMEssageTableCaptions("Subject", "BizTbl_MailQueue");
            }
        }
        public static string Content
        {
            get
            {
                return (string)GetMEssageTableCaptions("Body", "BizTbl_MailQueue");
            }
        }
        public static string SentStatus
        {
            get
            {
                return (string)GetMEssageTableCaptions("IsSent", "BizTbl_MailQueue");
            }
        }
        public static string ResentCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("ResentCount", "BizTbl_MailQueue");
            }
        }
        public static string SendingDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("SendingDateTime", "BizTbl_MailQueue");
            }
        }
        public static string Record
        {
            get
            {
                return (string)GetMEssageTableCaptions("RecordID", "BizTbl_MailQueue");
            }
        }
        public static string Operation
        {
            get
            {
                return (string)GetMEssageTableCaptions("Operation","");
            }
        }


       

    }
}