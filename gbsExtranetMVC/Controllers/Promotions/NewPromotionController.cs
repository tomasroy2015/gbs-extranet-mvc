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

namespace gbsExtranetMVC.Controllers.Promotions
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class NewPromotionController : Controller
    {
        BizContext BizContext = new BizContext();
        public JsonResult GetPromotions()
        {
            NewPromotionRepository modelRepo = new NewPromotionRepository();

            return Json(modelRepo.ReadAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PromotionInsert( string DiscountPercentage,string AccommodationStartDate,string AccommodationEndDate,
                string WeekDay, string PricePolicy, string RoomCount, string RoomType, string MinimumStayDayCount, string EarlyBookerMargin, 
                string LastMinuteMargin, string BookingDate,string PromotionID,string HasDiscount, string validForAllRoomTypes,int SecretDeal)
        {
            NewPromotionRepository modelRepo = new NewPromotionRepository();
            string Status = "";   
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int id = BizContext.HotelID;
            Session["GBAdminBizContext"] = BizContext;
            try { 
            string CheckPromotion = modelRepo.HotelPromotion(id, AccommodationStartDate, AccommodationEndDate, RoomType);
            if (CheckPromotion == "NoConflict")
            {
                Status = modelRepo.PromotionInsert(DiscountPercentage, AccommodationStartDate, AccommodationEndDate, WeekDay,
                    PricePolicy, RoomCount, RoomType, MinimumStayDayCount, EarlyBookerMargin, LastMinuteMargin, BookingDate, PromotionID,
                    HasDiscount, validForAllRoomTypes, id, SecretDeal, this);
            }
            else
            {
                Status = "Conflict";
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
            return Json(Status, JsonRequestBehavior.AllowGet);
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
