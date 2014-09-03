using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class Common
    {
        internal static int CompanyIdFetch(string key)
        {
            using (var db = new EPA.Data.Db())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).Select(a => a.COMPANY_ID).FirstOrDefault();
            }
        }
        internal static string CompanyKeyFetch(int companyId)
        {
            using (var db = new EPA.Data.Db())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.COMPANY_ID == companyId).Select(a => a.KEY).FirstOrDefault();
            }
        }

    }
}
