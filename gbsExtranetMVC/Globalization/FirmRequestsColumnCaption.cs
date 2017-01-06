using gbsExtranetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FirmRequestsColumnCaption

{
    public class FirmRequestsColumnCaption
    {
       // public static string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 174);
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

        public static string dgFirmRequestFirmName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestFirmName");
            }
        }

        public static string dgFirmRequestRequestTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestRequestTypeName");
            }
        }

        public static string dgFirmRequestRezervasyonKodu
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestRezervasyonKodu");
            }
        }
        public static string dgFirmRequestCheckInDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestCheckInDate");
            }
        }
        public static string dgFirmRequestCheckOutDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestCheckOutDate");
            }
        }
        public static string dgFirmRequestFirmRequestStatusName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestFirmRequestStatusName");
            }
        }

        public static string dgFirmRequestCreateDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestCreateDateTime");
            }
        }
        public static string dgFirmRequestOpDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRequestOpDateTime");
            }
        }

      

    }
}