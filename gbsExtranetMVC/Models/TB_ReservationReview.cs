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
    
    public partial class TB_ReservationReview
    {
        public long ID { get; set; }
        public long ReservationID { get; set; }
        public Nullable<int> ReviewStatusID { get; set; }
        public int TravellerTypeID { get; set; }
        public string ReviewPositive { get; set; }
        public decimal AveragePoint { get; set; }
        public bool Anonymous { get; set; }
        public bool Active { get; set; }
        public string IPAddress { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string Reviewnegative { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string travelerdate { get; set; }
        public string Review { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual TB_TypeReviewStatus TB_TypeReviewStatus { get; set; }
        public virtual TB_TypeTraveller TB_TypeTraveller { get; set; }
        public virtual TB_Reservation TB_Reservation { get; set; }
    }
}
