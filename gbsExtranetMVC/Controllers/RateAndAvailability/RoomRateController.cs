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


namespace gbsExtranetMVC.Controllers.Property
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class RoomRateController : Controller
    {
        //
        // GET: /RoomRate/

        //public ActionResult Index()
        //{
        //    return View();
        //}

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



        DataTable HotelRooms = new DataTable();
        DataTable Dates = new DataTable();
        DataTable HotelRate = new DataTable();
        DataTable cancelPolicy = new DataTable();

        RoomRateRepository modelRepo = new RoomRateRepository();
        HotelRateAndAvailabilityRepository modelHotelRateAndAvailability = new HotelRateAndAvailabilityRepository();

        List<HotelRateAndAvailabilityExt> Day = new List<HotelRateAndAvailabilityExt>();
        List<HotelRateAndAvailabilityExt> RoomHeader = new List<HotelRateAndAvailabilityExt>();
        List<RoomRateExt> RateDetails = new List<RoomRateExt>();

        // int HotelID = 101006;
        public ActionResult RoomRate()
        {
            Session["PageName"] = "RoomRate";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);

            LoadViewBagValues();
            if (Request.QueryString["HotelRoomID"] != null)
            {
                string RoomID = Request.QueryString["HotelRoomID"].ToString();
                AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();
                long HotelRoomID = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(RoomID)), "58421043"));
                ViewBag.HotelRoomID = HotelRoomID;
            }
            

            return View();
        }


        public void LoadViewBagValues()
        {
            AssignBizContext();
            int HotelID = BizContext.HotelID;
            NewPromotionRepository NewProm = new NewPromotionRepository();
            ViewBag.CurrencyName = BizContext.CurrencyName;
            //if (CultureCode == "ar-SA")
            //{
                ViewBag.StartDate = dateconvert(DateTime.Now.ToString("dd/MM/yyyy"));
                ViewBag.EndDate = dateconvert(DateTime.Now.AddDays(30).ToString("dd/MM/yyyy"));
                ViewBag.EndDate90 = dateconvert(DateTime.Now.AddDays(90).ToString("dd/MM/yyyy"));
            //}
            //else
            //{
            //    ViewBag.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            //    ViewBag.EndDate = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
            //    ViewBag.EndDate90 = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
            //}
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

       

        public JsonResult LoadHotelRate(string Mode, string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType)
        {
            int i = 1;
            int j = 1;
            RoomAvailabilityAndRateRepository ObjRep = new RoomAvailabilityAndRateRepository();
            List<RoomRateExt> RoomDetailList = new List<RoomRateExt>();
            try { 
            AssignBizContext();
            int HotelID = BizContext.HotelID;

            HotelRooms = modelHotelRateAndAvailability.GetHotelRooms("RoomTypeName", HotelID);
            foreach (DataRow hotelRoom in HotelRooms.Rows)
            {
                ObjRep.CreateHotelRoomRate(hotelRoom["ID"].ToString(), StartDate, Enddate, AccommodationType, PricePolicy);
                int RoomID = Convert.ToInt32(hotelRoom["ID"]);
                var HotelRoomsNew = ObjRep.GetHotelRooms(HotelID).FirstOrDefault(f => f.RoomID == RoomID);
                ObjRep.CreateHotelRoomAvailability(hotelRoom["ID"].ToString(), StartDate, Enddate, HotelRoomsNew);
            }
           
            if (Mode == "1")
            {

                Dates = modelRepo.GetDates("Date", StartDate, Enddate);
                HotelRate = modelRepo.GetHotelRate(HotelID, StartDate, Enddate, AccommodationType, PricePolicy);
                var count = HotelRate.Rows.Count;
                foreach (DataRow Date in Dates.Rows)
                {

                    foreach (DataRow Room in HotelRooms.Rows)
                    {
                        RoomRateExt Obj = new RoomRateExt();

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
                        if (j <= count)
                        {
                            DataRow hotelRoomRate = HotelRate.Select("HotelRoomID=" + hotelRoomID + " AND DateID=" + dateID)[0];

                            Obj.RoomTypeID = Convert.ToInt32(Room["RoomTypeID"]);
                            Obj.RoomTypeName = Convert.ToString(Room["RoomTypeName"]);
                            Obj.HotelRoomID = Convert.ToInt32(Room["ID"]);
                            Obj.MaxPeopleCount = Convert.ToInt32(Room["MaxPeopleCount"]);

                            if (hotelRoomID == RoomType || RoomType == string.Empty)
                            {
                                Obj.txtSinglePrice = true;
                            }
                            else
                            {
                                Obj.txtSinglePrice = false;
                            }
                            if (Obj.txtSinglePrice == true)
                            {
                                Obj.txtDoublePrice = true;
                            }
                            else
                            {
                                Obj.txtDoublePrice = false;
                            }
                            if (Obj.txtSinglePrice == true)
                            {
                                Obj.txtRoomPrice = true;
                            }
                            else
                            {
                                Obj.txtRoomPrice = false;
                            }
                            //Obj.txtSinglePrice = true;
                            //Obj.txtDoublePrice = true;
                            //Obj.txtRoomPrice = true;

                            Obj.SinglePrice = FormatToNumber(hotelRoomRate["SinglePrice"], "en-Gb", "en", 2);
                            Obj.DoublePrice = FormatToNumber(hotelRoomRate["DoublePrice"], "en-Gb", "en", 2);
                            Obj.RoomPrice = FormatToNumber(hotelRoomRate["RoomPrice"], "en-Gb", "en", 2);

                            if (Convert.ToBoolean(hotelRoomRate["Closed"]))
                            {
                                Obj.hotelRoomAvailabilityText = "Closed";
                                Obj.txtSinglePrice = false;
                                Obj.txtDoublePrice = false;
                                Obj.txtRoomPrice = false;
                            }
                            else if (Convert.ToInt16(hotelRoomRate["AvailableRoomCount"]) == 0)
                            {
                                Obj.hotelRoomAvailabilityText = "Full";
                            }
                            else if (Convert.ToBoolean(hotelRoomRate["RoomRateMissing"]))
                            {
                                Obj.hotelRoomAvailabilityText = "RateMissing";
                            }
                            else
                            {
                                Obj.hotelRoomAvailabilityText = "Available";
                            }
                        }
                        RoomDetailList.Add(Obj);
                    }
                    j = j + 1;

                }
            }

            
         
            else if (Mode == "2")
            {
                foreach (DataRow Room in HotelRooms.Rows)
                {

                    RoomRateExt Obj = new RoomRateExt();
                    string HotelRoomID = Room["ID"].ToString();
                    Obj.RoomTypeName = Convert.ToString(Room["RoomTypeName"]);

                    Obj.HotelRoomID = Convert.ToInt32(Room["ID"]);
                    Obj.MaxPeopleCount = Convert.ToInt32(Room["MaxPeopleCount"]);

                    if (HotelRoomID == RoomType || RoomType == string.Empty)
                    {
                        Obj.txtSinglePrice = true;
                    }
                    if (Obj.txtSinglePrice == true && Obj.MaxPeopleCount > 1)
                    {
                        Obj.txtDoublePrice = true;
                    }
                    if (Obj.txtSinglePrice == true && Obj.MaxPeopleCount > 2)
                    {
                        Obj.txtRoomPrice = true;
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

        public JsonResult SaveDailyRoomRate(string Mode, string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string DateIDArray, string HotelRoomID, string SinglePriceArray, string DoublePriceArray, string RoomPriceArray, int MaxPeopleCount)
        {
            int j = 1;
            RoomRateRepository Obj = new RoomRateRepository();
            try { 
            AssignBizContext();
            int HotelID = BizContext.HotelID;
            long UserSessionID = Convert.ToInt64(BizContext.UserSessionID);

            modelRepo.CreateHotelRoomRate(HotelRoomID, StartDate, Enddate, PricePolicy, AccommodationType);

            Obj.SaveHotelRate(HotelID, HotelRoomID, DateIDArray, PricePolicy, AccommodationType, SinglePriceArray, DoublePriceArray, RoomPriceArray, MaxPeopleCount, UserSessionID, DateTime.Now, this);
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

        public JsonResult SaveRoomRate(string Mode, string StartDate, string Enddate, string RoomType, string PricePolicy, string AccommodationType, string HotelRoomID, string SinglePrice, string DoublePrice, string RoomPrice)
        {
            int j = 0;
            RoomRateRepository Obj = new RoomRateRepository();
            try { 
            AssignBizContext();
            int HotelID = BizContext.HotelID;
            long UserSessionID = Convert.ToInt64(BizContext.UserSessionID);
            HotelRooms = modelHotelRateAndAvailability.GetHotelRooms("RoomTypeName", HotelID);
            foreach (DataRow hotelRoom in HotelRooms.Rows)
            {
                modelRepo.CreateHotelRoomRate(hotelRoom["ID"].ToString(), StartDate, Enddate, PricePolicy, AccommodationType);
            }

            double singlePrice = 0;
            double doublePrice = 0;
            double roomPrice = 0;

            if (SinglePrice != string.Empty)
            {
                singlePrice = Convert.ToDouble(FormatToNumber(SinglePrice, "en", "en-Gb", 2));
            }
            if (DoublePrice != string.Empty)
            {
                doublePrice = Convert.ToDouble(FormatToNumber(DoublePrice, "en", "en-Gb", 2));
            }
            if (RoomPrice != string.Empty)
            {
                roomPrice = Convert.ToDouble(FormatToNumber(RoomPrice, "en", "en-Gb", 2));
            }

            Obj.SaveHotelRoomRate(HotelID, HotelRoomID, StartDate, Enddate, PricePolicy, AccommodationType, singlePrice, doublePrice, roomPrice, UserSessionID, DateTime.Now, this);
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


        public string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }

        public static string FormatToNumber(object Value, string InputNumberCultureCode, string FormatNumberCultureCode, Int16 MaxDecimalLength = 5, bool RemoveDecimalZeros = true, double NumericValue = 0)
        {

            string numberStr = string.Empty;
            System.Globalization.CultureInfo inputNumberCultureInfo = new System.Globalization.CultureInfo(InputNumberCultureCode);
            System.Globalization.CultureInfo formatNumberCultureInfo = new System.Globalization.CultureInfo(FormatNumberCultureCode);
            double d = 0;

            if (Value != null && !object.ReferenceEquals(Value, DBNull.Value) && double.TryParse(Convert.ToString(Value), System.Globalization.NumberStyles.Number, inputNumberCultureInfo, out d))
            {
                if (Value is double || Value is decimal || Value is int || Value is long)
                {
                    d = Convert.ToDouble(Value);
                }

                if (d == Math.Floor(d) && RemoveDecimalZeros)
                {
                    formatNumberCultureInfo.NumberFormat.NumberDecimalDigits = 0;
                }
                else
                {
                    formatNumberCultureInfo.NumberFormat.NumberDecimalDigits = MaxDecimalLength;
                }
                numberStr = d.ToString("n", formatNumberCultureInfo);

                if (formatNumberCultureInfo.NumberFormat.NumberDecimalDigits > 0 && RemoveDecimalZeros)
                {

                    numberStr = numberStr.TrimEnd('0');
                    numberStr = numberStr.TrimEnd('.');
                    // numberStr = numberStr.TrimEnd(formatNumberCultureInfo.NumberFormat.NumberDecimalSeparator);
                }

                numberStr = numberStr.Replace(formatNumberCultureInfo.NumberFormat.NumberGroupSeparator, string.Empty);
                NumericValue = double.Parse(numberStr, formatNumberCultureInfo);

            }

            return numberStr;

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
