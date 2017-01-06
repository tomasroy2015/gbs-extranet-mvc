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

namespace PromotionColumnCaption
{
    public class PromotionColumnCaption
    {
      
        public static string GetMEssageTableCaptions(string ColumnName)
        {
             string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 198);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var ColumnCode = new SqlParameter("@ColumnCode", ColumnName);
                var result = entity.Database.SqlQuery<GetPageCaption_Result>("B_Ex_GetPageCaptions_BizTbl_PageControl_SP @PageID,@Culture,@ColumnCode", PageID, Culture, ColumnCode).ToList();
                PromotionColumn objN = new PromotionColumn();
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

        public static string dgHotelPromotionID
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionID");
            }
        }
        public static string dgHotelPromotionPromotionName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionPromotionName");
            }
        }
        public static string dgHotelPromotionAccommodationStartDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionAccommodationStartDate");
            }
        }
        public static string dgHotelPromotionAccommodationEndDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionAccommodationEndDate");
            }
        }
        public static string dgHotelPromotionDayName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionDayName");
            }
        }
        public static string dgHotelPromotionPricePolicyName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionPricePolicyName");
            }
        }
        public static string dgHotelPromotionKampanyaDahilindekiOdaTipi
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionKampanyaDahilindekiOdaTipi");
            }
        }
        public static string dgHotelPromotionAciklama
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionAciklama");
            }
        }
        public static string dgHotelPromotionGizliFirsat
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionGizliFirsat");
            }
        }
        public static string dgHotelPromotionAktif
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionAktif");
            }
        }
        public static string dgHotelPromotionCreateDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelPromotionCreateDateTime");
            }
        }
    }

    public class PromotionColumn
    {
        public string Name { get; set; }
        public string Caption { get; set; }
    }
   
}
    