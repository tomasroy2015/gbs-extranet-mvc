using System.Web.Helpers;
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
    public class PropertyOperationsController : Controller
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

        //
        // GET: /PropertyOperations/
        #region Read
        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
           PropertyOperationsRepository modelRepo = new PropertyOperationsRepository();
           DataSourceResult result = modelRepo.GetHotels().ToDataSourceResult(request);
           return Json(result);
        }
        #endregion

        [HttpGet]
        public ActionResult Create()
        {
            BindViewBagsForCreate();
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
          
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string HotelID)
         {
            AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();
            long id = Convert.ToInt64(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(HotelID)), "58421043"));
            //using (DBEntities db = new DBEntities())
            //{
            PropertyOperationsRepository modelRepo = new PropertyOperationsRepository();
            var Hotel = modelRepo.GetHotels().FirstOrDefault(f => f.ID == id);
            BindViewBags(Hotel);

            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
          

            return View(Hotel);
            //}
        }

        [HttpPost]
        public ActionResult Edit(HotelExt model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string Msg = "";
                    PropertyOperationsRepository modelRepo = new PropertyOperationsRepository();
                    //if (modelRepo.Update(model, ref Msg, this))
                    //{
                    //    return RedirectToAction("PropertyOperations", "Management");
                    //}
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
                    ModelState.AddModelError("", "Error: Please Correct/Enter All the Information below to Save this Record, All Fields are Mandatory");
                    ErrorHandling.HandleModelStateException(ex, ModelState);
                }
            }


            SecurityUtils.SetGlobalViewbags(this, ActiveMenu);

            return View(model);
        }

        public void BindViewBags(HotelExt Hotel)
        {
            ViewBag.Countries = DropDownLists.GetCountries(Convert.ToInt32(Hotel.CountryID));

            string RegionID = Hotel.ParentRegionID;
            if (RegionID == "")
            {
                RegionID = Hotel.RegionID;
            }
            ViewBag.MainRegions = DropDownLists.GetRegions(Hotel.CountryID, RegionID, Hotel.Latitude, Hotel.Longitude, Hotel.MainRegionID);
            ViewBag.MainRegionsMutipleDropdown = DropDownLists.GetRegionsMultpleDropdown(Hotel.CountryID, RegionID, Hotel.Latitude, Hotel.Longitude);
            ViewBag.MainRegionsByHotel = DropDownLists.GetRegionsByHotel(Hotel.ID);
            ViewBag.Firm = DropDownLists.GetFirms(Hotel.FirmID);
            ViewBag.Status = DropDownLists.GetStatus(Convert.ToInt32(Hotel.StatusID));
            ViewBag.PropertyType = DropDownLists.PropertyType(Hotel.HotelTypeID);
            ViewBag.TypeHotelClass = DropDownLists.TypeHotelClass(Hotel.HotelClassID);
            ViewBag.TypeHotelChain = DropDownLists.TypeHotelChain(Hotel.HotelChainID);
            ViewBag.TypeHotelAccommodation = DropDownLists.TypeHotelAccommodation(Hotel.HotelAccommodationTypeID);
            ViewBag.GetHotelCreditCardList = DropDownLists.GetHotelCreditCardList(Convert.ToString(Hotel.ID));
            ViewBag.GetAllCreditCardList = DropDownLists.GetAllCreditCardList();
            ViewBag.ChannelManager = DropDownLists.GetChannelManager(Hotel.ChannelManagerID);
            ViewBag.Culture = DropDownLists.GetCulture(Convert.ToString(Hotel.CultureID));
            ViewBag.FillYearListBuild = DropDownLists.FillYearList(Hotel.BuiltYear);
            ViewBag.FillYearListBuild = DropDownLists.FillYearList(Hotel.RenovationYear);
            ViewBag.CheckinFrom = DropDownLists.FillTimeList(7, 18, Hotel.CheckinStart);
            ViewBag.CheckinTo = DropDownLists.FillTimeList(12, 24, Hotel.CheckinEnd);
            ViewBag.CheckOutFrom = DropDownLists.FillTimeList(0, 14, Hotel.CheckoutStart);
            ViewBag.CheckOutTo = DropDownLists.FillTimeList(7, 18, Hotel.CheckoutEnd);
            ViewBag.GetCurrency = DropDownLists.GetCurrency(Convert.ToInt32(Hotel.CurrencyID));
            ViewBag.ClosestAirport = DropDownLists.GetClosestAirport(Hotel.CountryID, Hotel.Latitude, Hotel.Longitude,Hotel.ClosestAirportID);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult UpdatePropertyOperations(string HotelID,string HotelName,string Latitude,string Longitude, string Country, string Region, string MainRegion,string HotelAddress,
                string HotelPostCode,string HotelPhone, string HotelFax, string Firm, string HotelType,string HotelClass, string HotelChain,string AccommodationType,
                string RoomCount,string FloorCount,string BuiltYear,string RenovationYearYear, string WebAddress, string HotelEmail, string CheckinStart,string CheckinEnd,
                string CheckoutStart, string CheckoutEnd, string Culture, string Currency, string Description, string Sorts, string MapZoomIndex,string ClosestAirport, 
                string ClosestAirportDistance,string ChannelManager, string AvailabilityRateUpdate, string Status, string IsSecret,string IsPreferred,string IsActive, 
                string NotificationCulture, string RoutingName, string SelectedCards, string SelectedRegions)
        {
            PropertyOperationsRepository objupdate = new PropertyOperationsRepository();
            int i = 0;
            bool activateUserAndFirm = false;
            try { 
            if(Status=="1"&& IsActive=="true")
            {
                Status = "2";
                activateUserAndFirm = true;
            }
            int IDHotel = Convert.ToInt32(HotelID);
            var Hotel = objupdate.GetHotels().FirstOrDefault(f => f.RoutingName == RoutingName && f.ID != IDHotel);
           
            if (Hotel == null)
            {
                 i = objupdate.UpdatePropertyOperations(HotelID, HotelName, Latitude, Longitude, Country, Region, MainRegion, HotelAddress, HotelPostCode, HotelPhone, HotelFax, Firm, HotelType, HotelClass, HotelChain,
    AccommodationType, RoomCount, FloorCount, BuiltYear, RenovationYearYear, WebAddress, HotelEmail, CheckinStart, CheckinEnd, CheckoutStart, CheckoutEnd,
    Culture, Currency, Description, Sorts, MapZoomIndex, ClosestAirport, ClosestAirportDistance, ChannelManager, AvailabilityRateUpdate, Status, IsSecret,
    IsPreferred, IsActive, NotificationCulture, RoutingName, SelectedCards, SelectedRegions,this);

                if(activateUserAndFirm)
                {
                    bool Activeval=false;
                     List<FirmOperationsExt> RegionFromDBExt = objupdate.GetFirmOperations(Firm);
                     foreach (FirmOperationsExt item in RegionFromDBExt)
                     {
                         Activeval = item.Active;
       
                     }
                     if (IsActive == "true" && !Activeval)
                    {
                        var ActivateFirm = objupdate.Updatefirm(Convert.ToInt32(Firm),this);
                    }
                }
            }
            else
            {
                i = -5;
            }
                }
            catch (Exception ex)
            {
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }
            return Json(i, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetdescriptionbyCulture(string Culture, string HotelID)
        {
            string j;
            PropertyOperationsRepository objgetdec = new PropertyOperationsRepository();
            try { 
            j = objgetdec.GetdescriptionbyCulture(Culture, HotelID);
            if (j == "" || j == null)
            {
                j = "empty";
            }
            }
            catch (Exception ex)
           {
               string error = ErrorHandling.HandleException(ex);
               return this.Json(new DataSourceResult { Errors = error });
           }
            return Json(j, JsonRequestBehavior.AllowGet);
        }
         [HttpPost, ValidateInput(false)]
        public JsonResult InsertPropertyOperations(string HotelName, string Latitude, string Longitude, string Country, string Region, string MainRegion, string HotelAddress,
               string HotelPostCode, string HotelPhone, string HotelFax, string Firm, string HotelType, string HotelClass, string HotelChain, string AccommodationType,
               string RoomCount, string FloorCount, string BuiltYear, string RenovationYearYear, string WebAddress, string HotelEmail, string CheckinStart, string CheckinEnd,
               string CheckoutStart, string CheckoutEnd, string Culture, string Currency, string Description, string Sorts, string MapZoomIndex, string ClosestAirport,
               string ClosestAirportDistance, string ChannelManager, string AvailabilityRateUpdate, string Status, string IsSecret, string IsPreferred, string IsActive,
               string NotificationCulture, string RoutingName, string SelectedCards, string SelectedRegions)
        {
            PropertyOperationsRepository objupdate = new PropertyOperationsRepository();
                   string i = "0";
                   try { 
                   bool activateUserAndFirm = false;
                   if (Status == "1" && IsActive == "true")
                   {
                       Status = "2";
                       activateUserAndFirm = true;
                   }
            var Hotel = objupdate.GetHotels().FirstOrDefault(f => f.RoutingName == RoutingName);
            if (Hotel == null)
            {
             i = objupdate.InsertPropertyOperations(HotelName, Latitude, Longitude, Country, Region, MainRegion, HotelAddress, HotelPostCode, HotelPhone, HotelFax, Firm, HotelType, HotelClass, HotelChain,
            AccommodationType, RoomCount, FloorCount, BuiltYear, RenovationYearYear, WebAddress, HotelEmail, CheckinStart, CheckinEnd, CheckoutStart, CheckoutEnd,
            Culture, Currency, Description, Sorts, MapZoomIndex, ClosestAirport, ClosestAirportDistance, ChannelManager, AvailabilityRateUpdate, Status, IsSecret,
            IsPreferred, IsActive, NotificationCulture, RoutingName, SelectedCards, SelectedRegions,this);
             if (activateUserAndFirm)
             {
                 bool Activeval = false;
                 List<FirmOperationsExt> RegionFromDBExt = objupdate.GetFirmOperations(Firm);
                 foreach (FirmOperationsExt item in RegionFromDBExt)
                 {
                     Activeval = item.Active;

                 }
                 if (IsActive == "true" && !Activeval)
                 {
                     var ActivateFirm = objupdate.Updatefirm(Convert.ToInt32(Firm), this);
                 }
             }
            }
            else
            {
                i = "-5";
            }
            AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();
            string HotelID = Convert.ToString(ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(i)), "58421043"));
        using (BaseRepository baseRepo = new BaseRepository()) {

	
            int userID=Convert.ToInt32(Session["UserID"]);
            AssignBizContext();
             //string HotelID= i;
		if (Convert.ToString(userID) !=  string.Empty) {
			Business.BizTbl_User hotelUser = BizUser.GetUser(baseRepo.BizDB, userID);

			if (hotelUser.StatusID == Convert.ToInt32(BizCommon.Status.WaitingForApproval) && hotelUser.Active == false) {
				
				BizUser.ActivateUser(baseRepo.BizDB, userID.ToString(),Convert.ToString(DateTime.Now), userID.ToString());

				
				string userName = hotelUser.Email;
                string password = BizUtil.GenerateRandomCode( Convert.ToInt16(BizParameter.GetParameter(baseRepo.BizDB, "PasswordLength"))).ToLower(BizCommon.SystemCultureInfo);
				BizUser.UpdateUserNameAndPassword(baseRepo.BizDB, userID.ToString(), userName, (new BizCrypto.AES128()).Encrypt(password));

				//Kullanıcıya otel yetkisi verilir
				BizUser.SaveUserRight(baseRepo.BizDB, userID.ToString(), BizCommon.UserRole.HotelAdmin.ToString(), userID.ToString(), true);

				//Kullanıcıya ilgili otel yetkisi veririlir
				BizUser.SaveUserHotel(baseRepo.BizDB, userID.ToString(),HotelID, userID.ToString(), true);

                


                 ChangePasswordRepository homemail = new ChangePasswordRepository();

            //MailTemplate = homemail.GetMailTemplates("SendUserPasswordChangedEmail", Culturecode);


                 IList mailTemplate = BizMail.GetMailTemplates(baseRepo.BizDB, BizContext.CultureCode, 2.ToString(), "SendUserNameAndPasswordEmail", true);
                string mailTemplateID =  BizParameter.GetParameter(baseRepo.BizDB, "ID");
                  string mailFrom =  BizParameter.GetParameter(baseRepo.BizDB, "MailFrom");
                  string mailSubject =  BizParameter.GetParameter(baseRepo.BizDB, "Subject");
                  string mailBody =  BizParameter.GetParameter(baseRepo.BizDB, "Body");


                mailBody = mailBody.Replace("#UserFullName#", hotelUser.Name + " " + hotelUser.Surname);
                mailBody = mailBody.Replace("#UserName#", userName);
                //mailBody = mailBody.Replace("#Password#", password)
                mailBody = mailBody.Replace("#Password#", "<a href='" + BizParameter.GetParameter(baseRepo.BizDB, "AdminUserRemindLink") + "?RemindCode=" + BizUtil.EncryptQueryStringParam(userID + ";" + userName + ";" + DateTime.Now.Date.AddDays(14).ToString(BizCommon.DateSelectorFormat)) + "' target='_blank'>" + BizMessage.GetMessage(baseRepo.BizDB, "NewAdminPassword", BizContext.CultureCode) + "</a>");

                BizMail.AddMailForSending(baseRepo.BizDB, mailTemplateID, mailFrom, hotelUser.Email, string.Empty, mailSubject, mailBody, DateTime.Now, userID);

			}

		}

	}

    //DataContext.SubmitChanges();
    //ts.Complete();
    //MasterPage.AddSuccessfullyCompletedMessage();

    //lblHotelID.Text = HotelID;
    //if (statusID == BizCommon.Status.Approved) {
    //    BizUtil.SetListSelectedItem(ddlStatus, statusID);
    //    ddlStatus.Enabled = true;
    //}

//}
                   }
                   catch (Exception ex)
                   {
                       string error = ErrorHandling.HandleException(ex);
                       return this.Json(new DataSourceResult { Errors = error });
                   }



                return Json(i, JsonRequestBehavior.AllowGet);

        }


         public JsonResult Firm_Read([DataSourceRequest] DataSourceRequest request)
        {
            //var result = DropDownLists.GetFirms(null);
           // return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return new JsonResult()
            {
                Data = ( DropDownLists.GetFirms(null)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };
        }
         public JsonResult GetMainRegions([DataSourceRequest] DataSourceRequest request)
        {
             //var result = DropDownLists.GetFirms(null);
             // return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

             return new JsonResult()
             {
                 Data = (DropDownLists.GetRegions(null, null, null, null, null)),

                 JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                 MaxJsonLength = Int32.MaxValue
                 //Use this value to set your maximum size for all of your Requests
             };
         }
        public void BindViewBagsForCreate()
        {
            ViewBag.Countries = DropDownLists.GetCountries(-1);
            //var mRegions = Json(DropDownLists.GetRegions(null, null, null, null, null), JsonRequestBehavior.AllowGet);
            //mRegions.MaxJsonLength = Int32.MaxValue;
            //var mRegions = new JsonResult()
            //{
            //    Data = (DropDownLists.GetRegions(null, null, null, null, null)),

            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    MaxJsonLength = Int32.MaxValue
            //    //Use this value to set your maximum size for all of your Requests
            //};
            //ViewBag.MainRegions = mRegions;
           // ViewBag.MainRegionsMutipleDropdown = DropDownLists.GetRegionsMultpleDropdown(null, null, null, null);
           // ViewBag.MainRegionsByHotel = DropDownLists.GetRegionsByHotel(-1);
          //  ViewBag.Firm = DropDownLists.GetFirms(null);
           
         
            ViewBag.Status = DropDownLists.GetStatus(Convert.ToInt32(null));
            ViewBag.PropertyType = DropDownLists.PropertyType(null);
            ViewBag.TypeHotelClass = DropDownLists.TypeHotelClass(null);
            ViewBag.TypeHotelChain = DropDownLists.TypeHotelChain(null);
            ViewBag.TypeHotelAccommodation = DropDownLists.TypeHotelAccommodation(null);
            ViewBag.GetHotelCreditCardList = DropDownLists.GetHotelCreditCardList("-1");
            ViewBag.GetAllCreditCardList = DropDownLists.GetAllCreditCardList();
            ViewBag.ChannelManager = DropDownLists.GetChannelManager(null);
            ViewBag.Culture = DropDownLists.GetCulture(null);
            ViewBag.FillYearListBuild = DropDownLists.FillYearList(null);
            ViewBag.FillYearListBuild = DropDownLists.FillYearList(null);
            ViewBag.CheckinFrom = DropDownLists.FillTimeList(7, 18, null);
            ViewBag.CheckinTo = DropDownLists.FillTimeList(12, 24, null);
            ViewBag.CheckOutFrom = DropDownLists.FillTimeList(0, 14, null);
            ViewBag.CheckOutTo = DropDownLists.FillTimeList(7, 18, null);
            ViewBag.GetCurrency = DropDownLists.GetCurrency(-1);
            ViewBag.ClosestAirport = DropDownLists.GetClosestAirport(null, null, null, null);
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

        public JsonResult StoreBizContext(int HotelID, int FirmID, int HotelAccommodationTypeID)
         {
              int i=1;
              BizContext BizContext = new BizContext();
              if (Session["GBAdminBizContext"] != null)
              {
                  BizContext = (BizContext)Session["GBAdminBizContext"];
              }
              BizContext.HotelID = HotelID;
              BizContext.FirmID = Convert.ToString(FirmID);
              BizContext.HotelAccommodationTypeID = HotelAccommodationTypeID;
             // BizContext.CurrencyName = CurrencyName;
              Session["GBAdminBizContext"] = BizContext;
              return Json(i, JsonRequestBehavior.AllowGet);
         }

        public JsonResult DeleteHotelFunc(int HotelID)
         {
              int i=1;
              PropertyOperationsRepository obj = new PropertyOperationsRepository();
            try{
              i = obj.DeleteHotel(HotelID);
            }
                   catch (Exception ex)
                   {
                       string error = ErrorHandling.HandleException(ex);
                       return this.Json(new DataSourceResult { Errors = error });
                   }
              return Json(i, JsonRequestBehavior.AllowGet);
         }
        
        public JsonResult RediectToProperty()
        {
            string SelectedLanguage = "en";
            if (BizContext.CultureCode != null)
            {
                SelectedLanguage = BizContext.CultureCode;
            }
            string Url = "#";
            SecurityUtils.SetGlobalViewbags(this, "Property");
            BizContext = (BizContext)Session["GBAdminBizContext"];
            int id = BizContext.HotelID;
            PropertyOperationsRepository modelRepo = new PropertyOperationsRepository();
            var Hotel = modelRepo.GetHotels().FirstOrDefault(f => f.ID == id);
            HotelExt HotelEx = (HotelExt)Hotel;

            Session["GBAdminBizContext"] = BizContext;
            Url = "/Hotel_" + SelectedLanguage + "/" + HotelEx.CountryCode + "/" + HotelEx.RoutingName;

            return Json(Url, JsonRequestBehavior.AllowGet);
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
            //Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

    }
}
