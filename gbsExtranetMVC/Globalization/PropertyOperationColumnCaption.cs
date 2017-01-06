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
using System.Collections;
using gbsExtranetMVC.Models;
using Extension;
using System.Globalization;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;

namespace PropertyOperationColumnCaption
{
    public class PropertyOperationColumnCaption
    {
        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //string cultures = CultureInfo.CurrentUICulture.Name.ToString();
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 110);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var ColumnCode = new SqlParameter("@ColumnCode", ColumnName);
                var result = entity.Database.SqlQuery<GetPageCaption_Result>("B_Ex_GetPageCaptions_BizTbl_PageControl_SP @PageID,@Culture,@ColumnCode", PageID, Culture, ColumnCode).ToList();
                PropertyOperationColumn objN = new PropertyOperationColumn();
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

        public static string dgHotelName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelName");
            }
        }

        public static string dgHotelFirmName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelFirmName");
            }
        }

        public static string dgHotelHotelTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelHotelTypeName");
            }
        }
        public static string dgHotelHotelClassName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelHotelClassName");
            }
        }
        public static string dgHotelHotelAccomodationTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelHotelAccomodationTypeName");
            }
        }
        public static string dgHotelHotelChainName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelHotelChainName");
            }
        }

        public static string dgHotelCountryName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelCountryName");
            }
        }
        public static string dgHotelCityName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelCityName");
            }
        }
        public static string dgHotelAddress
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelAddress");
            }
        }
        public static string dgHotelPostCode
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPostCode");
            }
        }
        public static string dgHotelPhone
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPhone");
            }
        }

        public static string dgHotelFax
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelFax");
            }
        }
        public static string dgHotelEmail
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelEmail");
            }
        }

        public static string dgHotelWebAddress
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelWebAddress");
            }
        }
        public static string dgHotelStatusName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelStatusName");
            }
        }

        public static string dgHotelAktif
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelAktif");
            }
        }
        public static string dgHotelCreateDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelCreateDateTime");
            }
        }
        public static string dgHotelOpDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelOpDateTime");
            }
        }
        public static string dgHotelMainRegionName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelMainRegionName");
            }
        }
        public static string dgHotelHotelAccommodationTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelHotelAccommodationTypeName");
            }
        }
        public static string dgHotelRegionName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRegionName");
            }
        } 
    }
    public class PropertyOperationColumn
    {
        public string Name { get; set; }
        public string Caption { get; set; }
    }
}