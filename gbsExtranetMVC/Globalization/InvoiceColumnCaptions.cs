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

namespace InvoiceColumnCaptions
{
    public class InvoiceCaption
    {
        public string Name { get; set; }

        public string Caption { get; set; }
    }

    public class InvoiceColumnCaptions
    {  
       
        public static string GetMEssageTableCaptions(string ColumnName)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //string cultures = CultureInfo.CurrentUICulture.Name.ToString();
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 177);
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

        public static string InvoiceID
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceID");
            }
        }

        public static string FirmName
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceFirmName");
            }
        }

        public static string InvoiceDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceInvoiceDate");
            }
        }

        public static string Period
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoicePeriod");
            }
        }

        public static string DueDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceDueDate");
            }
        }
        public static string Invoice_Amount
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceFaturaTutari");
            }
        }
        public static string status
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceInvoiceStatusName"); 
            }
        }
        public static string InvoiceDonem
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceDonem"); 
            }
        }
        public static string Operation
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgInvoiceIslem");
            }
        }

       
    }
}