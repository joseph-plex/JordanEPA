//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRICE_AGREEMENT
    {
        public PRICE_AGREEMENT()
        {
            this.PRICE_AGREEMENT_ADJUSTMENTS = new HashSet<PRICE_AGREEMENT_ADJUSTMENTS>();
        }
    
        public int PRICE_AGREEMENT_ID { get; set; }
        public Nullable<int> PRICE_LIST_ID { get; set; }
        public Nullable<int> SUPPLIER_ID { get; set; }
        public Nullable<short> IS_SUPPLIER_AWARE_OF_UPDATE { get; set; }
        public Nullable<short> HAS_INCREASES { get; set; }
        public Nullable<int> COMPANY_SUPPLIER_ID { get; set; }
        public Nullable<short> HAS_STOCKING { get; set; }
        public string SUPPLIER_NOTES { get; set; }
        public Nullable<short> NOTIFY_SUPPLIER { get; set; }
        public Nullable<System.DateTime> DUE_DATE { get; set; }
        public Nullable<System.DateTime> EXPIRY_DATE { get; set; }
        public Nullable<System.DateTime> EFFECTIVE_DATE { get; set; }
        public Nullable<System.DateTime> AGREED_BY_DATE { get; set; }
        public string AGREED_BY { get; set; }
        public Nullable<int> STATUS { get; set; }
        public string REFERENCE { get; set; }
        public Nullable<int> GROUP_ID { get; set; }
        public Nullable<int> PROGRESSION { get; set; }
    
        public virtual ICollection<PRICE_AGREEMENT_ADJUSTMENTS> PRICE_AGREEMENT_ADJUSTMENTS { get; set; }
        public virtual PRICE_LIST PRICE_LIST { get; set; }
    }
}
