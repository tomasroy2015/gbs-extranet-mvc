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
    
    public partial class TB_TypeReservationStatus
    {
        public TB_TypeReservationStatus()
        {
            this.TB_HotelReservation = new HashSet<TB_HotelReservation>();
            this.TB_Reservation = new HashSet<TB_Reservation>();
            this.TB_ReservationStatusHistory = new HashSet<TB_ReservationStatusHistory>();
            this.TB_DealReservation = new HashSet<TB_DealReservation>();
            this.TB_TourReservation = new HashSet<TB_TourReservation>();
            this.TB_TransferReservation = new HashSet<TB_TransferReservation>();
        }
    
        public int ID { get; set; }
        public int PartID { get; set; }
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
        public virtual TB_Part TB_Part { get; set; }
        public virtual ICollection<TB_HotelReservation> TB_HotelReservation { get; set; }
        public virtual ICollection<TB_Reservation> TB_Reservation { get; set; }
        public virtual ICollection<TB_ReservationStatusHistory> TB_ReservationStatusHistory { get; set; }
        public virtual ICollection<TB_DealReservation> TB_DealReservation { get; set; }
        public virtual ICollection<TB_TourReservation> TB_TourReservation { get; set; }
        public virtual ICollection<TB_TransferReservation> TB_TransferReservation { get; set; }
    }
}
