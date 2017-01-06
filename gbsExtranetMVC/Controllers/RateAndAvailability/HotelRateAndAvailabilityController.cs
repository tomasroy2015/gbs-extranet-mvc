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


namespace gbsExtranetMVC.Controllers.Property
{
    [Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class HotelRateAndAvailabilityController : Controller
    {
        //
        // GET: /HotelRateAndAvailability/

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
        DataTable HotelRooms = new DataTable();
        HotelRateAndAvailabilityRepository modelRepo = new HotelRateAndAvailabilityRepository();

        DataTable Dates = new DataTable();
        DataTable HotelAvailability = new DataTable();

        List<HotelRateAndAvailabilityExt> HotelAvailabilityList = new List<HotelRateAndAvailabilityExt>();        
        List<HotelRateAndAvailabilityExt> Day = new List<HotelRateAndAvailabilityExt>();
        List<HotelRateAndAvailabilityExt> CloseOpenAvailabilityDay = new List<HotelRateAndAvailabilityExt>();
        List<HotelRateAndAvailabilityExt> Room = new List<HotelRateAndAvailabilityExt>();
        public Hashtable DateAvailability = new Hashtable();
        public ActionResult HotelRateAndAvailability()
        {
            //BizContext = (BizContext)Session["GBAdminBizContext"];
            //int HotelID = BizContext.HotelID;
            //Session["GBAdminBizContext"] = BizContext;
            AssignBizContext();
            int HotelID = BizContext.HotelID;
            HotelRooms = modelRepo.GetHotelRooms("Sort,RoomTypeName", HotelID);
            //if(id==0)
            //{
            //    return View();
            //}
            
            LoadCalendar();
            RoomDay();


            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            
            return View();
        }

        public void LoadCalendar()
        {
            //BizContext = (BizContext)Session["GBAdminBizContext"];
            //int HotelID = BizContext.HotelID;
            //Session["GBAdminBizContext"] = BizContext;

            AssignBizContext();
            int HotelID = BizContext.HotelID;

            //DateTime dt = DateTime.ParseExact(yourObject.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

           

            DateTime startDate = DateTime.Now.Date;
            //string s = startDate.ToString("yyyy/MM/dd");
            string formatted = startDate.ToString("yyyy/MM/dd");
            DateTime endDate = DateTime.Now.Date.AddDays(20);
            string formatted1 = endDate.ToString("yyyy/MM/dd");
            Dates = modelRepo.GetDates("Date", startDate, endDate);

            string startingDate = Convert.ToString(startDate);
            string endingDate = Convert.ToString(endDate);

            HotelAvailability = modelRepo.HotelAvailability(startDate, endDate, HotelID);
            HotelAvailabilityList = modelRepo.GetHotelAvailability();

            foreach (DataRow hotelRoom in HotelRooms.Rows)
            {
                //Müsaitlik datası olmayanlar için yüklenir
                int i = modelRepo.CreateHotelRoomAvailability(hotelRoom["ID"].ToString(), startDate, endDate, hotelRoom["RoomCount"].ToString());
            }

            if (startDate.Month != endDate.Month)
            {
                //int Colspan = DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1;
                //var Month1 = Dates.Select("Date='" + startDate + "'")[0]["MonthName"];

                //int Month2ColSpan = endDate.Day;
                //var Month2 = Dates.Select("Date='" + endDate + "'")[0]["MonthName"];

                ViewBag.ColSpan = DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1;
                ViewBag.Month1 = Dates.Select("Date='" + formatted + "'")[0]["MonthName"];
                var value = Dates.Select("Date='" + formatted + "'")[0]["MonthName"];
                ViewBag.Month2ColSpan = endDate.Day;
               // ViewBag.Month2 = 1;
                ViewBag.Month2 = Dates.Select("Date='" + formatted1 + "'")[0]["MonthName"];
            }
            else
            {
                ViewBag.ColSpan = endDate.Day - startDate.Day + 1;
                ViewBag.Month1 = Dates.Select("Date='" + formatted + "'")[0]["MonthName"];
                ViewBag.Month2ColSpan = 0;
            }
            Session["DateAvailability"] = new Hashtable();

            Day = modelRepo.GetDatesList();
            ViewBag.Day = Day;

            Room = modelRepo.GetHotelRoomsList();
            ViewBag.Room = Room;
            CloseOpenAvailabilityDay = modelRepo.GetDatesList();
            ViewBag.CloseOpenAvailabilityDay = CloseOpenAvailabilityDay;
        }

        protected void RoomDay()
        {
            List<HotelRateAndAvailabilityExt> RoomDayList = new List<HotelRateAndAvailabilityExt>();
            string DisplayResult = "";
            if (HotelAvailability != null)
            {
                if (HotelAvailability.Rows.Count > 0)
                {
                    DisplayResult = "Valid";
                    foreach (var RoomData in Room)
                    {

                        foreach (var Date in Day)
                        {
                            HotelRateAndAvailabilityExt Obj = new HotelRateAndAvailabilityExt();
                            Obj.DateID = Date.DateID;
                            Obj.HotelRoomID = RoomData.ID;
                            Obj.RoomTypeID = RoomData.RoomTypeID;
                            Obj.RoomTypeName = RoomData.RoomTypeName;
                            string hotelRoomID = Convert.ToString(RoomData.ID);
                            //string dateStr = Convert.ToString(Date.Date);
                            string formatted2 = Date.Date.ToString("yyyy/MM/dd");

                            DateTime date = Date.Date;
                            DataRow hotelRoomAvailability = HotelAvailability.Select("HotelRoomID=" + hotelRoomID + " AND Date='" + formatted2 + "'")[0];
                            DateAvailability = (Hashtable)Session["DateAvailability"];
                            if (!DateAvailability.Contains(date))
                            {
                                DateAvailability.Add(date, true);
                            }
                            if (Convert.ToBoolean(hotelRoomAvailability["Closed"]))
                            {
                                Obj.Date = Date.Date;
                                Obj.hotelRoomAvailabilityText = "Closed";
                                Obj.HotelAvailableStatus = "1";
                                DateAvailability[date] = false;
                            }
                            else if (Convert.ToBoolean(hotelRoomAvailability["RoomRateMissing"]))
                            {
                                Obj.Date = Date.Date;
                                Obj.hotelRoomAvailabilityText = "RoomRateMissing";
                                Obj.lbtnAvailableRoomCount = "?";
                                Obj.HotelAvailableStatus = "0";
                                DateAvailability[date] = true;
                            }
                            else if (Convert.ToInt16(hotelRoomAvailability["AvailableRoomCount"]) == 0)
                            {
                                Obj.Date = Date.Date;
                                Obj.hotelRoomAvailabilityText = "AvailableRoomCount";
                                Obj.lbtnAvailableRoomCount = "0";
                                Obj.HotelAvailableStatus = "0";
                                DateAvailability[date] = true;
                            }
                            else
                            {
                                Obj.Date = Date.Date;
                                Obj.lbtnAvailableRoomCount = Convert.ToString(hotelRoomAvailability["AvailableRoomCount"]);
                                Obj.HotelAvailableStatus = "0";
                                DateAvailability[date] = true;
                            }
                            RoomDayList.Add(Obj);
                        }
                    }
                }
            }

            ViewBag.RoomDay = RoomDayList;
            ViewBag.DisplayResult = DisplayResult;
        }

        public bool CloseOpenAvailabilityDayUpdate(string Datevalue, int HotelAvailabilityStatus, string HotelRoomID)
        {
             //string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

             Datevalue = Datevalue.Replace("12:00:00 ص", "12:00:00");

            DateTime StartDate = DateTime.Now.Date;
            string nw = Datevalue.ToString();
            DateTime dt = DateTime.Now.Date;
            //if (CultureValue == "ar")
            //{
            //    StartDate = DateTime.ParseExact(nw, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            //}
            //else
            //{
            if (Datevalue.Contains('.'))
            {

                string[] nwFormat = nw.Split(' ');

                StartDate = DateTime.ParseExact(nwFormat[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                StartDate = DateTime.ParseExact(nw, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            }
            //}
           


            AssignBizContext();
            int HotelID = BizContext.HotelID;
            bool closed = (HotelAvailabilityStatus == 1 ? false : true);

            bool status = modelRepo.CloseOpenHotelAvailability(StartDate, StartDate, closed, HotelID, HotelRoomID);
            return status;
        }
        public bool CloseOpenAvailabilityRefresh(string Datevalue)
        {
            DateTime StartDate = DateTime.Now;

            Datevalue = Datevalue.Replace("12:00:00 ص", "12:00:00");

            string nw = Datevalue.ToString();
            DateTime dt = DateTime.Now;
            if (Datevalue.Contains('.'))
            {
                string[] nwFormat = nw.Split(' ');
                StartDate = DateTime.ParseExact(nwFormat[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                StartDate = DateTime.ParseExact(nw, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            }

            AssignBizContext();
            int HotelID = BizContext.HotelID;
            string UserSessionID = BizContext.UserSessionID;

            DateAvailability = (Hashtable)Session["DateAvailability"];

            // bool closed = (HotelAvailabilityStatus == 1 ? false : true);

            bool i = modelRepo.CloseOpenHotelAvailability(StartDate, StartDate, Convert.ToBoolean(DateAvailability[StartDate]), HotelID);

            modelRepo.AddUserOperation(this, 101, "1", Convert.ToString(HotelID), UserSessionID);
            return i;
        }

        //protected string GetUserIPAddress()
        //{
        //    string VisitorsIPAddr = string.Empty;
        //    if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        //    {
        //        VisitorsIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        //    }
        //    else if (Request.UserHostAddress.Length != 0)
        //    {
        //        VisitorsIPAddr = Request.UserHostAddress;
        //    }
        //    return VisitorsIPAddr;
        //}


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
