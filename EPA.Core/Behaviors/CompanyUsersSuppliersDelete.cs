using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanyUsersSuppliersDelete
    {
        public static void Execute(string companyKey, int supplierId, int companyUserId)
        {

            var companyUser = CompanyUserFetch.Execute(companyKey, companyUserId).FirstOrDefault();
            if (companyUser == null || companyUser.COMPANY_USER_ID == 0)
                throw new Exception("Invalid Company User Information");

            if (companyUser == null)
                throw new Exception("Unauthorized Request");


            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_USER_SUPPLIERS.Where(a => a.COMPANY_USER_ID == companyUser.COMPANY_USER_ID && supplierId == a.SUPPLIER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid COMPANY_USER_SUPPLIER");
                db.SetToDeleted(model);
                db.SaveChanges();
            }



        }
    }
}
