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
using gbsExtranetMVC.Models;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.Text;
using gbsExtranetMVC.Models.Enumerations;
using System.Globalization;
using Business;
using System.Net;

namespace gbsExtranetMVC.Controllers
{
    [gbsExtranetMVC.Helpers.Authorization(Permissions.AllUsers)]
    [RequiresSSL]
    public class TablesController : Controller
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
        public ActionResult Index()
        {
            
            return View();
            
        }




        public JsonResult GetTablePartialView(int TableID)
        {
            ViewBag.TableID = TableID;
            string partialView = "";

            try { 
            switch (TableID)
            {
                case 2: //ID from BizTbl_Table for this Table
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_Culture_Grid", null, ViewData, TempData);
                    break;
                case 3:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_MailQueue_Grid", null, ViewData, TempData);
                    break;
                case 4:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_MailTemplate_Grid", null, ViewData, TempData);
                    break;
                case 5:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_Message_Grid", null, ViewData, TempData);
                    break;
                case 6:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/Biztbl_Page_Grid", null, ViewData, TempData);
                    break;
                case 7:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_PageControl_Grid", null, ViewData, TempData);
                    break;
                case 8:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_Parameter_Grid", null, ViewData, TempData);
                    break;
                case 9:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_Security_Grid", null, ViewData, TempData);
                    break;
                case 10:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_SecurityGroup_Grid", null, ViewData, TempData);
                    break;
                case 11:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_SecurityGroupRight_Grid", null, ViewData, TempData);
                    break;
                case 12:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_User_Grid", null, ViewData, TempData);
                    break;
                case 13:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_Table_Grid", null, ViewData, TempData);
                    break;
                case 14:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_TableSecurityGroupRight_Grid", null, ViewData, TempData);
                    break;
                case 26:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_TableColumn_Grid", null, ViewData, TempData);
                    break;
                case 27:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Part_Grid", null, ViewData, TempData);
                    break;
                case 28:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeOperation_Grid", null, ViewData, TempData);
                    break;
                case 29:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeRegion_Grid", null, ViewData, TempData);
                    break;
                case 30:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ExchangeRateHistory_Grid", null, ViewData, TempData);
                    break;
                case 31:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelComission_Grid", null, ViewData, TempData);
                    break;
                case 32:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeCreditCard_Grid", null, ViewData, TempData);
                    break;
                case 33:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Transfer_Grid", null, ViewData, TempData);
                    break;
                case 34:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_InvoiceDetailHistory_Grid", null, ViewData, TempData);
                    break;
                case 35:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_FirmHistory_Grid", null, ViewData, TempData);
                    break;
                case 36:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TourRegionHistory_Grid", null, ViewData, TempData);
                    break;
                case 37:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_UserBusinessPartner_Grid", null, ViewData, TempData);
                    break;
                case 38:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeSubRegion_Grid", null, ViewData, TempData);
                    break;
                case 39:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Message_Grid", null, ViewData, TempData);
                    break;
                case 40:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelReservation_Grid", null, ViewData, TempData);
                    break;
                case 41:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRoomHistory_Grid", null, ViewData, TempData);
                    break;
                case 42:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRoomAttribute_Grid", null, ViewData, TempData);
                    break;
                case 43:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Photo_Grid", null, ViewData, TempData);
                    break;
                case 44:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Hotel_Grid", null, ViewData, TempData);
                    break;
                case 45:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartnerRegion_Grid", null, ViewData, TempData);
                    break;
                case 46:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartnerPax_Grid", null, ViewData, TempData);
                    break; 
                case 47:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_FirmRequestHistory_Grid", null, ViewData, TempData);
                    break;
                case 48:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TransferReservationPromotion_Grid", null, ViewData, TempData);
                    break;
                case 49:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeDay_Grid", null, ViewData, TempData);
                    break;
                case 50:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelReservationHistory_Grid", null, ViewData, TempData);
                    break;
                case 51:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_InvoiceDetail_Grid", null, ViewData, TempData);
                    break;
                case 52:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeFirmRequest_Grid", null, ViewData, TempData);
                    break;
                case 53:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ReservationStatusHistory_Grid", null, ViewData, TempData);
                    break;
                case 54:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelCancelPolicy_Grid", null, ViewData, TempData);
                    break;

                case 55:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Invoice_Grid", null, ViewData, TempData);
                    break;
                case 56:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TourRegion_Grid", null, ViewData, TempData);
                    break;
               
                case 57:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeReview_Grid", null, ViewData, TempData);
                    break;
                case 58:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelAttribute_Grid", null, ViewData, TempData);
                    break;
                case 59:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Promotion_Grid", null, ViewData, TempData);
                    break;
                case 60:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ReservationHistory_Grid", null, ViewData, TempData);
                    break;
                case 61:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeMessageStatus_Grid", null, ViewData, TempData);
                    break;
                case 62:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeInvoiceStatus_Grid", null, ViewData, TempData);
                    break;
                case 63:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TransferHistory_Grid", null, ViewData, TempData);
                    break;
                case 64:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TourAttribute_Grid", null, ViewData, TempData);
                    break;
                case 65:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeMonth_Grid", null, ViewData, TempData);
                    break;
                case 66:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelAttributeHistory_Grid", null, ViewData, TempData);
                    break;
                case 67:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelComissionHistory_Grid", null, ViewData, TempData);
                    break;
                case 68:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeFirmRequestStatus_Grid", null, ViewData, TempData);
                    break;
                case 69:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Attribute_Grid", null, ViewData, TempData);
                    break;
                case 70:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_FAQ_Grid", null, ViewData, TempData);
                    break;
                case 71:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRegionHistory_Grid", null, ViewData, TempData);
                    break;
                case 72:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TransferPeriod_Grid", null, ViewData, TempData);
                    break;
                case 73:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_DealReservationHistory_Grid", null, ViewData, TempData);
                    break;
                case 74:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TourHistory_Grid", null, ViewData, TempData);
                    break;
                case 75:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeDeposit_Grid", null, ViewData, TempData);
                    break;
                case 76:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TourReservation_Grid", null, ViewData, TempData);
                    break;
                case 77:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeRoom_Grid", null, ViewData, TempData);
                    break;
                case 78:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Messenger_Grid", null, ViewData, TempData);
                    break;
                case 79:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeBusinessPartner_Grid", null, ViewData, TempData);
                    break;
                case 80:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeTourFrequency_Grid", null, ViewData, TempData);
                    break;
                case 81:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_EmailList_Grid", null, ViewData, TempData);
                    break;
                case 82:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_UserHistory_Grid", null, ViewData, TempData);
                    break;
                case 83:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Region_Grid", null, ViewData, TempData);
                    break;
                case 84:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRegion_Grid", null, ViewData, TempData);
                    break;
                case 85:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ReservationReviewDetail_Grid", null, ViewData, TempData);
                    break;
                case 86:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeHotel_Grid", null, ViewData, TempData);
                    break;
                case 87:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_DealHistory_Grid", null, ViewData, TempData);
                    break;
                case 88:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeReservationOperation_Grid", null, ViewData, TempData);
                    break;

                case 89:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelCreditCard_Grid", null, ViewData, TempData);
                    break;
                case 90:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_UserRight_Grid", null, ViewData, TempData);
                    break;
                case 91:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_DealReservation_Grid", null, ViewData, TempData);
                    break;
                case 92:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelReservationPromotion_Grid", null, ViewData, TempData);
                    break;
                case 93:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelSearch_Grid", null, ViewData, TempData);
                    break;
                case 94:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelPromotion_Grid", null, ViewData, TempData);
                    break;
                case 95:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeAttribute_Grid", null, ViewData, TempData);
                    break;
                case 97:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeData_Grid", null, ViewData, TempData);
                    break;
                case 98:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/Tb_HotelRateHistory_Grid", null, ViewData, TempData);
                    break;
                case 99:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelReservationRate_Grid", null, ViewData, TempData);
                    break;
                case 100:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypePricePolicy_Grid", null, ViewData, TempData);
                    break;
                case 101:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeSalutation_Grid", null, ViewData, TempData);
                    break;
                case 102:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeCancel_Grid", null, ViewData, TempData);
                    break;
                case 103:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HitCount_Grid", null, ViewData, TempData);
                    break;
                case 104:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Unit_Grid", null, ViewData, TempData);
                    break;
                case 105:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypePenaltyRate_Grid", null, ViewData, TempData);
                    break;
                case 106:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRoom_Grid", null, ViewData, TempData);
                    break;
                case 107:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_DealReservationPromotion_Grid", null, ViewData, TempData);
                    break;
                case 108:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Tour_Grid", null, ViewData, TempData);
                    break;
                case 109:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRoomBed_Grid", null, ViewData, TempData);
                    break;
                case 110:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ExchangeRate_Grid", null, ViewData, TempData);
                    break;
                case 111:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ReservationReview_Grid", null, ViewData, TempData);
                    break;
                case 112:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TourReservationPromotion_Grid", null, ViewData, TempData);
                    break;
                case 113:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeTraveller_Grid", null, ViewData, TempData);
                    break;
                case 114:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Date_Grid", null, ViewData, TempData);
                    break;
                case 115:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartnerPaxHistory_Grid", null, ViewData, TempData);
                    break;
                case 116:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeVehicle_Grid", null, ViewData, TempData);
                    break;
                case 117:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRate_Grid", null, ViewData, TempData);
                    break;
                case 118:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartnerCancelPolicy_Grid", null, ViewData, TempData);
                    break;
                case 119:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartner_Grid", null, ViewData, TempData);
                    break;  
                case 120:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_AttributeHeader_Grid", null, ViewData, TempData);
                    break;
                case 121:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeMessageSubject_Grid", null, ViewData, TempData);
                    break;
                case 122:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelPromotionRoom_Grid", null, ViewData, TempData);
                    break;
                case 123:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelAvailabilityHistory_Grid", null, ViewData, TempData);
                    break;
                case 124:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelCancelPolicyHistory_Grid", null, ViewData, TempData);
                    break;
                case 125:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartnerRegionHistory_Grid", null, ViewData, TempData);
                    break; 
                case 126:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ReservationReviewDetailHistory_Grid", null, ViewData, TempData);
                    break;
                case 127:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeHotelClass_Grid", null, ViewData, TempData);
                    break;
                case 128:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelReservationRateHistory_Grid", null, ViewData, TempData);
                    break;
                case 129:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeHotelBudget_Grid", null, ViewData, TempData);
                    break;
                case 130:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Currency_Grid", null, ViewData, TempData);
                    break;
                case 131:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Reservation_Grid", null, ViewData, TempData);
                    break;
                case 132:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeReservationStatus_Grid", null, ViewData, TempData);
                    break;
                case 133:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TourReservationHistory_Grid", null, ViewData, TempData);
                    break;
                case 134:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TransferReservationHistory_Grid", null, ViewData, TempData);
                    break;
                case 135:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_InvoiceHistory_Grid", null, ViewData, TempData);
                    break;
                case 136:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelPromotionHistory_Grid", null, ViewData, TempData);
                    break;
                case 137:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeReviewStatus_Grid", null, ViewData, TempData);
                    break;
                case 138:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TransferReservation_Grid", null, ViewData, TempData);
                    break;
                case 139:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_FirmRequest_Grid", null, ViewData, TempData);
                    break;
                case 140:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartnerHistory_Grid", null, ViewData, TempData);
                    break; 
                case 141:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelHistory_Grid", null, ViewData, TempData);
                    break;
                case 142:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeHotelAccommodation_Grid", null, ViewData, TempData);
                    break;
                case 143:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ReservationReviewHistory_Grid", null, ViewData, TempData);
                    break;
                case 144:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Deal_Grid", null, ViewData, TempData);
                    break;
                case 145:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeStatus_Grid", null, ViewData, TempData);
                    break;
                case 146:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeBed_Grid", null, ViewData, TempData);
                    break;
                case 147:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeHotelChain_Grid", null, ViewData, TempData);
                    break;
                case 148:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeReviewScale_Grid", null, ViewData, TempData);
                    break;
                case 149:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeHotelSearchSort_Grid", null, ViewData, TempData);
                    break;
                case 150:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Firm_Grid", null, ViewData, TempData);
                    break;
                case 151:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeView_Grid", null, ViewData, TempData);
                    break;
                case 152:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_IPToNation_Grid", null, ViewData, TempData);
                    break;
                case 153:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelCreditCardHistory_Grid", null, ViewData, TempData);
                    break;
                case 154:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Country_Grid", null, ViewData, TempData);
                    break;
                case 155:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_UserOperation_Grid", null, ViewData, TempData);
                    break;
                case 156:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRoomBedHistory_Grid", null, ViewData, TempData);
                    break;
                case 157:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeSmoking_Grid", null, ViewData, TempData);
                    break;
                case 158:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BusinessPartnerCancelPolicyHistory_Grid", null, ViewData, TempData);
                    break; 
                case 159:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeReviewEvaluation_Grid", null, ViewData, TempData);
                    break;
                case 160:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_UserHotel_Grid", null, ViewData, TempData);
                    break;
                case 161:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Period_Grid", null, ViewData, TempData);
                    break;
                case 162:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelAvailability_Grid", null, ViewData, TempData);
                    break; 
                case 163:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelRoomAttributeHistory_Grid", null, ViewData, TempData);
                    break;
                case 164:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelMinumumAccommodation_Grid", null, ViewData, TempData);
                    break;
                case 166:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_Error_Grid", null, ViewData, TempData);
                    break;
                case 167:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelSearchParameter_Grid", null, ViewData, TempData);
                    break;
                case 168:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_BankHistory_Grid", null, ViewData, TempData);
                    break;
                case 169:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_Bank_Grid", null, ViewData, TempData);
                    break;
                case 170:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_TypeFreebie_Grid", null, ViewData, TempData);
                    break;
                case 171:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_ChannelManager_Grid", null, ViewData, TempData);
                    break;
                case 172:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/BizTbl_UserSession_Grid", null, ViewData, TempData);
                    break;
                case 173:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_HotelPromotionFreebie_Grid", null, ViewData, TempData);
                    break;
                case 174:
                    partialView = SecurityUtils.RenderPartialToString(this, "TableGrids/TB_LatestNews_Grid", null, ViewData, TempData);
                    break;
               

                

               
              
                default:
                    break;
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
              

            return Json(partialView, JsonRequestBehavior.AllowGet);

            //TablesRepositary tbleobject = new TablesRepositary();
            //List<TablesExt> list = new List<TablesExt>();
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //dt = tbleobject.displayTables(TableID);
            //var asdas= toHTML_Table(dt);
            //return Json(asdas, JsonRequestBehavior.AllowGet);
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




