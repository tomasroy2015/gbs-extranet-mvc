using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Routing;
using gbsExtranetMVC.Models;
using System.Diagnostics;
using gbsExtranetMVC.Helpers;

namespace gbsExtranetMVC.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        public static ExceptionContext filterContext;
        public ActionResult getErrorCode(string id)
        {
            string errorCodeNumber = ErrorHandling.ErrorCode;
            ErrorHandling.ErrorCode = null;
            return new JsonResult { Data = new { errorCode = errorCodeNumber } };
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult UnAuthorizedAccess()
        {
            if (Request.QueryString["Locked"] != null)
            {
                ViewBag.Locked = "Your cannot log on the System, because your status is Locked. Please contact your Administrator.";
            }
            return View();
        }

        #region Page not Found Exception Handling Page

        public ActionResult PageNotFound()
        {

            if (Request.Url.Query.Equals(""))
            { }
            else
            {


                string Errordetail = Server.HtmlEncode("<b>Requested Page: </b>" + Request.Url + "<br/>");

                string err = "<b>Page Not Found.</b><hr><br>" +
                       "<br/><b>Error Occurrence: </b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
                       "<br/><b>Error Message: </b> Page Not Found (404) " +
                       "<br/><b>Requested Page: </b>" + Request.Url + "<br/>";

                if (SendSupportPageNotFoundEmail(err))
                    ViewBag.EmailSent = true;
                else
                    ViewBag.EmailSent = false;

                //ErrorLogs errlog = new ErrorLogs();
                //errlog.Occurrence = DateTime.Now;
                //errlog.ErrorMsg = "Page Not Found (404 Error)";
                //errlog.ErrorDetail = Errordetail;

                //errlog.IsSolved = false;

                //using (PMS3Entities db = new PMS3Entities())
                //{
                //    db.ErrorLogs.Add(errlog);
                //    db.SaveChanges();
                //}
            }
            return View();
        }

        #endregion

        #region Unhandled Exception view

        public ActionResult UnhandledException()
        {
            if (filterContext != null)
            {
                Exception ex = filterContext.Exception;

                StackTrace st = new StackTrace(ex, true);

                string Errordetail = Server.HtmlEncode("<b>Error Location: </b>" + st.GetFrame(5).ToString() + "<br/><br/>" +
                                        ex.StackTrace.ToString());



                if (ex != null)
                {
                    //Check if it is the Message from JQuery Ajax functions, for these functions I have throw an Exception Forcefully, So, Ignore such messages
                    if (ex.Message.ToString() != "" && !(ex.Message.ToString().Contains("executing child request for handler")))
                    {
                        string err = "<b>An unexpected error has been occurred.</b><hr><br>" +
                        "<br/><b>Error Occurrence: </b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
                        "<br/><b>Error Message: </b>" + ex.Message.ToString() +
                        "<br/><b>Stack Trace:</b><br/>" + Errordetail;

                        if (SendSupportEmail(err))
                            ViewBag.EmailSent = true;
                        else
                            ViewBag.EmailSent = false;

                        //ErrorLogs errlog = new ErrorLogs();
                        //errlog.Occurrence = DateTime.Now;
                        //errlog.ErrorMsg = ex.Message.ToString();
                        //errlog.ErrorDetail = Errordetail;

                        //errlog.IsSolved = false;

                        //using (TraksisDBEntities db = new TraksisDBEntities())
                        //{
                        //    db.ErrorLogs.Add(errlog);
                        //    db.SaveChanges();
                        //}
                    }
                    else
                    {
                        ViewBag.EmailSent = true;
                    }
                }
                else
                {
                    ViewBag.EmailSent = false;
                }

                filterContext = null;
            }
            else
            {
                ViewBag.EmailSent = false;
            }

            return View();
        }

        #endregion

        #region Send Emails

        public bool SendSupportEmail(string errorDetail)
        {
            if (SecurityUtils.SendEmail(SecurityUtils.AdminMAILING_EMAIL_ADDRESS, SecurityUtils.SupportEmailTo, "Unexpected Error Occured - Traksis", errorDetail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SendSupportPageNotFoundEmail(string errorDetail)
        {
            if (SecurityUtils.SendEmail(SecurityUtils.AdminMAILING_EMAIL_ADDRESS, SecurityUtils.SupportEmailTo, "Page Not Found - Traksis", errorDetail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }


    //public class ErrorHandlingActionInvoker :
    //ControllerActionInvoker
    //{
    //    private readonly IExceptionFilter filter;

    //    public ErrorHandlingActionInvoker(
    //        IExceptionFilter filter)
    //    {
    //        if (filter == null)
    //        {
    //            throw new ArgumentNullException("filter");
    //        }

    //        this.filter = filter;
    //    }

    //    protected override FilterInfo GetFilters(
    //        ControllerContext controllerContext,
    //        ActionDescriptor actionDescriptor)
    //    {
    //        var filterInfo =
    //            base.GetFilters(controllerContext,
    //            actionDescriptor);
    //        filterInfo.ExceptionFilters.Add(this.filter);
    //        return filterInfo;
    //    }
    //}


    //public class ErrorHandlingControllerFactory :
    //DefaultControllerFactory
    //{
    //    public override IController CreateController(
    //        RequestContext requestContext,
    //        string controllerName)
    //    {
    //        var controller =
    //            base.CreateController(requestContext,
    //            controllerName);

    //        var c = controller as Controller;
    //        if (c != null)
    //        {
    //            c.ActionInvoker =
    //                new ErrorHandlingActionInvoker(
    //                    new HandleErrorAttribute());
    //        }

    //        return controller;
    //    }
    //}



}

