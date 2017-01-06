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

namespace gbsExtranetMVC.Controllers.Reservations
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class PropertyReservationsController : Controller
    {
        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        //
        // GET: /PropertyReservations/
        public const string ActiveMenu = "Reservations";
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PropertyResrvation()
        {

            Session["PageName"] = "PropertyReservations";
            return View();
        }

        [HttpGet]
        public ActionResult View(string id) 
        {
                AssignBizContext();
                ViewBag.ReturnUrl = "/Reservation/PropertyReservations";
                if (Session["MenuTabid"] != null)
                {
                    if (Session["MenuTabid"].ToString() == "3")
                    {
                        ViewBag.ReturnUrl = "/Reservation/PropertyReservations?mtid=3";
                        SecurityUtils.SetGlobalViewbags(this, "Reservations_P", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
                    }
                }
                else
                {
                    ViewBag.ReturnUrl = "/Reservation/PropertyReservations";
                    SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
                }


                AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();
                long DecryptedReservationID = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(id)), "58421043"));
                PropertyReservationsRepository modelRepo = new PropertyReservationsRepository();

                var ReservationDetails = modelRepo.GetReservationsForView(DecryptedReservationID);

                var Reservation = ReservationDetails.FirstOrDefault(f => f.ReservationID == DecryptedReservationID);
                ViewBag.ReservationDetails = ReservationDetails;
                ViewBag.ReservationPromotion = modelRepo.ReservationPromotions( Convert.ToInt32(DecryptedReservationID));
            
               // PropertyConditions(Reservation);
                //BindViewBags(Firm);
                //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);            
            return View(Reservation);         
        }

        //public static void TrimEnd(ref string Str, string MatchStr)
        //{
        //    int index = Str.LastIndexOf(MatchStr);
        //    if (index != -1 && (index + MatchStr.Length) == Str.Length)
        //    {
        //        Str = Str.Substring(0, index);
        //    }

        //}
        public void PropertyConditions(PropertyReservationExt Reservation)
        {
            PropertyReservationsRepository modelRepo = new PropertyReservationsRepository();       
    
            try
            {
                List<string> Values = modelRepo.PropertyConditions(Reservation.HotelID, Reservation.ReservationDate);
                ViewBag.PropertyConditions = Values;


                string ReservationIDvalue = Convert.ToString(Reservation.ReservationID);
                string CheckInDatevalue =   Convert.ToString(Reservation.CheckInDate);
                string CheckOutDatevalue =  Convert.ToString(Reservation.CheckOutDate.AddDays(-1));

                BaseRepository Repo = new BaseRepository();

                DataTable HotelReservationsHistory = BizReservation.GetHotelReservationHistory(Repo.BizDB, "LogDateTime DESC", BizContext.CultureCode, "", ReservationIDvalue);

                DataTable roomRate = BizReservation.GetHotelReservationRate(Repo.BizDB, "Date", BizContext.CultureCode, "", Convert.ToString(Reservation.ID), CheckInDatevalue, CheckOutDatevalue);

                List<PropertyReservationExt> ObjList = new List<PropertyReservationExt>();
                if (roomRate != null)
                {
                    if (roomRate.Rows.Count > 0)
                    {
                        foreach (DataRow dr in roomRate.Rows)
                        {
                            PropertyReservationExt Obj = new PropertyReservationExt();

                            Obj.Day = Convert.ToString(dr["Day"]);
                            Obj.MonthName = Convert.ToString(dr["MonthName"]);
                            Obj.CurrencySymbol = Convert.ToString(dr["CurrencySymbol"]);
                            Obj.RoomPrice = Convert.ToString(dr["RoomPrice"]);
                            ObjList.Add(Obj);
                        }
                    }
                }

                DataRow[] roomBedInfo = BizHotel.GetHotelRoomBeds(Repo.BizDB, string.Empty, BizContext.CultureCode, "",
                   Convert.ToString(Reservation.HotelRoomID)).Select("OptionNo = " + Reservation.BedOptionNo);

                DataRow[] hotelReservationHistoryRows = HotelReservationsHistory.Select("HotelReservationID = " + ReservationIDvalue);
                DataTable hotelReservationHistory = new DataTable();
                DataRow[] reservationHistoryDR = null;
                DataView reservationHistoryDW = null;
                // string oldValueText = BizMessage.GetMessage(Repo.BizDB, "OldValue", BizContext.CultureCode);

                string oldValueText = Resources.Resources.OldValue;

                if (hotelReservationHistoryRows.Count() > 0)
                {
                    hotelReservationHistory = hotelReservationHistoryRows.CopyToDataTable();
                }



                string roomBedText = string.Empty;
                foreach (DataRow optionalBed in roomBedInfo)
                {
                    roomBedText += optionalBed["BedTypeNameWithCount"] + ", ";
                }

                string BedPreference = string.Empty;
                BizUtil.TrimEnd(ref roomBedText, ", ");



                BedPreference = roomBedText;

                if (hotelReservationHistoryRows.Count() > 0)
                {
                    reservationHistoryDR = hotelReservationHistory.Select("BedOptionNo <> " + Reservation.BedOptionNo);
                    if (reservationHistoryDR.Count() > 0)
                    {
                        reservationHistoryDW = reservationHistoryDR.CopyToDataTable().AsDataView();
                        reservationHistoryDW.Sort = "LogDateTime DESC";
                        DataRow[] oldRoomBedInfo = BizHotel.GetHotelRoomBeds(Repo.BizDB, string.Empty, BizContext.CultureCode, "", Convert.ToString(Reservation.HotelRoomID)).Select("OptionNo = " + reservationHistoryDW[0]["BedOptionNo"]);
                        string oldRoomBedText = string.Empty;
                        foreach (DataRow optionalBed in oldRoomBedInfo)
                        {
                            oldRoomBedText += optionalBed["BedTypeNameWithCount"] + ", ";
                        }

                        BizUtil.TrimEnd(ref oldRoomBedText, ", ");
                        // roomBedText.TrimEnd(',');
                        BedPreference += BizCommon.HTMLEmptyStr + "<i>(" + oldValueText + " : " + oldRoomBedText + ")</i>";
                    }
                }

                ViewBag.BedPreference = BedPreference;

                ViewBag.RoomRateDay = ObjList;
            }
            catch
            {
               
            }
        }

        public string ConvertStringToHex(string asciiString)
        {
            string hex = "";

            try
            {
                foreach (char c in asciiString)
                {
                    int tmp = c;
                    hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
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
                return error;
            }           
            return hex;
        }

        public string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            try
            {
                while (HexValue.Length > 0)
                {
                    StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                    HexValue = HexValue.Substring(2, HexValue.Length - 2);
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
                return error;
            }
           
            return StrValue;
        }
        
        #region Read
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            PropertyReservationsRepository modelRepo = new PropertyReservationsRepository();
            DataSourceResult result = new DataSourceResult();

               if(Session["MenuTabid"]!=null) 
               {
                   if (Session["MenuTabid"].ToString() == "3")
                   {
                       AssignBizContext();
                       int HotelID = BizContext.HotelID;
                       result = modelRepo.GetReservationsByProperty(HotelID).ToDataSourceResult(request);
                   }
               }


            else
            {
                result = modelRepo.GetReservations().ToDataSourceResult(request);
            }
            
            return Json(result);
        }
        #endregion

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
