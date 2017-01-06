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
    
    public partial class BizTbl_Table
    {
        public BizTbl_Table()
        {
            this.BizTbl_TableColumn = new HashSet<BizTbl_TableColumn>();
            this.BizTbl_TableSecurityGroupRight = new HashSet<BizTbl_TableSecurityGroupRight>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public bool Logged { get; set; }
        public bool Editable { get; set; }
        public string DescriptiveFields { get; set; }
        public string ViewableFields { get; set; }
        public string OrderFields { get; set; }
        public string FilterFields { get; set; }
        public string FilterExpression { get; set; }
        public string ForeignTableFilterExpression { get; set; }
        public Nullable<bool> NewRecordVisible { get; set; }
        public Nullable<short> PagingSize { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    
        public virtual BizTbl_User BizTbl_User { get; set; }
        public virtual ICollection<BizTbl_TableColumn> BizTbl_TableColumn { get; set; }
        public virtual ICollection<BizTbl_TableSecurityGroupRight> BizTbl_TableSecurityGroupRight { get; set; }
    }
}