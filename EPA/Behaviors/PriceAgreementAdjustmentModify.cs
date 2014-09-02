using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class PriceAgreementAdjustmentModify
    {
        public static Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS Execute(string companyKey, Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust)
        {
            /* 
             *   var paa = priceAgreementAdjust;
            var paaf = new PriceAgreementAdjustmentFetch { Repositories = Repositories };
            var adjustment = paaf.Strategy(companyKey, null, null, null, paa.PRICE_AGREEMENT_ADJUSTMENT_ID).FirstOrDefault();

            if (adjustment == null)
                throw new Exception("No Price AgreementAdjustment associated with the function");

            GetRepository<PRICE_AGREEMENT_ADJUSTMENTS>().Update(paa);
            return GetRepository<PRICE_AGREEMENT_ADJUSTMENTS>().Retrieve(p => p.PRICE_AGREEMENT_ADJUSTMENT_ID == paa.PRICE_AGREEMENT_ADJUSTMENT_ID);
               
             * 
             */

            var adjustment = PriceAgreementAdjustmentFetch.Execute(companyKey, null, priceAgreementAdjust.PRICE_AGREEMENT_ADJUSTMENT_ID).FirstOrDefault();
            if (adjustment == null)
                throw new Exception("No Price AgreementAdjustment associated with the function");


            using (var db = new EPA.Data.Db())
            {
                var model = db.PRICE_AGREEMENT_ADJUSTMENTS.Where(a => a.PRICE_AGREEMENT_ADJUSTMENT_ID == priceAgreementAdjust.PRICE_AGREEMENT_ADJUSTMENT_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find object to modify based on KEY " + priceAgreementAdjust.PRICE_AGREEMENT_ADJUSTMENT_ID);

                model = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS, EPA.Models.PRICE_AGREEMENT_ADJUSTMENTS>(priceAgreementAdjust, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS>(model);
            }
        }
    }
}
