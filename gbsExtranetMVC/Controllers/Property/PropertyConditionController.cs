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


namespace gbsExtranetMVC.Controllers.Property
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class PropertyConditionController : Controller
    {
        BizContext BizContext = new BizContext();
        public const string ActiveMenu = "Property";
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        DataTable AttributeHeader = new DataTable();
        DataTable HotelAttribute = new DataTable();
        DataTable Attributes = new DataTable();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PropertyCondition()
        {
            Session["PageName"] = "PropertyCondition";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            PropertyConditionsRepository modelRepo = new PropertyConditionsRepository();
            int HotelID = BizContext.HotelID;
            var currencyID = modelRepo.GetcurrencyID(HotelID);
            ViewBag.HotelcurrencyID = currencyID;

            return View();        
         
        }
       
        public JsonResult DisplayPropertyconditions()
        {
            AssignBizContext();
            int HotelID = BizContext.HotelID;

            DataTable AttributeHeader = new DataTable();
            DataTable HotelAttribute = new DataTable();
            DataTable Attributes = new DataTable();
            List<PropertyConditionsExt> PropertyConditions = new List<PropertyConditionsExt>();
            PropertyConditionsRepository modelRepo = new PropertyConditionsRepository();
            try { 
            AttributeHeader = modelRepo.GetAttributeHeaders();
            Attributes = modelRepo.GetAttributes();
            HotelAttribute = modelRepo.HotelPropertyConditions(HotelID);
           
            foreach (DataRow AttributeHeaders in AttributeHeader.Rows)
            {
                foreach (DataRow CommonAttributes in Attributes.Rows)
                {

                    if (Convert.ToInt32(CommonAttributes["AttributeHeaderID"]) == Convert.ToInt32(AttributeHeaders["ID"]))
                    {
                        PropertyConditionsExt Obj = new PropertyConditionsExt();
                        Obj.AttributeId = Convert.ToInt32(CommonAttributes["ID"].ToString());
                        Obj.AttributeName = CommonAttributes["Name"].ToString();
                        Obj.AttributeHeaderId = CommonAttributes["AttributeHeaderID"].ToString();
                        Obj.AttributeHeaderName = AttributeHeaders["Name"].ToString();
                        Obj.AttributeHeaderCode = AttributeHeaders["Code"].ToString();
                        Obj.Chargeable = Convert.ToBoolean(CommonAttributes["Chargeable"]);
                        int AttributeId = Convert.ToInt32(CommonAttributes["ID"].ToString());
                        int AttributeHeaderID = Convert.ToInt32(CommonAttributes["AttributeHeaderID"].ToString());
                        DataRow[] hotelRoomAttribute = HotelAttribute.Select("AttributeID=" + AttributeId);
                        Obj.hasAttribute = (hotelRoomAttribute.Length > 0);

                        if (Obj.hasAttribute == true)
                        {
                            DataRow HotelAttributes = HotelAttribute.Select("AttributeID=" + AttributeId + " AND AttributeHeaderID=" + AttributeHeaderID)[0];
                            Obj.UnitValue = HotelAttributes["UnitValue"].ToString();
                            Obj.AttributeTypeID = Convert.ToInt32(HotelAttributes["AttributeTypeID"]);
                            Obj.charge = Convert.ToBoolean(HotelAttributes["Charged"]);
                            bool charge1 = Convert.ToBoolean(HotelAttributes["Charged"]);
                            Obj.checkedradio = "checked";
                            if (charge1 == true)
                            {
                               Obj.UnitID = HotelAttributes["UnitID"].ToString();
                               Obj.HotelUnitID = HotelAttributes["HotelUnitID"].ToString();
                               Obj.HotelUnitName = HotelAttributes["HotelUnitName"].ToString();
                               string charge = HotelAttributes["Charge"].ToString();
                               if (charge != "")
                               {
                                   Obj.Chargedvalue = HotelAttributes["Charge"].ToString();
                               }
                               else
                               {
                                   Obj.Chargedvalue = "0.00";
                               }
                           

                            Obj.CurrencyID = HotelAttributes["CurrencyID"].ToString();
                            Obj.Currency = HotelAttributes["CurrencySymbolWithCode"].ToString();
                            }
                           


                        }

                        //
                        PropertyConditions.Add(Obj);
                    }

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
            ViewBag.PropertyConditions = PropertyConditions;
            return Json(PropertyConditions, JsonRequestBehavior.AllowGet);
        }

        public void Getunitcurrencydropdown(PropertyConditionsExt HotelPropertyConditions)
        {

            ViewBag.propertyunit = DropDownLists.Getunitdropdown(HotelPropertyConditions.UnitID);

        }

        public void Getcurrencydropdown(PropertyConditionsExt HotelPropertyConditions)
        {
            ViewBag.propertycurrency = DropDownLists.Getcurrencydropdown(HotelPropertyConditions.CurrencyID);
        }

        public JsonResult SavePropertyConditions(List<PropertyConditionSaveModel> Conditions)
        {
            PropertyConditionsRepository objText = new PropertyConditionsRepository();
            int i = 0;
            try
            {
                AssignBizContext();
                int HotelID = BizContext.HotelID;               
                bool status = objText.DeleteHotelAttributes(Convert.ToString(HotelID), "2");
                foreach (var obj in Conditions)
                {                   
                    //bool status = objText.DeleteHotelAttributes(Convert.ToString(HotelID), attribute[j]);
                    i = objText.InsertPropertyConditions(HotelID,obj.AttributeID,obj.UnitID,obj.CurrencyID,obj.Price,obj.Charged,obj.Unitvalue, this);
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

        public JsonResult InsertPropertyConditions(string AttributeIDs, string UnitIDs, string CurrencyIDs, string Price, string Charged, string Unitvalue)
        {
            PropertyConditionsRepository objText = new PropertyConditionsRepository();
            int i = 0;
            try {
            AssignBizContext();
            int HotelID = BizContext.HotelID;
            string[] attribute = AttributeIDs.Split(',');
            string[] UnitID = UnitIDs.Split(',');
            string[] CurrencyID = CurrencyIDs.Split(',');
            string[] Charge = Charged.Split(',');
            string[] Prices = Price.Split(',');
            string[] Unitvalues = Unitvalue.Split(',');
            bool status = objText.DeleteHotelAttributes(Convert.ToString(HotelID), "2");
            for (int j = 0; j < attribute.Length; j++)
            {
                if (attribute[j] != null && attribute[j] != "")
                {
                    //bool status = objText.DeleteHotelAttributes(Convert.ToString(HotelID), attribute[j]);
                    i = objText.InsertPropertyConditions(HotelID, attribute[j], UnitID[j], CurrencyID[j], Prices[j], Charge[j], Unitvalues[j],this);
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
