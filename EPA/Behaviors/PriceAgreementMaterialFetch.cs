using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;
using AutoMapper;

namespace EPA.Behaviors
{
    public static class PriceAgreementMaterialFetch
    {
        public static Dto.Models.PRICE_AGREEMENT_MATERIALS[] Execute(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null, int? priceAgreementMaterialId = null)
        {
            // ODD BEHAVIOR TO SEARCH BY PRIMARY KEY AND ALSO OPTIONAL PARAMETERS

            // CHANGED priceList TO priceAgreementMaterialId AT THE END

            /* var priceAgreementMaterials = GetRepository<PRICE_AGREEMENT_MATERIALS>().RetrieveAll();
            PriceAgreementFetch priceAgreementFetch = new PriceAgreementFetch { Repositories = Repositories };
            var priceAgreements = priceAgreementFetch.Strategy(companyKey, companyUserId, priceListId, priceAgreementId);
            priceAgreementMaterials = priceAgreementMaterials.Where(p => priceAgreements.Any(c => c.PRICE_AGREEMENT_ID == p.PRICE_AGREEMENT_ID));
            return ((priceAgreementMaterialId != null) ? priceAgreementMaterials.Where(p => p.PRICE_LIST_MATERIAL_ID == priceAgreementMaterialId) : priceAgreementMaterials).ToArray(); */


            var priceAgreements = PriceAgreementFetch.Execute(companyKey, companyUserId, priceListId, priceAgreementId);


            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_AGREEMENT_MATERIALS.AsNoTracking().AsQueryable();
                q = q.Where(a => priceAgreements.Any(b => b.PRICE_AGREEMENT_ID == a.PRICE_AGREEMENT_ID));
                int priceAgreementMaterialIdInt = priceAgreementMaterialId.ToInt();
                if (priceAgreementMaterialIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_AGREEMENT_MATERIAL_ID == priceAgreementMaterialIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS>(a)).ToArray();
            }

        }
    }
}
