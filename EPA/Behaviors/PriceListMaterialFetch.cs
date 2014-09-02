using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Plexxis.Helpers.Extensions;	



namespace EPA.Behaviors
{
    public static class PriceListMaterialFetch
    {
        public static Dto.Models.PRICE_LIST_MATERIALS[] Execute(string companyKey, int? companyUserId = null, int? priceListId = null, int? priceListMaterialId = null)
        {


            // PROBLEM - allowing to search by primary key should overule everything else

            /* OLD FUNCTION
             var priceListFetch = new PriceListFetch { Repositories = Repositories };
            var priceLists = priceListFetch.Strategy(companyKey, companyUserId, priceListId);
           
            var priceListMaterials = GetRepository<PRICE_LIST_MATERIALS>().RetrieveAll();

            priceListMaterials = priceListMaterials.Where(p => priceLists.Any(c => c.PRICE_LIST_ID == p.PRICE_LIST_ID));
            return ((priceListMaterialId != null) ? priceListMaterials.Where(p => p.PRICE_LIST_MATERIAL_ID == priceListMaterialId) : priceListMaterials).ToArray();

             */
            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST_MATERIALS.AsNoTracking().AsQueryable();

                var priceLists = PriceListFetch.Execute(companyKey, companyUserId, priceListId);
                q = q.Where(a => priceLists.Any(b => b.PRICE_LIST_ID == a.PRICE_LIST_ID));

                int priceListMaterialIdInt = priceListId.ToInt();
                if (priceListMaterialIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_LIST_MATERIAL_ID == priceListMaterialIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS>(a)).ToArray();

            }

        }
    }
}
