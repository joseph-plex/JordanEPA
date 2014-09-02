using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;
namespace EPA.Behaviors
{
    public static class CompanyUsersSuppliersExists
    {
        public static bool Execute(string companyKey, int supplierId, int companyUserId)
        {

            var companyUser = CompanyUserFetch.Execute(companyKey, companyUserId).FirstOrDefault();
            if (companyUser == null || companyUser.COMPANY_USER_ID == 0)
                throw new Exception("No CompanyUserId Key for that user");

            var supplier = SupplierFetch.Execute(supplierId).FirstOrDefault();
            if (supplier == null || supplier.SUPPLIER_ID == 0)
                throw new Exception("Invalid Supplier Id");


            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_USER_SUPPLIERS.AsNoTracking()
                    .Where(a => a.SUPPLIER_ID == supplierId)
                    .Where(a => a.COMPANY_USER_ID == companyUserId);
                return q.Any();
            }

        }
    }
}
