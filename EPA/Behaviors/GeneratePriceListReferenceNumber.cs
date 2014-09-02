using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    //create stubs for this.
    //warning - GeneratePriceListReferenceNumber trusts that informatino from the repository is in the correct format without checking for it.
    public  static class GeneratePriceListReferenceNumber 
    {
        public static string Execute(string companyKey, int companyUserId)
        {

            var companyUser =  CompanyUserFetch.Execute(companyKey, companyUserId).FirstOrDefault();
            if (companyUser == null)
                throw new Exception("No Company User associated with company with this Id");
            var pricelists =  PriceListFetch.Execute(companyKey, companyUserId);
            var ids = pricelists.Select(p => Convert.ToInt32(p.REFERENCE.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries).Last())).ToList();

            if (ids.Count() == 0)
                return "PL - " + 1;

            ids.Sort();

            return "PL - " + (ids.Last() + 1);
        }
    }
}
