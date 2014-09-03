using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;
using AutoMapper;

namespace EPA.Behaviors
{
    public  static class CompanySupplierFetch
    {
        public static Dto.Models.COMPANY_SUPPLIERS[] Execute(string companyKey, int? supplierId = null)
        {
            if (string.IsNullOrEmpty(companyKey))
                return null;

            var companyId = EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                return null; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_SUPPLIERS.AsNoTracking()
                    .Where(a => a.COMPANY_ID == companyId);

                var supplierIdInt = supplierId.ToInt();
                if (supplierIdInt != 0)
                    q = q.Where(a => a.SUPPLIER_ID == supplierIdInt);

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS>(a)).ToArray();

            }


        }
    }
}
