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
using System.Web;

namespace gbsExtranetMVC.Controllers
{
    //TODO: Uncomment to apply authorization to this Controller
    [Authorization(Permissions.AllUsers)]

   

    [RequiresSSL]
    public class HomeController : Controller
    {
        BizContext BizContext = new BizContext();

        public ActionResult Index()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;

            ViewBag.HotelID = BizContext.HotelID;

            ViewBag.HotelListVisible = "none";
            ViewBag.HotelLableVisible = "";
            if (BizContext.Hotels.Count > 0)
            {
                ViewBag.HotelList = BizContext.Hotels;
                if (BizContext.Hotels.Count > 1)
                {
                    ViewBag.HotelText = "True";
                    ViewBag.HotelListVisible = "";
                    ViewBag.HotelLableVisible = "none";
                }
              
            }
            //Session["SelectedHotelID"] = "";
            //Session["SelectedHotelName"] = "";
            if (BizContext.HotelID>0)
            {
                ViewBag.HasHotels = "True";
                ViewBag.HotelIndex = "block";
                ViewBag.CommonIndex = "none";
            }
            else
            {
                ViewBag.HasHotels = "false";
                ViewBag.HotelIndex = "none";
                ViewBag.CommonIndex = "block";
            }

            SecurityUtils.SetGlobalViewbags(this, "Home",BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(),BizContext.HotelID);

            StatsRepository modelRepo = new StatsRepository();
            return View(modelRepo.ReadStats());
        }

        public JsonResult ChageHotelValues(string HotelID, string Hotelname)
        {
            int i = 1;
            if (HotelID != "")
            {
                if (Session["GBAdminBizContext"] != null)
                {
                    BizContext = (BizContext)Session["GBAdminBizContext"];
                }
                BizContext.HotelID = Convert.ToInt32(HotelID);
                Session["GBAdminBizContext"] = BizContext;
            }
            Session["SelectedHotelID"] = HotelID;
            Session["SelectedHotelName"] = Hotelname;
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        #region Grid Create, Read, Update, Delete Functions

       

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var repository = new UsersRepository();
            var items = repository.ReadAll();
            var result = items.ToDataSourceResult(request);
            return Json(result);
        }


        #endregion Grid Create, Read, Update, Delete Functions

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

        public JsonResult  GetAllCultureDB()
        {
            Home obj= new Home();
            //List<HomeExt> CultureList = new List<HomeExt>();
           // CultureList=obj.GetCulturecode();
            return Json(obj.GetCulturecode(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveChangedLanguage(string Code,string SystemCode)
        {
            int i = 1;

            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            BizContext.SystemCultureCode = SystemCode;
            BizContext.CultureCode = Code;
            Session["GBAdminBizContext"] = BizContext;
            Session["CultureCode"] = Code;
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        #region Read

        public ActionResult _ReadCheckInFuture([DataSourceRequest]DataSourceRequest request)
        {
            Home modelRepo = new Home();
            NewPromotionRepository NewProm = new NewPromotionRepository();
            int RecentDayCount = NewProm.GetParameterValue("RecentDayCount");
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
            int HotelID= BizContext.HotelID;
            DateTime NowDate = DateTime.Now;
            DateTime EndDate = DateTime.Now.AddDays(RecentDayCount);
            DataSourceResult result = modelRepo.ReadCheckInFuture(HotelID, NowDate, EndDate).ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult _ReadCheckOutFuture([DataSourceRequest]DataSourceRequest request)
        {
            Home modelRepo = new Home();
            NewPromotionRepository NewProm = new NewPromotionRepository();
            int RecentDayCount = NewProm.GetParameterValue("RecentDayCount");
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
            int HotelID = BizContext.HotelID;
            DateTime NowDate = DateTime.Now;
            DateTime EndDate = DateTime.Now.AddDays(RecentDayCount);
            DataSourceResult result = modelRepo.ReadCheckOutFuture(HotelID, NowDate, EndDate).ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult _recentHotelReservations([DataSourceRequest]DataSourceRequest request)
        {
            Home modelRepo = new Home();
            NewPromotionRepository NewProm = new NewPromotionRepository();
            int RecentDayCount = NewProm.GetParameterValue("RecentDayCount");
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
            int HotelID = BizContext.HotelID;
            DateTime NowDate = DateTime.Now.AddDays(RecentDayCount * -1);
            DateTime EndDate = DateTime.Now;
            DataSourceResult result = modelRepo.recentHotelReservations(HotelID, NowDate, EndDate).ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult _recentHotelReservationCancels([DataSourceRequest]DataSourceRequest request)
        {
            Home modelRepo = new Home();
            NewPromotionRepository NewProm = new NewPromotionRepository();
            int RecentDayCount = NewProm.GetParameterValue("RecentDayCount");
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
            int HotelID = BizContext.HotelID;
            DateTime NowDate =DateTime.Now.AddDays(RecentDayCount*-1);
            DateTime EndDate = DateTime.Now;
            DataSourceResult result = modelRepo.recentHotelReservationCancels(HotelID, NowDate, EndDate).ToDataSourceResult(request);
            return Json(result);
        }
      
        #endregion
    }



}