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
    
    public partial class PRICE_LIST
    {
        public PRICE_LIST()
        {
            this.PRICE_AGREEMENT = new HashSet<PRICE_AGREEMENT>();
        }
    
        public int PRICE_LIST_ID { get; set; }
        public Nullable<int> COMPANY_USER_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> GROUP_ID { get; set; }
        public string TYPE { get; set; }
        public Nullable<System.DateTime> DESIRED_EFFECTIVE_DATE { get; set; }
        public Nullable<System.DateTime> DESIRED_EXPIRY_DATE { get; set; }
        public Nullable<short> STATUS { get; set; }
        public string NOTES { get; set; }
        public Nullable<int> ROW_VERSION { get; set; }
        public Nullable<short> NOTIFY_USER { get; set; }
        public Nullable<System.DateTime> STATUS_DATE { get; set; }
        public string REFERENCE { get; set; }
        public string TERMS { get; set; }
    
        public virtual ICollection<PRICE_AGREEMENT> PRICE_AGREEMENT { get; set; }
    }
}
