using gbsExtranetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ReservationscolumnCaption
{
    public class ReservationscolumnCaption
    {
        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string cultures = CultureInfo.CurrentUICulture.Name.ToString();
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 209);
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

        public static string dgReservationID
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationRezervasyonNo");
            }
        }
        public static string dgHotelReservationPinCode
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationPinCode");
            }
        }

        public static string dgHotelReservationReservationDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationReservationDate");
            }
        }

        public static string dgReservationFullName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationFullName");
            }
        }
        public static string dgReservation
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationRezervasyon");
            }
        }
        public static string dgHotelReservationTutar
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationTutar");
            }
        }
        public static string dgPayableAmount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationOdenecekTutar");
            }
        }

        public static string dgCost
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationMaliyet");
            }
        }
        public static string dgDeposit
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationDeposito");
            }
        }

        public static string dgChargedAmount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationAlinanUcret");
            }
        }
        public static string dgStatusName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationStatusName");
            }
        }

        public static string dgReservationOperation
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgReservationReservationOperationName");
            }
        }
        
       
    }
    }
