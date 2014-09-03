using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;

namespace EPA.Behaviors
{
    public static class PriceAgreementFetch
    {
        public static Dto.Models.PRICE_AGREEMENT[] Execute(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null)
        {
            /*    var priceAgreements = GetRepository<PRICE_AGREEMENT>().RetrieveAll();
                        var priceListFetch = new PriceListFetch { Repositories = Repositories };
                        var PriceLists = priceListFetch.Strategy(companyKey, companyUserId, priceListId);
                        priceAgreements = priceAgreements.Where(p => PriceLists.Any(c => c.PRICE_LIST_ID == p.PRICE_LIST_ID));
                        return ((priceAgreementId != null) ? priceAgreements.Where(p => p.PRICE_AGREEMENT_ID == priceAgreementId) : priceAgreements).ToArray(); */


            var priceLists = PriceListFetch.Execute(companyKey, companyUserId, priceListId);

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_AGREEMENT.AsNoTracking().AsQueryable();
                q = q.Where(a => priceLists.Any(b => b.PRICE_LIST_ID == a.PRICE_LIST_ID));
                int priceAgreementIdInt = priceAgreementId.ToInt();
                if (priceAgreementIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_AGREEMENT_ID == priceAgreementIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT>(a)).ToArray();
            }
        }
    }
}
