using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Models.Repositories;
using gbsExtranetMVC.Models.Enumerations;
using Kendo.Mvc.Extensions;
using gbsExtranetMVC.Helpers;
using Business;
using System.Net;

namespace gbsExtranetMVC.Controllers.Maintenance
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class RegionController : Controller
    {
        //
        // GET: /Region/       

        #region Read,Update,Delete
      

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {        
            DataTable RegionTbl = new DataTable();       
            RegionRepository obj = new RegionRepository();
            DataSourceResult result = obj.GetAllRegions().ToDataSourceResult(request);
            return Json(result);          
        }

        public ActionResult _Create([DataSourceRequest]DataSourceRequest request, RegionExt model)
        {
            string Msg = "";
            try
            {
                RegionRepository modelRepo = new RegionRepository();
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

            return Json(request);  
        }
        public ActionResult _Update([DataSourceRequest]DataSourceRequest request, RegionExt model)
        {

            string Msg = "";
            try
            {
                RegionRepository modelRepo = new RegionRepository();
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
        public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, RegionExt model)
        {
            string Msg = "";
            try
            {
                RegionRepository modelRepo = new RegionRepository();
                if (modelRepo.Delete(model, ref Msg, this) == false)
                {
                    return this.Json(new DataSourceResult { Errors = Msg });
                }
            }
            catch (Exception ex)
            {
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }


            return Json(request);
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
        }
    }
}
