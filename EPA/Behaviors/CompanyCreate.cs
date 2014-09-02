using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static partial class CompanyCreate
    {

        public static Dto.Models.COMPANY Execute(string description)
        {
            return Execute(description);
        }
        public static Dto.Models.COMPANY Execute(Dto.Models.COMPANY company)
        {
            if (string.IsNullOrEmpty(company.DESCRIPTION))
                throw new Exception("Description cannot be empty or null");

            if (string.IsNullOrEmpty(company.KEY))
                company.KEY = EPA.Services.IdGenerator.CreateUniqueKeyForCompany();

            company.ROW_VERSION = 1;

            using (var db = new EPA.Data.Db())
            {
                var model = new EPA.Models.COMPANY();
                model = Mapper.Map<EPA.Dto.Models.COMPANY, EPA.Models.COMPANY>(company, model);

                
                db.COMPANIES.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }
    }
}
