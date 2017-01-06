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

namespace BankColumnCaption
{
    public class BankCaption
    {
        public string Name { get; set; }
        public string Caption { get; set; }
    }
    public class BankColumnCaption
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
                BankCaption objN = new BankCaption();
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

        public static string Country
        {
            get
            {
                return (string)GetMEssageTableCaptions("CountryID", "TB_Bank");
            }
        }
        public static string Currency
        {
            get
            {
                return (string)GetMEssageTableCaptions("CurrencyID", "TB_Bank");
            }
        }
        public static string BankName
        {
            get
            {
                return (string)GetMEssageTableCaptions("BankName", "TB_Bank");
            }
        }
        public static string BankBranchName
        {
            get
            {
                return (string)GetMEssageTableCaptions("BankBranchName", "TB_Bank");
            }
        }
        public static string BankAccountNumber
        {
            get
            {
                return (string)GetMEssageTableCaptions("BankAccountNumber", "TB_Bank");
            }
        }
        public static string IBAN
        {
            get
            {
                return (string)GetMEssageTableCaptions("IBAN", "TB_Bank");
            }
        }
        public static string SWIFT
        {
            get
            {
                return (string)GetMEssageTableCaptions("SWIFT", "TB_Bank");
            }
        }
        public static string OtherInfo
        {
            get
            {
                return (string)GetMEssageTableCaptions("OtherInfo", "TB_Bank");
            }
        }      
    }
}