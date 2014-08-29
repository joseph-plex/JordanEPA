using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Dto.Models
{
    public class COMPANY
    {
        public string KEY { get; set; }
        public int COMPANY_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string EMAIL { get; set; }
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; }
        public string FAX { get; set; }
        public Nullable<int> ROW_VERSION { get; set; }
        public string CODE { get; set; }
        public Nullable<int> PA_REFERENCE { get; set; }
    }

    public partial class COMPANY_SUPPLIERS
    {
        public int COMPANY_ID { get; set; }
        public int SUPPLIER_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string EMAIL { get; set; }
        public Nullable<int> ROW_VERSION { get; set; }
        public Nullable<int> PAYEE_ID { get; set; }
        public Nullable<short> SEND_PASSWORD { get; set; }
        public Nullable<short> IS_TRUSTED_BY_SUPPLIER { get; set; }
        public Nullable<int> COMPANY_SUPPLIERS_ID { get; set; }


    }

    public partial class COMPANY_USER_SUPPLIERS
    {
        public Nullable<int> SUPPLIER_ID { get; set; }
        public Nullable<int> COMPANY_USER_ID { get; set; }
        public int COMPANY_USER_SUPPLIERS_ID { get; set; }
    }
    public partial class COMPANY_USERS
    {
        public int COMPANY_USER_ID { get; set; }
        public Nullable<int> COMPANY_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string EMAIL { get; set; }
        public Nullable<short> STATUS { get; set; }
        public Nullable<int> ROW_VERSION { get; set; }
        public Nullable<int> DB_USER_ID { get; set; }
        public string VALIDATION_CODE { get; set; }
        public Nullable<short> VALIDATION_EMAIL_SENT { get; set; }
        public string PHONE { get; set; }
    }


    public partial class PRICE_AGREEMENT
    {
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
    }

    public partial class PRICE_AGREEMENT_ADJUSTMENTS
    {
        public int PRICE_AGREEMENT_ADJUSTMENT_ID { get; set; }
        public Nullable<int> PRICE_AGREEMENT_ID { get; set; }
        public Nullable<short> RECORD_TYPE { get; set; }
        public Nullable<int> RATE_TYPE { get; set; }
        public Nullable<decimal> RATE { get; set; }
        public string RATE_UOM { get; set; }
        public Nullable<System.DateTime> RATE_DATE { get; set; }
        public string CODE { get; set; }
    }

    public partial class PRICE_AGREEMENT_MATERIALS 
    {
        public int PRICE_AGREEMENT_ID { get; set; }
        public int PRICE_LIST_MATERIAL_ID { get; set; }
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

    public partial class PRICE_LIST
    {
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
    }

    public partial class PRICE_LIST_ITEM_TYPES
    {
        public string ITEM_TYPE { get; set; }
        public string ITEM_TYPE_GROUP { get; set; }
        public string PROP_TITLE_1 { get; set; }
        public string PROP_TITLE_2 { get; set; }
        public string PROP_TITLE_3 { get; set; }
        public string PROP_TITLE_4 { get; set; }
        public string PROP_TITLE_5 { get; set; }
        public string PROP_TITLE_6 { get; set; }
        public int PRICE_LIST_ID { get; set; }
        public Nullable<int> PRICE_LIST_ITEM_TYPES_ID { get; set; }
    }

    public partial class PRICE_LIST_MATERIALS
    {
        public int PRICE_LIST_MATERIAL_ID { get; set; }
        public Nullable<int> PRICE_LIST_ID { get; set; }
        public Nullable<int> ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public Nullable<int> DIMENSION_ID { get; set; }
        public string DIMENSION_CODE { get; set; }
        public Nullable<int> DIMENSION_CONVERSION { get; set; }
        public Nullable<decimal> QTY { get; set; }
        public string QTY_UOM { get; set; }
        public string MEASURE_TYPE { get; set; }
        public string ITEM_TYPE_GROUP { get; set; }
        public string ITEM_TYPE { get; set; }
        public string PROP_1 { get; set; }
        public string PROP_2 { get; set; }
        public string PROP_3 { get; set; }
        public string PROP_4 { get; set; }
        public string PROP_5 { get; set; }
        public string PROP_6 { get; set; }
        public Nullable<decimal> RATE_UOM { get; set; }
    }

    public partial class SUPPLIER
    {
        public int SUPPLIER_ID { get; set; }
        public string EMAIL { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string COMPANY_NAME { get; set; }
        public string PHONE { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<short> STATUS { get; set; }
        public string VALIDATION_CODE { get; set; }
        public Nullable<short> VALIDATION_EMAIL_SENT { get; set; }
    }
}
