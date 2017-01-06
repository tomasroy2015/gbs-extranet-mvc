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

namespace UserMessagesColumnCaption
{

    public class UserMessagesCaption
    {
        public string Name { get; set; }

        public string Caption { get; set; }
    }
    public class UserMessagesColumnCaption
    {


        static BaseRepository bR = new BaseRepository();
        public static Hashtable TableColumns = new Hashtable();
        public static string GetMEssageTableCaptions(string value, string TableName1)
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
                UserMessagesCaption objN = new UserMessagesCaption();
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

        public static string ID
        {
            get
            {
                return (string)GetMEssageTableCaptions("ID", "TB_Message");
            }
        }

        public static string Subject
        {
            get
            {
                return (string)GetMEssageTableCaptions("MessageSubjectTypeID", "TB_Message");
            }
        }

        public static string Title
        {
            get
            {
                return (string)GetMEssageTableCaptions("SalutationTypeID", "TB_Message");
            }
        }

        public static string Name
        {
            get
            {
                return (string)GetMEssageTableCaptions("Name", "TB_Message");
            }
        }

        public static string Surname
        {
            get
            {
                return (string)GetMEssageTableCaptions("Surname", "TB_Message");
            }
        }
        public static string EmailID
        {
            get
            {
                return (string)GetMEssageTableCaptions("Email", "TB_Message");
            }
        }
        public static string Phone
        {
            get
            {
                return (string)GetMEssageTableCaptions("Phone", "TB_Message");
            }
        }

        public static string Country
        {
            get
            {
                return (string)GetMEssageTableCaptions("CountryID", "TB_Message");
            }
        }

        public static string Message
        {
            get
            {
                return (string)GetMEssageTableCaptions("Text", "TB_Message");
            }
        }
        public static string Status
        {
            get
            {
                return (string)GetMEssageTableCaptions("MessageStatusID", "TB_Message");
            }
        }

        public static string CreatedDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("CreateDateTime", "TB_Message");
            }
        }
        public static string UpdatedDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("OpDateTime", "TB_Message");
            }
        }
        public static string IPaddress
        {
            get
            {
                return (string)GetMEssageTableCaptions("IPAddress", "TB_Message");
            }
        }     
    }
}