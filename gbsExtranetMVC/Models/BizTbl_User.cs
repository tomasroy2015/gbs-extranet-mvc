//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gbsExtranetMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BizTbl_User
    {
        public BizTbl_User()
        {
            this.BizTbl_Culture = new HashSet<BizTbl_Culture>();
            this.BizTbl_MailQueue = new HashSet<BizTbl_MailQueue>();
            this.BizTbl_MailTemplate = new HashSet<BizTbl_MailTemplate>();
            this.BizTbl_Message = new HashSet<BizTbl_Message>();
            this.BizTbl_Page = new HashSet<BizTbl_Page>();
            this.BizTbl_PageControl = new HashSet<BizTbl_PageControl>();
            this.BizTbl_Parameter = new HashSet<BizTbl_Parameter>();
            this.BizTbl_Security = new HashSet<BizTbl_Security>();
            this.BizTbl_SecurityGroup = new HashSet<BizTbl_SecurityGroup>();
            this.BizTbl_SecurityGroupRight = new HashSet<BizTbl_SecurityGroupRight>();
            this.BizTbl_User1 = new HashSet<BizTbl_User>();
            this.BizTbl_User11 = new HashSet<BizTbl_User>();
            this.TB_Bank = new HashSet<TB_Bank>();
            this.TB_Country1 = new HashSet<TB_Country>();
            this.TB_Region2 = new HashSet<TB_Region>();
            this.TB_TypeSalutation1 = new HashSet<TB_TypeSalutation>();
            this.BizTbl_Table = new HashSet<BizTbl_Table>();
            this.BizTbl_TableColumn = new HashSet<BizTbl_TableColumn>();
            this.BizTbl_TableSecurityGroupRight = new HashSet<BizTbl_TableSecurityGroupRight>();
            this.TB_FAQ = new HashSet<TB_FAQ>();
            this.TB_Firm1 = new HashSet<TB_Firm>();
            this.TB_Firm2 = new HashSet<TB_Firm>();
            this.TB_TypeDeposit = new HashSet<TB_TypeDeposit>();
            this.TB_TypeFirmRequest = new HashSet<TB_TypeFirmRequest>();
            this.TB_TypeFirmRequestStatus = new HashSet<TB_TypeFirmRequestStatus>();
            this.TB_TypeFreebie = new HashSet<TB_TypeFreebie>();
            this.TB_TypeHotel = new HashSet<TB_TypeHotel>();
            this.TB_TypeHotelAccommodation = new HashSet<TB_TypeHotelAccommodation>();
            this.TB_TypeHotelBudget = new HashSet<TB_TypeHotelBudget>();
            this.TB_TypeHotelChain = new HashSet<TB_TypeHotelChain>();
            this.TB_TypeHotelClass = new HashSet<TB_TypeHotelClass>();
            this.TB_HotelRate = new HashSet<TB_HotelRate>();
            this.TB_HotelRate1 = new HashSet<TB_HotelRate>();
            this.TB_HotelPromotionFreebie = new HashSet<TB_HotelPromotionFreebie>();
            this.TB_HotelPromotionFreebie1 = new HashSet<TB_HotelPromotionFreebie>();
            this.TB_HotelPromotionHistory = new HashSet<TB_HotelPromotionHistory>();
            this.TB_HotelPromotionHistory1 = new HashSet<TB_HotelPromotionHistory>();
            this.TB_HotelPromotionHistory2 = new HashSet<TB_HotelPromotionHistory>();
            this.TB_HotelRate2 = new HashSet<TB_HotelRate>();
            this.TB_TypeHotelSearchSort = new HashSet<TB_TypeHotelSearchSort>();
            this.TB_TypeInvoiceStatus = new HashSet<TB_TypeInvoiceStatus>();
            this.TB_TypeMessageStatus = new HashSet<TB_TypeMessageStatus>();
            this.TB_TypeMessageSubject = new HashSet<TB_TypeMessageSubject>();
            this.TB_TypeMonth = new HashSet<TB_TypeMonth>();
            this.TB_TypeOperation = new HashSet<TB_TypeOperation>();
            this.TB_TypePenaltyRate = new HashSet<TB_TypePenaltyRate>();
            this.TB_TypePricePolicy = new HashSet<TB_TypePricePolicy>();
            this.TB_TypeReservationOperation = new HashSet<TB_TypeReservationOperation>();
            this.TB_TypeRegion = new HashSet<TB_TypeRegion>();
            this.TB_Photo = new HashSet<TB_Photo>();
            this.TB_Photo1 = new HashSet<TB_Photo>();
            this.TB_TypeStatus1 = new HashSet<TB_TypeStatus>();
            this.BizTbl_UserSession = new HashSet<BizTbl_UserSession>();
            this.TB_FirmRequest = new HashSet<TB_FirmRequest>();
            this.TB_FirmRequest1 = new HashSet<TB_FirmRequest>();
            this.TB_HotelPromotion = new HashSet<TB_HotelPromotion>();
            this.TB_HotelPromotion1 = new HashSet<TB_HotelPromotion>();
            this.TB_HotelPromotionRoom = new HashSet<TB_HotelPromotionRoom>();
            this.TB_HotelPromotionRoom1 = new HashSet<TB_HotelPromotionRoom>();
            this.TB_HotelRegion = new HashSet<TB_HotelRegion>();
            this.TB_TypeReviewStatus = new HashSet<TB_TypeReviewStatus>();
            this.TB_TypeReservationStatus = new HashSet<TB_TypeReservationStatus>();
            this.TB_TypeReview = new HashSet<TB_TypeReview>();
            this.TB_TypeReviewEvaluation = new HashSet<TB_TypeReviewEvaluation>();
            this.TB_TypeReviewScale = new HashSet<TB_TypeReviewScale>();
            this.TB_TypeRoom = new HashSet<TB_TypeRoom>();
            this.TB_TypeSmoking = new HashSet<TB_TypeSmoking>();
            this.TB_TypeSubRegion = new HashSet<TB_TypeSubRegion>();
            this.TB_TypeTourFrequency = new HashSet<TB_TypeTourFrequency>();
            this.TB_TypeTraveller = new HashSet<TB_TypeTraveller>();
            this.TB_TypeVehicle = new HashSet<TB_TypeVehicle>();
            this.TB_TypeView = new HashSet<TB_TypeView>();
            this.TB_Unit = new HashSet<TB_Unit>();
            this.TB_Attribute = new HashSet<TB_Attribute>();
            this.TB_AttributeHeader = new HashSet<TB_AttributeHeader>();
            this.TB_Transfer = new HashSet<TB_Transfer>();
            this.TB_Transfer1 = new HashSet<TB_Transfer>();
            this.TB_ExchangeRate = new HashSet<TB_ExchangeRate>();
            this.TB_Period = new HashSet<TB_Period>();
            this.TB_TransferPeriod = new HashSet<TB_TransferPeriod>();
            this.TB_Message = new HashSet<TB_Message>();
            this.TB_Currency = new HashSet<TB_Currency>();
            this.TB_HotelRoom = new HashSet<TB_HotelRoom>();
            this.TB_HotelRoom1 = new HashSet<TB_HotelRoom>();
            this.TB_HotelRoomAttribute = new HashSet<TB_HotelRoomAttribute>();
            this.TB_HotelRoomBed = new HashSet<TB_HotelRoomBed>();
            this.TB_TypeAttribute = new HashSet<TB_TypeAttribute>();
            this.TB_TypeBed = new HashSet<TB_TypeBed>();
            this.TB_TypeBusinessPartner = new HashSet<TB_TypeBusinessPartner>();
            this.TB_TypeCancel = new HashSet<TB_TypeCancel>();
            this.TB_TypeCreditCard = new HashSet<TB_TypeCreditCard>();
            this.TB_TypeDay = new HashSet<TB_TypeDay>();
            this.TB_BusinessPartnerPax = new HashSet<TB_BusinessPartnerPax>();
            this.TB_BusinessPartnerRegion = new HashSet<TB_BusinessPartnerRegion>();
            this.TB_ChannelManager = new HashSet<TB_ChannelManager>();
            this.TB_Tour = new HashSet<TB_Tour>();
            this.TB_Tour1 = new HashSet<TB_Tour>();
            this.TB_BusinessPartnerCancelPolicy = new HashSet<TB_BusinessPartnerCancelPolicy>();
            this.TB_HotelCancelPolicy = new HashSet<TB_HotelCancelPolicy>();
            this.TB_HotelComission = new HashSet<TB_HotelComission>();
            this.TB_HotelCreditCard = new HashSet<TB_HotelCreditCard>();
            this.TB_HotelMinumumAccommodation = new HashSet<TB_HotelMinumumAccommodation>();
            this.TB_BusinessPartner = new HashSet<TB_BusinessPartner>();
            this.TB_BusinessPartner1 = new HashSet<TB_BusinessPartner>();
            this.TB_Deal = new HashSet<TB_Deal>();
            this.TB_Deal1 = new HashSet<TB_Deal>();
            this.TB_HotelAvailability = new HashSet<TB_HotelAvailability>();
            this.TB_TourRegion = new HashSet<TB_TourRegion>();
            this.TB_TourAttribute = new HashSet<TB_TourAttribute>();
            this.TB_Promotion = new HashSet<TB_Promotion>();
            this.TB_HotelAttribute = new HashSet<TB_HotelAttribute>();
            this.TB_Hotel = new HashSet<TB_Hotel>();
            this.TB_Hotel1 = new HashSet<TB_Hotel>();
            this.TB_Invoice = new HashSet<TB_Invoice>();
            this.TB_ReservationReview = new HashSet<TB_ReservationReview>();
            this.BizTbl_UserOperation = new HashSet<BizTbl_UserOperation>();
            this.TB_HotelReservation = new HashSet<TB_HotelReservation>();
            this.TB_Reservation = new HashSet<TB_Reservation>();
            this.TB_Reservation1 = new HashSet<TB_Reservation>();
            this.TB_Reservation2 = new HashSet<TB_Reservation>();
            this.TB_ReservationStatusHistory = new HashSet<TB_ReservationStatusHistory>();
            this.BizTbl_UserRight = new HashSet<BizTbl_UserRight>();
            this.BizTbl_UserRight1 = new HashSet<BizTbl_UserRight>();
            this.TB_DealReservation = new HashSet<TB_DealReservation>();
            this.TB_HotelReservationRate = new HashSet<TB_HotelReservationRate>();
            this.TB_TourReservation = new HashSet<TB_TourReservation>();
            this.TB_TransferReservation = new HashSet<TB_TransferReservation>();
        }
    
        public long ID { get; set; }
        public Nullable<int> SalutationTypeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<long> RegionID { get; set; }
        public Nullable<long> CityID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> FirmID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<bool> PromotionalEmail { get; set; }
        public string VerificationCode { get; set; }
        public string DisplayName { get; set; }
        public Nullable<bool> Genius { get; set; }
        public Nullable<bool> Locked { get; set; }
        public Nullable<bool> Active { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public long CreateUserID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string IPAddress { get; set; }
        public string Userphoto { get; set; }
        public string Country { get; set; }
        public string GoogleProfileID { get; set; }
        public string FacebookProfileID { get; set; }
    
        public virtual ICollection<BizTbl_Culture> BizTbl_Culture { get; set; }
        public virtual ICollection<BizTbl_MailQueue> BizTbl_MailQueue { get; set; }
        public virtual ICollection<BizTbl_MailTemplate> BizTbl_MailTemplate { get; set; }
        public virtual ICollection<BizTbl_Message> BizTbl_Message { get; set; }
        public virtual ICollection<BizTbl_Page> BizTbl_Page { get; set; }
        public virtual ICollection<BizTbl_PageControl> BizTbl_PageControl { get; set; }
        public virtual ICollection<BizTbl_Parameter> BizTbl_Parameter { get; set; }
        public virtual ICollection<BizTbl_Security> BizTbl_Security { get; set; }
        public virtual ICollection<BizTbl_SecurityGroup> BizTbl_SecurityGroup { get; set; }
        public virtual ICollection<BizTbl_SecurityGroupRight> BizTbl_SecurityGroupRight { get; set; }
        public virtual ICollection<BizTbl_User> BizTbl_User1 { get; set; }
        public virtual BizTbl_User BizTbl_User2 { get; set; }
        public virtual ICollection<BizTbl_User> BizTbl_User11 { get; set; }
        public virtual BizTbl_User BizTbl_User3 { get; set; }
        public virtual TB_Country TB_Country { get; set; }
        public virtual TB_Region TB_Region { get; set; }
        public virtual TB_Region TB_Region1 { get; set; }
        public virtual TB_TypeSalutation TB_TypeSalutation { get; set; }
        public virtual ICollection<TB_Bank> TB_Bank { get; set; }
        public virtual ICollection<TB_Country> TB_Country1 { get; set; }
        public virtual ICollection<TB_Region> TB_Region2 { get; set; }
        public virtual ICollection<TB_TypeSalutation> TB_TypeSalutation1 { get; set; }
        public virtual ICollection<BizTbl_Table> BizTbl_Table { get; set; }
        public virtual ICollection<BizTbl_TableColumn> BizTbl_TableColumn { get; set; }
        public virtual ICollection<BizTbl_TableSecurityGroupRight> BizTbl_TableSecurityGroupRight { get; set; }
        public virtual TB_Firm TB_Firm { get; set; }
        public virtual ICollection<TB_FAQ> TB_FAQ { get; set; }
        public virtual ICollection<TB_Firm> TB_Firm1 { get; set; }
        public virtual ICollection<TB_Firm> TB_Firm2 { get; set; }
        public virtual ICollection<TB_TypeDeposit> TB_TypeDeposit { get; set; }
        public virtual ICollection<TB_TypeFirmRequest> TB_TypeFirmRequest { get; set; }
        public virtual ICollection<TB_TypeFirmRequestStatus> TB_TypeFirmRequestStatus { get; set; }
        public virtual ICollection<TB_TypeFreebie> TB_TypeFreebie { get; set; }
        public virtual ICollection<TB_TypeHotel> TB_TypeHotel { get; set; }
        public virtual ICollection<TB_TypeHotelAccommodation> TB_TypeHotelAccommodation { get; set; }
        public virtual ICollection<TB_TypeHotelBudget> TB_TypeHotelBudget { get; set; }
        public virtual ICollection<TB_TypeHotelChain> TB_TypeHotelChain { get; set; }
        public virtual ICollection<TB_TypeHotelClass> TB_TypeHotelClass { get; set; }
        public virtual ICollection<TB_HotelRate> TB_HotelRate { get; set; }
        public virtual ICollection<TB_HotelRate> TB_HotelRate1 { get; set; }
        public virtual ICollection<TB_HotelPromotionFreebie> TB_HotelPromotionFreebie { get; set; }
        public virtual ICollection<TB_HotelPromotionFreebie> TB_HotelPromotionFreebie1 { get; set; }
        public virtual ICollection<TB_HotelPromotionHistory> TB_HotelPromotionHistory { get; set; }
        public virtual ICollection<TB_HotelPromotionHistory> TB_HotelPromotionHistory1 { get; set; }
        public virtual ICollection<TB_HotelPromotionHistory> TB_HotelPromotionHistory2 { get; set; }
        public virtual ICollection<TB_HotelRate> TB_HotelRate2 { get; set; }
        public virtual ICollection<TB_TypeHotelSearchSort> TB_TypeHotelSearchSort { get; set; }
        public virtual ICollection<TB_TypeInvoiceStatus> TB_TypeInvoiceStatus { get; set; }
        public virtual ICollection<TB_TypeMessageStatus> TB_TypeMessageStatus { get; set; }
        public virtual ICollection<TB_TypeMessageSubject> TB_TypeMessageSubject { get; set; }
        public virtual ICollection<TB_TypeMonth> TB_TypeMonth { get; set; }
        public virtual ICollection<TB_TypeOperation> TB_TypeOperation { get; set; }
        public virtual ICollection<TB_TypePenaltyRate> TB_TypePenaltyRate { get; set; }
        public virtual ICollection<TB_TypePricePolicy> TB_TypePricePolicy { get; set; }
        public virtual ICollection<TB_TypeReservationOperation> TB_TypeReservationOperation { get; set; }
        public virtual ICollection<TB_TypeRegion> TB_TypeRegion { get; set; }
        public virtual ICollection<TB_Photo> TB_Photo { get; set; }
        public virtual ICollection<TB_Photo> TB_Photo1 { get; set; }
        public virtual TB_TypeStatus TB_TypeStatus { get; set; }
        public virtual ICollection<TB_TypeStatus> TB_TypeStatus1 { get; set; }
        public virtual ICollection<BizTbl_UserSession> BizTbl_UserSession { get; set; }
        public virtual ICollection<TB_FirmRequest> TB_FirmRequest { get; set; }
        public virtual ICollection<TB_FirmRequest> TB_FirmRequest1 { get; set; }
        public virtual ICollection<TB_HotelPromotion> TB_HotelPromotion { get; set; }
        public virtual ICollection<TB_HotelPromotion> TB_HotelPromotion1 { get; set; }
        public virtual ICollection<TB_HotelPromotionRoom> TB_HotelPromotionRoom { get; set; }
        public virtual ICollection<TB_HotelPromotionRoom> TB_HotelPromotionRoom1 { get; set; }
        public virtual ICollection<TB_HotelRegion> TB_HotelRegion { get; set; }
        public virtual ICollection<TB_TypeReviewStatus> TB_TypeReviewStatus { get; set; }
        public virtual ICollection<TB_TypeReservationStatus> TB_TypeReservationStatus { get; set; }
        public virtual ICollection<TB_TypeReview> TB_TypeReview { get; set; }
        public virtual ICollection<TB_TypeReviewEvaluation> TB_TypeReviewEvaluation { get; set; }
        public virtual ICollection<TB_TypeReviewScale> TB_TypeReviewScale { get; set; }
        public virtual ICollection<TB_TypeRoom> TB_TypeRoom { get; set; }
        public virtual ICollection<TB_TypeSmoking> TB_TypeSmoking { get; set; }
        public virtual ICollection<TB_TypeSubRegion> TB_TypeSubRegion { get; set; }
        public virtual ICollection<TB_TypeTourFrequency> TB_TypeTourFrequency { get; set; }
        public virtual ICollection<TB_TypeTraveller> TB_TypeTraveller { get; set; }
        public virtual ICollection<TB_TypeVehicle> TB_TypeVehicle { get; set; }
        public virtual ICollection<TB_TypeView> TB_TypeView { get; set; }
        public virtual ICollection<TB_Unit> TB_Unit { get; set; }
        public virtual ICollection<TB_Attribute> TB_Attribute { get; set; }
        public virtual ICollection<TB_AttributeHeader> TB_AttributeHeader { get; set; }
        public virtual ICollection<TB_Transfer> TB_Transfer { get; set; }
        public virtual ICollection<TB_Transfer> TB_Transfer1 { get; set; }
        public virtual ICollection<TB_ExchangeRate> TB_ExchangeRate { get; set; }
        public virtual ICollection<TB_Period> TB_Period { get; set; }
        public virtual ICollection<TB_TransferPeriod> TB_TransferPeriod { get; set; }
        public virtual ICollection<TB_Message> TB_Message { get; set; }
        public virtual ICollection<TB_Currency> TB_Currency { get; set; }
        public virtual ICollection<TB_HotelRoom> TB_HotelRoom { get; set; }
        public virtual ICollection<TB_HotelRoom> TB_HotelRoom1 { get; set; }
        public virtual ICollection<TB_HotelRoomAttribute> TB_HotelRoomAttribute { get; set; }
        public virtual ICollection<TB_HotelRoomBed> TB_HotelRoomBed { get; set; }
        public virtual ICollection<TB_TypeAttribute> TB_TypeAttribute { get; set; }
        public virtual ICollection<TB_TypeBed> TB_TypeBed { get; set; }
        public virtual ICollection<TB_TypeBusinessPartner> TB_TypeBusinessPartner { get; set; }
        public virtual ICollection<TB_TypeCancel> TB_TypeCancel { get; set; }
        public virtual ICollection<TB_TypeCreditCard> TB_TypeCreditCard { get; set; }
        public virtual ICollection<TB_TypeDay> TB_TypeDay { get; set; }
        public virtual ICollection<TB_BusinessPartnerPax> TB_BusinessPartnerPax { get; set; }
        public virtual ICollection<TB_BusinessPartnerRegion> TB_BusinessPartnerRegion { get; set; }
        public virtual ICollection<TB_ChannelManager> TB_ChannelManager { get; set; }
        public virtual ICollection<TB_Tour> TB_Tour { get; set; }
        public virtual ICollection<TB_Tour> TB_Tour1 { get; set; }
        public virtual ICollection<TB_BusinessPartnerCancelPolicy> TB_BusinessPartnerCancelPolicy { get; set; }
        public virtual ICollection<TB_HotelCancelPolicy> TB_HotelCancelPolicy { get; set; }
        public virtual ICollection<TB_HotelComission> TB_HotelComission { get; set; }
        public virtual ICollection<TB_HotelCreditCard> TB_HotelCreditCard { get; set; }
        public virtual ICollection<TB_HotelMinumumAccommodation> TB_HotelMinumumAccommodation { get; set; }
        public virtual ICollection<TB_BusinessPartner> TB_BusinessPartner { get; set; }
        public virtual ICollection<TB_BusinessPartner> TB_BusinessPartner1 { get; set; }
        public virtual ICollection<TB_Deal> TB_Deal { get; set; }
        public virtual ICollection<TB_Deal> TB_Deal1 { get; set; }
        public virtual ICollection<TB_HotelAvailability> TB_HotelAvailability { get; set; }
        public virtual ICollection<TB_TourRegion> TB_TourRegion { get; set; }
        public virtual ICollection<TB_TourAttribute> TB_TourAttribute { get; set; }
        public virtual ICollection<TB_Promotion> TB_Promotion { get; set; }
        public virtual ICollection<TB_HotelAttribute> TB_HotelAttribute { get; set; }
        public virtual ICollection<TB_Hotel> TB_Hotel { get; set; }
        public virtual ICollection<TB_Hotel> TB_Hotel1 { get; set; }
        public virtual ICollection<TB_Invoice> TB_Invoice { get; set; }
        public virtual ICollection<TB_ReservationReview> TB_ReservationReview { get; set; }
        public virtual ICollection<BizTbl_UserOperation> BizTbl_UserOperation { get; set; }
        public virtual ICollection<TB_HotelReservation> TB_HotelReservation { get; set; }
        public virtual ICollection<TB_Reservation> TB_Reservation { get; set; }
        public virtual ICollection<TB_Reservation> TB_Reservation1 { get; set; }
        public virtual ICollection<TB_Reservation> TB_Reservation2 { get; set; }
        public virtual ICollection<TB_ReservationStatusHistory> TB_ReservationStatusHistory { get; set; }
        public virtual ICollection<BizTbl_UserRight> BizTbl_UserRight { get; set; }
        public virtual ICollection<BizTbl_UserRight> BizTbl_UserRight1 { get; set; }
        public virtual ICollection<TB_DealReservation> TB_DealReservation { get; set; }
        public virtual ICollection<TB_HotelReservationRate> TB_HotelReservationRate { get; set; }
        public virtual ICollection<TB_TourReservation> TB_TourReservation { get; set; }
        public virtual ICollection<TB_TransferReservation> TB_TransferReservation { get; set; }
    }
}
