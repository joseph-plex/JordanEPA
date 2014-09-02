using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class GenerateAdjustmentReferenceNumber
    {

        // PROBLEM - adjustment IS NOT USED


        public static string Execute(string companyKey, int companyUserId)
        {

            var companyUser = CompanyUserFetch.Execute(companyKey, companyUserId);


            if (companyUser == null)
                throw new Exception("No Company User associated with company with this Id");
            using (var db = new EPA.Data.Db())
            {

                var priceListIds = db.PRICE_LIST.AsNoTracking()
                    .Where(p => p.COMPANY_USER_ID == companyUserId)
                    .Select(a => a.PRICE_LIST_ID).ToList();
                // GetRepository<PRICE_LIST>().RetrieveAll().Where(p => p.COMPANY_USER_ID == companyUserId);

                var agreements = db.PRICE_AGREEMENT.AsNoTracking()
                    .Where(p => priceListIds.Any(q => q == p.PRICE_LIST_ID)).ToList();

                // THIS IS NOT NEEDED, NOT USED
                // var adjustment = db.PRICE_AGREEMENT_ADJUSTMENTS.AsNoTracking().Where(p => agreements.Any(q => q.PRICE_AGREEMENT_ID == p.PRICE_AGREEMENT_ID));


                var ids = agreements.Select(p => Convert.ToInt32(p.REFERENCE.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries).Last())).ToList();

                if (ids.Count() == 0)
                    return "PA - " + 1;

                ids.Sort();

                return "ADJ - " + ids.Last();
            }


        }

    }

    /*  public String Strategy(string companyKey, int companyUserId)
        {
            var companyUser = new CompanyUserFetch { Repositories = Repositories }.Strategy(companyKey, companyUserId).FirstOrDefault();

            if (companyUser == null)
                throw new Exception("No Company User associated with company with this Id");

            var priceLists = GetRepository<PRICE_LIST>().RetrieveAll().Where(p => p.COMPANY_USER_ID == companyUserId);
            var agreements = GetRepository<PRICE_AGREEMENT>().RetrieveAll().Where(p => priceLists.Any(q => q.PRICE_LIST_ID == p.PRICE_LIST_ID));
            var adjustment = GetRepository<PRICE_AGREEMENT_ADJUSTMENTS>().RetrieveAll().Where(p => agreements.Any(q => q.PRICE_AGREEMENT_ID == p.PRICE_AGREEMENT_ID));
            var ids = agreements.Select(p => Convert.ToInt32(p.REFERENCE.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries).Last())).ToList();
          
            if (ids.Count() == 0)
                return "PA - " + 1;

            ids.Sort();
         
            return "ADJ - " + ids.Last();
        }*/
}
