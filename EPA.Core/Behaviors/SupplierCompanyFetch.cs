using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class SupplierCompanyFetch
    {
        public static EPA.Dto.Models.COMPANY[] Execute(string email)
        {

            if (string.IsNullOrEmpty(email))
                throw new Exception("Must be a valid email");

            var supplier = SupplierFetchByEmail.Execute(email).FirstOrDefault();

            if (supplier == null)
                throw new Exception("Email Not Recognized.");

            using (var db = new EPA.Data.Db())
            {
                var companySupplierCompanyIds = db.COMPANY_SUPPLIERS.AsNoTracking()
                    .Where(p => p.SUPPLIER_ID == supplier.SUPPLIER_ID).Select(a => a.COMPANY_ID).ToList();

                var q = db.COMPANIES.AsNoTracking().AsQueryable();
                q = q.Where(p => companySupplierCompanyIds.Any(b => b == p.COMPANY_ID));
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.COMPANY>(a)).ToArray();
            }

        }
    }
}
