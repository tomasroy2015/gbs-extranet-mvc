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


namespace gbsExtranetMVC.Controllers.Management
{
    [Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class UserMessagesController : Controller
    {
        //
        // GET: /UserMessages/

        public ActionResult Index()
        {
            return View();
        }
        #region Read
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            UserMessagesRepository modelRepo = new UserMessagesRepository();
            DataSourceResult result = modelRepo.GetUserMessages().ToDataSourceResult(request);
            return Json(result);
        }
        #endregion


        BizContext BizContext = new BizContext();


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

        public JsonResult Update(long id)
        {
            UserMessagesRepository modelRepo = new UserMessagesRepository();
            int i = modelRepo.UpdateUserMessage(id, this);

            return Json(i, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Delete(long id)
        {
            UserMessagesRepository modelRepo = new UserMessagesRepository();
            int i = modelRepo.DeleteUserMessage(id, this);

            return Json(i, JsonRequestBehavior.AllowGet);

        }

    }
}
