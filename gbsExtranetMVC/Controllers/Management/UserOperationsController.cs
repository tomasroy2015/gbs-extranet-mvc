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

namespace gbsExtranetMVC.Controllers.Management
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class UserOperationsController : Controller
    {
        public const string ActiveMenu = "Management";
        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        public ActionResult UserOperations()
        {
            return View();
        }

        #region Read
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            UserOperationsRepository modelRepo = new UserOperationsRepository();
            DataSourceResult result = modelRepo.GetUserOperations().ToDataSourceResult(request);
            return Json(result);
        }
        #endregion

        public ActionResult _Destroy([DataSourceRequest]DataSourceRequest request, UsersOperationsExt model)
        {
            string Msg = "";
            try
            {
                UserOperationsRepository modelRepo = new UserOperationsRepository();
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


        public JsonResult UpdateUserOperations(string UserCode, string SalutionId, string Name, string SurName, string Country, string Region, string Address, string PostalCode,
           string Email, string Phone, string Firm, string UserName, string Status, string Active, string IPAddress, string Password, string Locked, string hdnRegionId)
        {
            UserOperationsRepository objupdate = new UserOperationsRepository();
            int i;
            try {

            i = objupdate.UpdateUserOperations(UserCode, SalutionId, Name, SurName, Country, Region, Address, PostalCode, Email, Phone, Firm, UserName, Status, Active, IPAddress, Password, Locked, hdnRegionId,this);
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

        
        public JsonResult InsertAuthurization(string Selectedsecurity, string Selectedhotels, string Selectedbusinesspartner)
        {

            string UserID = Convert.ToString(Session["UserID"]);
            UserOperationsRepository objupdate = new UserOperationsRepository();
            int i;
            try { 
            i = objupdate.AuthurizationUserOperations(Selectedsecurity, Selectedhotels, Selectedbusinesspartner, UserID);
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

        public JsonResult DeleteUserFunc(int UserID)
        {
            int i = 1;
            UserOperationsRepository obj = new UserOperationsRepository();
            try { 
            i = obj.DeleteUser(UserID);
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
          [HttpGet]
        public ActionResult UserOperationsEdit(string UserID)
        {
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
          
            UserOperationsRepository modelRepo = new UserOperationsRepository();
            UserOperationsRepository.Encryption64 ob = new UserOperationsRepository.Encryption64();
           // ViewBag.Countries = DropDownLists.GetCountries(1);
            var Countries = new SelectList(DropDownLists.GetCountries(1), "Value", "Text").OrderBy(i => i.Text);
            ViewBag.Countries = new SelectList(Countries, "Value", "Text");

            ViewBag.status = DropDownLists.GetStatus(null);
            ViewBag.SalutionType = DropDownLists.GetTitle(null);
            //ViewBag.Firm = DropDownLists.GetFirms("100001");
            ViewBag.Firm = DropDownLists.GetFirms(null);
          
            if (UserID != "")
            {
                long ID = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(UserID)), "58421043"));
                var UserOperations = modelRepo.GetUserOperations().FirstOrDefault(f => f.ID == ID);
                return View(UserOperations);
                // BindViewBagsUsers(UserOperations);
            }
            //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View();
            //}
        }


        public string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }


        public ActionResult UserRole(string id, string FirmID)
        {
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
          
            UserOperationsRepository.Encryption64 ob = new UserOperationsRepository.Encryption64();

            string Decryptedoperations = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(FirmID)), "58421043");


           // string Decryptedoperations = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode("71536E455441584D3568633D")), "58421043");

            long ID = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(id)), "58421043"));
            Session["UserID"] = ID;
            UserOperationsRepository modelRepo = new UserOperationsRepository();

            
            

            ViewBag.securityGroups = UserOperationsRepository.GetSecurityGroup();


          //  ViewBag.BusinessPartners = model.GetBusinessPartner().FirstOrDefault(f => f.FirmID == Decryptedoperations);
            ViewBag.BusinessPartners = UserOperationsRepository.GetBusinessPartnersList(Decryptedoperations);
           // BaseRepository baseRepo = new BaseRepository();

           // List<UsersOperationsExt> GetBusinessPartners = new List<UsersOperationsExt>();

           //var  GetBusinessPartners1 = BizBusinessPartner.GetBusinessPartners(baseRepo.BizDB, null, FirmID);
           //ViewBag.BusinessPartners = GetBusinessPartners1;

            ViewBag.Hotels = UserOperationsRepository.GetHotelsList(Decryptedoperations);
            ViewBag.UserRights = UserOperationsRepository.GetUserRights(ID);
            ViewBag.UserHotels = UserOperationsRepository.GetUserHotels(ID);
            ViewBag.UserBusinessPartners = UserOperationsRepository.GetUserBusinessPartners(ID);
            ViewBag.Username = modelRepo.GetUsername(ID);
            //ViewBag.SecurityGroup = modelRepo.GetSecurityGroup1(ID);
           // SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
            return View();

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
