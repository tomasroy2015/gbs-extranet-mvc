using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Web.Mvc;
using gbsExtranetMVC.Models.Enumerations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Net;
using Business;

namespace gbsExtranetMVC.Controllers
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class ParametersController : Controller
    {
        public ActionResult Parameters()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Grid Create, Read, Update, Delete Functions

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            ParametersRepositary modelRepo = new ParametersRepositary();
            DataSourceResult result = modelRepo.GetParameters().ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult _Create([DataSourceRequest]DataSourceRequest request, ParameterExt model)
        {
            if (ModelState.IsValid)
            {
                string Msg = "";
                try
                {
                    ParametersRepositary modelRepo = new ParametersRepositary();
                    if (modelRepo.Create(model, ref Msg, this) == false)
                    {
                        return this.Json(new DataSourceResult { Errors = Msg });
                    }
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
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, ParameterExt model)
        {
            string Msg = "";
            try
            {
                ParametersRepositary modelRepo = new ParametersRepositary();
                if (modelRepo.Delete(model, ref Msg, this) == false)
                {
                    return this.Json(new DataSourceResult { Errors = Msg });
                }
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


            return Json(request);
        }

        public ActionResult _Update([DataSourceRequest]DataSourceRequest request, ParameterExt model)
        {
            if (ModelState.IsValid)
            {
                string Msg = "";
                try
                {
                    ParametersRepositary modelRepo = new ParametersRepositary();
                    if (modelRepo.Update(model, ref Msg, this) == false)
                    {
                        return this.Json(new DataSourceResult { Errors = Msg });
                    }
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
            }
            return Json(request);
        }

        #endregion


   
    }
}