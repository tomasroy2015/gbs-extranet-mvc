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
    
    public partial class TB_TypeDeposit
    {
        public TB_TypeDeposit()
        {
            this.TB_Tour = new HashSet<TB_Tour>();
            this.TB_BusinessPartner = new HashSet<TB_BusinessPartner>();
            this.TB_BusinessPartner1 = new HashSet<TB_BusinessPartner>();
            this.TB_BusinessPartner2 = new HashSet<TB_BusinessPartner>();
            this.TB_Deal = new HashSet<TB_Deal>();
            this.TB_DealReservation = new HashSet<TB_DealReservation>();
            this.TB_TourReservation = new HashSet<TB_TourReservation>();
            this.TB_TransferReservation = new HashSet<TB_TransferReservation>();
        }
    
        public int ID { get; set; }
        public string Name_tr { get; set; }
        public string Name_en { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public Nullable<short> Sort { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual ICollection<TB_Tour> TB_Tour { get; set; }
        public virtual ICollection<TB_BusinessPartner> TB_BusinessPartner { get; set; }
        public virtual ICollection<TB_BusinessPartner> TB_BusinessPartner1 { get; set; }
        public virtual ICollection<TB_BusinessPartner> TB_BusinessPartner2 { get; set; }
        public virtual ICollection<TB_Deal> TB_Deal { get; set; }
        public virtual ICollection<TB_DealReservation> TB_DealReservation { get; set; }
        public virtual ICollection<TB_TourReservation> TB_TourReservation { get; set; }
        public virtual ICollection<TB_TransferReservation> TB_TransferReservation { get; set; }
    }
}
