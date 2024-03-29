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
    
    public partial class PRICE_AGREEMENT_MATERIALS
    {
        public Nullable<int> PRICE_AGREEMENT_ID { get; set; }
        public Nullable<int> PRICE_LIST_MATERIAL_ID { get; set; }
        public Nullable<decimal> RATE { get; set; }
        public string RATE_UOM { get; set; }
        public Nullable<decimal> RATE_UOM_UNITS { get; set; }
        public Nullable<short> RATE_STATUS { get; set; }
        public Nullable<int> ROW_VERSION { get; set; }
        public Nullable<int> SUPPLIER_ITEM_CODE { get; set; }
        public Nullable<short> INCREASE_TYPE { get; set; }
        public Nullable<decimal> INCREASE_RATE { get; set; }
        public Nullable<System.DateTime> INCREASE_DATE { get; set; }
        public Nullable<short> STOCKING_TYPE { get; set; }
        public Nullable<decimal> STOCKING_RATE { get; set; }
        public Nullable<System.DateTime> RATE_DATE { get; set; }
        public Nullable<decimal> S_RATE { get; set; }
        public string S_RATE_UOM { get; set; }
        public Nullable<decimal> S_RATE_CONVERSION { get; set; }
        public string ITEM_UOM { get; set; }
        public string MANUFACTURER { get; set; }
        public string S_RATE_DESCRIPTION { get; set; }
        public string S_RATE_NOTES { get; set; }
        public string INCREASE_CODE { get; set; }
        public string STOCKING_CODE { get; set; }
        public Nullable<int> PA_ADJUSTMENT_ID_STOCKING { get; set; }
        public Nullable<int> PA_ADJUSTMENT_ID_INCREASE { get; set; }
        public int PRICE_AGREEMENT_MATERIAL_ID { get; set; }
    }
}
