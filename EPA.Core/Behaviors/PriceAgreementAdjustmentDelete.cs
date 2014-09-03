using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class PriceAgreementAdjustmentDelete
    {
        public static void Execute(string companyKey, int priceAgreementAdjustmentId)
        {


            var adjustment = PriceAgreementAdjustmentFetch.Execute(companyKey, null, priceAgreementAdjustmentId).FirstOrDefault();
            if (adjustment == null)
                throw new Exception("No Price Agreement Adjustment associated with the function");


            using (var db = new EPA.Data.Db())
            {
                var model = db.PRICE_AGREEMENT_ADJUSTMENTS.Where(a => a.PRICE_AGREEMENT_ADJUSTMENT_ID == priceAgreementAdjustmentId).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find object to modify based on KEY " + priceAgreementAdjustmentId);

                db.SetToDeleted(model);
                db.SaveChanges();

            }

        }
    }
}
