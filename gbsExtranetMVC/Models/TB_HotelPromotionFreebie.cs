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
    
    public partial class TB_HotelPromotionFreebie
    {
        public int ID { get; set; }
        public int HotelPromotionID { get; set; }
        public int FreebieID { get; set; }
        public bool Active { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public long CreateUserID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual BizTbl_User BizTbl_User1 { get; set; }
        public virtual TB_TypeFreebie TB_TypeFreebie { get; set; }
        public virtual TB_HotelPromotion TB_HotelPromotion { get; set; }
    }
}
