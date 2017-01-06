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
using System.Globalization;
using System.Net;

namespace gbsExtranetMVC.Controllers
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class AuthorizationController : Controller
    {
        //
        // GET: /Authorization/
        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSecurityGroup()
        {
            AuthenticationRepository Securityobject = new AuthenticationRepository();
            List<AuthenticationExt> list = new List<AuthenticationExt>();
            DataTable dt = new DataTable();

            try { 
            string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            dt = Securityobject.GetSecurityGroup(CultureCode);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AuthenticationExt obj = new AuthenticationExt();
                    obj.ID = Convert.ToInt32(dr["ID"]);
                    obj.Info = dr["Info"].ToString();
                    list.Add(obj);
                }
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
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SecurityGroupRights(int SecurityGroupID)
        {
            AuthenticationRepository Securityobject = new AuthenticationRepository();
            List<AuthenticationExt> list = new List<AuthenticationExt>();
            DataTable dt = new DataTable();
            try
            {
            string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            dt = Securityobject.GetSecurityGroupRights(CultureCode, SecurityGroupID);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AuthenticationExt obj = new AuthenticationExt();
                    obj.ID = Convert.ToInt32(dr["ID"]);
                    obj.Code = dr["Code"].ToString();
                    obj.Description = dr["Description"].ToString();
                    obj.HasRight = dr["HasRight"].ToString();
                    string HasValue = dr["HasRight"].ToString();
                    if (HasValue == "1")
                    {
                        obj.HasValue = true;
                    }
                    else
                    {
                        obj.HasValue = false;
                    }
                   
                    list.Add(obj);
                }
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
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateSecutiryID(int SecurityGroupID, string SecutiryIDs)
        {
            AuthenticationRepository Securityobject = new AuthenticationRepository();
            bool Update = false;
            try
            {
                bool Delete = Securityobject.Delete(SecurityGroupID);
                Update = Securityobject.Update(SecurityGroupID, SecutiryIDs, this);
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

            return Json(Update, JsonRequestBehavior.AllowGet);
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

            if (SelectedLanguage != "en-Gb")
            {
                try
                {
                    CultureInfo TempCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                    TempCulture.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    TempCulture.NumberFormat.NumberDecimalSeparator = ".";
                    System.Threading.Thread.CurrentThread.CurrentCulture = TempCulture;
                    CultureInfo TempCulture1 = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentUICulture.Clone();
                    TempCulture1.DateTimeFormat.Calendar = System.Globalization.CultureInfo.GetCultureInfo("en-Gb").DateTimeFormat.Calendar;
                    TempCulture1.NumberFormat.NumberDecimalSeparator = ".";
                    System.Threading.Thread.CurrentThread.CurrentUICulture = TempCulture1;
                }
                catch
                {

                }

                //base.Initialize(requestContext);
            }
        }

    }
}
