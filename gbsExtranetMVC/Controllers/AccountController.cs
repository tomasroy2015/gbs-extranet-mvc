using Business;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Models.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Controllers
{
    public class RequiresSSL : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase req = filterContext.HttpContext.Request;
            HttpResponseBase res = filterContext.HttpContext.Response;

            string[] AcceptType = req.AcceptTypes;
            string TypeValue = "";
            if (AcceptType.Length > 1)
            {
                TypeValue = AcceptType[0];
            }
            if (TypeValue == "text/html")
            {
                // bool IsRequireSSL = filterContext.ActionDescriptor.GetCustomAttributes(typeof(RequiresSSL), true).Length > 0;
                //Check if we're secure or not and if we're on the local box
                //if (!req.IsSecureConnection && !req.IsLocal)
                Uri baseUri = req.Url;

                if (!req.IsSecureConnection && !req.IsLocal)
                {
                    string SslUrl = baseUri.ToString().Replace(baseUri.Scheme, Uri.UriSchemeHttps);
                    res.Redirect(SslUrl);
                }
                //else if (req.IsSecureConnection && !IsRequireSSL && !req.IsLocal)
                //{
                //    string SslUrl = baseUri.ToString().Replace(baseUri.Scheme, Uri.UriSchemeHttp);
                //    res.Redirect(SslUrl);
                //}
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class AccountController : Controller
    {
        private string Smtp_Username = ConfigurationManager.AppSettings["SMTP_Server"].ToString();
        private string Smtp_Password = ConfigurationManager.AppSettings["SMTP_Password"].ToString();
        private string Smtp_Mail = ConfigurationManager.AppSettings["SMTP_Mail"].ToString();
        private int Smtp_PortNo = Convert.ToInt32(ConfigurationManager.AppSettings["SMTP_PortNo"].ToString());
        private string SMTP_TempEmailTo = ConfigurationManager.AppSettings["TempEmailTo"].ToString();
        private string SMTP_SupportEmailTo = ConfigurationManager.AppSettings["SupportEmailTo"].ToString();
        private string SMTP_SenderName = ConfigurationManager.AppSettings["SMTP_SenderName"].ToString();
        

        // private MeadowhallSCPSDBEntities db = new MeadowhallSCPSDBEntities();
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        //public DataContext DataContext = new DataContext();
        //public BizCommon BizCommondvdv = new BizCommon();
        //public BizApplication BizApplicationObj = new BizApplication();
        
        BizContext BizContext = new BizContext();
        public void AssignBizContext()
        {
            if (Session["GBAdminBizContext"] != null)
            {
                BizContext = (BizContext)Session["GBAdminBizContext"];
            }
            Session["GBAdminBizContext"] = BizContext;
        }
        #region Logon
         [RequiresSSL]
        public ActionResult LogOn()
        {
            Home obj = new Home();
           
         
           bool val=true;
            if (Request.QueryString["RemindCode"] != null)
            {
                string RemindCode = BizUtil.DecryptQueryStringParam(Request.QueryString["RemindCode"], ref val, true);


                if (RemindCode != "")
                {
                   
			bool checkDate = false;
            string[] tokens = RemindCode.Split(';');
            if (tokens.Length >= 3)
            {
                    string userID = tokens[0];
                    string userName = tokens[1];
                    Session["RemindUserid"] = userID;
                 
                    double DateDiff1 = (Convert.ToDateTime(tokens[2]) - DateTime.Today.Date).TotalDays;
                    if (!checkDate || DateDiff1 <= 2)
                    {
                        //BizTbl_User userInfo = BizUser.GetUser(DataContext, userID);
                        ViewBag.RemindUsername = userName;
                       
                    }

            } 
			
		}
                ViewBag.AllCulture = obj.GetCulturecode();
                RemoveUserfromSession();

                Response.StatusCode = 200;
                string userIpAddress = GetUserIPAddress();
                //GetCultureByIpaddress(userIpAddress);
                //AssignBizContext();

                AssignBizContext();
                if (BizContext.SystemCultureCode == null && BizContext.CultureCode == null)
                {
                    GetCultureByIpaddress(userIpAddress);
                }


                if (BizContext.SystemCultureCode != null && BizContext.CultureCode != null)
                {
                    ViewBag.SelectedLanguage = BizContext.CultureCode + "," + BizContext.SystemCultureCode;
                }
                else
                {
                    ViewBag.SelectedLanguage = null;
                }
                
	}
            else
            {

                //return View("ResetPassword");
                ViewBag.AllCulture = obj.GetCulturecode();
                RemoveUserfromSession();

                Response.StatusCode = 200;
                string userIpAddress = GetUserIPAddress();
                //GetCultureByIpaddress(userIpAddress);
                AssignBizContext();
                if (BizContext.SystemCultureCode == null && BizContext.CultureCode == null)
                {
                  GetCultureByIpaddress(userIpAddress);
                }

                if (BizContext.SystemCultureCode != null && BizContext.CultureCode != null)
                {
                    ViewBag.SelectedLanguage = BizContext.CultureCode + "," + BizContext.SystemCultureCode;
                }
                else
                {
                    ViewBag.SelectedLanguage = null;
                }

            }


          
          
            //Set Application Header with Application Type + Version and Build Date
            //  Session["ApplicationHeader"] = SecurityUtils.ApplicationHeader;
            return View();
        }

        //public ActionResult LogOn()
        //{
        //  //  Decrypt128New("c2Dyio5u53UMGd+AxA3AZQ==");
        //  //  Decrypt128New("FOPm7+WZDHCnXXnswwLvDAhG9Z9ZsKui76VVMG0P6zk=");

        //  //string wsdd=  Encrypt("gbsuser");

        //  //string wsdd1 = Encrypt("gbsuser@786");

        //  //Decrypt128New(wsdd);
        //  //Decrypt128New(wsdd1);

        //    Business.BizApplication.ApplicationInitialize();
        //    Home obj = new Home();

        //    ViewBag.AllCulture = obj.GetCulturecode();
        //    AssignBizContext();

        //    if (BizContext.SystemCultureCode != null && BizContext.CultureCode!=null)
        //    {
        //        ViewBag.SelectedLanguage = BizContext.CultureCode + "," + BizContext.SystemCultureCode;                                                                        
        //    }
        //    else
        //    {
        //        ViewBag.SelectedLanguage=null;
        //    }
           

        //    RemoveUserfromSession();

        //    Response.StatusCode = 200;
            
        //    //Set Application Header with Application Type + Version and Build Date
        //    //  Session["ApplicationHeader"] = SecurityUtils.ApplicationHeader;
        //    return View();
        //}

        public void RemoveUserfromSession()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
        }

        //
        // POST: /Account/LogOn
        //public string Decrypt128New(string Pass)
        //{

        //    //  string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] cipherBytes = Convert.FromBase64String(Pass);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        encryptor.Key = Encoding.UTF8.GetBytes("2428598755421637");
        //        encryptor.IV = Encoding.UTF8.GetBytes("5369523205842148");

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            Pass = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return Pass;
        //}

        //public string Encrypt(string clearText)
        //{
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        encryptor.Key = Encoding.UTF8.GetBytes("2428598755421637");
        //        encryptor.IV = Encoding.UTF8.GetBytes("5369523205842148");

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return clearText;
        //}
     

        //public ActionResult LogOn(LogOnModel model, string returnUrl, FormCollection formcollection)
        //{

        //    // Decrypt128New("M5z8bP0bD/auyD/1wfthvCCkrr6q+quBRRRcyTGBh5c=");


        //    Home obj = new Home();

        //    ViewBag.AllCulture = obj.GetCulturecode();

        //    RemoveUserfromSession();
        //    string Msg = "";
        //    //Set Application Header with Application Type + Version and Build Date
        //    //  Session["ApplicationHeader"] = SecurityUtils.ApplicationHeader;
        //    if (returnUrl != null)
        //        if (returnUrl.Contains("%2f"))
        //            returnUrl = Server.UrlDecode(returnUrl);
        //    bool locked = false;

        //    //string kjhgkhjk = model.Language;
        //    bool val = true;
           

        //    if (ModelState.IsValid)
        //    {
        //        if (ValidateUser(model.UserName, model.Password, model.Language, ref locked, ref Msg))
        //        {
        //            if (!locked)
        //            {

        //                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Your cannot log on the System, because your status is Locked. Please contact your Department Administrator.");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", Msg);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        public ActionResult updatepassword(string Language, string UserName, string Password)
        {
            string status = "false";
            bool locked = false;
            string Msg = "";

            string ReminduserID = Convert.ToString(Session["RemindUserid"]);
            using (BaseRepository baseRepo = new BaseRepository())
            {
                bool RememberMe = true;
                BizContext BizContext = new BizContext();
                BizUser.UnlockUser(baseRepo.BizDB, "", UserName);
                BizUser.UpdateUserPassword(baseRepo.BizDB, ReminduserID, (new BizCrypto.AES128()).Encrypt(Password));
                if (ValidateUser(UserName, Password, Language, ref locked, ref Msg))
                {
                    if (!locked)
                    {
                        FormsAuthentication.SetAuthCookie(UserName, RememberMe);

                          
                        //return RedirectToAction("Index", "Home");
                    }

                }

            }
            return RedirectToAction("Index", "Home");

            return View(status);
        }


           [HttpPost]

        public ActionResult LogOn(LogOnModel model, string returnUrl, FormCollection formcollection)
        {

            // Decrypt128New("M5z8bP0bD/auyD/1wfthvCCkrr6q+quBRRRcyTGBh5c=");


            Home obj = new Home();

            ViewBag.AllCulture = obj.GetCulturecode();

            RemoveUserfromSession();
            string Msg = "";
            //Set Application Header with Application Type + Version and Build Date
            //  Session["ApplicationHeader"] = SecurityUtils.ApplicationHeader;
            if (returnUrl != null)
                if (returnUrl.Contains("%2f"))
                    returnUrl = Server.UrlDecode(returnUrl);
            bool locked = false;

            //string kjhgkhjk = model.Language;
            bool val = true;
            if (Request.QueryString["RemindCode"] != null)
            {
                string userID = "";
                string userName = "";
                string RemindCode = BizUtil.DecryptQueryStringParam(Request.QueryString["RemindCode"], ref val, true);
                bool checkDate = false;
                string[] tokens = RemindCode.Split(';');
                if (tokens.Length >= 3)
                {
                    userID = tokens[0];
                    userName = tokens[1];

                }

                if (RemindCode != "")
                {
                    using (BaseRepository baseRepo = new BaseRepository())
                    {
                        if (model.Password == model.ConfirmPassword)
                        {
                            BizContext BizContext = new BizContext();
                            if (userName != "" )
                            {
                                BizUser.UnlockUser(baseRepo.BizDB, "", userName);
                            }
                         
                            BizUser.UpdateUserPassword(baseRepo.BizDB, userID, (new BizCrypto.AES128()).Encrypt(model.Password));
                            if (ValidateUser(model.UserName, model.Password, model.Language, ref locked, ref Msg))
                            {
                                if (!locked)
                                {
                                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Your cannot log on the System, because your status is Locked. Please contact your Department Administrator.");
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", Msg);
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password, model.Language, ref locked, ref Msg))
                {
                    if (!locked)
                    {

                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your cannot log on the System, because your status is Locked. Please contact your Department Administrator.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Msg);
                }
            }
            ViewBag.Message = Msg;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //*********The Following Method can be used if Authentication take place from custom users Table***********//

        public Boolean ValidateUser(string Username, string password, string UserLanguage, ref bool locked, ref string Msg)
        {
            Boolean status = false;

            using (BaseRepository baseRepo = new BaseRepository())
            {
                // BizContext bc = new BizContext();

                Business.BizTbl_User user = BizUser.GetUser(baseRepo.BizDB, string.Empty, Username, password);
                BizContext BizContext = new BizContext();
                // BizContext bc = new BizContext();
                UserContext uc = new UserContext();
                // BizApplication.GetBizContext(Username, password, CultureCode,ref bc);
                if (user != null)
                {
                    status = true;
                    BizApplication.SetUserContext(baseRepo.BizDB, ref uc, Convert.ToInt64(user.ID), CultureCode);
                    BizContext.UserContext = uc;
                    
                        //if (uc.IsHotelAdmin()) {
                        //    System.Linq.IQueryable<Business.TB_Hotel> userHotels = BizHotel.GetHotels(baseRepo.BizDB, null, uc.FirmID, null, user.ID.ToString());
                        //    foreach (Business.TB_Hotel hotel in userHotels)
                        //    {
                        //        BizContext.Hotels.Add(hotel.ID, hotel.Name);
                        //    }
                        //    Business.TB_Hotel userHotel = userHotels.First();
                        //    BizContext.HotelID = userHotel.ID;                            
                        //    BizContext.HotelCountryID = userHotel.CountryID;
                        //    BizContext.HotelRegionID = Convert.ToInt64(userHotel.RegionID);
                        //    BizContext.HotelCityID = Convert.ToInt64(userHotel.CityID);
                        //    BizContext.HotelCurrencyID = Convert.ToString(userHotel.CurrencyID);
                        //   // BizContext.HotelCurrencyName = dc.GetColumn(GetCurrencies(dc, CultureCode, bc.HotelCurrencyID)(0), "Name");
                        //    BizContext.HotelAccommodationTypeID = userHotel.HotelAccommodationTypeID;
                        //    BizContext.HotelAvailabilityRateUpdate = Convert.ToBoolean(userHotel.AvailabilityRateUpdate);
                        //    BizContext.HotelRoutingName = userHotel.RoutingName;
                        //    BizContext.FirmID = Convert.ToString(userHotel.FirmID);
                        //    Session["SelectedHotelID"] = userHotel.ID;
                        //    Session["SelectedHotelName"] = userHotel.Name;
                        //}


                    if (BizContext.UserContext.IsHotelAdmin())
                    {




                       // System.Linq.IQueryable<Business.TB_Hotel> userHotels = BizHotel.GetHotels(baseRepo.BizDB, null, uc.FirmID, null, user.ID.ToString());
                        int i = 0;
                        baseRepo.SQLCon.Open();
                        DataTable dt = new DataTable();
                        SqlCommand cmd = new SqlCommand("B_Ex_GetUserHotelByUserID_TB_Hotel_SP", baseRepo.SQLCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", user.ID.ToString());
                        cmd.Parameters.AddWithValue("@FirmID", uc.FirmID);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        baseRepo.SQLCon.Close();
                        foreach (DataRow hotel in dt.Rows)
                        {
                            BizContext.Hotels.Add(Convert.ToInt32(hotel["ID"]), (hotel["Name"].ToString()));
                            if (i == 0)
                            {
                                BizContext.HotelID = Convert.ToInt32(hotel["ID"]);
                                BizContext.FirmID = hotel["FirmID"].ToString();
                                BizContext.HotelAccommodationTypeID = Convert.ToInt32(hotel["HotelAccommodationTypeID"]);
                                BizContext.HotelRoutingName = hotel["Name"].ToString();
                                Session["SelectedHotelID"] = Convert.ToInt32(hotel["ID"]);
                                Session["SelectedHotelName"] = hotel["Name"].ToString();
                            }
                            i++;
                        }
                    }
                    int userCountryID = 0;
                    //if (bc.UserContext.IPAddress == string.Empty)
                    //{
                    if (Session["GBAdminBizContext"] != null)
                    {
                        BizContext = (BizContext)Session["GBAdminBizContext"];
                    }
                    Session["GBAdminBizContext"] = BizContext;

                    if (UserLanguage != "")
                    {
                        try
                        {
                            string[] words = UserLanguage.Split(',');

                            BizContext.SystemCultureCode = words[1];
                            BizContext.CultureCode = words[0];
                            Session["CultureCode"] = words[0];
                            Session["GBAdminBizContext"] = BizContext;
                        }
                        catch
                        {
                            BizContext.SystemCultureCode = "en-GB";
                            BizContext.CultureCode = "en";
                            Session["GBAdminBizContext"] = BizContext;
                            Session["CultureCode"] = "en";
                        }


                    }
                    else
                    {
                        BizContext.SystemCultureCode = "en-GB";
                        BizContext.CultureCode = "en";
                        Session["GBAdminBizContext"] = BizContext;
                        Session["CultureCode"] = "en";
                    }

                    string userIpAddress = GetUserIPAddress();
                    //GetCultureByIpaddress(userIpAddress);
                    try
                    {
                        CountriesRepository countryRepo = new CountriesRepository();
                        Business.TB_Country userCountryInfo = BizApplication.GetCountryInfoFromIPAddress(baseRepo.BizDB, userIpAddress);

                        userCountryID = userCountryInfo.ID;
                    }
                    catch
                    {
                        userCountryID = 0;
                    }

                    // bc.UserContext.IPAddress = userIpAddress;
                    // }

                    //if (bc.UserSessionID == string.Empty)
                    //{
                    AuthenticationRepository authRepo = new AuthenticationRepository();
                    string countryID = (userCountryID == 0 ? String.Empty : userCountryID.ToString());
                    string UserSessionID = BizUser.SaveUserSession(baseRepo.BizDB, String.Empty, Guid.NewGuid().ToString(), user.ID.ToString(), countryID, userIpAddress, DateTime.Now.ToString()).ToString();
                    //}

                    if (Session["GBAdminBizContext"] != null)
                    {
                        BizContext = (BizContext)Session["GBAdminBizContext"];
                    }
                    BizContext.UserSessionID = UserSessionID;
                    Session["GBAdminBizContext"] = BizContext;

                    // Session[BizCommon.AdminBizContextName] = bc;
                    BizUser.AddUserOperation(baseRepo.BizDB, user.ID.ToString(), DateTime.Now.ToString(), BizCommon.Operation.Login, "", "", GetUserIPAddress(), UserSessionID);

                    Session["username"] = user.DisplayName;
                    Session["UserID"] = user.ID;
                }
                else
                {
                    Msg = Resources.Resources.CheckYourUserNameAndPasswordWarning; //BizMessage.GetMessage(baseRepo.BizDB, "CheckYourUserNameAndPasswordWarning", "en");
                }
            }

            return status;
        }

        //public Boolean ValidateUser(string Username, string password,string UserLanguage, ref bool locked, ref string Msg)
        //{
        //    Boolean status = false;

        //    using (BaseRepository baseRepo = new BaseRepository())
        //    {
        //       // BizContext bc = new BizContext();

        //      // Business.BizTbl_User user = BizUser.GetUser(baseRepo.BizDB, string.Empty, Username, password);
              
        //        BizContext BizContext = new BizContext();
        //        //UserContext uc = new UserContext();
        //        Business.BizApplication.ApplicationInitialize();
        //        bool CheckUser = false;
        //        try
        //        {
        //             CheckUser = BizApplication.GetBizContext(Username, password, CultureCode, ref BizContext);
        //        }
        //        catch
        //        {
        //            CheckUser = false;
        //        }
                
        //        //BizApplication.GetBizContext(txtUserName.Text.Trim, txtPassword.Text, hdnCultureCode.Value, bc)
        //        if (CheckUser)
        //        {
        //            status = true;
        //            int userCountryID = 0;

        //            if (Session["GBAdminBizContext"] != null)
        //            {
        //                BizContext = (BizContext)Session["GBAdminBizContext"];
        //            }
        //            Session["GBAdminBizContext"] = BizContext;

        //           if(UserLanguage!="")
        //           {
        //               try
        //               {
        //                   string[] words = UserLanguage.Split(',');

        //                   BizContext.SystemCultureCode = words[1];
        //                   BizContext.CultureCode = words[0];
        //                   Session["CultureCode"] = words[0];
        //                   Session["GBAdminBizContext"] = BizContext;
        //               }
        //               catch
        //               {
        //                   BizContext.SystemCultureCode = "en-GB";
        //                   BizContext.CultureCode = "en";
        //                   Session["GBAdminBizContext"] = BizContext;
        //                   Session["CultureCode"] = "en";
        //               }
                      

        //           }
        //           else
        //           {
        //               BizContext.SystemCultureCode = "en-GB";
        //               BizContext.CultureCode = "en";
        //               Session["GBAdminBizContext"] = BizContext;
        //               Session["CultureCode"] = "en";
        //           }

        //                string userIpAddress = GetUserIPAddress();
        //                //GetCultureByIpaddress(userIpAddress);
        //                BizContext.UserContext.IPAddress = userIpAddress;
        //                try
        //                {
        //                    CountriesRepository countryRepo = new CountriesRepository();
        //                    Business.TB_Country userCountryInfo = BizApplication.GetCountryInfoFromIPAddress(baseRepo.BizDB, userIpAddress);

        //                    userCountryID = userCountryInfo.ID;
        //                }
        //                catch
        //                {
        //                    userCountryID = 0;
        //                }

        //               // bc.UserContext.IPAddress = userIpAddress;
        //           // }

        //            //if (bc.UserSessionID == string.Empty)
        //            //{
        //                AuthenticationRepository authRepo = new AuthenticationRepository();
        //                string countryID = (userCountryID == 0 ? String.Empty : userCountryID.ToString());
        //                string UserSessionID = BizUser.SaveUserSession(baseRepo.BizDB, String.Empty, Guid.NewGuid().ToString(), BizContext.UserContext.OriginalUserID, countryID, userIpAddress, DateTime.Now.ToString()).ToString();
        //            //}
                        
        //                if (Session["GBAdminBizContext"] != null)
        //                {
        //                    BizContext = (BizContext)Session["GBAdminBizContext"];
        //                }
        //                BizContext.UserSessionID = UserSessionID;
        //                Session["GBAdminBizContext"] = BizContext;

        //           // Session[BizCommon.AdminBizContextName] = bc;
        //                BizUser.AddUserOperation(baseRepo.BizDB, BizContext.UserContext.OriginalUserID, DateTime.Now.ToString(), BizCommon.Operation.Login, "", "", GetUserIPAddress(), UserSessionID);

        //                Session["username"] = BizContext.UserContext.DisplayName;
        //                Session["UserID"] = BizContext.UserContext.OriginalUserID;
        //        }
        //        else
        //        {
        //            Msg = Resources.Resources.CheckYourUserNameAndPasswordWarning; //BizMessage.GetMessage(baseRepo.BizDB, "CheckYourUserNameAndPasswordWarning", "en");
        //        }
        //    }

        //    return status;
        //}

        protected string GetUserIPAddress()
        {
            string VisitorsIPAddr = string.Empty;
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }

        public string ChangeCulture(string UserLanguage)
        {
            string status = "false";
            if(UserLanguage!="")
            {
                string[] words = UserLanguage.Split(',');

                BizContext.SystemCultureCode = words[1];
                BizContext.CultureCode = words[0];
                Session["CultureCode"] = words[0];
                Session["GBAdminBizContext"] = BizContext;
                status = "true";
            }
           
            return status;
        }
           

        public void GetCultureByIpaddress(string UserIP)
        {
            //UserIP = "5.11.79.255";
            using (BaseRepository baseRepo = new BaseRepository())
            {
                BizContext BizContext = new BizContext();
                try
                {

                    try
                    {
                        string url = "http://freegeoip.lwan.ws/json/" + UserIP.ToString();
                        WebClient client = new WebClient();
                        string jsonstring = client.DownloadString(url);
                        dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
                        // System.Web.HttpContext.Current.Session["LocId"] = dynObj.country_code;

                        string countryCode = dynObj.country_code;
                        Session["CountryCodeFromIP"] = countryCode;

                        DataTable Cultures = new DataTable();
                        baseRepo.SQLCon.Open();
                        SqlCommand cmd = new SqlCommand("B_GetCultureAvailableCount_BizTbl_Culture_SP", baseRepo.SQLCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Culture", countryCode);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(Cultures);
                        baseRepo.SQLCon.Close();

                        if (Cultures != null)
                        {
                            if (Cultures.Rows.Count > 0)
                            {
                                foreach (DataRow dr in Cultures.Rows)
                                {

                                    if (Session["GBAdminBizContext"] != null)
                                    {
                                        BizContext = (BizContext)Session["GBAdminBizContext"];
                                    }
                                    BizContext.SystemCultureCode = dr["SystemCode"].ToString();
                                    BizContext.CultureCode = dr["CultureCode"].ToString();
                                    Session["CultureCode"] = dr["CultureCode"].ToString();
                                    Session["GBAdminBizContext"] = BizContext;
                                }
                            }
                            else
                            {
                                if (Session["GBAdminBizContext"] != null)
                                {
                                    BizContext = (BizContext)Session["GBAdminBizContext"];
                                }
                                BizContext.SystemCultureCode = "en-Gb";
                                BizContext.CultureCode = "en";
                                Session["GBAdminBizContext"] = BizContext;
                                Session["CultureCode"] = "en";
                            }
                        }

                        else
                        {
                            if (Session["GBAdminBizContext"] != null)
                            {
                                BizContext = (BizContext)Session["GBAdminBizContext"];
                            }
                            BizContext.SystemCultureCode = "en-Gb";
                            BizContext.CultureCode = "en";
                            Session["GBAdminBizContext"] = BizContext;
                            Session["CultureCode"] = "en";
                        }

                    }
                    catch
                    {
                        if (Session["GBAdminBizContext"] != null)
                        {
                            BizContext = (BizContext)Session["GBAdminBizContext"];
                        }
                        BizContext.SystemCultureCode = "en-Gb";
                        BizContext.CultureCode = "en";
                        Session["GBAdminBizContext"] = BizContext;
                        Session["CultureCode"] = "en";
                    }
                }
                catch
                {
                    string url = "http://freegeoip.net/json/" + UserIP.ToString();
                    WebClient client = new WebClient();
                    string jsonstring = client.DownloadString(url);
                    dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
                    // System.Web.HttpContext.Current.Session["LocId"] = dynObj.country_code;

                    string countryCode = dynObj.country_code;
                    Session["CountryCodeFromIP"] = countryCode;

                    DataTable Cultures = new DataTable();
                    baseRepo.SQLCon.Open();
                    SqlCommand cmd = new SqlCommand("B_GetCultureAvailableCount_BizTbl_Culture_SP", baseRepo.SQLCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Culture", countryCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(Cultures);
                    baseRepo.SQLCon.Close();

                    if (Cultures != null)
                    {
                        if (Cultures.Rows.Count > 0)
                        {
                            foreach (DataRow dr in Cultures.Rows)
                            {

                                if (Session["GBAdminBizContext"] != null)
                                {
                                    BizContext = (BizContext)Session["GBAdminBizContext"];
                                }
                                BizContext.SystemCultureCode = dr["SystemCode"].ToString();
                                BizContext.CultureCode = dr["CultureCode"].ToString();
                                Session["CultureCode"] = dr["CultureCode"].ToString();
                                Session["GBAdminBizContext"] = BizContext;
                            }
                        }
                        else
                        {

                            if (Session["GBAdminBizContext"] != null)
                            {
                                BizContext = (BizContext)Session["GBAdminBizContext"];
                            }
                            BizContext.SystemCultureCode = "en-Gb";
                            BizContext.CultureCode = "en";
                            Session["GBAdminBizContext"] = BizContext;
                            Session["CultureCode"] = "en";
                        }
                    }

                    else
                    {

                        if (Session["GBAdminBizContext"] != null)
                        {
                            BizContext = (BizContext)Session["GBAdminBizContext"];
                        }
                        BizContext.SystemCultureCode = "en-Gb";
                        BizContext.CultureCode = "en";
                        Session["GBAdminBizContext"] = BizContext;
                        Session["CultureCode"] = "en";
                    }
                }
            }
        }

        public string GetUserinfo(string username)
        {
            string status = "false";
              int ID=0;
                string name = "";
                 string Surname = "";
                string Email = "";
                string mailTemplateID = "";
                string mailFrom = "";
                string mailSubject = "";
                string mailBody = "";
                string UserName="";
            {
                PropertyOperationsRepository objupdate = new PropertyOperationsRepository();
                var userinfo = objupdate.GetUserinfo(username);
                foreach (var items in userinfo)
                {
                    ID=items.ID;
                    name = items.Name;
                    Surname = items.Surname;
                    UserName=items.UserName;
                   // DateTime date = DateTime.Now;
                    if (userinfo != null && (items.FirmID != null ))
                      {
               // Mail(CultureCode, username);
              

                List<ChangePasswordExt> objList = new List<ChangePasswordExt>();
                DataTable MailTemplate = new DataTable();
                DataTable dt = new DataTable();
                ChangePasswordRepository homemail = new ChangePasswordRepository();

                MailTemplate = homemail.GetMailTemplates("SendRemindPasswordEmail", CultureCode);

               

                
              
                if (MailTemplate != null)
                {
                    if (MailTemplate.Rows.Count > 0)
                    {
                        foreach (DataRow dr in MailTemplate.Rows)
                        {
                            mailTemplateID = dr["ID"].ToString();
                            mailFrom = dr["MailFrom"].ToString();
                            mailSubject = dr["MailSubject"].ToString();
                            mailBody = dr["MailBody"].ToString();

                            mailBody = mailBody.Replace("#UserFullName#", items.Name +  items.Surname);
                            mailBody = mailBody.Replace("#RemindLink#", homemail.GetParameter("AdminUserRemindLink"));
                            mailBody = mailBody.Replace("#RemindCode#", BizUtil.EncryptQueryStringParam(items.ID  + ";"  + items.UserName + ";" + DateTime.Now));
                            System.Net.Mail.MailAddress from = new MailAddress(SMTP_TempEmailTo, SMTP_SenderName);
                            System.Net.Mail.MailAddress to = new MailAddress(username);
                            System.Net.Mail.MailMessage m = new MailMessage(from, to);
                            m.IsBodyHtml = true;
                            m.Subject = mailSubject;
                            m.Priority = System.Net.Mail.MailPriority.High;
                            SmtpClient smtp = new SmtpClient(Smtp_Mail, Smtp_PortNo);
                            smtp.UseDefaultCredentials = false;
                            // smtp.EnableSsl = true;
                            // m.Body = sb.ToString();
                            m.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                            (System.Text.RegularExpressions.Regex.Replace(mailBody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mailBody, null, "text/html");
                            m.AlternateViews.Add(plainView);
                            m.AlternateViews.Add(htmlView);
                            smtp.Credentials = new NetworkCredential(Smtp_Username, Smtp_Password);

                            try
                            {
                                smtp.Send(m);
                                status = "Success";
                               
                            }
                            catch
                            {
                                status = "Failure";
                            }
                        }
                    }

                }
            }
            }
            }
          

                return status;
            }



        public JsonResult Mail(string CultureCode, string username)
        {
            string Username = "";
            string Email = "";
            string mailTemplateID = "";
            string mailFrom = "";
            string mailSubject = "";
            string mailBody = "";
            string status = "";

            List<ChangePasswordExt> objList = new List<ChangePasswordExt>();
            DataTable MailTemplate = new DataTable();
            DataTable dt = new DataTable();
            ChangePasswordRepository homemail = new ChangePasswordRepository();

            MailTemplate = homemail.GetMailTemplates("SendRemindPasswordEmail", CultureCode);

            List<ChangePasswordExt> values = homemail.GetUserDetails(this, Username, mailFrom, status);

            foreach (var items in values)
            {
                Username = items.Username;
                Email = items.EmailID;
            }

            if (MailTemplate != null)
            {
                if (MailTemplate.Rows.Count > 0)
                {
                    foreach (DataRow dr in MailTemplate.Rows)
                    {
                        mailTemplateID = dr["ID"].ToString();
                        mailFrom = dr["MailFrom"].ToString();
                        mailSubject = dr["MailSubject"].ToString();
                        mailBody = dr["MailBody"].ToString();
                        mailBody = mailBody.Replace("#UserFullName#", Username);
                        System.Net.Mail.MailAddress from = new MailAddress(SMTP_TempEmailTo, SMTP_SenderName);
                        System.Net.Mail.MailAddress to = new MailAddress(username);
                        System.Net.Mail.MailMessage m = new MailMessage(from, to);
                        m.IsBodyHtml = true;
                        m.Subject = mailSubject;
                        m.Priority = System.Net.Mail.MailPriority.High;
                        SmtpClient smtp = new SmtpClient(Smtp_Mail, Smtp_PortNo);
                        smtp.UseDefaultCredentials = false;
                        // smtp.EnableSsl = true;
                        // m.Body = sb.ToString();
                        m.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                        (System.Text.RegularExpressions.Regex.Replace(mailBody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                        System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mailBody, null, "text/html");
                        m.AlternateViews.Add(plainView);
                        m.AlternateViews.Add(htmlView);
                        smtp.Credentials = new NetworkCredential(Smtp_Username, Smtp_Password);

                        try
                        {
                            smtp.Send(m);
                            status = "Success";
                        }
                        catch
                        {
                            status = "Failure";
                        }
                    }
                }

            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        //public string StoreSession(string username,string password)
        //{
        //    string status = "login Success";
        //    Session["username"] = username;
        //    Session["password"] = password;
        //    return status;
        //}

        //public string GetUserIPAddress()
        //{
        //    string userIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (userIpAddress == string.Empty || userIpAddress.Trim().ToLower() == "unknown")
        //    {
        //        userIpAddress = Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    else
        //    {
        //        string[] userIpAddressList = userIpAddress.Split(',');
        //        userIpAddress = userIpAddressList[userIpAddressList.Length - 1].Trim();
        //    }

        //    return userIpAddress;
        //}

        #endregion Logon

        #region LogOff

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            //SecurityUtils.AddAuditLog("Log Off", this);
            FormsAuthentication.SignOut();

            Session.RemoveAll();
            return RedirectToAction("LogOn", "Account");
        }

        #endregion LogOff

       
        //TODO: Uncomment the following to enable Forgotten Password Recovery Functionality

        //#region Forgotten Password

        //public ActionResult ForgottenPassword()
        //{
        //    ViewBag.status = "";
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ForgottenPassword(ForgottenPasswordModel model)
        //{
        //    bool status = true;
        //    string exMsg = "";
        //    if (ModelState.IsValid)
        //    {
        //        using (PMS3Entities db = new PMS3Entities())
        //        {
        //            Users user = db.Users.FirstOrDefault(u => u.Email == model.Email);
        //            if (user != null)
        //            {
        //                //Now Send an Email to User for Username and Password!
        //                if (SecurityUtils.IsSendEmail)
        //                {
        //                    try
        //                    {
        //                        status = SendEmail(user);

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ViewBag.status = "Failed to send an Email. Please contact your Site Administrator" + ex.Message;
        //                        exMsg = ex.Message;
        //                    }

        //                    if (status)
        //                    {
        //                        //Add To Log
        //                        SecurityUtils.AddAuditLog("User \"" + user.Username + "\" requested for Forgotten Password , Email Sent to: \"" + user.Email + "\"",null, this);
        //                        ViewBag.status = "Your Login Details has been sent to above email address. <a style='color:black' href='/Account/Logon'> Log On </a>";
        //                    }
        //                    else
        //                    {
        //                        ViewBag.status = "Failed to send an Email. Please contact your Site Administrator." + exMsg + " - " + SecurityUtils.EmailErrorMsg;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.status = "Failed to send an Email. Please contact your Site Administrator." + " User Not Found! " + exMsg + " - " + SecurityUtils.EmailErrorMsg; ;
        //            }
        //        }

        //    }

        //    return View();
        //}

        //public bool SendEmail(Users user)
        //{
        //    string Body = "Dear <br/>"
        //        + user.Username
        //        + ",<br/><br/> Your Login Information at xxx are <br/>Username: " + user.Username + "<br/>Password : " + SecurityUtils.DecryptCypher(user.Password);

        //    return SecurityUtils.SendEmail(SecurityUtils.AdminMAILING_EMAIL_ADDRESS, user.Email, "Your Requested Login Info at xxx", Body);
        //}

        //#endregion

        //#region Change Password
        ////
        //// GET: /Account/ChangePassword

        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ChangePassword

        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // ChangePassword will throw an exception rather
        //        // than return false in certain failure scenarios.
        //        bool changePasswordSucceeded;
        //        using (PMS3Entities db = new PMS3Entities())
        //        {
        //            try
        //            {
        //                Users user = db.Users.Where(u => u.Username == User.Identity.Name).FirstOrDefault();

        //                if (user != null)
        //                {
        //                    string oldPassword = SecurityHelper.SecurityUtils.DecryptCypher(user.Password);
        //                    if (oldPassword.Equals(model.OldPassword))
        //                    {
        //                        user.Password = SecurityUtils.EncryptText(model.ConfirmPassword);
        //                        db.UpdateUserPassword(user.Username, user.Password);
        //                        db.SaveChanges();
        //                        //Add To Log
        //                        SecurityUtils.AddAuditLog("User password Changed Username: \"" + user.Username + "\"", this);
        //                        changePasswordSucceeded = true;
        //                    }
        //                    else
        //                    {
        //                        changePasswordSucceeded = false;
        //                    }
        //                }
        //                else
        //                {
        //                    changePasswordSucceeded = false;
        //                }

        //                //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
        //                //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
        //            }
        //            catch (Exception)
        //            {
        //                changePasswordSucceeded = false;
        //            }
        //        }

        //        if (changePasswordSucceeded)
        //        {
        //            ViewBag.status = "Password Successfully Changed";
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The current password is incorrect");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/ChangePasswordSuccess

        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}

        //#endregion

        protected override void OnException(ExceptionContext filterContext)
        { ErrorController.filterContext = filterContext; }

        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion Status Codes


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