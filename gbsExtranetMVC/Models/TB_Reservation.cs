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
    
    public partial class TB_Reservation
    {
        public TB_Reservation()
        {
            this.TB_FirmRequest = new HashSet<TB_FirmRequest>();
            this.TB_HotelReservation = new HashSet<TB_HotelReservation>();
            this.TB_ReservationStatusHistory = new HashSet<TB_ReservationStatusHistory>();
            this.TB_ReservationReview = new HashSet<TB_ReservationReview>();
            this.TB_DealReservation = new HashSet<TB_DealReservation>();
            this.TB_TourReservation = new HashSet<TB_TourReservation>();
            this.TB_TransferReservation = new HashSet<TB_TransferReservation>();
        }
    
        public long ID { get; set; }
        public string PinCode { get; set; }
        public Nullable<int> PartID { get; set; }
        public Nullable<int> FirmID { get; set; }
        public long UserID { get; set; }
        public System.DateTime ReservationDate { get; set; }
        public int StatusID { get; set; }
        public int CountryID { get; set; }
        public Nullable<int> VAT { get; set; }
        public string City { get; set; }
        public Nullable<int> SalutationTypeID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public decimal GeneralPromotionDiscountPercentage { get; set; }
        public decimal PromotionDiscountPercentage { get; set; }
        public decimal PayableAmount { get; set; }
        public bool ActualAmount { get; set; }
        public int CurrencyID { get; set; }
        public Nullable<short> ComissionRate { get; set; }
        public Nullable<decimal> ComissionAmount { get; set; }
        public Nullable<int> ComissionCurrencyID { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<int> DepositCurrencyID { get; set; }
        public Nullable<decimal> DepositInTL { get; set; }
        public string Note { get; set; }
        public bool CreditCardUsed { get; set; }
        public Nullable<int> CCTypeID { get; set; }
        public string CCFullName { get; set; }
        public string CCNo { get; set; }
        public string CCExpiration { get; set; }
        public string CCCVC { get; set; }
        public Nullable<int> ReservationOperationID { get; set; }
        public Nullable<decimal> ChargedAmount { get; set; }
        public Nullable<int> ChargedAmountCurrencyID { get; set; }
        public Nullable<System.DateTime> ChargeDate { get; set; }
        public bool Active { get; set; }
        public int CultureID { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> CancelDateTime { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public Nullable<long> CreateUserID { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
        public string EncReservationID { get; set; }
        public string EncPinCode { get; set; }
        public Nullable<long> UserSessionID { get; set; }
    
        public virtual BizTbl_Culture BizTbl_Culture { get; set; }
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual BizTbl_User BizTbl_User2 { get; set; }
        public virtual BizTbl_UserSession BizTbl_UserSession { get; set; }
        public virtual TB_Country TB_Country { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual TB_Currency TB_Currency1 { get; set; }
        public virtual TB_Currency TB_Currency2 { get; set; }
        public virtual TB_Currency TB_Currency3 { get; set; }
        public virtual TB_Firm TB_Firm { get; set; }
        public virtual ICollection<TB_FirmRequest> TB_FirmRequest { get; set; }
        public virtual ICollection<TB_HotelReservation> TB_HotelReservation { get; set; }
        public virtual TB_Part TB_Part { get; set; }
        public virtual TB_TypeReservationStatus TB_TypeReservationStatus { get; set; }
        public virtual TB_Reservation TB_Reservation1 { get; set; }
        public virtual TB_Reservation TB_Reservation2 { get; set; }
        public virtual TB_Reservation TB_Reservation11 { get; set; }
        public virtual TB_Reservation TB_Reservation3 { get; set; }
        public virtual TB_TypeCreditCard TB_TypeCreditCard { get; set; }
        public virtual TB_TypeReservationOperation TB_TypeReservationOperation { get; set; }
        public virtual TB_TypeSalutation TB_TypeSalutation { get; set; }
        public virtual ICollection<TB_ReservationStatusHistory> TB_ReservationStatusHistory { get; set; }
        public virtual ICollection<TB_ReservationReview> TB_ReservationReview { get; set; }
        public virtual ICollection<TB_DealReservation> TB_DealReservation { get; set; }
        public virtual ICollection<TB_TourReservation> TB_TourReservation { get; set; }
        public virtual ICollection<TB_TransferReservation> TB_TransferReservation { get; set; }
    }
}