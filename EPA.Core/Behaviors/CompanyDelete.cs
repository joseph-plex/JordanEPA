using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;

namespace EPA.Behaviors
{
   public static     class CompanyDelete
    {
        public static void Execute(int companyId)
        {
            Execute(CompanyFetch.Execute(companyId));
        }
        public static void Execute(Dto.Models.COMPANY company)
        {
            if (company == null)
                throw new Exception("Your model can't be null");
            else if (company.COMPANY_ID == 0 || company.KEY.IsNullOrEmpty())
                throw new Exception("Need a Primary KEY to reference");

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.Where(a => a.COMPANY_ID == company.COMPANY_ID && a.KEY == company.KEY).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid Company");
                db.SetToDeleted(model);
                db.SaveChanges();
            }
        }
    }
}
