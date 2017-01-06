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

namespace RegionColumnCaptions
{

        public class RegionCaption
        {
            public string Name { get; set; }
            public string Caption { get; set; }
        }

        public class RegionColumnCaptions
        {
            public static string GetRegionTableCaptions(string ColumnName, string TableNameParam)
            {
                string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                string Caption = "";
                try
                {

                    DBEntities entity = new DBEntities();
                    var TableName = new SqlParameter("@TableName", TableNameParam);
                    var Culture = new SqlParameter("@Culture", CultureValue);
                    var MessageCode = new SqlParameter("@MessageCode", ColumnName);
                    var result = entity.Database.SqlQuery<GetTableCaptionValue_Result>("B_Ex_GetCaptionValues_BizTbl_TableColumn_SP @TableName,@Culture,@MessageCode", TableName, Culture, MessageCode).ToList();
                    RegionCaption objN = new RegionCaption();
                    foreach (GetTableCaptionValue_Result Val in result)
                    {
                        string name = Val.Name;
                        Caption = Val.Caption;
                        if (Caption == "")
                        {
                            Caption = Val.Name;
                        }
                    }
                }
                catch
                {

                }


                return Caption;
            }

            public static string Country
            {
                get
                {
                    return (string)GetRegionTableCaptions("CountryID", "TB_Region");
                }
            }

            public static string ParentID
            {
                get
                {
                    return (string)GetRegionTableCaptions("ParentID", "TB_Region");
                }
            }
            public static string secondParentID
            {
                get
                {
                    return (string)GetRegionTableCaptions("SecondParentID", "TB_Region");
                }
            }
            public static string RegionType
            {
                get
                {
                    return (string)GetRegionTableCaptions("RegionType", "TB_Region");
                }
            }
            public static string SubRegionType
            {
                get
                {
                    return (string)GetRegionTableCaptions("SubRegionType", "TB_Region");
                }
            }
            public static string Name
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name", "TB_Region");
                }
            }
            public static string NameASCII
            {
                get
                {
                    return (string)GetRegionTableCaptions("NameASCII", "TB_Region");
                }
            }
            public static string Name_tr
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_tr", "TB_Region");
                }
            }
            public static string Name_en
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_en", "TB_Region");
                }
            }
            public static string Name_de
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_de", "TB_Region");
                }
            }
            public static string Name_es
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_es", "TB_Region");
                }
            }
            public static string Name_fr
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_fr", "TB_Region");
                }
            }
            public static string Name_ru
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_ru", "TB_Region");
                }
            }
            public static string Name_it
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_it", "TB_Region");
                }
            }
            public static string Name_ar
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_ar", "TB_Region");
                }
            }
            public static string Name_ja
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_ja", "TB_Region");
                }
            }
            public static string Name_pt
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_pt", "TB_Region");
                }
            }
            public static string Name_zh
            {
                get
                {
                    return (string)GetRegionTableCaptions("Name_zh", "TB_Region");
                }
            }
            public static string Code
            {
                get
                {
                    return (string)GetRegionTableCaptions("Code", "TB_Region");
                }
            }
            public static string Population
            {
                get
                {
                    return (string)GetRegionTableCaptions("Population", "TB_Region");
                }
            }
            public static string IsIncludedInSearch
            {
                get
                {
                    return (string)GetRegionTableCaptions("IsIncludedInDestinationSearch", "TB_Region");
                }
            }
            public static string IsCity
            {
                get
                {
                    return (string)GetRegionTableCaptions("IsCity", "TB_Region");
                }
            }
            public static string IsPopular
            {
                get
                {
                    return (string)GetRegionTableCaptions("IsPopular", "TB_Region");
                }
            }
            public static string IsFilter
            {
                get
                {
                    return (string)GetRegionTableCaptions("IsFilter", "TB_Region");
                }
            }
            public static string IsMainPageDisplay
            {
                get
                {
                    return (string)GetRegionTableCaptions("IsMainPageDisplay", "TB_Region");
                }
            }
            public static string MainPageDisplaySort
            {
                get
                {
                    return (string)GetRegionTableCaptions("MainPageDisplaySort", "TB_Region");
                }
            }
            public static string HitCount
            {
                get
                {
                    return (string)GetRegionTableCaptions("HitCount", "TB_Region");
                }
            }
            public static string Sort
            {
                get
                {
                    return (string)GetRegionTableCaptions("Sort", "TB_Region");
                }
            }
            public static string Latitude
            {
                get
                {
                    return (string)GetRegionTableCaptions("Latitude", "TB_Region");
                }
            }
            public static string Longitude
            {
                get
                {
                    return (string)GetRegionTableCaptions("Longitude", "TB_Region");
                }
            }
            public static string MapZoomIndex
            {
                get
                {
                    return (string)GetRegionTableCaptions("MapZoomIndex", "TB_Region");
                }
            }
            public static string CityTax
            {
                get
                {
                    return (string)GetRegionTableCaptions("HasCityTax", "TB_Region");
                }
            }
            public static string Active
            {
                get
                {
                    return (string)GetRegionTableCaptions("Active", "TB_Region");
                }
            }


    }
    
}