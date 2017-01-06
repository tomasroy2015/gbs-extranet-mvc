using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Web.Mvc;
using gbsExtranetMVC.Models.Enumerations;
using MessageColumnCaptions;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Globalization;
using Business;
using System.Net;

namespace gbsExtranetMVC.Controllers.Property
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class PropertyStatisticsController : Controller
    {
        public const string ActiveMenu = "Property";
        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PropertyStatistics()
        {
            Session["PageName"] = "PropertyStatistics";
            //BizContext = (BizContext)Session["GBAdminBizContext"];
            //int id = BizContext.HotelID;
            //Session["GBAdminBizContext"] = BizContext;
            DisplaypageloadPropertyStatistics();
            YearlyPropertyStatistics();
            AssignBizContext();
            ViewBag.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.EndDate = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy");
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View();
        }

        public string dateconvert(string date)
        {
            string covertdate="";
            try
            {
                DateTime dt23 = Convert.ToDateTime(date);
                CultureInfo arCI = new CultureInfo("ar-SA");
                string ar = dt23.ToString(new CultureInfo("ar-SA"));
                GregorianCalendar enCalendar = new GregorianCalendar();
                int year = enCalendar.GetYear(dt23);
                int month = enCalendar.GetMonth(dt23);
                int day = enCalendar.GetDayOfMonth(dt23);
                covertdate = (string.Format("{0}-{1}-{2}", year, month, day));
            }
            catch
            {

                IFormatProvider culture = new CultureInfo("en-US", true);
                //DateTime dateVal = DateTime.ParseExact(date, "yyyy-MM-dd", culture);
                DateTime dt1 = DateTime.ParseExact(date, "dd/MM/yyyy", culture);
                CultureInfo arCI = new CultureInfo("en-US");
                string ar = date.ToString(new CultureInfo("en-US"));
                GregorianCalendar enCalendar = new GregorianCalendar();
                int year = enCalendar.GetYear(dt1);
                int month = enCalendar.GetMonth(dt1);
                int day = enCalendar.GetDayOfMonth(dt1);
               
                covertdate = (string.Format("{0}-{1}-{2}", year, month, day));

               // covertdate = dateVal.ToString("yyyy-MM-dd");
               
            }

            return (covertdate);

        }
       
        public JsonResult DisplaypageloadPropertyStatistics()
        {
            string HitCountPeriodID ="3";
            //DateTime startdate = DateTime.Now;
            ////DateTime dt1 = DateTime.ParseExact(startdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        
            //string Datefromm = "2015-11-16";
            //string Datetoo = startdate.ToString("yyyy-MM-dd");
            string Datefromm = DateTime.Now.ToString("yyyy-MM-dd");
            string Datetoo = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
            string startdate = dateconvert(Datefromm);
            string Enddate = dateconvert(Datetoo);
            string PartID = "1";
            //string HotelID =Convert.ToString( Session["GBAdminBizContext"]);
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID;
            Session["GBAdminBizContext"] = BizContext;

            PropertyStatisticsRepository objupdate = new PropertyStatisticsRepository();
            var DailyStatistics = objupdate.DisplayPropertyStatistics(PartID, HitCountPeriodID, startdate, Enddate, HotelID);
            ViewBag.DailyStatistics = DailyStatistics;
            return Json(DailyStatistics, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DailyPropertyStatistics(string HitCountPeriodID, string StartDate, string EndDate)
        {
            //DateTime dt = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dt1 = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string Datefromm = Convert.ToString(dt);
            //string Datetoo = Convert.ToString(dt1);
            //string Datefromm = dt.ToString("yyyy-MM-dd");
            //string Datetoo = dt1.ToString("yyyy-MM-dd");
            string startdate = dateconvert(StartDate);
            string Enddate = dateconvert(EndDate);
            var PartID = "1";
            //string HotelID = Convert.ToString(Session["GBAdminBizContext"]);
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID;
            Session["GBAdminBizContext"] = BizContext;

            DataTable dts = new DataTable();
            PropertyStatisticsRepository objupdate = new PropertyStatisticsRepository();
            List<PropertyStatisticsExt> list = new List<PropertyStatisticsExt>();
            try{
            dts = objupdate.DisplaydatewisePropertyStatistics(PartID, HitCountPeriodID, startdate, Enddate, HotelID);
            if (dts != null)
            {
                if (dts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dts.Rows)
                    {
                        PropertyStatisticsExt FirmObj = new PropertyStatisticsExt();
                        FirmObj.PartID = dr["PartID"].ToString();
                        FirmObj.RecordID = dr["RecordID"].ToString();
                        FirmObj.ReservationCount = dr["ReservationCount"].ToString();
                        FirmObj.Date = dr["Date"].ToString();
                        FirmObj.HitCount = dr["HitCount"].ToString();
                        FirmObj.Month = dr["Month"].ToString();
                        FirmObj.MonthName = dr["MonthName"].ToString();
                        FirmObj.Day = dr["Day"].ToString();
                        FirmObj.DayName = dr["DayName"].ToString();
                        list.Add(FirmObj);
                    }
                }
            }
        }
            catch (Exception ex)
           {
               string hostName1 = Dns.GetHostName();
               string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
               string PageName = Convert.ToString(Session["PageName"]);
               //string GetUserIPAddress = GetUserIPAddress1();
               using (BaseRepository baseRepo = new BaseRepository())
               {
                   //BizContext BizContext1 = new BizContext();
                   BizApplication.AddError(baseRepo.BizDB, PageName, ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
               }
               Session["PageName"] = "";
               string error = ErrorHandling.HandleException(ex);
               return this.Json(new DataSourceResult { Errors = error });
           }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public JsonResult MonthlyPropertyStatistics(string Year)
        {
          //  string HotelID = Convert.ToString(Session["GBAdminBizContext"]);
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID;
            Session["GBAdminBizContext"] = BizContext;

            var PartID = "1";
            if (Year == "")
            {
                Year = "2015";
            }
            DataTable dt = new DataTable();
            PropertyStatisticsRepository objupdate = new PropertyStatisticsRepository();
            List<PropertyStatisticsExt> list = new List<PropertyStatisticsExt>();
            try
            {
                dt = objupdate.MonthlyPropertyStatistics(PartID, HotelID, Year);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PropertyStatisticsExt FirmObj = new PropertyStatisticsExt();
                        FirmObj.PartID = dr["PartID"].ToString();
                        FirmObj.RecordID = dr["RecordID"].ToString();
                        FirmObj.ReservationCount = dr["ReservationCount"].ToString();
                        FirmObj.HitCount = dr["HitCount"].ToString();
                        FirmObj.MonthName = dr["MonthName"].ToString();
                        list.Add(FirmObj);
                    }
                }
            }
            catch (Exception ex)
            {
                string hostName1 = Dns.GetHostName();
                string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
                string PageName = Convert.ToString(Session["PageName"]);
                //string GetUserIPAddress = GetUserIPAddress1();
                using (BaseRepository baseRepo = new BaseRepository())
                {
                    //BizContext BizContext1 = new BizContext();
                    BizApplication.AddError(baseRepo.BizDB, PageName, ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
                }
                Session["PageName"] = "";
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public JsonResult YearlyPropertyStatistics()
        {
           // string HotelID = Convert.ToString(Session["GBAdminBizContext"]);
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID;
            Session["GBAdminBizContext"] = BizContext;

            var PartID = "1";

            PropertyStatisticsRepository objupdate = new PropertyStatisticsRepository();
            var yearlyPropertyStatistics = objupdate.YearlyPropertyStatistics(PartID, HotelID);
            ViewBag.yearlyPropertyStatistics = yearlyPropertyStatistics;
            return Json(yearlyPropertyStatistics, JsonRequestBehavior.AllowGet);

        }


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //  string CurrentCulture_TwoLetter = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (requestContext.HttpContext.Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)requestContext.HttpContext.Session["GBAdminBizContext"];
            }
            //string Nameax = ReturnSyatemCulture();
            string SelectedLanguage = "en-Gb";
            if (BizContext.SystemCultureCode != null)
            {
                SelectedLanguage = BizContext.SystemCultureCode;
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(SelectedLanguage);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(SelectedLanguage);

            base.Initialize(requestContext);
            if (SelectedLanguage != "en-Gb")
            {
                try
                {
                    CultureInfo TempCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                    TempCulture.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    TempCulture.NumberFormat.NumberDecimalSeparator = ".";
                    System.Threading.Thread.CurrentThread.CurrentCulture = TempCulture;
                    CultureInfo TempCulture1 = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentUICulture.Clone();
                    TempCulture1.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    TempCulture1.NumberFormat.NumberDecimalSeparator = ".";
                    System.Threading.Thread.CurrentThread.CurrentUICulture = TempCulture1;
                }
                catch
                {
                }

                //base.Initialize(requestContext);
            }
        }

    }
}
