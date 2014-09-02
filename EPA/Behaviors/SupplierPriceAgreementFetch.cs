using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class SupplierPriceAgreementFetch
    {
        public static EPA.Dto.Models.PRICE_AGREEMENT[] Execute(string email)
        {

            if (String.IsNullOrEmpty(email))
                throw new Exception("Must be a valid email");

            var supplier = SupplierFetchByEmail.Execute(email).FirstOrDefault();
            if (supplier == null)
                throw new Exception("Email Not Recognized.");


            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_AGREEMENT.AsNoTracking().Where(p => p.SUPPLIER_ID == supplier.SUPPLIER_ID);
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT>(a)).ToArray();
            }

        }
    }
}
