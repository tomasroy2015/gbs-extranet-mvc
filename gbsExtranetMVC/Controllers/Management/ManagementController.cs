using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Enumerations;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using System.Globalization;
using System.Net;

namespace gbsExtranetMVC.Controllers.Management
{
     [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
     [RequiresSSL]

    public class ManagementController : Controller
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
        //
        // GET: /Management/
        public const string ActiveMenu = "Management";

        public ActionResult PropertyOperations()
        {
            Session["PageName"] = "PropertyOperations";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
           // SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
            return View();
        }
        public ActionResult UserOperations()
        {
            Session["PageName"] = "UserOperations";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
           // SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View();
        }
        public ActionResult Reviews()
        {
            Session["PageName"] = "Reviews";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Communications", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
          //  SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View();
        }
        public ActionResult FirmRequests()
        {
            Session["PageName"] = "FirmRequests";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Communications", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
           // SecurityUtils.SetGlobalViewbags(this, "Communications");

            return View();
        }
        public ActionResult FirmOperations()
        {
            Session["PageName"] = "FirmOperations";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
           // SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View();
        }

        public ActionResult UserMessages()
        {
            Session["PageName"] = "UserMessages";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, "Communications", BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            //SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View();
        }

        [HttpGet]
        //public ActionResult UserOperationsEdit(long id)
        //{
        //    UserOperationsRepository modelRepo = new UserOperationsRepository();
        //    var UserOperations = modelRepo.GetUserOperations().FirstOrDefault(f => f.ID == id);
        //    BindViewBagsUsers(UserOperations);

        //    SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

        //    return View(UserOperations);
        //    //}
        //}
        public void BindViewBagsUsers(UsersOperationsExt UserOperations)
        {
            ViewBag.Countries = DropDownLists.GetCountries(Convert.ToInt32(UserOperations.CountryID));
            ViewBag.status = DropDownLists.GetStatus(Convert.ToInt32(UserOperations.StatusID));
            ViewBag.SalutionType = DropDownLists.GetTitle(Convert.ToInt32(UserOperations.SalutionTypeID));
            ViewBag.Firm = DropDownLists.GetFirms(Convert.ToString(UserOperations.FirmID));
        }

        public ActionResult UserOperationsEdit(string UserID)
        {
            UserOperationsRepository modelRepo = new UserOperationsRepository();
            UserOperationsRepository.Encryption64 ob = new UserOperationsRepository.Encryption64();
            ViewBag.Countries = DropDownLists.GetCountries(1);
            ViewBag.status = DropDownLists.GetStatus(0);
            ViewBag.SalutionType = DropDownLists.GetTitle(1);
            ViewBag.Firm = DropDownLists.GetFirms("100001");
            try
            {
                if (UserID != "")
                {
                    long ID = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(UserID)), "58421043"));
                    var UserOperations = modelRepo.GetUserOperations().FirstOrDefault(f => f.ID == ID);
                    return View(UserOperations);
                    // BindViewBagsUsers(UserOperations);
                }
                SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
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
            UserOperationsRepository.Encryption64 ob = new UserOperationsRepository.Encryption64();
            string Decryptedoperations = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(FirmID)), "58421043");
            long ID = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(id)), "58421043"));
            Session["UserID"] = ID;
            UserOperationsRepository modelRepo = new UserOperationsRepository();
            ViewBag.securityGroups = UserOperationsRepository.GetSecurityGroup();
            ViewBag.BusinessPartners = UserOperationsRepository.GetBusinessPartners();
            ViewBag.Hotels = UserOperationsRepository.GetHotels();
            ViewBag.UserRights = UserOperationsRepository.GetUserRights(ID);
            ViewBag.UserHotels = UserOperationsRepository.GetUserHotels(ID);
            ViewBag.UserBusinessPartners = UserOperationsRepository.GetUserBusinessPartners(ID);
            ViewBag.Username = modelRepo.GetUsername(ID);
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu);
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
