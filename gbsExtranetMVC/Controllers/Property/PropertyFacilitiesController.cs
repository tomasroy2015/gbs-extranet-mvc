using Business;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Enumerations;
using gbsExtranetMVC.Models.Repositories;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Controllers.Property
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class PropertyFacilitiesController : Controller
    {
        //
        // GET: /PropertyFacilities/

        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        public const string ActiveMenu = "Property";
        #region old code
        //public ActionResult PropertyFacilities()
        //{
        //    Session["PageName"] = "PropertyFacilities";
        //    AssignBizContext();
        //    int id = BizContext.HotelID;
        //    GetPropertyFacilitiesHeader();
        //    //for( int i = 1 ; i <= 3 ; i++)
        //    //{
        //    //      GetPropertyFacility(i);
        //    //}
        //    for (int i = 1; i <= 3; i++)
        //    {
        //        GetHotelAttributes(i, id);
        //    }
        //   // int AttributeHeaderID = 1;
        //   // SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
            
        //    SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
        //    return View();
        //   //eturn View();
        //}
        #endregion

        #region new code
        public ActionResult PropertyFacilities()
        {
            Session["PageName"] = "PropertyFacilities";
            AssignBizContext();
            int id = BizContext.HotelID;
            GetPropertyFacilitiesHeader(id);
            //for( int i = 1 ; i <= 3 ; i++)
            //{
            //      GetPropertyFacility(i);
            //}
            //for (int i = 1; i <= 3; i++)
            //{
            //    GetHotelAttributes(i, id);
            //}
            // int AttributeHeaderID = 1;
            // SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View();
            //eturn View();
        }
        #endregion
        #region old code
        //public JsonResult GetPropertyFacilitiesHeader()
        //{
        //    PropertyFacilitiesRepository modelRepo = new PropertyFacilitiesRepository();
        //    var PropertyConditionsheader = modelRepo.GetPropertyFacilitiesHeader();
        //    ViewBag.PropertyConditionsheader = PropertyConditionsheader;
        //    return Json(PropertyConditionsheader);

        //}
        #endregion

        #region new code 10-April-2016
        public JsonResult GetPropertyFacilitiesHeader(int hotelID)
        {
            PropertyFacilitiesRepository modelRepo = new PropertyFacilitiesRepository();
            var PropertyConditionsheader = modelRepo.GetPropertyFacilitiesHeader(hotelID);
            if (PropertyConditionsheader != null && PropertyConditionsheader.Count > 0)
            {
                foreach (var header in PropertyConditionsheader)
                {
                    header.PropertyFacilitiesItems = GetHotelAttributes(header.ID, hotelID);
                }
            }
            ViewBag.PropertyConditionsheader = PropertyConditionsheader;
            return Json(PropertyConditionsheader);

        }
        #endregion

        #region old code
        //public JsonResult GetHotelAttributes(int AttributeHeaderID, int HotelID)
        //{
        //    PropertyFacilitiesRepository modelRepo = new PropertyFacilitiesRepository();
        //    List<PropertyFacilitiesExt> Attributes = modelRepo.GetHotelAttributes(AttributeHeaderID, HotelID);
        //    try
        //    {
        //        if (AttributeHeaderID == 1)
        //        {
        //            ViewBag.HotelAttributes1 = Attributes;
        //        }
        //        else if (AttributeHeaderID == 2)
        //        {
        //            ViewBag.HotelAttributes2 = Attributes;
        //        }
        //        else if (AttributeHeaderID == 3)
        //        {
        //            ViewBag.HotelAttributes3 = Attributes;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string hostName1 = Dns.GetHostName();
        //        string GetUserIPAddress = Dns.GetHostByName(hostName1).AddressList[0].ToString();
        //        string PageName = Convert.ToString(Session["PageName"]);
        //        //string GetUserIPAddress = GetUserIPAddress1();
        //        using (BaseRepository baseRepo = new BaseRepository())
        //        {
        //            //BizContext BizContext1 = new BizContext();
        //            BizApplication.AddError(baseRepo.BizDB, PageName, ex.Message, ex.StackTrace, DateTime.Now, GetUserIPAddress);
        //        }
        //        Session["PageName"] = "";
        //        string error = ErrorHandling.HandleException(ex);
        //        return this.Json(new DataSourceResult { Errors = error });
        //    }
        //    return Json(Attributes);
        //}
        #endregion

        #region new code by tomas  10-April-2016
        public List<PropertyFacilitiesExt> GetHotelAttributes(int AttributeHeaderID, int HotelID)
        {
            PropertyFacilitiesRepository modelRepo = new PropertyFacilitiesRepository();
            List<PropertyFacilitiesExt> Attributes = null;
            try
            {
                Attributes = modelRepo.GetHotelAttributes(AttributeHeaderID, HotelID);
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
                // return this.Json(new DataSourceResult { Errors = error });
            }
            return Attributes;
        }
        #endregion
        public JsonResult SavePropertyFacility(string HotelAttributes, string Charged)
        {
            PropertyFacilitiesRepository objText = new PropertyFacilitiesRepository();
            AssignBizContext();
            int id = BizContext.HotelID;
            // int i = objText.SavePropertyFacility(HotelID, HotelAttributes, HotelAttributes1, HotelAttributes2, Charged);
            int i = 0;
            try 
            { 
                string[] AttributeID = HotelAttributes.Split(',');

                //string[] AttributeID1 = HotelAttributes1.Split(',');
                //string[] AttributeID2 = HotelAttributes2.Split(',');
                string[] Charge = Charged.Split(',');
                //string[] combined = AttributeID.Concat(AttributeID1).Concat(AttributeID2).ToArray();
                bool status = objText.DeleteHotelAttributes(Convert.ToString(id), "1");
                for (int j = 0; j < AttributeID.Length; j++)
                {
                    if (AttributeID[j] != null && AttributeID[j] != "")
                    {
                        i = objText.SavePropertyFacility(id, AttributeID[j], Convert.ToInt32(Charge[j]), this);
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
