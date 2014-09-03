using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class GenerateAgreementReferenceNumber 
    {
        public static string Execute(string companyKey, int companyUserId)
        {
            var companyUser =  CompanyUserFetch.Execute(companyKey, companyUserId).FirstOrDefault();

            if (companyUser == null)
                throw new Exception("No Company User associated with company with this Id");

            var agreements =  PriceAgreementFetch.Execute(companyKey, companyUserId, null, null);
            var ids = agreements.Select(p => Convert.ToInt32(p.REFERENCE.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries).Last())).ToList();

            if (ids.Count() == 0)
                return "PA - " + 1;

            ids.Sort();
            return "PA - " + (ids.Last() + 1);
        }
    }
}
