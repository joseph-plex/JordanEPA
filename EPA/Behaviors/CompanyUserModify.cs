using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanyUserModify
    {
        public static Dto.Models.COMPANY_USERS Execute(string companyKey, Dto.Models.COMPANY_USERS companyUser)
        {

            var companyId = EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("No company exists with this company key");

            if (companyUser == null)
                throw new Exception("Your model can't be null");
            else if (companyUser.COMPANY_USER_ID == 0)
                throw new Exception("Need a Primary Key ID to reference");
            else if (string.IsNullOrWhiteSpace(companyUser.EMAIL))
                throw new Exception("Email must have a valid value");
            else if (string.IsNullOrWhiteSpace(companyUser.DESCRIPTION))
                throw new Exception("Description must have valid value");


            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_USERS.Where(a => a.COMPANY_USER_ID == companyUser.COMPANY_USER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find object to modify based on ID " + companyUser.COMPANY_USER_ID);

                model = Mapper.Map<EPA.Dto.Models.COMPANY_USERS, EPA.Models.COMPANY_USERS>(companyUser, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_USERS>(model);
            }
        }
    }
}
