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
using Business;
using System.Globalization;
using System.Net;
namespace gbsExtranetMVC.Controllers.RateAndAvailability
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class RoomRateAndAvailabilityController : Controller
    {
        //
        // GET: /RateAndAvailability/

        public string CultureCode = "";

          BizContext BizContext = new BizContext();
          public const string ActiveMenu = "Rate & Availability";
          public void AssignBizContext()
          {
              if (Session["GBAdminBizContext"] != null)
              {
                  BizContext = (BizContext)Session["GBAdminBizContext"];
              }
              Session["GBAdminBizContext"] = BizContext;
          }
        public ActionResult RoomRateAndAvailability()
        {
            Session["PageName"] = "RoomRateAndAvailability";
           // SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            LoadViewBagValues();
            return View();
        }

        public void LoadViewBagValues()
        {
            AssignBizContext();
            NewPromotionRepository NewProm = new NewPromotionRepository();
            ViewBag.CurrencyName = BizContext.CurrencyName;
            //ViewBag.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            //ViewBag.EndDate = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
            //ViewBag.EndDate90 = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
            ViewBag.DaysDetails = NewProm.GetDay();
            ////if (CultureCode == "ar-SA")
            ////{
            ViewBag.StartDate = dateconvert(DateTime.Now.ToString("dd/MM/yyyy"));
            ViewBag.EndDate = dateconvert(DateTime.Now.AddDays(30).ToString("dd/MM/yyyy"));
            ViewBag.EndDate90 = dateconvert(DateTime.Now.AddDays(90).ToString("dd/MM/yyyy"));
            ////}
            ////else
            ////{
            ////    ViewBag.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            ////    ViewBag.EndDate = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
            ////    ViewBag.EndDate90 = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
            ////}
        }

        public string dateconvert(string date)
        {
            string covertdate = "";
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
               // covertdate = date;
            }

            return (covertdate);

        }     
        public JsonResult CheckTodayDate(string StartDate)
        {
            string nw = StartDate.ToString();
            DateTime dt = DateTime.Now;
            double DateDiff1;
            try { 
            if(StartDate.Contains('.'))
            {
                dt = DateTime.ParseExact(nw, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dt = DateTime.ParseExact(nw, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

           
            DateTime TodayDate = DateTime.Now;
            DateDiff1 = (TodayDate - dt).TotalDays;
            }
            catch (Exception ex)
            {
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }
            return Json(DateDiff1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckDateDiff(string StartDate, string Enddate)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            double DateDiff1;
            try
            { 
            if (StartDate.Contains('.'))
            {
               dtStart= DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart=DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (Enddate.Contains('.'))
            {
                dtEnd=DateTime.ParseExact(Enddate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
              dtEnd=  DateTime.ParseExact(Enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            //  DateTime TodayDate = DateTime.Now;
            DateDiff1 = (dtStart - dtEnd).TotalDays;
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
            return Json(DateDiff1, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult LoadHotelAvailabilityAndRate(string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string WeekDay)
        //{
        //    int i = 1;
        //    int RoomID = Convert.ToInt32(RoomType);
        //    BizContext = (BizContext)Session["GBAdminBizContext"];
        //    int Hotelid = BizContext.HotelID;
        //    Session["GBAdminBizContext"] = BizContext;
        //    RoomAvailabilityAndRateRepository ObjRep = new RoomAvailabilityAndRateRepository();
        //    var HotelRooms = ObjRep.GetHotelRooms(Hotelid).FirstOrDefault(f => f.RoomID == RoomID);
        //    int MaximamPeopleCount = GetMaximamPeopleCount(HotelRooms);
        //    ObjRep.CreateHotelRoomAvailability(RoomType, StartDate, Enddate, HotelRooms);
        //    ObjRep.CreateHotelRoomRate(RoomType, StartDate, Enddate, AccommodationType, PricePolicy);
        //    // ObjRep.Getdates(StartDate, Enddate, WeekDay);
        //    //ObjRep.GetHotelAvailability(Hotelid,StartDate, Enddate);
        //    //ObjRep.GetHotelRate(Hotelid, StartDate, Enddate, AccommodationType, PricePolicy);

        //    return Json(ObjRep.Getdates(StartDate, Enddate, WeekDay), JsonRequestBehavior.AllowGet);
        //}

        //public string ConvertToDateTime(string strDateTime)
        //{
        //    DateTime dtFinaldate;
        //    string sDateTime;

        //    dtFinaldate = Convert.ToDateTime(strDateTime); 
            
        //        string[] sDate = strDateTime.Split('.');
        //        sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];


        //        return sDateTime;
        //}
        public JsonResult LoadHotelAvailabilityAndRate(string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string WeekDay)
        {
            //if(CultureCode =="de-DE" || CultureCode=="ru-RU")
            //{
                
            //    DateTime dt = Convert.ToDateTime(StartDate);
            //    DateTime dt1 = Convert.ToDateTime(Enddate);
            //    string strDate = ConvertToDateTime(StartDate);
            //    StartDate = strDate;
            //    string Enddat = ConvertToDateTime(Enddate);
            //    Enddate = Enddat;           
            //}

            int RoomID = 0;
            try
            {
                RoomID = Convert.ToInt32(RoomType);
            }
            catch
            {
                RoomID = 0;
            }
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int Hotelid= BizContext.HotelID;
            Session["GBAdminBizContext"] = BizContext;
            RoomAvailabilityAndRateRepository ObjRep = new RoomAvailabilityAndRateRepository();
            var HotelRooms = ObjRep.GetHotelRooms(Hotelid).FirstOrDefault(f => f.RoomID == RoomID);
            int MaximamPeopleCount = GetMaximamPeopleCount(HotelRooms);
            ObjRep.CreateHotelRoomAvailability(RoomType, StartDate, Enddate, HotelRooms);
            ObjRep.CreateHotelRoomRate(RoomType, StartDate, Enddate, AccommodationType, PricePolicy);
            DataTable AvailabilityTable = ObjRep.GetHotelAvailability(Hotelid, StartDate, Enddate);
            DataTable RateTable = ObjRep.GetHotelRate(Hotelid, StartDate, Enddate, AccommodationType, PricePolicy);
            return Json(ObjRep.Getdates(StartDate, Enddate, WeekDay, AvailabilityTable, RateTable, Convert.ToInt32(RoomType), MaximamPeopleCount), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRoomAvailabilityAndRate(string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string WeekDay,
            string DateIDArray,string WeekDayIDArray,string SinglePriceNameArray,string DoublePriceNameArray,string RoomPriceNameArray,
            string AvailableRoomCountNameArray, string MinimumStayNameArray, string CloseToArrivalArray, string CloseToDepartureArray, string ClosedArray)
        {
            int i = 1;
            int RoomID = Convert.ToInt32(RoomType);
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int Hotelid = BizContext.HotelID;
            long UserSessionID= Convert.ToInt64(BizContext.UserSessionID);
            Session["GBAdminBizContext"] = BizContext;
            RoomAvailabilityAndRateRepository ObjRep = new RoomAvailabilityAndRateRepository();
            try { 
            var HotelRooms = ObjRep.GetHotelRooms(Hotelid).FirstOrDefault(f => f.RoomID == RoomID);
            int MaximamPeopleCount = GetMaximamPeopleCount(HotelRooms);
            ObjRep.CreateHotelRoomAvailability(RoomType, StartDate, Enddate, HotelRooms);
            ObjRep.CreateHotelRoomRate(RoomType, StartDate, Enddate, AccommodationType, PricePolicy);
            ObjRep.SaveRoomAvailabilityAndRate(Convert.ToInt32(AccommodationType), Convert.ToInt32(PricePolicy), Convert.ToInt32(RoomType), MaximamPeopleCount,
                DateIDArray, SinglePriceNameArray, DoublePriceNameArray, RoomPriceNameArray, AvailableRoomCountNameArray, MinimumStayNameArray,
                CloseToArrivalArray, CloseToDepartureArray, ClosedArray,UserSessionID, this);
            
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
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetHotelAvailability(string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string WeekDay)
        //{
        //    BizContext = (BizContext)Session["GBAdminBizContext"];
        //    int Hotelid = BizContext.HotelID;
        //    Session["GBAdminBizContext"] = BizContext;
        //    RoomAvailabilityAndRateRepository ObjRep = new RoomAvailabilityAndRateRepository();

        //    return Json(ObjRep.GetHotelAvailability(Hotelid,StartDate, Enddate), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetHotelRate(string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string WeekDay)
        //{
        //    BizContext = (BizContext)Session["GBAdminBizContext"];
        //    int Hotelid = BizContext.HotelID;
        //    Session["GBAdminBizContext"] = BizContext;
        //    RoomAvailabilityAndRateRepository ObjRep = new RoomAvailabilityAndRateRepository();
        //    return Json(ObjRep.GetHotelRate(Hotelid, StartDate, Enddate, AccommodationType, PricePolicy), JsonRequestBehavior.AllowGet);
        //}
        public int GetMaximamPeopleCount(RoomAvailabilityAndRateRepositoryExt HotelRooms)
        {
            return HotelRooms.MaxPeopleCount;
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
                CultureCode = BizContext.SystemCultureCode;
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
