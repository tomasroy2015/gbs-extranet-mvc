using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Enumerations;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Business;

namespace gbsExtranetMVC.Controllers
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    // [Authorization(Permissions.Administrator)]
    public class FirmController : Controller
    {
        //
        // GET: /Firm/

        public ActionResult Index()
        {
            return View();
        }

        #region Grid Create, Read, Update, Delete Functions

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            FirmRepository modelRepo = new FirmRepository();
            DataSourceResult result = modelRepo.ReadAll().ToDataSourceResult(request);
            return Json(result);
        }

        //public ActionResult _Create([DataSourceRequest]DataSourceRequest request, FirmExt model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string Msg = "";
        //        try
        //        {
        //            TempRepository modelRepo = new TempRepository();
        //            if (modelRepo.Create(model, ref Msg, this) == false)
        //            {
        //                return this.Json(new DataSourceResult { Errors = Msg });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string error = ErrorHandling.HandleException(ex);
        //            return this.Json(new DataSourceResult { Errors = error });
        //        }
        //    }

        //    return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        //}

        //public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, TempExt model)
        //{
        //    string Msg = "";
        //    try
        //    {
        //        TempRepository modelRepo = new TempRepository();
        //        if (modelRepo.Delete(model, ref Msg, this) == false)
        //        {
        //            return this.Json(new DataSourceResult { Errors = Msg });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ErrorHandling.HandleException(ex);
        //        return this.Json(new DataSourceResult { Errors = error });
        //    }


        //    return Json(request);
        //}

        //public ActionResult _Update([DataSourceRequest]DataSourceRequest request, TempExt model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string Msg = "";
        //        try
        //        {
        //            TempRepository modelRepo = new TempRepository();
        //            if (modelRepo.Update(model, ref Msg, this) == false)
        //            {
        //                return this.Json(new DataSourceResult { Errors = Msg });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string error = ErrorHandling.HandleException(ex);
        //            return this.Json(new DataSourceResult { Errors = error });
        //        }
        //    }
        //    return Json(request);
        //}

        #endregion Grid Create, Read, Update, Delete Functions

        #region Edit User Details

        [HttpGet]
        public ActionResult Edit(long id)
        {
            //using (DBEntities db = new DBEntities())
            //{
                FirmRepository modelRepo = new FirmRepository();
                var firm = modelRepo.ReadAll().FirstOrDefault(f => f.FirmID == id);
                BindViewBags(firm);

                return View(firm);
            //}
        }

        [HttpPost]
        public ActionResult Edit(FirmExt model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Msg = "";
                    FirmRepository uRepo = new FirmRepository();
                    if (uRepo.Update(model, ref Msg, this))
                    {
                        return RedirectToAction("Index", "Firm");
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
                    ModelState.AddModelError("", "Error: Please Correct/Enter All the Information below to Save this Record, All Fields are Mandatory");
                    ErrorHandling.HandleModelStateException(ex, ModelState);
                }
            }


            return View(model);
        }

        #endregion Edit User Details


        #region populate dropdownlist 

       public void BindViewBags(FirmExt firm)
        {
            ViewBag.Countries = DropDownLists.GetCountries(firm.CountryID);

            ViewBag.Status = DropDownLists.GetStatus(firm.StatusID);

            ViewBag.Title = DropDownLists.GetStatus(firm.ExecutiveTitleID);
        }

        #endregion

    }
}
