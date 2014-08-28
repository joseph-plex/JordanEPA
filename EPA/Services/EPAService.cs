using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EPA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EPAService" in both code and config file together.
    public class EPAService : IEPAService
    {
        public void DoWork()
        {
        }
        public Dto.COMPANY CompanyFetch(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            using (var db = new EPA.Models.DbFirstEntities())
            {
                var model = db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault();
                return Mapper.Map<EPA.Dto.COMPANY>(model);
            }
        }


        public Dto.COMPANY CompanyCreate(string description)
        {
            return CompanyCreate(new Dto.COMPANY { DESCRIPTION = description });
        }

        public Dto.COMPANY CompanyCreate(Dto.COMPANY company)
        {
            if (string.IsNullOrEmpty(company.DESCRIPTION))
                throw new Exception("Description cannot be empty or null");

            if (string.IsNullOrEmpty(company.KEY))
            {
                company.KEY = IdGenerator.CreateUniqueKeyForCompany();
            }

            company.ROW_VERSION = 1;

            using (var db = new EPA.Models.DbFirstEntities())
            {

                var model = new EPA.Models.COMPANY();
                model = Mapper.Map<EPA.Dto.COMPANY, EPA.Models.COMPANY>(company, model);
                db.COMPANIES.Add(model);
                db.Save();
                return Mapper.Map<EPA.Dto.COMPANY>(model);
            }
        }

        public Dto.COMPANY CompanyModify(Dto.COMPANY company)
        {

            using (var db = new EPA.Models.DbFirstEntities())
            {
                var model = db.COMPANIES.Where(a => a.COMPANY_ID == company.COMPANY_ID).FirstOrDefault();
                model =  Mapper.Map<EPA.Dto.COMPANY, EPA.Models.COMPANY>(company, model);
                db.Save();
                return Mapper.Map<EPA.Dto.COMPANY>(model);
            }
        }

        public void CompanyDelete(Dto.COMPANY company)
        {
            throw new NotImplementedException();
        }

   
    }
}
