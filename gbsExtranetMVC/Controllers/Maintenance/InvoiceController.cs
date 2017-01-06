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


namespace gbsExtranetMVC.Controllers.Maintenance
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class InvoiceController : Controller
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
        public const string ActiveMenu = "Management";

        [HttpGet]
        public ActionResult View(long id)
        {
            //using (DBEntities db = new DBEntities())
            //{
            InvoiceRepository modelRepo = new InvoiceRepository();
            var Invoice = modelRepo.GetInvoices(id).FirstOrDefault(f => f.ID == id);

            AssignBizContext();


            //bool IsAdmin = BizContext.UserContext.IsAdmin();

            //if(IsAdmin==true)
            //{
            //    string name = "vdwfg";
            //}
            //else if (BizContext.UserContext.IsHotelAdmin()==true)
            //{
            //    string name = "";
            //}
            //else
            //{
            //    string name = "";
            //}

            //string IsAdmin1 = Convert.ToString(BizContext.UserContext.IsAdmin());

            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
          //  SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View(Invoice);
            //}
        }
        public JsonResult Update(long id)
        {
            InvoiceRepository modelRepo = new InvoiceRepository();
            int i;
            try 
            { 
            i = modelRepo.UpdateInvoice(id, this);
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
       
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {

            AssignBizContext();
            InvoiceRepository modelRepo = new InvoiceRepository();

          //  long FirmIDValue = Convert.ToInt64(BizContext.UserContext.FirmID);
            //ViewBag.IsAdmin = BizContext.UserContext.IsAdmin();
            //DataSourceResult result = modelRepo.GetInvoice(BizContext.UserContext.IsAdmin()).ToDataSourceResult(request);
            DataSourceResult result = new DataSourceResult();
            if (BizContext.UserContext.IsAdmin() == true)
            {
                result = modelRepo.GetInvoice(BizContext.UserContext.IsAdmin()).ToDataSourceResult(request);
            }
            else
            {
                result = modelRepo.GetInvoice(BizContext.UserContext.IsAdmin()).Where(o => o.FirmID == BizContext.UserContext.FirmID).ToDataSourceResult(request);
            }
            
            return Json(result);
        }

        public JsonResult FirmDropDown()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            //return Json(model.ReadFirm().OrderBy(o => o.Name).Select(c => new { FirmID = c.ID, Firm = c.Name }).OrderBy(o => o.Firm), JsonRequestBehavior.AllowGet);
            AssignBizContext();

            long FirmIDValue = Convert.ToInt64(BizContext.UserContext.FirmID);

            if (BizContext.UserContext.IsAdmin() == true)
            {
                return Json(model.ReadFirm().OrderBy(o => o.Name).Select(c => new { FirmID = c.ID, Firm = c.Name })
                .OrderBy(o => o.Firm)
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                //return Json(model.ReadFirm().OrderBy(o => o.Name).Select(c => new { FirmID = c.ID, Firm = c.Name })
                // .Where(o => o.FirmID == FirmIDValue).FirstOrDefault()
                // , JsonRequestBehavior.AllowGet);
                return Json(model.ReadFirm().Where(o => o.ID == FirmIDValue)
                    .Select(c => new { FirmID = c.ID, Firm = c.Name })                
                , JsonRequestBehavior.AllowGet);
            }

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
        }

       
    }
}