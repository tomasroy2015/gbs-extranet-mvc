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

namespace PropertyComissionColumnCaption
{

    public class InvoiceCaption
    {
        public string Name { get; set; }

        public string Caption { get; set; }
    }
    public class PropertyComissionColumnCaption
    {
      
        public static string GetMEssageTableCaptions(string ColumnName)
        {
           string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var PageID = new SqlParameter("@PageID", 126);
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
        public static string StartDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgComissionStartDate");
            }
        }

        public static string EndDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgComissionEndDate");
            }
        }
        public static string Comission
        {
            get
            {
                return (string)GetMEssageTableCaptions("dgComissionComission");
            }
        }
    }
        
}