using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanyFetch
    {
        public static Dto.Models.COMPANY Execute(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }
        public static Dto.Models.COMPANY Execute(int companyId)
        {
            if (companyId == 0)
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.AsNoTracking().Where(a => a.COMPANY_ID == companyId).FirstOrDefault();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }
    }
}
