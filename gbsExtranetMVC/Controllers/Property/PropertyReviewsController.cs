using Business;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Enumerations;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Controllers.Property
{
     [Authorization(Permissions.AllUsers)]
     [RequiresSSL]
    public class PropertyReviewsController : Controller
    {
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

        public ActionResult PropertyReviews ()
        {
            Session["PageName"] = "PropertyReviews";
            AssignBizContext();
            int HotelID = BizContext.HotelID; ;
            Session["HotelID"] = HotelID;

            //GetReviews(HotelID);

            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View();
        }

        public JsonResult GetReviews()
        {
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID; ;
            int HotelID1 = Convert.ToInt32(Session["HotelID"]);
            PropertyReviewRepository modelRepo = new PropertyReviewRepository();

            var PropertyReviews = modelRepo.GetReviews(HotelID);

            return Json(PropertyReviews, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetReviews1()
        {
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int HotelID = BizContext.HotelID; ;
            int HotelID1 = Convert.ToInt32(Session["HotelID"]);
            PropertyReviewRepository modelRepo = new PropertyReviewRepository();

            var PropertyReviews = modelRepo.GetReviews1(HotelID);

            return Json(PropertyReviews, JsonRequestBehavior.AllowGet);
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
