using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanySupplierCreate
    {
        public static Dto.Models.COMPANY_SUPPLIERS Execute(string companyKey, int supplierId, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new Exception("Description must not be null or empty");


            using (var db = new EPA.Data.Db())
            {
                var company = CompanyFetch.Execute(companyKey);
                if (company == null || company.COMPANY_ID == 0)
                    throw new Exception("Company does not exist");

                var supplier = SupplierFetch.Execute(supplierId).FirstOrDefault();
                if (supplier == null || supplier.SUPPLIER_ID == 0)
                    throw new Exception("Supplier does not exist");

                var model = new EPA.Models.COMPANY_SUPPLIERS()
                {
                    COMPANY_ID = company.COMPANY_ID,
                    DESCRIPTION = description,
                    SUPPLIER_ID = supplier.SUPPLIER_ID,
                     // EMAIL = company.EMAIL,
                };

                db.COMPANY_SUPPLIERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS>(model);
            }
        }

  
    }
}
