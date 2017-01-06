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
    
    public partial class TB_Deal
    {
        public TB_Deal()
        {
            this.TB_DealReservation = new HashSet<TB_DealReservation>();
        }
    
        public int ID { get; set; }
        public int BusinessPartnerID { get; set; }
        public long RegionID { get; set; }
        public string Name_tr { get; set; }
        public string Name_en { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_jp { get; set; }
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_es { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_it { get; set; }
        public string Description_ar { get; set; }
        public string Description_jp { get; set; }
        public string SpecialNote_tr { get; set; }
        public string SpecialNote_en { get; set; }
        public string SpecialNote_de { get; set; }
        public string SpecialNote_es { get; set; }
        public string SpecialNote_fr { get; set; }
        public string SpecialNote_ru { get; set; }
        public string SpecialNote_it { get; set; }
        public string SpecialNote_ar { get; set; }
        public string SpecialNote_jp { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public short Quota { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public decimal Cost { get; set; }
        public int CostCurrencyID { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<int> DepositCurrencyID { get; set; }
        public Nullable<int> DepositTypeID { get; set; }
        public Nullable<int> BusinessPartnerCancelPolicyID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> MapZoomIndex { get; set; }
        public Nullable<bool> CreditCardRequired { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<bool> IsPopular { get; set; }
        public Nullable<int> Sort { get; set; }
        public string RoutingName { get; set; }
        public bool Active { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public long CreateUserID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string IPAddress { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual TB_BusinessPartner TB_BusinessPartner { get; set; }
        public virtual TB_BusinessPartnerCancelPolicy TB_BusinessPartnerCancelPolicy { get; set; }
        public virtual TB_Currency TB_Currency { get; set; }
        public virtual TB_Currency TB_Currency1 { get; set; }
        public virtual TB_Currency TB_Currency2 { get; set; }
        public virtual TB_Region TB_Region { get; set; }
        public virtual TB_TypeDeposit TB_TypeDeposit { get; set; }
        public virtual ICollection<TB_DealReservation> TB_DealReservation { get; set; }
    }
}