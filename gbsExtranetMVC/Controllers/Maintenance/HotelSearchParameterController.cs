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

namespace gbsExtranetMVC.Controllers
{
    [Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class HotelSearchParameterController : Controller
    {
        //
        // GET: /HotelSearchParameter/

        public ActionResult Index()
        {
            return View();
        }



        #region Read
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {

            HotelSearchParameterRepository obj = new HotelSearchParameterRepository();


            DataSourceResult result = obj.GetHotelSearchParameterDetails().ToDataSourceResult(request);

            return Json(result);
        }
        #endregion
    }
}
