using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using gbsExtranetMVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gbsExtranetMVC.Models.Repositories;
using System.Data;
using gbsExtranetMVC.Models.Enumerations;
using Business;
using System.Net;

namespace gbsExtranetMVC.Controllers
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class EMailsController : Controller
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
        public const string ActiveMenu = "Communications";
        public ActionResult Index()
        {
            return View();
        }

        #region Grid Create, Read, Update, Delete Functions

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
          

            EmailRepository modelRepo = new EmailRepository();

            DataSourceResult result = modelRepo.ReadAll(this).ToDataSourceResult(request);
            return Json(result);

        }


        public ActionResult _Create([DataSourceRequest]DataSourceRequest request, EmailExt model)
        {

            string Msg = "";
            try
            {
                EmailRepository modelRepo = new EmailRepository();
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


            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, EmailExt model)
        {
            string Msg = "";
            try
            {
                EmailRepository modelRepo = new EmailRepository();
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

        public ActionResult _Update([DataSourceRequest]DataSourceRequest request, EmailExt model)
        {
           
                string Msg = "";
                try
                {
                    EmailRepository modelRepo = new EmailRepository();
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
            
            return Json(request);
        }

        #endregion

        [HttpGet]
        public ActionResult Create()
        {
            EmailExt model = new EmailExt();
            ViewBag.EmailTemplates = DropDownLists.GetListEmailTemplates(model.MailTemplateID);
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EmailExt model)
        {
            string Msg = "";
            EmailRepository modelRepo = new EmailRepository();
            if (modelRepo.Create(model, ref Msg, this))
            {
                return RedirectToAction("EMails", "Communications");
            }
            else
            {
                return View(modelRepo.GetEmailByID(model.ID));
            }

        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            EmailRepository modelRepo = new EmailRepository();
            EmailExt model = modelRepo.GetEmailByID(id);

            ViewBag.EmailTemplates = DropDownLists.GetListEmailTemplates(model.MailTemplateID);
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmailExt model)
        {
            string Msg = "";
            EmailRepository modelRepo = new EmailRepository();
            if (modelRepo.Update(model, ref Msg, this))
           {
               return RedirectToAction("EMails", "Communications");
           }
           else
           {
               return View(modelRepo.GetEmailByID(model.ID));
           }

        }

        [HttpPost, ValidateInput(false)]
        public JsonResult InsertEmail(string Template, string MailFrom, string MailTo, string ResentCount, string SendingDate, string Subject, string Content, string MailCC, string SentStatus, string Record)
        {
            EmailRepository objinsert = new EmailRepository();
            string i = "";
            try
            {
                i = objinsert.InsertEmail(Template, MailFrom, MailTo, ResentCount, SendingDate, Subject, Content, MailCC, SentStatus, Record, this);
            }
            catch (Exception ex)
            {
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }
            return Json(i, JsonRequestBehavior.AllowGet);

        }

        [HttpPost, ValidateInput(false)]
        public JsonResult UpdateEmail(string ID,string Template, string MailFrom, string MailTo, string ResentCount, string SendingDate, string Subject, string Content, string MailCC, string SentStatus, string Record)
        {
            EmailRepository objinsert = new EmailRepository();
            string i = "";
            try
            {
                i = objinsert.UpdateEmail(ID, Template, MailFrom, MailTo, ResentCount, SendingDate, Subject, Content, MailCC, SentStatus, Record, this);
            }
            catch (Exception ex)
            {
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }
            return Json(i, JsonRequestBehavior.AllowGet);

        }

        public JsonResult _ReadPart()
        {


            EmailRepository modelRepo = new EmailRepository();

            // DataSourceResult result = ListOfModel.ToDataSourceResult(request);

            return Json(modelRepo.GetTemplate(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteEMail(int ID)
        {
            int i = 1;
            EmailRepository obj = new EmailRepository();
            try
            {
                i = obj.DeleteEMail(ID);
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
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult _ReadPart([DataSourceRequest]DataSourceRequest request)
        //{


        //    EmailRepository modelRepo = new EmailRepository();

        //    DataSourceResult result = modelRepo.GetTemplate(this).ToDataSourceResult(request);
        //    return Json(result);

        //}
        //public JsonResult _ReadPart()
        //{

        //    DataTable Table = new DataTable();
        //    List<EmailExt> ListOfModel = new List<EmailExt>();
        //    EmailRepository obj = new EmailRepository();
        //    string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        //    Table = obj.GetTemplate(CultureCode);
            

        //    // DataSourceResult result = ListOfModel.ToDataSourceResult(request);

        //    return Json(ListOfModel, JsonRequestBehavior.AllowGet);
        //}


    }
}