using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanyUserExists
    {
        public static bool Execute(string companyKey, string email)
        {
            var companyId = EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                return false; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_USERS.AsNoTracking()
                    .Where(a => a.COMPANY_ID == companyId)
                    .Where(a => string.Equals(a.EMAIL, email, StringComparison.CurrentCultureIgnoreCase));

                return q.Any();
            }

        }
    }
}
