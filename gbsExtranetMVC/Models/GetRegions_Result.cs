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
    
    public partial class GetRegions_Result
    {
        public long ID { get; set; }
        public string FK_CountryID_ID { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<long> SecondParentID { get; set; }
        public string RegionType { get; set; }
        public string SubRegionType { get; set; }
        public string Name { get; set; }
        public string NameASCII { get; set; }
        public string Name_en { get; set; }
        public string Name_tr { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
        public string Code { get; set; }
        public Nullable<long> Population { get; set; }
        public Nullable<bool> IsIncludedInDestinationSearch { get; set; }
        public Nullable<bool> IsCity { get; set; }
        public bool IsPopular { get; set; }
        public bool IsFilter { get; set; }
        public bool IsMainPageDisplay { get; set; }
        public Nullable<int> MainPageDisplaySort { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<short> Sort { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> MapZoomIndex { get; set; }
        public Nullable<bool> HasCityTax { get; set; }
        public bool Active { get; set; }
    }
}
