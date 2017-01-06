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

namespace gbsExtranetMVC.Controllers
{
    [Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class DailyStatisticsController : Controller
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
        // GET: /DailyStatistics/

        public ActionResult Index()
        {
            return View();
        }

        #region Read,Update,Delete
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            DailyStatisticsRepository obj = new DailyStatisticsRepository();


            DataSourceResult result = obj.GetHitCountTableValue().ToDataSourceResult(request);

            return Json(result);
        }
        #endregion

        #region DropdownData Append

        public JsonResult _ReadPart()
        {


            DailyStatisticsRepository obj = new DailyStatisticsRepository();

           // DataSourceResult result = ListOfModel.ToDataSourceResult(request);

            return Json(obj.GetPartTableValue(), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
