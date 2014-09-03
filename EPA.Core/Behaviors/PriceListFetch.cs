using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;	


namespace EPA.Behaviors
{
    public static class PriceListFetch
    {
        public static Dto.Models.PRICE_LIST[] Execute(string companyKey, int? companyUserId = null, int? priceListId = null)
        {


            /*
            var companyUserFetch = new CompanyUserFetch { Repositories = Repositories };
            var companyUsers = companyUserFetch.Strategy(companyKey, companyUserId);

            var priceList = GetRepository<PRICE_LIST>().RetrieveAll();
            priceList = priceList.Where(p => companyUsers.Any(c => c.COMPANY_USER_ID == p.COMPANY_USER_ID));
            return ((priceListId != null) ? priceList.Where(p => p.PRICE_LIST_ID == priceListId) : priceList).ToArray();
            */


            var companyId = EPA.Behaviors.Common.CompanyIdFetch(companyKey);
            if (companyId == 0)
                return null; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST.AsNoTracking().AsQueryable();

                var companyUsers = CompanyUserFetch.Execute(companyKey, companyUserId);

                q = q.Where(a => companyUsers.Any(b => b.COMPANY_USER_ID == a.COMPANY_USER_ID));

                int priceListIdInt = priceListId.ToInt();
                if (priceListIdInt != 0)
                    q = q.Where(a => a.PRICE_LIST_ID == priceListIdInt);

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_LIST>(a)).ToArray();

            }
        }
    }
}
