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

namespace gbsExtranetMVC.Controllers.RoomAvailability
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class RoomAvailabilityController : Controller
    {
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

        RoomAvailabilityRepository modelHotelAvailability = new RoomAvailabilityRepository();
        RoomAvailabilityRepository modelRepo = new RoomAvailabilityRepository();
        
        DataTable HotelRooms = new DataTable();
        DataTable Dates = new DataTable();
        DataTable HotelAvilabilty = new DataTable();
        public ActionResult RoomAvailability()
        {
            Session["PageName"] = "RoomAvailability";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            
            LoadViewBagValues();
          
            return View();
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

                // covertdate = dateVal.ToString("yyyy-MM-dd");

            }

            return (covertdate);

        }
        public void LoadViewBagValues()
        {
            BizContext = (BizContext)Session["GBAdminBizContext"];
            //string CurrencyName= BizContext.CurrencyName;
            Session["GBAdminBizContext"] = BizContext;

            NewPromotionRepository NewProm = new NewPromotionRepository();
           // ViewBag.CurrencyName = BizContext.CurrencyName;
            ViewBag.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.EndDate = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
            ViewBag.DaysDetails = NewProm.GetDay();
        }

        public JsonResult CheckTodayDate(string StartDate)
        {
            int i = 1;
            try { 
            string nw = StartDate.ToString();
            DateTime dt = DateTime.ParseExact(nw, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime TodayDate = DateTime.Now;
            TimeSpan timespan = dt - TodayDate;
            i = Convert.ToInt32(timespan);
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
        public JsonResult CheckDateDiff(string StartDate, string Enddate)
        {
            int i = 1;
            try { 
            DateTime dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtEnd = DateTime.ParseExact(Enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //  DateTime TodayDate = DateTime.Now;
            TimeSpan timespan = dtStart - dtEnd;
            i = Convert.ToInt32(timespan);
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

       

        public JsonResult HotelRoomAvailability(string Mode, string StartDates, string Enddates, string RoomType)
        {
            int i = 1;

            string StartDate = dateconvert(StartDates);
            string Enddate = dateconvert(Enddates);
            List<RoomAvailabilityExt> RoomDetailList = new List<RoomAvailabilityExt>();
            try{
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID;
            // HotelID = 101006;
            Session["GBAdminBizContext"] = BizContext;

            HotelRooms = modelHotelAvailability.GetHotelRooms("RoomTypeName", HotelID);
            foreach (DataRow hotelRoom in HotelRooms.Rows)
            {
                modelRepo.CreateHotelRoomAvailability(hotelRoom["ID"].ToString(),hotelRoom["RoomCount"].ToString(), StartDate, Enddate);
            }

            if (Mode == "1")
            {

                Dates = modelRepo.GetDates("Date", StartDate, Enddate);
                HotelAvilabilty = modelRepo.GetHotelAvilabilty(StartDate, Enddate, HotelID);
                if (HotelAvilabilty.Rows.Count > 0)
                {
                    foreach (DataRow Date in Dates.Rows)
                    {
                        foreach (DataRow Room in HotelRooms.Rows)
                        {
                            RoomAvailabilityExt Obj = new RoomAvailabilityExt();

                            Obj.DateID = Convert.ToInt32(Date["ID"]);
                            Obj.Date = Convert.ToDateTime(Date["Date"]);
                            Obj.DayID = Convert.ToInt32(Date["DayID"]);
                            Obj.Day = Convert.ToInt32(Date["Day"]);
                            Obj.WeekDay = Convert.ToInt32(Date["WeekDay"]);
                            Obj.DayName = Convert.ToString(Date["DayName"]);
                            Obj.MonthID = Convert.ToInt32(Date["MonthID"]);
                            Obj.MonthName = Convert.ToString(Date["MonthName"]);
                            Obj.Year = Convert.ToInt32(Date["Year"]);

                            string hotelRoomID = Room["ID"].ToString();
                            string dateID = Date["ID"].ToString();
                            if(RoomType!="")
                            {


                            }
                            DataRow hotelRoom = HotelAvilabilty.Select("HotelRoomID=" + hotelRoomID + " AND DateID=" + dateID)[0];

                            Obj.RoomTypeID = Convert.ToInt32(Room["RoomTypeID"]);
                            Obj.RoomTypeName = Convert.ToString(Room["RoomTypeName"]);
                            Obj.HotelRoomID = Convert.ToInt32(Room["ID"]);
                            Obj.MaxPeopleCount = Convert.ToInt32(Room["MaxPeopleCount"]);
                            if (hotelRoomID == RoomType || RoomType == string.Empty)
                            {
                                Obj.txtAvailableRoomCount = true;
                            }
                            else
                            {
                                Obj.txtAvailableRoomCount = false;
                            }
                            Obj.AvailableRoomCount = Convert.ToInt32(hotelRoom["AvailableRoomCount"]);
                           


                            if (Convert.ToBoolean(hotelRoom["Closed"]))
                            {
                                Obj.hotelRoomAvailabilityText = "Closed";
                                Obj.txtAvailableRoomCount = false;
                                //Obj.txtDoublePrice = false;
                                //Obj.txtRoomPrice = false;
                            }
                            else if (Convert.ToInt16(hotelRoom["AvailableRoomCount"]) == 0)
                            {
                                Obj.hotelRoomAvailabilityText = "Full";
                            }
                            else if (Convert.ToBoolean(hotelRoom["RoomRateMissing"]))
                            {
                                Obj.hotelRoomAvailabilityText = "RateMissing";
                            }
                            else
                            {
                                Obj.hotelRoomAvailabilityText = "Available";
                            }
                            RoomDetailList.Add(Obj);
                        }
                    }
                }
                }
            else if (Mode == "2")
            {
                foreach (DataRow Room in HotelRooms.Rows)
                {
                    RoomAvailabilityExt Obj = new RoomAvailabilityExt();
                    string HotelRoomID = Room["ID"].ToString();
                    Obj.RoomTypeName = Convert.ToString(Room["RoomTypeName"]);

                    Obj.HotelRoomID = Convert.ToInt32(Room["ID"]);
                    Obj.MaxPeopleCount = Convert.ToInt32(Room["MaxPeopleCount"]);
                    if (HotelRoomID == RoomType || RoomType == string.Empty)
                    {
                        Obj.txtAvailableRoomCount = true;
                    }
                    else
                    {
                        Obj.txtAvailableRoomCount = false;
                    }

                    RoomDetailList.Add(Obj);
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
              return Json(RoomDetailList, JsonRequestBehavior.AllowGet);

            }

            

       
        public JsonResult SaveDailyRoomAvailability(string Mode, string StartDates, string Enddates, string RoomType, string DateIDArray, string HotelRoomID, string AvailRoomcountArray, string MaxPeopleCount)
        {
            int j = 1;
            RoomAvailabilityRepository Obj = new RoomAvailabilityRepository();
            try { 
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID;
            // HotelID = 101006;
            string StartDate = dateconvert(StartDates);
            string Enddate = dateconvert(Enddates);
            Session["GBAdminBizContext"] = BizContext;

            modelRepo.CreateHotelRoomAvailability(HotelRoomID, MaxPeopleCount, StartDate, Enddate);

            //Obj.SaveHotelRate(HotelRoomID, DateIDArray, AvailRoomcountArray, DateTime.Now, this);
            Obj.SaveHotelAvailabilty(HotelRoomID, DateIDArray, AvailRoomcountArray,DateTime.Now, this);
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
            return Json(j, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveRoomAvailability(string Mode, string StartDate, string Enddate, string RoomType, string RoomIDArray, string availabiltyArray, string CloseOpenAvailabilityArray)
        {
            int j = 0;
            RoomAvailabilityRepository Obj = new RoomAvailabilityRepository();
            try { 
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID;
            // HotelID = 101006;

            Session["GBAdminBizContext"] = BizContext;

            HotelRooms = modelHotelAvailability.GetHotelRooms("RoomTypeName", HotelID);
            foreach (DataRow hotelRoom in HotelRooms.Rows)
            {
                modelRepo.CreateHotelRoomAvailability(hotelRoom["ID"].ToString(), hotelRoom["RoomCount"].ToString(), StartDate, Enddate);
            }

           
            string[] RoomID = RoomIDArray.Split(',');
            string[] availabiltyArrayvalue = availabiltyArray.Split(',');
            string[] CloseOpenAvailability = CloseOpenAvailabilityArray.Split(',');
          
            for (int i = 0; i < RoomID.Length; i++)
            {
                string HotelRoomID = RoomID[i];
                string availabiltyValue = availabiltyArrayvalue[i];
                string CloseOpenAvailabilityValue = CloseOpenAvailability[i];
               // string RoomPriceValue = RoomPrice[i];
                //if (SinglePriceValue != string.Empty)
                //{
                //    singlePrice = Convert.ToDouble(FormatToNumber(SinglePriceValue, "en", "en-Gb", 2));
                //}
                //if (DoublePriceValue != string.Empty)
                //{
                //    doublePrice = Convert.ToDouble(FormatToNumber(DoublePriceValue, "en", "en-Gb", 2));
                //}
                //if (RoomPriceValue != string.Empty)
                //{
                //    roomPrice = Convert.ToDouble(FormatToNumber(RoomPriceValue, "en", "en-Gb", 2));
                //}

                //FormatToNumber(DoublePriceValue, "en", "en-Gb", 2, true, doublePrice);
                //FormatToNumber(RoomPriceValue, "en", "en-Gb", 2, true, roomPrice);
                if (availabiltyValue != "" )
                {
                    Obj.SaveRoomAvailability(StartDate, Enddate, HotelRoomID, availabiltyValue, CloseOpenAvailabilityValue, DateTime.Now, this);
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
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        public int GetMaximamPeopleCount(RoomAvailabilityExt HotelRooms)
        {
            return HotelRooms.RoomCount;
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
