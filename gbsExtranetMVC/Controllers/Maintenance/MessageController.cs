﻿using Kendo.Mvc.Extensions;
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
using System.Net;
using Business;

namespace gbsExtranetMVC.Controllers
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class MessageController : Controller
    {
        //
        // GET: /Message/

    

        #region Read,Update,Delete
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {

            MessageRepository obj = new MessageRepository();

            DataSourceResult result = obj.GetAllMessages().ToDataSourceResult(request);

            return Json(result);
        }

        [ValidateInput(false)]
        public ActionResult _Create([DataSourceRequest]DataSourceRequest request, MessageExt model)
        {
            if (ModelState.IsValid)
            {
                string Msg = "";
                try
                {
                    MessageRepository modelRepo = new MessageRepository();
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
        [ValidateInput(false)]
        public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, MessageExt model)
        {
            string Msg = "";
            try
            {
                MessageRepository modelRepo = new MessageRepository();
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
         [ValidateInput(false)]
        public ActionResult _Update([DataSourceRequest]DataSourceRequest request, MessageExt model)
        {
            if (ModelState.IsValid)
            {
                string Msg = "";
                try
                {
                    MessageRepository modelRepo = new MessageRepository();
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
