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
    
    public partial class TB_Promotion
    {
        public TB_Promotion()
        {
            this.TB_HotelPromotion = new HashSet<TB_HotelPromotion>();
            this.TB_HotelPromotionHistory = new HashSet<TB_HotelPromotionHistory>();
        }
    
        public int ID { get; set; }
        public int PartID { get; set; }
        public string Type { get; set; }
        public string Name_tr { get; set; }
        public string Name_en { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
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
        public bool GeneralPromotion { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> TargetStartDate { get; set; }
        public Nullable<System.DateTime> TargetEndDate { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> DiscountPercentage { get; set; }
        public Nullable<long> RegionID { get; set; }
        public Nullable<short> Sort { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual ICollection<TB_HotelPromotion> TB_HotelPromotion { get; set; }
        public virtual ICollection<TB_HotelPromotionHistory> TB_HotelPromotionHistory { get; set; }
        public virtual TB_Part TB_Part { get; set; }
        public virtual TB_Region TB_Region { get; set; }
    }
}