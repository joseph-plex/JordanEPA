using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;

namespace EPA.Behaviors
{
    public static class PriceAgreementAdjustmentFetch
    {
        public static Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS[] Execute(string companyKey, int? priceAgreementId = null, int? priceAgreementAdjustmentId = null)
        {
            // PROBLEM
            // ERROR IN THIS
            /* InvokePreMethodExecutionEvent(MethodInfo.GetCurrentMethod());
            // PROBABLY NOT WHATS INTENDED
            var result = new PriceAgreementAdjustmentFetch { Repositories = repositories }.Strategy(companyKey, priceAgreementId, priceAdustmentId);
            InvokePostMethodExecutionEvent(MethodInfo.GetCurrentMethod());
            return result;
             * 
             *  var priceAgreementFetch = new PriceAgreementFetch { Repositories = Repositories };
            var priceAgreementAdjustments = GetRepository<PRICE_AGREEMENT_ADJUSTMENTS>().RetrieveAll();
            var priceAgreements = priceAgreementFetch.Strategy(companyKey, companyUserId, priceListId, PriceAgreementId);
            priceAgreementAdjustments = priceAgreementAdjustments.Where(p=> priceAgreements.Any(c => c.PRICE_AGREEMENT_ID == p.PRICE_AGREEMENT_ID));
            return ((PriceAdjustmentId != null)?priceAgreementAdjustments.Where(p=>p.PRICE_AGREEMENT_ADJUSTMENT_ID == PriceAdjustmentId):priceAgreementAdjustments).ToArray();
             * 
            */
            var priceAgreements = PriceAgreementFetch.Execute(companyKey, null, null, priceAgreementId);

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_AGREEMENT_MATERIALS.AsNoTracking().AsQueryable();
                q = q.Where(a => priceAgreements.Any(b => b.PRICE_AGREEMENT_ID == a.PRICE_AGREEMENT_ID));
                int priceAgreementMaterialIdInt = priceAgreementAdjustmentId.ToInt();
                if (priceAgreementMaterialIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_AGREEMENT_MATERIAL_ID == priceAgreementMaterialIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS>(a)).ToArray();
            }


        }
    }
}
