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

namespace FirmOperationsColumnCaption
{

    public class FirmOperationsCaption
    {
        public string Name { get; set; }
        public string Caption { get; set; }
    }
    public class FirmOperationsColumnCaption
    {
       

        static BaseRepository bR = new BaseRepository();
        public static Hashtable TableColumns = new Hashtable();

        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //string cultures = CultureInfo.CurrentUICulture.Name.ToString();
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 108);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var ColumnCode = new SqlParameter("@ColumnCode", ColumnName);
                var result = entity.Database.SqlQuery<GetPageCaption_Result>("B_Ex_GetPageCaptions_BizTbl_PageControl_SP @PageID,@Culture,@ColumnCode", PageID, Culture, ColumnCode).ToList();
                FirmOperationsCaption objN = new FirmOperationsCaption();
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

        public static string ID
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmID");
            }
        }
        public static string Firm
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmName");
            }
        }
        public static string Country
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmCountryName");
            }
        }
        public static string Region
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmRegionName");
            }
        }
        public static string City
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmCityName");
            }
        }
        public static string Address
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmAddress");
            }
        }
        public static string PostalCode
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmPostCode");
            }
        }
        public static string Phone
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmPhone");
            }
        }
        public static string Fax
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmFax");
            }
        }
        public static string TaxOffice
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmTaxDepartment");
            }
        }
        public static string TaxID
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmTaxNo");
            }
        }
        public static string ExacutiveTitle
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmContactPersonSalutationName");
            }
        }
        public static string ExecutiveName 
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmContactPersonName");
            }
        }
        public static string ExecutiveSurname
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmContactPersonSurname");
            }
        }
        public static string ExecutivePosition
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmContactPersonPosition");
            }
        }
        public static string ExecutivePhone
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmContactPersonPhone");
            }
        }
        public static string ExecutiveEmail
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmContactPersonEmail");
            }
        }
        public static string Status
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmStatusName");
            }

        }
        public static string Active
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmAktif");
            }
        }
        public static string Createddate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmCreateDateTime");
            }
        }
        public static string Updateddate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgFirmOpDateTime");
            }
        }

    }
}