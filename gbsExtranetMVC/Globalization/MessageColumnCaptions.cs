using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using gbsExtranetMVC.Models;
using Extension;
using System.Globalization;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
namespace MessageColumnCaptions
{
    public class MessageCaption
    {
        public string Name { get; set; }
        public string Caption { get; set; }
    }
    public class MessageColumnCaptions
    {
       
        public static string GetMEssageTableCaptions(string ColumnName, string TableNameParam)
        {
           string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var TableName = new SqlParameter("@TableName", TableNameParam);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var MessageCode = new SqlParameter("@MessageCode", ColumnName);
                var result = entity.Database.SqlQuery<GetTableCaptionValue_Result>("B_Ex_GetCaptionValues_BizTbl_TableColumn_SP @TableName,@Culture,@MessageCode", TableName, Culture, MessageCode).ToList();
                MessageCaption objN = new MessageCaption();
                foreach (GetTableCaptionValue_Result Val in result)
               {
                   string name = Val.Name;
                   Caption = Val.Caption;
                    if(Caption=="")
                    {
                        Caption = Val.Name;
                    }
               }
            }
            catch
            {

            }
            return Caption;
        }



        public static string GetPageTitle(string TableNameParam)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var TableName = new SqlParameter("@Name", TableNameParam);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var result = entity.Database.SqlQuery<GetPageTitle_Result>("B_Ex_GetpageTitle_BizTbl_Table_SP @Culture,@Name", Culture, TableName).ToList();
                MessageCaption objN = new MessageCaption();
                foreach (GetPageTitle_Result Val in result)
                {
                    Caption = Val.ColumnName;
                    if (Caption == "")
                    {
                        Caption = Val.ColumnName;
                    }
                }
            }
            catch
            {

            }

            return Caption;
        }
        public static string GetPageNameFromBizPage(string TableNameParam)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string Caption = "";
            try
            {

                DBEntities entity = new DBEntities();
                var TableName = new SqlParameter("@Name", TableNameParam);
                var Culture = new SqlParameter("@Culture", CultureValue);
                var result = entity.Database.SqlQuery<GetPageNameValue_Result>("B_Ex_GetPageName_BizTbl_Page_SP @Culture,@Name", Culture, TableName).ToList();
                MessageCaption objN = new MessageCaption();
                foreach (GetPageNameValue_Result Val in result)
                {
                    Caption = Val.ColumnName;
                    if (Caption == "")
                    {
                        Caption = Val.ColumnName;
                    }
                }
            }
            catch
            {

            }

            return Caption;
        }

        public static string DynamicPageName(string PageName)
        {
            return (string)GetPageTitle(PageName);
        }

        public static string DynamicPageTitle(string PageName)
        {
            return (string)GetPageNameFromBizPage(PageName);
        }

        public static string BizTbl_MessageCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_Message");
        }
        public static string BizTbl_TableCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_Table");
        }
        public static string BizTbl_ErrorCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_Error");
        }
        public static string BizTbl_TableColumnCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_TableColumn");
        }
        public static string BizTbl_TableSecurityGroupRightCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_TableSecurityGroupRight");
        }
        public static string BizTbl_UserHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_UserHistory");
        }

        public static string TB_ReservationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Reservation");
        }

        public static string TB_ReservationHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ReservationHistory");
        }
        public static string TB_ReservationReviewCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ReservationReview");
        }

        public static string TB_ReservationReviewDetailCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ReservationReviewDetail");
        }
        public static string TB_TypeDepositCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeDeposit");
        }

        public static string TB_TypeFirmRequestCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeFirmRequest");
        }

        public static string TB_TypeFirmRequestStatusCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeFirmRequestStatus");
        }

        public static string TB_TypeFreebieCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeFreebie");
        }

        public static string TB_TypeHotelCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeHotel");
        }

        public static string TB_TypeHotelAccommodationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeHotelAccommodation");
        }

        public static string TB_TypeHotelBudgetCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeHotelBudget");
        }

        public static string TB_TypeHotelChainCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeHotelChain");
        }

        public static string TB_FAQCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_FAQ");
        }

        public static string TB_FirmCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Firm");
        }

        public static string TB_FirmHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_FirmHistory");
        }

        public static string BizTbl_SecurityGroupRightCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_SecurityGroupRight");
        }

        public static string TB_FirmRequestCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_FirmRequest");
        }
        public static string TB_HotelPromotionHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelPromotionHistory");
        }

        public static string TB_HotelRate(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRate");
        }


        public static string TB_TypeHotelClassCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeHotelClass");
        }

        public static string TB_TypeHotelSearchSortCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeHotelSearchSort");
        }

        public static string TB_TypeInvoiceStatusCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeInvoiceStatus");
        }

        public static string TB_TypeMessageStatusCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeMessageStatus");
        }

        public static string TB_TypeMessageSubjectCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeMessageSubject");
        }

        public static string TB_TypeMonthCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeMonth");
        }
        public static string TB_TypeOperationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeOperation");
        }

        public static string TB_TypePenaltyRateCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypePenaltyRate");
        }
        public static string TB_TypePricePolicyCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypePricePolicy");
        }
        public static string TB_TypeRegionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeRegion");
        }

        public static string TB_TypeReservationOperationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeReservationOperation");
        }

        public static string Tb_HotelRateHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "Tb_HotelRateHistory");
        }

        public static string TB_PhotoCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Photo");
        }


        public static string BizTbl_UserBusinessPartnerCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_UserBusinessPartner");
        }
        public static string BizTbl_UserHotelCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_UserHotel");
        }
        public static string BizTbl_UserOperationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_UserOperation");
        }
        public static string BizTbl_UserRightCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_UserRight");
        }
        public static string BizTbl_UserSessionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_UserSession");
        }

        public static string TB_HotelPromotionRoom(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelPromotionRoom");
        }

        public static string TB_HotelRegion(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRegion");
        }

        public static string TB_HotelRegionHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRegionHistory");
        }

        public static string TB_HotelReservation(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelReservation");
        }

        public static string TB_HotelReservationHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelReservationHistory");
        }


        public static string TB_TypeReservationStatusCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeReservationStatus");
        }
        public static string TB_TypeReviewCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeReview");
        }
        public static string TB_TypeReviewEvaluationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeReviewEvaluation");
        }
        public static string TB_TypeReviewScaleCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeReviewScale");
        }
        public static string TB_TypeReviewStatusCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeReviewStatus");
        }
        public static string TB_TypeRoomCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeRoom");
        }
        public static string TB_TypeSalutationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeSalutation");
        }
        public static string TB_TypeSmokingCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeSmoking");
        }
        public static string TB_TypeStatusCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeStatus");
        }
        public static string TB_TypeSubRegionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeSubRegion");
        }
        public static string TB_TypeTourFrequencyCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeTourFrequency");
        }
        public static string TB_TypeTravellerCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeTraveller");
        }
        public static string TB_TypeVehicleCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeVehicle");
        }
        public static string TB_TypeViewCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeView");
        }
        public static string TB_UnitCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Unit");
        }

        public static string TB_ReservationReviewDetailHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ReservationReviewDetailHistory");
        }

        public static string TB_ReservationReviewHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ReservationReviewHistory");
        }

        public static string TB_ReservationStatusHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ReservationStatusHistory");
        }

        public static string TB_AttributeCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Attribute");
        }
        public static string TB_AttributeHeaderCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_AttributeHeader");
        }

        public static string TB_HotelReservationPromotion(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelReservationPromotion");
        }

        public static string TB_HotelReservationRate(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelReservationRate");

        }

        public static string TB_HotelReservationRateHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelReservationRateHistory");

        }

        public static string TB_HotelRoom(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRoom");

        }

        public static string BizTbl_UserCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_User");
        }
        public static string TB_BankHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BankHistory");
        }

        public static string TB_ExchangeRateHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ExchangeRateHistory");
        }
        public static string TB_ExchangeRateCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ExchangeRate");
        }
        public static string TB_EmailListCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_EmailList");
        }

        public static string TB_HotelAttributeHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelAttributeHistory");
        }

        public static string TB_HotelAvailabilityHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelAvailabilityHistory");
        }

        public static string TB_HotelCancelPolicyHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelCancelPolicyHistory");
        }

        public static string TB_HotelComissionHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelComissionHistory");
        }

        public static string TB_HitCountCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HitCount");
        }

        public static string TB_HotelCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Hotel");
        }

        public static string TB_PeriodCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Period");
        }

        public static string TB_PartCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Part");
        }
        public static string TB_TransferCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Transfer");
        }

        public static string TB_TransferHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TransferHistory");
        }

        public static string TB_TransferPeriodCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TransferPeriod");
        }

        public static string TB_TransferReservationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TransferReservation");
        }

        public static string TB_TransferReservationHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TransferReservationHistory");
        }
        public static string TB_TransferReservationPromotionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TransferReservationPromotion");
        }

        public static string TB_DealReservationPromotionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_DealReservationPromotion");
        }
        public static string TB_DealReservationHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_DealReservationHistory");
        }

        public static string TB_DealReservationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_DealReservation");
        }
        public static string TB_DealHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_DealHistory");
        }

        public static string TB_DateCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Date");
        }
        public static string TB_CurrencyCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Currency");
        }
        public static string TB_HotelRoomAttributeHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRoomAttributeHistory");

        }
        public static string TB_HotelRoomAttribute(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRoomAttribute");

        }

        public static string TB_HotelRoomBed(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRoomBed");

        }

        public static string TB_HotelRoomBedHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRoomBedHistory");

        }
        public static string TB_MessageCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Message");
        }

        public static string TB_HotelCreditCardHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelCreditCardHistory");
        }

        public static string TB_FirmRequestHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_FirmRequestHistory");
        }

        public static string TB_HotelHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelHistory");
        }

        public static string TB_HotelCancelPolicyCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelCancelPolicy");
        }

        //===================
        public static string TB_TypeAttributeCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeAttribute");
        }
        public static string TB_TypeBedCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeBed");
        }
        public static string TB_TypeBusinessPartnerCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeBusinessPartner");
        }
        public static string TB_TypeCancelCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeCancel");
        }
        public static string TB_TypeCreditCardCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeCreditCard");
        }
        public static string TB_TypeDataCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeData");
        }
        public static string TB_TypeDayCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TypeDay");
        }
        public static string TB_TourReservationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TourReservation");
        }
        public static string TB_TourReservationHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TourReservationHistory");
        }
        public static string TB_TourReservationPromotionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TourReservationPromotion");
        }
        public static string TB_HotelRoomHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelRoomHistory");

        }

        public static string TB_HotelSearchParameter(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelSearchParameter");

        }

        public static string TB_Invoice(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Invoice");

        }
        public static string TB_InvoiceDetail(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_InvoiceDetail");

        }
        public static string TB_InvoiceDetailHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_InvoiceDetailHistory");

        }

        public static string TB_InvoiceHistory(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_InvoiceHistory");

        }
        public static string TB_IPToNation(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_IPToNation");

        }

        public static string TB_ChannelManagerCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_ChannelManager");
        }
        public static string TB_BusinessPartnerRegionHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BusinessPartnerRegionHistory");
        }
        public static string TB_BusinessPartnerRegionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BusinessPartnerRegion");
        }
        public static string TB_BusinessPartnerPaxHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BusinessPartnerPaxHistory");
        }

        public static string TB_BusinessPartnerPaxCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BusinessPartnerPax");
        }
        public static string TB_BusinessPartnerHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BusinessPartnerHistory");
        }
        public static string TB_BusinessPartnerCancelPolicyHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BusinessPartnerCancelPolicyHistory");
        }

        public static string TB_TourCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Tour");
        }

        public static string TB_TourAttributeCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TourAttribute");
        }
        public static string TB_BusinessPartnerCancelPolicyCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_BusinessPartnerCancelPolicy");
        }
        public static string TB_HotelComissionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelComission");
        }

        public static string TB_HotelCreditCardCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelCreditCard");
        }

        public static string TB_HotelMinumumAccommodationCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelMinumumAccommodation");
        }
        public static string TB_DealCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Deal");
        }
        public static string TB_HotelSearch(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelSearch");

        }

        public static string TB_HotelPromotion(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelPromotion");

        }
        public static string TB_HotelAvailabilityCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelAvailability");

        }

        public static string TB_MessengerCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Messenger");
        }

        public static string TB_TourRegionHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TourRegionHistory");
        }

        public static string TB_TourRegionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TourRegion");
        }



        public static string TB_TourHistoryCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_TourHistory");
        }

        public static string TB_RegionCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_Region");
        }

        public static string TB_PromotionCaption(string Text)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (Text == "Name")
            {
                Text = Text + "_" + CultureValue;
            }
            else if (Text == "Description")
            {
                Text = Text + "_" + CultureValue;
            }
            return (string)GetMEssageTableCaptions(Text, "TB_Promotion");
        }

        public static string TB_HotelAttributeCaption(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelAttribute");
        }
        //public static string Description_tr
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_tr", "BizTbl_Message");
        //    }
        //}
        //public static string Description_de
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_de", "BizTbl_Message");
        //    }
        //}
        //public static string Description_es
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_es", "BizTbl_Message");
        //    }
        //}

        //public static string Description_fr
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_fr", "BizTbl_Message");
        //    }
        //}
        //public static string Description_ru
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_ru", "BizTbl_Message");
        //    }
        //}
        //public static string Description_it
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_it", "BizTbl_Message");
        //    }
        //}
        //public static string Description_ar
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_ar", "BizTbl_Message");
        //    }

        //}
        //public static string Description_pt
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_pt", "BizTbl_Message");
        //    }
        //}
        //public static string Description_ja
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_ja", "BizTbl_Message");
        //    }
        //}
        //public static string Description_zh
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Description_zh", "BizTbl_Message");
        //    }
        //}
        //public static string Common
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("IsCommon", "BizTbl_Message");
        //    }
        //}

        #region TB_HitCount Captions

        public static string PartID
        {
            get
            {
                return (string)GetMEssageTableCaptions("PartID", "TB_HitCount");
            }
        }
        public static string Date
        {
            get
            {
                return (string)GetMEssageTableCaptions("Date", "TB_HitCount");
            }
        }
        public static string Description
        {
            get
            {
                return (string)GetMEssageTableCaptions("Description", "TB_HitCount");
            }
        }
        public static string HitCount
        {
            get
            {
                return (string)GetMEssageTableCaptions("HitCount", "TB_HitCount");
            }
        }
        public static string RecordID
        {
            get
            {
                return (string)GetMEssageTableCaptions("RecordID", "TB_HitCount");
            }
        }
        #endregion

        #region BizTbl_UserOperation Captions
        public static string DateUP
        {
            get
            {
                return (string)GetMEssageTableCaptions("Date", "BizTbl_UserOperation");
            }
        }

        public static string IPAddressUP
        {
            get
            {
                return (string)GetMEssageTableCaptions("IPAddress", "BizTbl_UserOperation");
            }
        }

        public static string PartIDUP
        {
            get
            {
                return (string)GetMEssageTableCaptions("PartID", "BizTbl_UserOperation");
            }
        }

        public static string OperationTypeIDUP
        {
            get
            {
                return (string)GetMEssageTableCaptions("OperationTypeID", "BizTbl_UserOperation");
            }
        }

        public static string RecordIDUP
        {
            get
            {
                return (string)GetMEssageTableCaptions("RecordID", "BizTbl_UserOperation");
            }
        }

        public static string UserIDUP
        {
            get
            {
                return (string)GetMEssageTableCaptions("UserID", "BizTbl_UserOperation");
            }
        }

        public static string UserSessionIDUP
        {
            get
            {
                return (string)GetMEssageTableCaptions("UserSessionID", "BizTbl_UserOperation");
            }
        }


        #endregion

        #region TB_HotelSearchParameter Caption

        public static string CheckInDateHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("CheckInDate", "TB_HotelSearchParameter");
            }
        }

        public static string CheckOutDateHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("CheckOutDate", "TB_HotelSearchParameter");
            }
        }

        public static string CityIDHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("CityID", "TB_HotelSearchParameter");
            }
        }

        public static string CountryIDHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("CountryID", "TB_HotelSearchParameter");
            }
        }
        public static string CultureHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("Culture", "TB_HotelSearchParameter");
            }
        }

        public static string DateHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("Date", "TB_HotelSearchParameter");
            }
        }

        public static string GuestCountHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("GuestCount", "TB_HotelSearchParameter");
            }
        }

        public static string LowerUSDPriceHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("LowerUSDPrice", "TB_HotelSearchParameter");
            }
        }

        public static string RegionIDsHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("RegionIDs", "TB_HotelSearchParameter");
            }
        }

        public static string RegionNamesHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("RegionNames", "TB_HotelSearchParameter");
            }
        }
        public static string RoomCountHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("RoomCount", "TB_HotelSearchParameter");
            }
        }
        public static string UpperUSDPriceHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("UpperUSDPrice", "TB_HotelSearchParameter");
            }
        }
        public static string UserCountryIDHSP
        {
            get
            {
                return (string)GetMEssageTableCaptions("UserCountryID", "TB_HotelSearchParameter");
            }
        }
      
        #endregion

        public static string LogDate
        {
            get
            {
                return (string)GetMEssageTableCaptions("LogDateTime", "TB_FirmHistory");
            }
        }

        public static string UserSessionId
        {
            get
            {
                return (string)GetMEssageTableCaptions("ID", "BizTbl_UserSession");
            }
        }
        //public static string Date
        //{
        //    get
        //    {
        //        return (string)GetMEssageTableCaptions("Date", "BizTbl_UserOperation");
        //    }
        //}
        public static string OperationType
        {
            get
            {
                return (string)GetMEssageTableCaptions("Name_en", "TB_TypeOperation");
            }
        }
        public static string Record
        {
            get
            {
                return (string)GetMEssageTableCaptions("RecordID", "BizTbl_UserOperation");
            }
        }
        public static string BusinessPartnerCancelPolicyID
        {
            get
            {
                return (string)GetMEssageTableCaptions("BusinessPartnerCancelPolicyID", "TB_BusinessPartnerCancelPolicyHistory");
            }
        }
        public static string PaxID
        {
            get
            {
                return (string)GetMEssageTableCaptions("BusinessPartnerPaxID", "TB_BusinessPartnerPaxHistory");
            }
        }
        public static string BusinessPartnerRegionID
        {
            get
            {
                return (string)GetMEssageTableCaptions("BusinessPartnerID", "TB_BusinessPartnerRegion");
            }
        }

        public static string BizTbl_MailTemplate(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "BizTbl_MailTemplate");
        }

        public static string TB_HotelPromotionFreebie(string Text)
        {
            return (string)GetMEssageTableCaptions(Text, "TB_HotelPromotionFreebie");

        }
    }
}