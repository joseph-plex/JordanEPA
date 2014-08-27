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
        public Models.COMPANY CompanyFetch(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            using (var db = new EPA.Models.DbFirstEntities())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault();
            }
        }
    }
}
