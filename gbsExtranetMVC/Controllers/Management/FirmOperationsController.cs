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
using System.Net;

namespace gbsExtranetMVC.Controllers.Management
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class FirmOperationsController :Controller
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
        public const string ActiveMenu = "Management";

        [HttpGet]
        public ActionResult Edit(long id)
        {
            //using (DBEntities db = new DBEntities())
            //{
            AssignBizContext();
            FirmOperationsRepository modelRepo = new FirmOperationsRepository();
            var Firm = modelRepo.GetFirmOperations().FirstOrDefault(f => f.ID == id);
            BindViewBags(Firm);
           
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);

            return View(Firm);
            //}
        }
        

        public ActionResult _Read([DataSourceRequest]DataSourceRequest request)
        {
            FirmOperationsRepository modelRepo = new FirmOperationsRepository();

            DataSourceResult result = modelRepo.GetFirmOperations().ToDataSourceResult(request);
            return Json(result);
        }

        public JsonResult FirmOperationsStatusReject(int ID)
        {
            string Msg = "";
            string request = "false";
            try
            {
                FirmOperationsRepository modelRepo = new FirmOperationsRepository();
                //  FirmRequestExt model = new FirmRequestExt();

                if (modelRepo.UpdateRejectStatus(ID, this) == false)
                {
                    return this.Json(new DataSourceResult { Errors = Msg });
                }
                request = "true";
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

        #region Create New User

        [HttpGet]
        public ActionResult Create()
        {
            BindViewBagss();
            AssignBizContext();
            SecurityUtils.SetGlobalViewbags(this, ActiveMenu, BizContext.UserContext.IsAdmin(), BizContext.UserContext.IsHotelAdmin(), BizContext.HotelID);
            return View();
        }

        [HttpPost]
        public ActionResult Create(FirmOperationsExt model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FirmOperationsRepository uRepo = new FirmOperationsRepository();
                    if (uRepo.Create(model, ModelState, this))
                    {
                        return RedirectToAction("Index", "FirmOperations");
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
                    ModelState.AddModelError("", "Error: Please Correct/Enter All the Information below to Save this Record, All Fields are Mandatory");
                    ErrorHandling.HandleModelStateException(ex, ModelState);
                }
            }

            return View(model);
        }

        #endregion Create New User
      
        public void BindViewBags(FirmOperationsExt Firm)
        {
                ViewBag.Countries = Firm.CountryID!=null && Firm.CountryID!=string.Empty ? DropDownLists.GetCountries(Convert.ToInt32(Firm.CountryID)) : DropDownLists.GetCountries(null);
                ViewBag.Salutation = Firm.Executive_TitleID != null && Firm.Executive_TitleID != string.Empty ? DropDownLists.GetTitle(Convert.ToInt32(Firm.Executive_TitleID)) : DropDownLists.GetTitle(null);
                ViewBag.Status = Firm.StatusID != null && Firm.StatusID != string.Empty ? DropDownLists.GetStatus(Convert.ToInt32(Firm.StatusID)) : DropDownLists.GetStatus(null); 
           
        }

        public void BindViewBagss()
        {
            ViewBag.Countries = DropDownLists.GetCountries(Convert.ToInt32(null));
            ViewBag.Salutation = DropDownLists.GetTitle(Convert.ToInt32(null));
            ViewBag.Status = DropDownLists.GetStatus(Convert.ToInt32(null));

        }

        public JsonResult UpdateFirmOperations(string UserCode, string Firm, string Country, string Region, string Address, string Phone, string Fax, string Postcode,
           string Email, string TaxOffice, string TaxID, string ExecutiveTitle, string ExecutiveName, string ExecutiveSurname, string ExecutivePosition, string ExecutivePhone, string ExecutiveEmail,
            string Active, string Status, string IPAddress)
        {
            FirmOperationsRepository objupdate = new FirmOperationsRepository();
            int i;
            try { 
            i = objupdate.UpdateFirmOperations(UserCode, Firm, Country, Region,Address,Phone,Fax,Postcode,
           Email, TaxOffice, TaxID, ExecutiveTitle, ExecutiveName, ExecutiveSurname, ExecutivePosition, ExecutivePhone, ExecutiveEmail,
           Active, Status, IPAddress);
                }
            catch (Exception ex)
            {
                string error = ErrorHandling.HandleException(ex);
                return this.Json(new DataSourceResult { Errors = error });
            }
            return Json(i, JsonRequestBehavior.AllowGet);

        }
        public JsonResult InsertFirmOperations(string Firm, string Country, string Region, string Address, string Phone, string Fax, string Postcode,
           string Email, string TaxOffice, string TaxID, string ExecutiveTitle, string ExecutiveName, string ExecutiveSurname, string ExecutivePosition, string ExecutivePhone, string ExecutiveEmail,
            string Active, string Status, string IPAddress)
        {
            FirmOperationsRepository objinsert = new FirmOperationsRepository();
            int i;
            try { 
           i = objinsert.InsertFirmOperations( Firm, Country, Region,Address,Phone,Fax,Postcode,
           Email, TaxOffice, TaxID, ExecutiveTitle, ExecutiveName, ExecutiveSurname, ExecutivePosition, ExecutivePhone, ExecutiveEmail,
           Active, Status, IPAddress,this);
                }
            catch (Exception ex)
            {
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
        }
        

        public JsonResult GetDestinationSearchResult( string Keyword, string CountryID)
        {
            string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<DestinationAutoCompleteSearch> objvalues = new List<DestinationAutoCompleteSearch>();
           
            {
                DataTable SearchResults = GetDestinationSearchResults(CultureCode, Keyword, CountryID);
                foreach (DataRow dr in SearchResults.Rows)
                {
                    objvalues.Add(new DestinationAutoCompleteSearch(ID: dr["ID"].ToString(), DestinationSearchType: dr["DestinationSearchType"].ToString(), CountryID: dr["CountryID"].ToString(), CountryName: dr["CountryName"].ToString(), RegionID: dr["RegionID"].ToString(), RegionName: dr["RegionName"].ToString(), ParentID: dr["ParentID"].ToString(), ParentName: dr["ParentName"].ToString(), SecondParentName: dr["SecondParentName"].ToString(), RegionType: dr["RegionType"].ToString(), Code: dr["Code"].ToString(), HotelID: dr["HotelID"].ToString(), HotelName: dr["HotelName"].ToString(), IsPopular: dr["IsPopular"].ToString()));
                }
            }
           

            return Json(objvalues, JsonRequestBehavior.AllowGet);

        }

        public DataTable GetDestinationSearchResults(string Culture, string Keyword, string CountryID)
        {
            DataTable dt = new DataTable();

            FirmOperationsRepository obj = new FirmOperationsRepository();
           
                dt = obj.GetSearchDestination(Keyword, Culture, CountryID);
          
            return dt;
        }

        public class DestinationAutoCompleteSearch
        {

            public string ID;

            public string DestinationSearchType;

            public string Name;

            public string CountryID;

            public string CountryName;

            public string Msgtopdest;

            public string RegionID;

            public string RegionName;

            public string ParentID;

            public string ParentName;

            public string SecondParentName;

            public string DisplayName;

            public string RegionType;

            public string Code;

            public Boolean IsPopular;

            public string HotelID;

            public string HotelName;

            public string DestinationSearchTypeImagePath;

            public const string ImagePath = "~/Image/";

            public DestinationAutoCompleteSearch(string ID, string DestinationSearchType, string CountryID, string CountryName, string RegionID, string RegionName, string ParentID, string ParentName, string SecondParentName, string RegionType, string Code, string HotelID, string HotelName, string IsPopular)
            {
                this.ID = ID;
                this.DestinationSearchType = DestinationSearchType;
                this.CountryID = CountryID;
                this.CountryName = CountryName;
                this.RegionID = RegionID;
                this.RegionName = RegionName;
                this.ParentID = ParentID;
                this.ParentName = ParentName;
                this.SecondParentName = SecondParentName;
                this.RegionType = RegionType;
                this.Code = Code;
                this.HotelID = HotelID;
                this.HotelName = HotelName;
                this.IsPopular = Convert.ToBoolean(IsPopular);

                //bool IsPopular = Convert.ToBoolean(this.IsPopular);

                //DestinationSearchType1 Regionenum = DestinationSearchType1.Region;
                int Regionvalue = (int)DestinationSearchType1.Region;
                string Region = Convert.ToString(Regionvalue);

                //DestinationSearchType1 Hotelenum = DestinationSearchType1.Hotel;
                int Hotelvalue = (int)DestinationSearchType1.Hotel;
                string Hotel = Convert.ToString(Hotelvalue);

                //RegionType1 AIRPenum = RegionType1.AIRP;
                int AIRPvalue = (int)RegionType1.AIRP;
                string AIRP = Convert.ToString(AIRPvalue);

                if ((DestinationSearchType == Region))
                {
                    this.Name = RegionName;
                    this.DisplayName = RegionName;
                }


                else if ((DestinationSearchType == Hotel))
                {
                    this.Name = (HotelName + (", " + RegionName));
                    this.DisplayName = this.Name;
                }

                if ((this.RegionType == AIRP && this.Code != String.Empty))
                {

                    this.DisplayName += (" (" + (this.Code + ")"));
                    this.RegionName += (" (" + (this.Code + ")"));
                    this.Name = this.RegionName;
                }


                if ((this.ParentName != string.Empty))
                {
                    this.DisplayName += (", " + this.ParentName);
                }

                if ((this.SecondParentName != String.Empty))
                {
                    this.DisplayName += (", " + this.SecondParentName);
                }

                this.DisplayName += (", " + this.CountryName);

                if ((DestinationSearchType == Region))
                {
                    if ((this.RegionType == AIRP))
                    {
                        this.DestinationSearchTypeImagePath = System.Web.VirtualPathUtility.ToAbsolute((ImagePath + "AirportIcon_ds.png"));
                    }
                    else if (this.IsPopular)
                    {
                        this.DestinationSearchTypeImagePath = System.Web.VirtualPathUtility.ToAbsolute((ImagePath + "PopularIcon_ds.png"));
                    }
                    else
                    {
                        this.DestinationSearchTypeImagePath = System.Web.VirtualPathUtility.ToAbsolute((ImagePath + "RegionIcon_ds.png"));
                    }
                }
                else if ((DestinationSearchType == Hotel))
                {
                    this.DestinationSearchTypeImagePath = System.Web.VirtualPathUtility.ToAbsolute((ImagePath + "HotelIcon_ds.png"));
                }
            }
        }

        public enum RegionType1
        {
            None = 0,
            ADM1 = 1,
            ADM2 = 2,
            RGN = 3,
            AIRP = 4,
            PPLA = 5,
            PPLA2 = 6,
            PPLA3 = 7,
            PPLC = 8,
            PPL = 9,
            PPLX = 10,
            POI = 11,
        }

        public enum DestinationSearchType1
        {
            Region = 1,
            Hotel = 2,
        }
    }
}