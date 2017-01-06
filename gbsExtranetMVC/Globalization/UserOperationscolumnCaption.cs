using gbsExtranetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace UserOperationscolumnCaption
{
    public class UserOperationscolumnCaption
    {
        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 105);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var ColumnCode = new SqlParameter("@ColumnCode", ColumnName);
                var result = entity.Database.SqlQuery<GetPageCaption_Result>("B_Ex_GetPageCaptions_BizTbl_PageControl_SP @PageID,@Culture,@ColumnCode", PageID, Culture, ColumnCode).ToList();
               // UserOperationscolumn objN = new UserOperationscolumn();
                foreach (GetPageCaption_Result Val in result)
                {
                    Caption = Val.Caption;
                }
            }
            catch
            {

            }


            return Caption;
        }

        public static string dgUserSalutationName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserSalutationName");
            }
        }

        public static string dgUserName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserName");
            }
        }

        public static string dgUserSurname
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserSurname");
            }
        }
        public static string dgUserUserName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserUserName");
            }
        }
        public static string dgUserFirmName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserFirmName");
            }
        }
        public static string dgUserCountryName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserCountryName");
            }
        }

        public static string dgUserCityName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserCityName");
            }
        }
        public static string dgUserAddress
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserAddress");
            }
        }
       
        public static string dgUserEmail
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserEmail");
            }
        }
        public static string dgUserPhone
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserPhone");
            }
        }

        public static string dgUserPostCode
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserPostCode");
            }
        }
        public static string dgUserStatusName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserStatusName");
            }
        }

        public static string dgUserAktif
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserAktif");
            }
        }
        public static string dgUserCreateDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserCreateDateTime");
            }
        }

        public static string dgUserOpDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserOpDateTime");
            }
        }
        public static string dgUserIslem
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgUserIslem");
            }
        }
       
       
    }
}