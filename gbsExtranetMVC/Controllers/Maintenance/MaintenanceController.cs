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
using Business;
using System.Globalization;

namespace gbsExtranetMVC.Controllers
{
    //TODO: Uncomment to apply authorization to this Controller
    [Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class MaintenanceController : Controller
    {
        //This Controller will be used to Display all the maintenance Screens, But their CRUD (Create, Read, Update, Delete) Login will remain in their specific Controllers
        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        public ActionResult Bank()
        {

            Session["PageName"] = "Bank";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Bank", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
           // SecurityUtils.SetGlobalViewbags(this, "Bank");

            return View();
        }

        public ActionResult Countries()
        {
            Session["PageName"] = "Countries";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Configurations", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
            //SecurityUtils.SetGlobalViewbags(this, "Configurations");

            return View();
        }

        public ActionResult DailyStatistics()
        {
            Session["PageName"] = "DailyStatistics";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Statistics", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
          //  SecurityUtils.SetGlobalViewbags(this, "Statistics");

            return View();
        }

        public ActionResult EMails()
        {
            Session["PageName"] = "EMails";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Communications", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
          //  SecurityUtils.SetGlobalViewbags(this, "Communications");

            return View();
        }

        public ActionResult Firms()
        {
            Session["PageName"] = "Firms";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Management", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
            //SecurityUtils.SetGlobalViewbags(this, "Management");

            return View();
        }

        public ActionResult HotelSearchParameter()
        {
            Session["PageName"] = "HotelSearchParameter";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Statistics", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
          //  SecurityUtils.SetGlobalViewbags(this, "Configurations");

            return View();
        }

        public ActionResult Message()
        {
            Session["PageName"] = "Message";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Communications", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
           // SecurityUtils.SetGlobalViewbags(this, "Communications");

            return View();
        }

        public ActionResult Parameter()
        {
            Session["PageName"] = "Parameter";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Configurations", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
          //  SecurityUtils.SetGlobalViewbags(this, "Configurations");

            return View();
        }

        public ActionResult Tables()
        {
            Session["PageName"] = "Tables";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Configurations", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
           // SecurityUtils.SetGlobalViewbags(this, "Configurations");

            ViewBag.TablesList = DropDownLists.GetListOfTables();
            return View();
        }

        public ActionResult UserOperation()
        {
            Session["PageName"] = "UserOperation";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Configurations", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
           // SecurityUtils.SetGlobalViewbags(this, "Management");

            return View();
        }
        public ActionResult Authorization()
        {
            Session["PageName"] = "Authorization";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Configurations", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
           // SecurityUtils.SetGlobalViewbags(this, "Configurations");

            return View();
        }

        public ActionResult Region()
        {
            Session["PageName"] = "Region";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Configurations", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
  
           // SecurityUtils.SetGlobalViewbags(this, "Configurations");

            return View();
        }

        public ActionResult Invoice()
        {
            Session["PageName"] = "Invoice";

            //string checkvarificationcodes = Request.QueryString["VerificationCode"].ToString();
            if (Request.QueryString["mtid"] == "7")
            {
                AssignBizContext();
                SecurityUtils.SetGlobalViewbags(this, "Finance", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            }
            else
            {
                //string id = Request.QueryString["mtid"].ToString();
                AssignBizContext();
                SecurityUtils.SetGlobalViewbags(this, "Management", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            }
           // SecurityUtils.SetGlobalViewbags(this, "Invoice");

            return View();
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

            if (SelectedLanguage == "ar-SA")
            {
                try
                {
                    CultureInfo TempCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                    TempCulture.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    System.Threading.Thread.CurrentThread.CurrentCulture = TempCulture;
                    CultureInfo TempCulture1 = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentUICulture.Clone();
                    TempCulture1.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = TempCulture1;
                }
                catch
                {
                }
            }
        }



    }



}