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

namespace ExtranetWork.Controllers
{
    [Authorization(Permissions.AllUsers)]
    public class UserOperationController : Controller
    {
        //
        // GET: /UserOperation/

        public ActionResult Index()
        {
            return View();
        }

        #region _Read

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
 
            UserOperationRepository obj = new UserOperationRepository();

            DataSourceResult result = obj.GetUserOperationDetails().ToDataSourceResult(request);

            return Json(result);
        }


        #endregion

        #region TypeOperationAppend
        public JsonResult _ReadOperationType()
        {
            UserOperationRepository obj = new UserOperationRepository();
  

            // DataSourceResult result = ListOfModel.ToDataSourceResult(request);

            return Json(obj.GetOperationTypeTableValue(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
