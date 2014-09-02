using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanySupplierModify
    {
        public static Dto.Models.COMPANY_SUPPLIERS Execute(Dto.Models.COMPANY_SUPPLIERS companySupplier)
        {
            if (companySupplier == null)
                throw new Exception("Your model can't be null");
            else if (companySupplier.COMPANY_SUPPLIERS_ID == 0)
                throw new Exception("Need the Primary Key");


            var companyId = companySupplier.COMPANY_ID; // EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("Company does not exist");


            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_SUPPLIERS.Where(a => a.COMPANY_SUPPLIERS_ID == companySupplier.COMPANY_SUPPLIERS_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find company to modify based on primary key " + companySupplier.COMPANY_SUPPLIERS_ID);

                model = Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS, EPA.Models.COMPANY_SUPPLIERS>(companySupplier, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS>(model);
            }
        }

        public static Dto.Models.COMPANY_SUPPLIERS Execute(string companyKey, Dto.Models.COMPANY_SUPPLIERS companySuppliers)
        {
            return Execute(companySuppliers);
        }

    }
}
