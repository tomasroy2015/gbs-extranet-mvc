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
    
    public partial class TB_Unit
    {
        public TB_Unit()
        {
            this.TB_Attribute = new HashSet<TB_Attribute>();
            this.TB_Tour = new HashSet<TB_Tour>();
            this.TB_HotelAttribute = new HashSet<TB_HotelAttribute>();
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
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
        public bool DateUnit { get; set; }
        public Nullable<bool> ConditionUnit { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual ICollection<TB_Attribute> TB_Attribute { get; set; }
        public virtual ICollection<TB_Tour> TB_Tour { get; set; }
        public virtual ICollection<TB_HotelAttribute> TB_HotelAttribute { get; set; }
    }
}