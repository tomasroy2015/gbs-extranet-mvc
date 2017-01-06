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

namespace gbsExtranetMVC.Controllers.Finance
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class FinancialOverviewController : Controller 
    {
        //
        // GET: /FirmInformation/
        BizContext BizContext = new BizContext();
        public ActionResult Index()
        {
           
            return View();
        }
        public const string ActiveMenu = "Finance";
        public ActionResult FinancialOverview()
        {
            Session["PageName"] = "Finance";
            try
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
                int id = Convert.ToInt32(BizContext.FirmID);
                Session["GBAdminBizContext"] = BizContext;
                SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
                return View();
            }
            catch(Exception ex)
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
            
        }
        //public const string ActiveMenu = "Finance";
        //public ActionResult FirmInformation()             
        //{
        //    Session["PageName"] = "Finance";
        //    try
        //    {
        //        BizContext = (BizContext)Session["GBAdminBizContext"];
        //        int id = Convert.ToInt32(BizContext.FirmID);
        //        Session["GBAdminBizContext"] = BizContext;
        //        //  long id = 100003;

        //        FirmInformationRepository objupdate = new FirmInformationRepository();
        //        var Firm = objupdate.GetFirmInfo(id).FirstOrDefault(f => f.ID == id);
        //        SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
        //        return View(Firm);
        //    }
        //    catch(Exception ex)
        //    {
        //        string hostName1 = Dns.GetHostName();
        //        string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
        //        string PageName = Convert.ToString(Session["PageName"]);
        //        //string GetUserIPAddress = GetUserIPAddress1();
        //        using (BaseRepository baseRepo = new BaseRepository())
        //        {
        //            //BizContext BizContext1 = new BizContext();
        //            BizApplication.AddError(baseRepo.BizDB, PageName, ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
        //        }
        //        Session["PageName"] = "";
        //        string error = ErrorHandling.HandleException(ex);
        //        return this.Json(new DataSourceResult { Errors = error });
        //    }

            
        //}
        //public JsonResult DisplayFirmInformation()
        //{

        //    var Value = "";

        //    try
        //    {
        //        BizContext = (BizContext)Session["GBAdminBizContext"];
        //        int id = Convert.ToInt32(BizContext.FirmID);
        //        Session["GBAdminBizContext"] = BizContext;
        //        FirmInformationRepository objupdate = new FirmInformationRepository();
        //        var i = objupdate.GetFirmInfo(id);
        //        Value = Convert.ToString(i);
        //    }
        //    catch(Exception ex)
        //    {
        //        string hostName1 = Dns.GetHostName();
        //        string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
        //        string PageName = Convert.ToString(Session["PageName"]);
        //        //string GetUserIPAddress = GetUserIPAddress1();
        //        using (BaseRepository baseRepo = new BaseRepository())
        //        {
        //            //BizContext BizContext1 = new BizContext();
        //            BizApplication.AddError(baseRepo.BizDB, PageName, ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
        //        }
        //        Session["PageName"] = "";
        //        string error = ErrorHandling.HandleException(ex);
        //        return this.Json(new DataSourceResult { Errors = error });
        //    }



        //    return Json(Value, JsonRequestBehavior.AllowGet);

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
