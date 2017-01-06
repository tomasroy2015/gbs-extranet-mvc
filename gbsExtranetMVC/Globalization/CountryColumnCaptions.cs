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

namespace CountryColumnCaptions
{
    public class CountryCaption
    {
        public string Name { get; set; }
        public string Caption { get; set; }
    }
    public class CountryColumnCaptions
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
                CountryCaption objN = new CountryCaption();
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

        public static string Name
        {
            get
            {
                return (string)GetMEssageTableCaptions("Name_en", "TB_Country");
            }
        }
        public static string Code
        {
            get
            {
                return (string)GetMEssageTableCaptions("Code", "TB_Country");
            }
        }
        public static string CultureCode
        {
            get
            {
                return (string)GetMEssageTableCaptions("CultureCode", "TB_Country");
            }
        }
        public static string Currency
        {
            get
            {
                return (string)GetMEssageTableCaptions("Symbol", "TB_Currency");
            }
        }
        public static string VAT
        {
            get
            {
                return (string)GetMEssageTableCaptions("VAT", "TB_Country");
            }
        }
        public static string CityTax
        {
            get
            {
                return (string)GetMEssageTableCaptions("HasCityTax", "TB_Country");
            }
        }
        public static string HitCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("HitCount", "TB_Country");
            }
        }
        public static string Sort
        {
            get
            {
                return (string)GetMEssageTableCaptions("Sort", "TB_Country");
            }
        }
        public static string Active
        {
            get
            {
                return (string)GetMEssageTableCaptions("Active", "TB_Country");
            }
        }
        public static string TemparoryCode
        {
            get
            {
                return (string)GetMEssageTableCaptions("TempCode", "TB_Country");
            }
        }
        public static string Operation
        {
            get
            {
                return (string)GetMEssageTableCaptions("Operation", "");
            }
        }
    }
}