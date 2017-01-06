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

namespace gbsExtranetMVC.Controllers.PropertyRooms
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class HotelRoomController : Controller
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
        public const string ActiveMenu = "Property";
        //
        // GET: /HotelRoom/

        //public ActionResult Index()
        //{
        //    return View();
        //}


        public ActionResult HotelRoom()
        {

            Session["PageName"] = "AdminHotelRoom";
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);

            ViewBag.PageTitle = "AdminHotelRoom";
            AdminHotelReservationRepository.Encryption64 ob = new AdminHotelReservationRepository.Encryption64();

            HotelRoomRepository modelRepo = new HotelRoomRepository();
            ViewBag.GetRoomType = DropDownLists.GetRoomType();
            ViewBag.GetSmokingStatus = DropDownLists.GetSmokingType();
            ViewBag.GetTypeView = DropDownLists.GetTypeView();
            ViewBag.GetCulture = DropDownLists.GetCulture("en");

            List<HotelRoomExt> GetBedTypes = modelRepo.GetBedTypes();

            ArrayList OptionNo = new ArrayList();
            for (int i = 1; i <= 3; i++)
            {
                OptionNo.Add(i);
            }

            ViewBag.OptionNo = OptionNo;

            List<HotelRoomExt> AttributeHeaders = modelRepo.GetAttributeHeaders();
            ViewBag.AttributeHeaders = AttributeHeaders;

            List<HotelRoomExt> Attributes = new List<HotelRoomExt>();

            if (AttributeHeaders != null)
            {
                foreach (var items in AttributeHeaders)
                {
                    Attributes = modelRepo.GetAttributes(items.AttributeHeaderId);                   
                }
            }

            //List<HotelRoomExt> Attributes = modelRepo.GetAttributes(DecryptedHotelRoomID);
            //ViewBag.Attributes = Attributes;

            if (Request.QueryString["HotelRoomID"] != null)
            {
                string HotelRoomID = Request.QueryString["HotelRoomID"].ToString();

                if (HotelRoomID != "")
                {
                    string DecryptedHotelRoomID = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(HotelRoomID)), "58421043");
                    ViewBag.HotelRoomID = DecryptedHotelRoomID;

                    var HotelRooms = modelRepo.GetHotelRooms(DecryptedHotelRoomID).FirstOrDefault(f => f.ID == Convert.ToInt64(DecryptedHotelRoomID));

                    //List<HotelRoomExt> RoomAttributes = modelRepo.GetHotelRoomAttributes(HotelRooms.ID, 1, "AttributeName");
                    //ViewBag.RoomAttributes = RoomAttributes;
                    Attributes = modelRepo.GetHotelRoomAttributes(HotelRooms.ID, 1, "AttributeName");
                    ViewBag.Attributes = Attributes;

                    GetBedTypes = modelRepo.GetHotelRoomBeds(HotelRooms.ID);
                    ViewBag.RoomBedInfo = GetBedTypes;

                    return View(HotelRooms);
                }
            }
            ViewBag.Attributes = Attributes;
            ViewBag.RoomBedInfo = GetBedTypes;
            return View();
        }
        public JsonResult GetdescriptionbyCulture(string Culture, string Roomid)
        {
            string j;
            try { 
            HotelRoomRepository modelRepo = new HotelRoomRepository();
            j = modelRepo.GetdescriptionbyCulture(Culture, Roomid);
            if(j=="" || j==null)
            {
                j = "empty";
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
            return Json(j, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveHotelRoom(string HotelRoomID,string RoomType, string RoomCount, string RoomSize, string RoomMaxPeopleCount, string RoomMaxChildrenCount, string BabyCotCount, string ExtraBedCount,
            string SmokingStatus, string ViewType, string Culture, string Description, string HotelAttributes, string BedCountText, string Language)
        {
            AssignBizContext();
            int j;
            try { 
            int HotelID = BizContext.HotelID;
            //string HotelID = "100003";
            bool isNewRecord = (HotelRoomID == string.Empty);
            HotelRoomRepository modelRepo = new HotelRoomRepository();
            HotelRoomID = Convert.ToString(modelRepo.SaveHotelRoom(HotelRoomID, HotelID, RoomType, RoomCount, RoomSize, RoomMaxPeopleCount, RoomMaxChildrenCount, BabyCotCount, ExtraBedCount,
                SmokingStatus, ViewType, Culture, Description,this));

            bool BedDeleteStatus = modelRepo.DeleteHotelRoomBeds(HotelRoomID);

            string[] BedTextValues = BedCountText.Split(',');

            for (int k = 0; k < BedTextValues.Length; k++)
            {
                string[] BedValues = BedTextValues[k].Split(';');
                string OptionNo = BedValues[0];
                string txtBedCount = BedValues[1];
                string BedTypeID = BedValues[2];
                if (txtBedCount != string.Empty)
                {
                    modelRepo.SaveHotelRoomBed(string.Empty, OptionNo, HotelRoomID, BedTypeID, txtBedCount, this);
                }

            }

            bool DeleteHotelAttribute = modelRepo.DeleteHotelRoomAttributes(HotelRoomID, "1");

            string[] AttributeID=HotelAttributes.Split(',');


            for (int i = 0; i < AttributeID.Length; i++)
            {
                if (AttributeID[i] != "")
                {
                    int AttributeIDValue = Convert.ToInt32(AttributeID[i]);
                    bool SaveHotelRoomAttributes = modelRepo.SaveHotelRoomAttributes(HotelRoomID, 0, AttributeIDValue, string.Empty, string.Empty, string.Empty, this);
                }              
            }

          
           
            if(isNewRecord)
            {
                 DateTime theDate = DateTime.Now;
                 DateTime yearInTheFuture = theDate.AddYears(1);
                 modelRepo.CreateHotelRoomAvailability(HotelRoomID, DateTime.Now, yearInTheFuture, RoomCount);
            }

            j = 1;
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

            string encriptedHotelRoomID = "";
            if(j==1)
            {
                Encryption64 objEncryptreservation = new Encryption64();

                encriptedHotelRoomID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(HotelRoomID, "58421043")));
            }

            return Json(encriptedHotelRoomID, JsonRequestBehavior.AllowGet);
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
