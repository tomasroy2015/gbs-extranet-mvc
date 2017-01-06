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
using System.Linq;
using Business;
using System.Globalization;
using System.Net;


namespace gbsExtranetMVC.Controllers.Settings
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class PropertyComissionController : Controller
    {
        //
        // GET: /PropertyComission/
        BizContext BizContext = new BizContext();
        public const string ActiveMenu = "Setting";
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

        public ActionResult PropertyComission()
        {
            
            Session["PageName"] = "ProperttyComission";
            //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
            try
            {
                AssignBizContext();
                SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            }
            catch(Exception ex)
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
            
            return View();
        }

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = null;
            try
            {
                AssignBizContext();
                int id = Convert.ToInt32(BizContext.HotelID);
                PropertyComissionRepository modelRepo = new PropertyComissionRepository();
                result = modelRepo.GetComission(id).ToDataSourceResult(request);
            }
            catch(Exception ex)
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
            return Json(result);
        }

        public JsonResult SaveComission(string StartDate, string EndDate, string Comission)
        {
            PropertyComissionRepository objupdate = new PropertyComissionRepository();
            int i = 0;
            try
            {
                AssignBizContext();
               
                int id = Convert.ToInt32(BizContext.HotelID);
                DateTime StartDat = DateTime.ParseExact(StartDate, @"d/M/yyyy", null);
                DateTime EndDat = DateTime.ParseExact(EndDate, @"d/M/yyyy", null);
                DataTable ComissionTable = objupdate.GetComissionTable(id);
                DataRow[] sameIntervalComission = ComissionTable.Select(string.Format("StartDate ='{0}' and EndDate='{1}'", StartDat, EndDat));

                if (sameIntervalComission.Length > 0)
                {
                    i = objupdate.UpdateComission(Convert.ToInt16(sameIntervalComission[0]["ID"]), Comission, this);
                }
                else
                {
                    string comissionIDsToBeDeleted = "";
                    DataRow[] previousComission = ComissionTable.Select(string.Format("StartDate <'{0}' and EndDate>='{1}'", StartDat, EndDat));
                    if (previousComission.Length > 0)
                    {
                        i = objupdate.SaveComission(id, Convert.ToDateTime((previousComission[0]["StartDate"])), StartDat.AddDays(-1), Convert.ToString(previousComission[0]["Comission"]), this);
                        if (comissionIDsToBeDeleted == "")
                        {
                            comissionIDsToBeDeleted = Convert.ToString(previousComission[0]["ID"]);
                        }
                        else
                        {
                            comissionIDsToBeDeleted = comissionIDsToBeDeleted + "," + Convert.ToString(previousComission[0]["ID"]);
                        }

                    }

                    DataRow[] nextComission = ComissionTable.Select(string.Format("StartDate <='{0}' and EndDate>'{1}'", StartDat, EndDat));
                    if (nextComission.Length > 0)
                    {
                        i = objupdate.SaveComission(id, StartDat.AddDays(1), Convert.ToDateTime(nextComission[0]["EndDate"]), Convert.ToString(nextComission[0]["Comission"]), this);
                        if (comissionIDsToBeDeleted == "")
                        {
                            comissionIDsToBeDeleted = Convert.ToString(nextComission[0]["ID"]);
                        }
                        else
                        {
                            comissionIDsToBeDeleted = comissionIDsToBeDeleted + "," + Convert.ToString(nextComission[0]["ID"]);
                        }
                    }

                    DataRow[] intervalComissions = ComissionTable.Select(string.Format("StartDate >='{0}' and EndDate<='{1}'", StartDat, EndDat));
                    if (intervalComissions.Length > 0)
                    {
                        foreach (DataRow DRIC in intervalComissions)
                        {
                            if (comissionIDsToBeDeleted == "")
                            {
                                comissionIDsToBeDeleted = Convert.ToString(DRIC["ID"]);
                            }
                            else
                            {
                                comissionIDsToBeDeleted = comissionIDsToBeDeleted + "," + Convert.ToString(DRIC["ID"]);
                            }
                        }
                    }
                    i = objupdate.SaveComission(id, StartDat, EndDat, Comission, this);
                    if (comissionIDsToBeDeleted != "")
                    {
                        objupdate.DeleteComission(comissionIDsToBeDeleted);
                    }
                }

            }
            catch(Exception ex)
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
