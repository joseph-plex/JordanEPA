using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{

    public static class CompanyModify
    {
        public static Dto.Models.COMPANY Execute(Dto.Models.COMPANY company)
        {
            if (company == null)
                throw new Exception("Your model can't be null");
            else if (company.COMPANY_ID == 0)
                throw new Exception("Need a Primary Key ID to reference");

            
            using (var db = new EPA.Data.Db())
            {
              var model = db.COMPANIES.Where(a => a.COMPANY_ID == company.COMPANY_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find company to modify based on COMPANY_ID " + company.COMPANY_ID);

                model = Mapper.Map<EPA.Dto.Models.COMPANY, EPA.Models.COMPANY>(company, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }
    }
}
