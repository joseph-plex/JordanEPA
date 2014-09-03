using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class CompanyUserDelete
    {
        public static void Execute(string companyKey, int companyUserId)
        {

            var companyUsers = CompanyUserFetch.Execute(companyKey, companyUserId);

            if (companyUsers == null || companyUsers.Any() == false)
                throw new Exception("Can't find the user");

            var companyUser = companyUsers.FirstOrDefault();

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_USERS.Where(a => a.COMPANY_USER_ID == companyUser.COMPANY_USER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid Company User");
                db.SetToDeleted(model);
                db.SaveChanges();
            }
        }
    }
}
