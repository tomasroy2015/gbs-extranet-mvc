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
    public class PropertyInformationController : Controller
    {
        BizContext BizContext = new BizContext();
        public const string ActiveMenu = "Property";
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        //
        // GET: /Property/
         [HttpGet]
        public ActionResult PropertyInformation()
        {
            Session["PageName"] = "PropertyInformation";
             AssignBizContext();
             int id = BizContext.HotelID;
             PropertyOperationsRepository modelRepo = new PropertyOperationsRepository();
             HotelExt Val= new HotelExt();
             var Hotel = modelRepo.GetHotels().FirstOrDefault(f => f.ID == id);
             BindViewBags(Hotel);
             StoreHotelInformation(Hotel);
             AssignBizContext();
             SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);


            return View(Hotel);
        }
         public void StoreHotelInformation(HotelExt Hotel)
         {
             AssignBizContext();
             BizContext.HotelID = Hotel.ID;
             BizContext.FirmID = Convert.ToString(Hotel.FirmID);
             BizContext.HotelAccommodationTypeID = Convert.ToInt32(Hotel.HotelAccommodationTypeID);
             BizContext.CurrencyName = Hotel.CurrencyName;
             Session["GBAdminBizContext"] = BizContext;
         }
         public void BindViewBags(HotelExt Hotel)
         {
             ViewBag.GetHotelCreditCardList = DropDownLists.GetHotelCreditCardList(Convert.ToString(Hotel.ID));
             ViewBag.GetAllCreditCardList = DropDownLists.GetAllCreditCardList();
             ViewBag.CheckinFrom = DropDownLists.FillTimeList(7, 18, Hotel.CheckinStart);
             ViewBag.CheckinTo = DropDownLists.FillTimeList(12, 24, Hotel.CheckinEnd);
             ViewBag.CheckOutFrom = DropDownLists.FillTimeList(0, 14, Hotel.CheckoutStart);
             ViewBag.CheckOutTo = DropDownLists.FillTimeList(7, 18, Hotel.CheckoutEnd);
         }
         public JsonResult UpdateHotelGeneralInfo(int HotelID, string CheckinStart, string CheckinEnd,string CheckoutStart, string CheckoutEnd,  string SelectedCards)
         {
             PropertyInformationRepository objupdate = new PropertyInformationRepository();
             int i;
             try { 
             i= objupdate.UpdateHotelGeneralInfo(HotelID, CheckinStart, CheckinEnd, CheckoutStart, CheckoutEnd, SelectedCards);
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
             // int i = 1;
             return Json(i, JsonRequestBehavior.AllowGet);

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
