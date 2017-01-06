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
    
    public partial class TB_HotelCancelPolicy
    {
        public TB_HotelCancelPolicy()
        {
            this.TB_HotelReservation = new HashSet<TB_HotelReservation>();
        }
    
        public int ID { get; set; }
        public int HotelID { get; set; }
        public int CancelTypeID { get; set; }
        public Nullable<int> RefundableDayCount { get; set; }
        public Nullable<int> PenaltyRateTypeID { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual TB_TypeCancel TB_TypeCancel { get; set; }
        public virtual TB_TypePenaltyRate TB_TypePenaltyRate { get; set; }
        public virtual TB_Hotel TB_Hotel { get; set; }
        public virtual ICollection<TB_HotelReservation> TB_HotelReservation { get; set; }
    }
}