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
    
    public partial class TB_BusinessPartner
    {
        public TB_BusinessPartner()
        {
            this.TB_BusinessPartnerCancelPolicy = new HashSet<TB_BusinessPartnerCancelPolicy>();
            this.TB_BusinessPartnerPax = new HashSet<TB_BusinessPartnerPax>();
            this.TB_BusinessPartnerRegion = new HashSet<TB_BusinessPartnerRegion>();
            this.TB_Tour = new HashSet<TB_Tour>();
            this.TB_Transfer = new HashSet<TB_Transfer>();
            this.TB_TransferPeriod = new HashSet<TB_TransferPeriod>();
            this.TB_Deal = new HashSet<TB_Deal>();
        }
    
        public int ID { get; set; }
        public int BusinessPartnerTypeID { get; set; }
        public int FirmID { get; set; }
        public int CountryID { get; set; }
        public Nullable<long> RegionID { get; set; }
        public Nullable<long> CityID { get; set; }
        public string Name { get; set; }
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_es { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_it { get; set; }
        public string Description_ar { get; set; }
        public string Description_ja { get; set; }
        public string Description_pt { get; set; }
        public string Description_zh { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public string WebAddress { get; set; }
        public string Email { get; set; }
        public Nullable<int> TransferCostCurrencyID { get; set; }
        public Nullable<int> TransferCurrencyID { get; set; }
        public Nullable<int> TransferDepositTypeID { get; set; }
        public Nullable<int> TourCostCurrencyID { get; set; }
        public Nullable<int> TourCurrencyID { get; set; }
        public Nullable<int> TourDepositTypeID { get; set; }
        public Nullable<int> DealCostCurrencyID { get; set; }
        public Nullable<int> DealCurrencyID { get; set; }
        public Nullable<int> DealDepositTypeID { get; set; }
        public Nullable<int> TransferContactPersonSalutationID { get; set; }
        public string TransferContactPersonFullName { get; set; }
        public string TransferContactPersonPhone { get; set; }
        public Nullable<int> TourContactPersonSalutationID { get; set; }
        public string TourContactPersonFullName { get; set; }
        public string TourContactPersonPhone { get; set; }
        public Nullable<int> DealContactPersonSalutationID { get; set; }
        public string DealContactPersonFullName { get; set; }
        public string DealContactPersonPhone { get; set; }
        public Nullable<bool> CreditCardRequired { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<int> Sort { get; set; }
        public int StatusID { get; set; }
        public bool Active { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public long CreateUserID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string IPAddress { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual TB_Country TB_Country { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual TB_Currency TB_Currency1 { get; set; }
        public virtual TB_Currency TB_Currency2 { get; set; }
        public virtual TB_Currency TB_Currency3 { get; set; }
        public virtual TB_Currency TB_Currency4 { get; set; }
        public virtual TB_Currency TB_Currency5 { get; set; }
        public virtual TB_Firm TB_Firm { get; set; }
        public virtual TB_Region TB_Region { get; set; }
        public virtual TB_Region TB_Region1 { get; set; }
        public virtual TB_TypeBusinessPartner TB_TypeBusinessPartner { get; set; }
        public virtual TB_TypeStatus TB_TypeStatus { get; set; }
        public virtual TB_TypeDeposit TB_TypeDeposit { get; set; }
        public virtual TB_TypeDeposit TB_TypeDeposit1 { get; set; }
        public virtual TB_TypeDeposit TB_TypeDeposit2 { get; set; }
        public virtual TB_TypeSalutation TB_TypeSalutation { get; set; }
        public virtual TB_TypeSalutation TB_TypeSalutation1 { get; set; }
        public virtual TB_TypeSalutation TB_TypeSalutation2 { get; set; }
        public virtual ICollection<TB_BusinessPartnerCancelPolicy> TB_BusinessPartnerCancelPolicy { get; set; }
        public virtual ICollection<TB_BusinessPartnerPax> TB_BusinessPartnerPax { get; set; }
        public virtual ICollection<TB_BusinessPartnerRegion> TB_BusinessPartnerRegion { get; set; }
        public virtual ICollection<TB_Tour> TB_Tour { get; set; }
        public virtual ICollection<TB_Transfer> TB_Transfer { get; set; }
        public virtual ICollection<TB_TransferPeriod> TB_TransferPeriod { get; set; }
        public virtual ICollection<TB_Deal> TB_Deal { get; set; }
    }
}