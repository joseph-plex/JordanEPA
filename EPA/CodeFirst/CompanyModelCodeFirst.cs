using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Attributes;
using EPA.Attributes.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EPA.CodeFirst
{
    public class Company : BaseEntities.Entity // .EntityCompany
    {
            [StringRequired(20), StringProperty(20, true)]
        public string KEY { get; set; }
      //  public int COMPANY_ID { get; set; }

        [StringRequired(1024), StringProperty(1024, true)]
        public string DESCRIPTION { get; set; }

        [StringNotRequired(64), EmailProperty(false)]
        public string EMAIL { get; set; }
        [StringNotRequired(64), PhoneNumberProperty(false)]
        public string PHONE1 { get; set; }
        [StringNotRequired(64), PhoneNumberProperty(false)]
        public string PHONE2 { get; set; }
        [StringNotRequired(64), PhoneNumberProperty(false)]
        public string FAX { get; set; }

        public Nullable<int> ROW_VERSION { get; set; }
        public string CODE { get; set; }
        public Nullable<int> PA_REFERENCE { get; set; }

    }
}

