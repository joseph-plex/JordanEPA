using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanyUsersSuppliersCreate
    {
        public static Dto.Models.COMPANY_USER_SUPPLIERS Execute(string companyKey, int supplierId, int companyUserId)
        {

            var companyId = EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("No company exists with this company key");
            else if (companyUserId == 0)
                throw new Exception("Need the company User Id");



            var companyUser = CompanyUserFetch.Execute(companyKey, companyUserId).FirstOrDefault();
            if (companyUser == null || companyUser.COMPANY_USER_ID == 0)
                throw new Exception("Invalid Company User Information");

            var supplier = SupplierFetch.Execute(supplierId).FirstOrDefault();
            if (supplier == null || supplier.SUPPLIER_ID == 0)
                throw new Exception("Invalid Supplier Information");


            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.COMPANY_USER_SUPPLIERS() { COMPANY_USER_ID = companyUserId, SUPPLIER_ID = supplierId };
                // THIS BREAKS THE NORMAL CODE
                model = db.AssignPrimaryKey(model, (() => model.COMPANY_USER_SUPPLIERS_ID), Data.Sequence.COMPANY_USER_SUPPLIERS_ID);
                db.COMPANY_USER_SUPPLIERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_USER_SUPPLIERS>(model);
            }

        }
    }
}
