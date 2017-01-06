using gbsExtranetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PropertyRoomsColumnCaption
{
    public class PropertyRoomsColumnCaption
    {
        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 121);
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

        public static string btnNewRecord
        {
            get
            {
                return (string)GetMEssageTableCaptions("btnNewRecord");
            }
        }
        public static string dgHotelRoomID
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomID");
            }
        }

        public static string dgHotelRoomHotelRoomTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomHotelRoomTypeName");
            }
        }
        public static string dgHotelRoomRoomSize
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomRoomSize");
            }
        }


        public static string dgHotelRoomMaxPeopleCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomMaxPeopleCount");
            }
        }
        public static string dgHotelRoomMaxChildrenCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomMaxChildrenCount");
            }
        }

        public static string dgHotelRoomBedCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomBedCount");
            }
        }
        public static string dgHotelRoomBabyCotCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomBabyCotCount");
            }
        }

        public static string dgHotelRoomExtraBedCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomExtraBedCount");
            }
        }
        public static string dgHotelRoomYatakOzellikleri
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomYatakOzellikleri");
            }
        }

        public static string dgHotelRoomViewTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomViewTypeName");
            }
        }
        public static string dgHotelRoomSigaraIcilebilir
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomSigaraIcilebilir");
            }
        }


        public static string dgHotelRoomCreateDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomCreateDateTime");
            }
        }
        public static string dgHotelRoomOpDateTime
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomOpDateTime");
            }
        }

        public static string dgHotelRoomIslem
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomIslem");
            }
        }
        public static string dgHotelRoomYatakBilgisi
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomYatakBilgisi");
            }
        }


        public static string dgHotelRoomSigaraIcilmezOda
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomSigaraIcilmezOda");
            }
        }
        public static string dgHotelRoomRoomTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomRoomTypeName");
            }
        }


        public static string dgHotelRoomSmokingTypeName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomSmokingTypeName");
            }
        }
        public static string lbtnEdit
        {
            get
            {
                return (string)GetMEssageTableCaptions("lbtnEdit");
            }
        }

        public static string lbtnDelete
        {
            get
            {
                return (string)GetMEssageTableCaptions("lbtnDelete");
            }
        }
        public static string dgHotelRoomRoomCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgHotelRoomRoomCount");
            }
        }

    }
}