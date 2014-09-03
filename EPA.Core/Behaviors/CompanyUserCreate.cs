using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanyUserCreate
    {
        public static Dto.Models.COMPANY_USERS Execute(Dto.Models.COMPANY_USERS companyUser)
        {

            if (string.IsNullOrWhiteSpace(companyUser.DESCRIPTION))
                throw new Exception("Description must have valid value");
            if (string.IsNullOrWhiteSpace(companyUser.EMAIL))
                throw new Exception("Email must have a valid value");

            var companyId = companyUser.COMPANY_ID; // EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("No company exists with this company key");

            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.COMPANY_USERS();
                model = Mapper.Map<EPA.Dto.Models.COMPANY_USERS, EPA.Models.COMPANY_USERS>(companyUser, model);
                model = db.AssignPrimaryKey(model, (() => model.COMPANY_USER_ID), Data.Sequence.COMPANY_USER_ID);
                db.COMPANY_USERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_USERS>(model);
            }


        }

        public static Dto.Models.COMPANY_USERS Execute(string companyKey, string descripton, string email)
        {
            return Execute(new Dto.Models.COMPANY_USERS { EMAIL = email, DESCRIPTION = descripton, COMPANY_ID = EPA.Behaviors.Common.CompanyIdFetch(companyKey) });
        }
    }
}
