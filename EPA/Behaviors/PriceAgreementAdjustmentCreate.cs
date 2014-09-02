using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{

    public static class PriceAgreementAdjustmentCreate
    {
        public static Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS Execute(string companyKey, int priceAgreementId)
        {

            var priceAgreement = PriceAgreementFetch.Execute(companyKey, null, null, priceAgreementId).FirstOrDefault();

            if (priceAgreement == null)
                throw new Exception("PriceAgreementId Must be a valid price agreement that is associated with the company");

            using (var db = new EPA.Data.Db())
            {
                var model = new EPA.Models.PRICE_AGREEMENT_ADJUSTMENTS() { PRICE_AGREEMENT_ID = priceAgreementId };
                db.PRICE_AGREEMENT_ADJUSTMENTS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS>(model);
            }

        }

    }
}
