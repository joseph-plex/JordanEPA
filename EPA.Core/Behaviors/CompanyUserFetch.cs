using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;
using AutoMapper;

namespace EPA.Behaviors
{
    public static class CompanyUserFetch
    {
        public static Dto.Models.COMPANY_USERS[] Execute(string companyKey, int? companyUserId = null)
        {

            if (string.IsNullOrEmpty(companyKey))
                return null;

            var companyId = EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                return null; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_USERS.AsNoTracking()
                    .Where(a => a.COMPANY_ID == companyId);

                var companyUserIdInt = companyUserId.ToInt();
                if (companyUserIdInt != 0)
                    q = q.Where(a => a.COMPANY_USER_ID == companyUserIdInt);

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.COMPANY_USERS>(a)).ToArray();

            }
        }
    }
}
