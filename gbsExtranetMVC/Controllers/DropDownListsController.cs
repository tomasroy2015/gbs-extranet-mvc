using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gbsExtranetMVC.Models;
using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using Business;
using gbsExtranetMVC.Models.Enumerations;

namespace gbsExtranetMVC.Controllers
{

    public class DropDownListsController : Controller
    {
        // GET: DropDownLists
        BizContext BizContext = new BizContext();
        DBEntities db = new DBEntities();

        //Following Drop Downs can be Accessed be Controls on the View via Ajax Call and Return Data in json Format

        #region DropDowns

        public JsonResult GetJsonDropDown()
        {
            //return Json(new List<Users>().OrderBy(o => o.UserID).Select(c => new { UserID = c.UserID, Username = c.Username }).OrderBy(o => o.Username), JsonRequestBehavior.AllowGet);

            return Json("");
        }

        public JsonResult GetCountries()
        {
            CountryRepository modelRepo = new CountryRepository();

            return Json(modelRepo.ReadAll().OrderBy(o => o.Name).Select(c => new { CountryID = c.ID, Country = c.Name }).OrderBy(o => o.Country), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCurrencies()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { CurrencyID = c.ID, Currency = c.Name }).OrderBy(o => o.Currency), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetPages()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadPages().OrderBy(o => o.Name).Select(c => new { PageID = c.ID, Page = c.Name }).OrderBy(o => o.Page), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTemplate()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadTemplate().OrderBy(o => o.Name).Select(c => new { MailTemplateID = c.ID, Template = c.Name }).OrderBy(o => o.Template), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTables()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadTables().OrderBy(o => o.Name).Select(c => new { TableID = c.ID, Table = c.Name }).OrderBy(o => o.Table), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSecurityGroup()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadSecurityGroup().OrderBy(o => o.Name).Select(c => new { SecurityGroupId = c.ID, Role = c.Name }).OrderBy(o => o.Role), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPart()
        {
            DailyStatisticsRepository modelRepo = new DailyStatisticsRepository();

            return Json(modelRepo.GetPartTableValue().OrderBy(o => o.Name).Select(c => new { PartID = c.ID, PartName = c.Name }).OrderBy(o => o.PartName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHotels()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetHotels().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetHotelroom()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetHotelroom().Select(c => new { HotelRoomID = c.ID }).OrderBy(o => o.HotelRoomID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHotelTypeAccommodation()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetTypeHotelAccommodation().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHotelTypeAccommodationEditor()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetTypeHotelAccommodation().OrderBy(o => o.Name).Select(c => new { AccommodationID = c.ID, HotelAccommodation = c.Name }).OrderBy(o => o.HotelAccommodation), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPricePlicy()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetPricePlicy().OrderBy(o => o.ID).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPricePlicyEditorTemplate()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetPricePlicy().OrderBy(o => o.ID).Select(c => new { PricePolicyTypeID = c.ID, PricePolicy = c.Name }).OrderBy(o => o.PricePolicyTypeID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUser()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadUser().OrderBy(o => o.Name).Select(c => new { UserID = c.ID, User = c.Name }).OrderBy(o => o.User), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Role()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadRoles().OrderBy(o => o.Code).Select(c => new { RoleID = c.ID, Role = c.Code }).OrderBy(o => o.Role), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCities().OrderBy(o => o.Name).Select(c => new { CityID = c.ID, City = c.Name }).OrderBy(o => o.City), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Description()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.ReadDescription().OrderBy(o => o.Security).Select(c => new { SecurityID = c.ID, SecurityCode = c.Security }).OrderBy(o => o.SecurityCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Firm()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.ReadFirm().OrderBy(o => o.Name).Select(c => new { FirmID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RequestType()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.ReadRequest().OrderBy(o => o.RequestType).Select(c => new { RequestTypeID = c.ID, RequestType = c.RequestType }).OrderBy(o => o.RequestType), JsonRequestBehavior.AllowGet);
        }


        public JsonResult ReservationID()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.ReadReservationID().OrderBy(o => o.ID).Select(c => new { ReservationID = c.ID }).OrderBy(o => o.ReservationID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadFirmRequestStatus()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.ReadFirmRequestStatus().OrderBy(o => o.RequestStatus).Select(c => new { FirmRequestStatusID = c.ID, FirmRequestStatus = c.RequestStatus }).OrderBy(o => o.FirmRequestStatus), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDates()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetDates().OrderBy(o => o.Name).Select(c => new { DateID = c.ID, Date = c.Name }).OrderBy(o => o.Date), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetHotelPromotionID()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetHotelPromotionID().OrderBy(o => o.ID).Select(c => new { HotelPromotionID = c.ID, Date = c.ID }).OrderBy(o => o.HotelPromotionID), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetRegion()
        //{
        //    DropDownListsRepository modelRepo = new DropDownListsRepository();

        //    // return Json(modelRepo.GetRegion().OrderBy(o => o.ID).Select(c => new { RegionID = c.ID, Region = c.ID }).OrderBy(o => o.RegionID), JsonRequestBehavior.AllowGet);
        //    return new JsonResult()
        //    {
        //        Data = (modelRepo.GetRegion().OrderBy(o => o.Name).Select(c => new { RegionID = c.ID, Region = c.Name }).OrderBy(o => o.Region)),

        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //        MaxJsonLength = Int32.MaxValue
        //        //Use this value to set your maximum size for all of your Requests
        //    };

        //}
        public JsonResult GetRegion()
        {
            RegionRepository modelRepo = new RegionRepository();

            // return Json(modelRepo.GetRegion().OrderBy(o => o.ID).Select(c => new { RegionID = c.ID, Region = c.ID }).OrderBy(o => o.RegionID), JsonRequestBehavior.AllowGet);
            return new JsonResult()
            {
                Data = (modelRepo.ReadAll().OrderBy(o => o.Name).Select(c => new { RegionID = c.ID, Region = c.Name }).OrderBy(o => o.Region)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };

        }

        public JsonResult GetMainRegion(string Regionid)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            // return Json(modelRepo.GetRegion().OrderBy(o => o.ID).Select(c => new { RegionID = c.ID, Region = c.ID }).OrderBy(o => o.RegionID), JsonRequestBehavior.AllowGet);
            return new JsonResult()
            {
                Data = (modelRepo.GetMainRegionsDropdown(Regionid).OrderBy(o => o.Name).Select(c => new { Value = c.ID, Text = c.Name }).OrderBy(o => o.Text)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };

        }
        public JsonResult GetAirportDropDown()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            // return Json(modelRepo.GetRegion().OrderBy(o => o.ID).Select(c => new { RegionID = c.ID, Region = c.ID }).OrderBy(o => o.RegionID), JsonRequestBehavior.AllowGet);
            return new JsonResult()
            {
                Data = (modelRepo.GetClosestAirportDropdown().OrderBy(o => o.Name).Select(c => new { Value = c.ID, Text = c.Name }).OrderBy(o => o.Text)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };

        }
        public JsonResult GetRegionsDropdown(string RCountryID, string ParentRegionID, string Latitude, string Longitude)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            return new JsonResult()
            {
                Data = (modelRepo.GetRegionsDropdown(RCountryID, ParentRegionID, Latitude, Longitude).OrderBy(o => o.Name).Select(c => new { Value = c.ID, Text = c.Name }).OrderBy(o => o.Text)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };
        }

        public JsonResult GetClosestAirport(string RCountryID, string Latitude, string Longitude)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            return new JsonResult()
            {
                Data = (modelRepo.GetClosestAirport(RCountryID, Latitude, Longitude).OrderBy(o => o.Name).Select(c => new { Value = c.ID, Text = c.Name }).OrderBy(o => o.Text)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };
        }
        public JsonResult GetHotelsdropdown()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetHotels().OrderBy(o => o.Name).Select(c => new { HotelID = c.ID, Hotel = c.Name }).OrderBy(o => o.Hotel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReservationDropdown()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetReservationStatus().OrderBy(o => o.Name).Select(c => new { ReservationID = c.ID, ReservationName = c.Name }).OrderBy(o => o.ReservationName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTraveller()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetTravellerType().OrderBy(o => o.Name).Select(c => new { TravellerID = c.ID, TravellerName = c.Name }).OrderBy(o => o.TravellerName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTypeStatus()
        {
            CommonRepository modelRepo = new CommonRepository();
            // var status = modelRepo.ReadAllStatus();

            //  return Json(modelRepo.ReadAllStatus().OrderBy(o => o.Name).Select(c => new { StatusID = c.ID, StatusName = c.Name }).OrderBy(o => o.TravellerID), JsonRequestBehavior.AllowGet);
            return Json(modelRepo.ReadAllStatus(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReviewScaleDropdown()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetReviewScale().OrderBy(o => o.Name).Select(c => new { ReviewScaleID = c.ID, ReviewScaleName = c.Name }).OrderBy(o => o.ReviewScaleName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReviewTypeDropdown()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetReviewType().OrderBy(o => o.Name).Select(c => new { ReviewTypeID = c.ID, ReviewTypeName = c.Name }).OrderBy(o => o.ReviewTypeName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFirmDropdown()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

           // return Json(modelRepo.GetFirmDropdown().OrderBy(o => o.Name).Select(c => new { FirmID = c.ID, FirmName = c.Name }).OrderBy(o => o.FirmName), JsonRequestBehavior.AllowGet);
            return new JsonResult()
            {
                Data = (modelRepo.GetFirmDropdown().OrderBy(o => o.Name).Select(c => new { FirmID = c.ID, FirmName = c.Name }).OrderBy(o => o.FirmName)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };
        }

        public JsonResult GetUnit()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadUnit().OrderBy(o => o.Name).Select(c => new { UnitID = c.ID, Unit = c.Name }).OrderBy(o => o.Unit), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAttributeType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadAttributeType().OrderBy(o => o.Name).Select(c => new { AttributeTypeID = c.ID, AttributeType = c.Name }).OrderBy(o => o.AttributeType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAttributeCategory()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadAttributeCategory().OrderBy(o => o.Name).Select(c => new { AttributeHeaderID = c.ID, AttributeCategory = c.Name }).OrderBy(o => o.AttributeCategory), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRelatedAttribute()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadRelatedAttribute().OrderBy(o => o.Name).Select(c => new { RelatedAttributeID = c.ID, RelatedAttribute = c.Name }).OrderBy(o => o.RelatedAttribute), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPromotion()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetPromotion().OrderBy(o => o.ID).Select(c => new { PromotionID = c.ID, Promotion = c.Name }).OrderBy(o => o.Promotion), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChannelManager()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetChannelManager().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Deal()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.ReadDeal().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRoomType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetRoomType().OrderBy(o => o.ID).Select(c => new { RoomTypeID = c.ID, RoomType = c.Name }).OrderBy(o => o.RoomType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSmokingType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetSmokingType().OrderBy(o => o.ID).Select(c => new { SmokingTypeID = c.ID, SmokingType = c.Name }).OrderBy(o => o.SmokingType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTypeView()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetTypeView().OrderBy(o => o.ID).Select(c => new { ViewTypeID = c.ID, TypeView = c.Name }).OrderBy(o => o.TypeView), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBedType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetBedType().OrderBy(o => o.ID).Select(c => new { BedTypeID = c.ID, BedType = c.Name }).OrderBy(o => o.BedType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTypeSalutation()
        {
            CommonRepository modelRepo = new CommonRepository();
            // var status = modelRepo.ReadAllStatus();

            return Json(modelRepo.ReadAllTitles().OrderBy(o => o.TitleName).Select(c => new { TitleID = c.TitleID, TitleName = c.TitleName }).OrderBy(o => o.TitleName), JsonRequestBehavior.AllowGet);
            //return Json(modelRepo.ReadAllTitles(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMessageStatus()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetMessageStatus().OrderBy(o => o.Name).Select(c => new { MessageStatusID = c.ID, MessageStatusName = c.Name }).OrderBy(o => o.MessageStatusName), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMessageSubject()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetMessageSubject().OrderBy(o => o.Name).Select(c => new { MessageSubjectID = c.ID, MessageSubjectName = c.Name }).OrderBy(o => o.MessageSubjectName), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCancelType()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetCancelType().OrderBy(o => o.Name).Select(c => new { CancelTypeID = c.ID, CancelType = c.Name }).OrderBy(o => o.CancelType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPenaltyRate()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetPenaltyRate().OrderBy(o => o.Name).Select(c => new { PenaultyRateID = c.ID, PenaultyRate = c.Name }).OrderBy(o => o.PenaultyRate), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBusinessPartner()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetBusinessPartner().OrderBy(o => o.Name).Select(c => new { BusinessPartnerID = c.ID, BusinessPartner = c.Name }).OrderBy(o => o.BusinessPartner), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTransferPeriod()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetTransferPeriod().OrderBy(o => o.Name).Select(c => new { TransferPeriodID = c.ID, TransferPeriodName = c.Name }).OrderBy(o => o.TransferPeriodName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBusinessPartnerPax()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetBusinessPartnerPax().OrderBy(o => o.Name).Select(c => new { TransferPaxID = c.ID, TransferPaxName = c.Name }).OrderBy(o => o.TransferPaxName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDepartureRegion()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            return new JsonResult()
            {
                Data = (modelRepo.GetRegion().OrderBy(o => o.Name).Select(c => new { DepartureRegionID = c.ID, DepartureRegionName = c.Name }).OrderBy(o => o.DepartureRegionName)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };
        }
        public JsonResult GetDestinationRegion()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            return new JsonResult()
            {
                Data = (modelRepo.GetRegion().OrderBy(o => o.Name).Select(c => new { DestinationRegionID = c.ID, DestinationRegionName = c.Name }).OrderBy(o => o.DestinationRegionName)),

                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
                //Use this value to set your maximum size for all of your Requests
            };

        }

        public JsonResult GetCostCurrencies()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { CostCurrencyID = c.ID, CostCurrencyName = c.Name }).OrderBy(o => o.CostCurrencyName), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepositCurrency()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { DepositCurrencyID = c.ID, DepositCurrencyName = c.Name }).OrderBy(o => o.DepositCurrencyName), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPeriod()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetPeriod().OrderBy(o => o.Name).Select(c => new { PeriodID = c.ID, Period = c.Name }).OrderBy(o => o.Period), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInvoiceID()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetInvoiceID().OrderBy(o => o.ID).Select(c => new { InvoiceID = c.ID }).OrderBy(o => o.InvoiceID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourFrequency()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetTourFrequency().OrderBy(o => o.Name).Select(c => new { TourFrequencyID = c.ID, TourFrequency = c.Name }).OrderBy(o => o.TourFrequency), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTypeDeposit()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetTypeDeposit().OrderBy(o => o.Name).Select(c => new { TypeDepositID = c.ID, TypeDeposit = c.Name }).OrderBy(o => o.TypeDeposit), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBusinessPartnerCancelPolicyID()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetBusinessPartnerCancelPolicyID().OrderBy(o => o.ID).Select(c => new { BusinessPartnerCancelPolicyID = c.ID }).OrderBy(o => o.BusinessPartnerCancelPolicyID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCreditCardType()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetCreditCardType().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreditCardTypeEditorTemp()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetCreditCardType().OrderBy(o => o.Name).Select(c => new { CreditCardTypeID = c.ID, CreditCardType = c.Name }).OrderBy(o => o.CreditCardType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTransferCostCurrency()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { TransferCostCurrencyID = c.ID, TransferCostCurrency = c.Name }).OrderBy(o => o.TransferCostCurrency), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTransferCurrency()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { TransferCurrencyID = c.ID, TransferCurrency = c.Name }).OrderBy(o => o.TransferCurrency), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTransferDepositType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetTypeDeposit().OrderBy(o => o.Name).Select(c => new { TransferDepositTypeID = c.ID, TransferDepositType = c.Name }).OrderBy(o => o.TransferDepositType), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTourCostCurrency()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { TourCostCurrencyID = c.ID, TourCostCurrency = c.Name }).OrderBy(o => o.TourCostCurrency), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTourCurrency()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { TourCurrencyID = c.ID, TourCurrency = c.Name }).OrderBy(o => o.TourCurrency), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTourDepositType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetTypeDeposit().OrderBy(o => o.Name).Select(c => new { TourDepositTypeID = c.ID, TourDepositType = c.Name }).OrderBy(o => o.TourDepositType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDealCostCurrency()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { DealCostCurrencyID = c.ID, DealCostCurrency = c.Name }).OrderBy(o => o.DealCostCurrency), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDealCurrency()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.ReadCurrencies().OrderBy(o => o.Name).Select(c => new { DealCurrencyID = c.ID, DealCurrency = c.Name }).OrderBy(o => o.DealCurrency), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDealDepositType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetTypeDeposit().OrderBy(o => o.Name).Select(c => new { DealDepositTypeID = c.ID, DealDepositType = c.Name }).OrderBy(o => o.DealDepositType), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBusinessPartnerType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetBusinessPartnerType().OrderBy(o => o.Name).Select(c => new { BusinessPartnerTypeID = c.ID, BusinessPartnerType = c.Name }).OrderBy(o => o.BusinessPartnerType), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetFreebieID()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();

            return Json(modelRepo.GetFreebieID().OrderBy(o => o.ID).Select(c => new { FreebieID = c.ID }).OrderBy(o => o.FreebieID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCreateUser()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetCreateUser().OrderBy(o => o.Name).Select(c => new { CreateUserID = c.ID, CreateUser = c.Name }).OrderBy(o => o.CreateUser), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTourddl()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetTourddl().OrderBy(o => o.Name).Select(c => new { TourID = c.ID, Tour = c.Name }).OrderBy(o => o.Tour), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHotelAttribute()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetHotelAttribute().OrderBy(o => o.Name).Select(c => new { AttributeID = c.ID, Attribute = c.Name }).OrderBy(o => o.Attribute), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAuthority()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetAuthority().OrderBy(o => o.Name).Select(c => new { AuthorityID = c.ID, Authority = c.Name }).OrderBy(o => o.AuthorityID), JsonRequestBehavior.AllowGet);
        }

         public JsonResult GetTypeFirmRequest()
        {
            DropDownListsRepository model = new DropDownListsRepository();

            return Json(model.GetTypeFirmRequest().OrderBy(o => o.Name).Select(c => new { FirmRequestID = c.ID, FirmRequest = c.Name }).OrderBy(o => o.FirmRequestID), JsonRequestBehavior.AllowGet);
        }
 

 public JsonResult GetReservationOperation()
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();

     return Json(modelRepo.GetReservationOperation().OrderBy(o => o.Name).Select(c => new { ReservationOperationID = c.ID, ReservationOperationName = c.Name }).OrderBy(o => o.ReservationOperationID), JsonRequestBehavior.AllowGet);
 }

 public JsonResult GetTypeInvoiceStatus()
 {
     DropDownListsRepository model = new DropDownListsRepository();

     return Json(model.GetTypeInvoiceStatus().OrderBy(o => o.Name).Select(c => new { InvoiceStatusID = c.ID, InvoiceStatus = c.Name }).OrderBy(o => o.InvoiceStatus), JsonRequestBehavior.AllowGet);
 }

 public JsonResult GetRegiondrodown_Region(int CountryID)
 {

     DropDownListsRepository modelRepo = new DropDownListsRepository();
     var GetRegiondrodown = modelRepo.GetRegiondrodown_Region().FindAll(f => f.CountryID == CountryID);

     return Json(GetRegiondrodown, JsonRequestBehavior.AllowGet);

 }

 public JsonResult GetPropertyType()
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();

     return Json(modelRepo.GetPropertyType().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
 }
 public JsonResult GetPropertyClass()
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();

     return Json(modelRepo.GetPropertyClass().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
 }
 public JsonResult GetTypeHotelChain()
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();
     return Json(modelRepo.GetTypeHotelChain().OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
 }
 public JsonResult GetRegionsByCountry(int CountryID)
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();
     return Json(modelRepo.GetRegionsByCountry(CountryID).OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
 }
 public JsonResult GetCityByCountry(int CountryID)
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();
     return Json(modelRepo.GetCityByCountry(CountryID).OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
 }
 public JsonResult GetTypePenaltyRatewithPartID()
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();
     int PartID = 1;
     var PenaltyRate = modelRepo.GetPenaltyRate().FindAll(f => f.PartID == PartID);
     return Json(PenaltyRate, JsonRequestBehavior.AllowGet);
 }

 public JsonResult GetComission()
 {
     DropDownListsRepository model = new DropDownListsRepository();
     NewPromotionRepository NewProm = new NewPromotionRepository();
     int HotelMinumumComissionRate = NewProm.GetParameterValue("HotelMinumumComissionRate");
     return Json(model.GetComission(HotelMinumumComissionRate).OrderBy(o => o.Comission).Select(c => new { Comission = c.Comission }).OrderBy(o => o.Comission), JsonRequestBehavior.AllowGet);
 }

 public JsonResult FillUnitList( int StartIndex, int EndIndex)
 {
     DropDownListsRepository model = new DropDownListsRepository();

     return Json(model.FillUnitList(StartIndex, EndIndex), JsonRequestBehavior.AllowGet);
 }
 public JsonResult GetRoomsByHotel()
 {
     BizContext = (BizContext)Session["GBAdminBizContext"];
     int id = BizContext.HotelID;
     Session["GBAdminBizContext"] = BizContext;
     NewPromotionRepository model = new NewPromotionRepository();

     return Json(model.GetHotelRooms(id), JsonRequestBehavior.AllowGet);
 }
 public JsonResult GetAccomodationByID()
 {
     BizContext = (BizContext)Session["GBAdminBizContext"];
     int id = BizContext.HotelAccommodationTypeID;
     Session["GBAdminBizContext"] = BizContext;
     DropDownListsRepository model = new DropDownListsRepository();
     return Json(model.GetTypeHotelAccommodationByID(id).OrderBy(o => o.Name).Select(c => new { ID = c.ID, Name = c.Name }).OrderBy(o => o.Name), JsonRequestBehavior.AllowGet);
 }

 public JsonResult GetYear(int startyear)
 {
     DropDownListsRepository modelRepo = new DropDownListsRepository();

     return Json(modelRepo.GetYear(startyear).OrderBy(o => o.year).Select(c => new { year = c.year }).OrderBy(o => o.year), JsonRequestBehavior.AllowGet);
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

        #endregion
    }
}