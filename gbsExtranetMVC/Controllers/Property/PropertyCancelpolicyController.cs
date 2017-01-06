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
    public class PropertyCancelPolicyController : Controller
    {
        //
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

        public ActionResult PropertyCancelPolicy()
        {
            Session["PageName"] = "PropertyCancelPolicy";
            AssignBizContext();
            int HotelID = BizContext.HotelID;
            PropertyCancelPolicyRepository modelRepo = new PropertyCancelPolicyRepository();
            PropertyCancelPolicyExt Val = new PropertyCancelPolicyExt();
            var HotelCancelPolicy = modelRepo.GetHotelCancelPolicy().FirstOrDefault(f => f.HotelID == HotelID);
            ViewBag.PenaltyRate = DropDownLists.GetPenaltyRatedropdown(1);
           // ViewBag.HotelCancelPolicy = HotelCancelPolicyinfo;
            if (HotelCancelPolicy != null)
            {
                List<PropertyCancelPolicyExt> HotelCancelPolicyinfo = modelRepo.GetHotelCancelPolicyinfo(HotelID);
                GetTypePenaltyRatewithPartID(HotelCancelPolicy);
                AssignBizContext();
                SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
                return View(HotelCancelPolicy);
            }
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            
            return View();
          
        }
        public void GetTypePenaltyRatewithPartID(PropertyCancelPolicyExt HotelCancelPolicy)
        {
           
                ViewBag.PenaltyRate = DropDownLists.GetPenaltyRatedropdown(HotelCancelPolicy.PenaltyRateTypeID);
          
        }
        public JsonResult UpdatePropertyCancelPolicyInfo( string CanceltypeID, string PenaltyRateType, string RefundableDayCount)
        {
            AssignBizContext();
            int HotelID = BizContext.HotelID;
           
            int i;
            PropertyCancelPolicyRepository objupdate = new PropertyCancelPolicyRepository();
            try { 
           i = objupdate.UpdatePropertyCancelPolicyInfo(HotelID, CanceltypeID, PenaltyRateType, RefundableDayCount,this);
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
