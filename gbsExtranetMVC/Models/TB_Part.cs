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
    
    public partial class TB_Part
    {
        public TB_Part()
        {
            this.TB_Attribute = new HashSet<TB_Attribute>();
            this.TB_HitCount = new HashSet<TB_HitCount>();
            this.TB_Photo = new HashSet<TB_Photo>();
            this.TB_TypeFirmRequest = new HashSet<TB_TypeFirmRequest>();
            this.TB_TypePenaltyRate = new HashSet<TB_TypePenaltyRate>();
            this.TB_TypeReservationOperation = new HashSet<TB_TypeReservationOperation>();
            this.TB_TypeReservationStatus = new HashSet<TB_TypeReservationStatus>();
            this.TB_TypeReview = new HashSet<TB_TypeReview>();
            this.TB_TypeCancel = new HashSet<TB_TypeCancel>();
            this.TB_BusinessPartnerCancelPolicy = new HashSet<TB_BusinessPartnerCancelPolicy>();
            this.TB_Promotion = new HashSet<TB_Promotion>();
            this.BizTbl_UserOperation = new HashSet<BizTbl_UserOperation>();
            this.TB_Reservation = new HashSet<TB_Reservation>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<TB_Attribute> TB_Attribute { get; set; }
        public virtual ICollection<TB_HitCount> TB_HitCount { get; set; }
        public virtual ICollection<TB_Photo> TB_Photo { get; set; }
        public virtual ICollection<TB_TypeFirmRequest> TB_TypeFirmRequest { get; set; }
        public virtual ICollection<TB_TypePenaltyRate> TB_TypePenaltyRate { get; set; }
        public virtual ICollection<TB_TypeReservationOperation> TB_TypeReservationOperation { get; set; }
        public virtual ICollection<TB_TypeReservationStatus> TB_TypeReservationStatus { get; set; }
        public virtual ICollection<TB_TypeReview> TB_TypeReview { get; set; }
        public virtual ICollection<TB_TypeCancel> TB_TypeCancel { get; set; }
        public virtual ICollection<TB_BusinessPartnerCancelPolicy> TB_BusinessPartnerCancelPolicy { get; set; }
        public virtual ICollection<TB_Promotion> TB_Promotion { get; set; }
        public virtual ICollection<BizTbl_UserOperation> BizTbl_UserOperation { get; set; }
        public virtual ICollection<TB_Reservation> TB_Reservation { get; set; }
    }
}