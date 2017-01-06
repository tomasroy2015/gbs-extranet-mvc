using gbsExtranetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ReviewsColumnCaptions
{
    public class ReviewsColumnCaptions
    {

        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 180);
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


        public static string lblReservationIDFilter
        {
            get
            {
                return (string)GetMEssageTableCaptions("lblReservationIDFilter");
            }
        }
        public static string lblFirmFilter
        {
            get
            {
                return (string)GetMEssageTableCaptions("lblFirmFilter");
            }
        }
        public static string btnFilter
        {
            get
            {
                return (string)GetMEssageTableCaptions("btnFilter");
            }
        }
        public static string btnFilterRemove
        {
            get
            {
                return (string)GetMEssageTableCaptions("btnFilterRemove");
            }
        }
        public static string lblPart
        {
            get
            {
                return (string)GetMEssageTableCaptions("lblPart");
            }
        }
        public static string lblStatusFilter
        {
            get
            {
                return (string)GetMEssageTableCaptions("lblStatusFilter");
            }
        }
        public static string dgReviewID
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewID");
            }
        }
        public static string dgReviewFirmName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewFirmName");
            }
        }
        public static string dgReviewPart
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewPart");
            }
        }
        public static string dgReviewRezervasyonKodu
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewRezervasyonKodu");
            }
        }
        public static string dgReviewYorum
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewYorum");
            }
        }
        public static string dgReviewReviewStatusName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewReviewStatusName");
            }
        }
        public static string dgReviewAktif
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewAktif");
            }
        }
        public static string dgReviewCreateDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewCreateDateTime");
            }
        }
        public static string dgReviewOpDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewOpDateTime");
            }
        }
        public static string dgReviewIPAddress
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewIPAddress");
            }
        }
        public static string dgReviewIslem
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewIslem");
            }
        }
        public static string lblAverage
        {
            get
            {
                return (string)GetMEssageTableCaptions("lblAverage");
            }
        }
        public static string lbtnApprove
        {
            get
            {
                return (string)GetMEssageTableCaptions("lbtnApprove");
            }
        }
        public static string lbtnReject
        {
            get
            {
                return (string)GetMEssageTableCaptions("lbtnReject");
            }
        }
        public static string lbtnDelete
        {
            get
            {
                return (string)GetMEssageTableCaptions("lbtnDelete");
            }
        }
        public static string dgReviewYorumSahibi
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewYorumSahibi");
            }
        }
        public static string dgReviewIPAdresi
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewIPAdresi");
            }
        }
        public static string dgReviewAnonim
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReviewAnonim");
            }
        }


    }
}