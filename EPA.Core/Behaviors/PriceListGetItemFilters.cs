using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{

    public static class PriceListGetItemFilters
    {

        public static Dto.Services.ItemFilter[] Execute(string companyKey, int priceListId)
        {

   
            var priceList = PriceListFetch.Execute(companyKey, null, priceListId).FirstOrDefault();
            if (priceList == null)
                throw new Exception("Price List does not exist for specified company Key");
            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST_MATERIALS.AsNoTracking()
                    .Where(p => p.PRICE_LIST_ID == priceListId);

                   return q.ToList().Select(p => new EPA.Dto.Services.ItemFilter
                {
                    ItemId = p.ITEM_ID ?? -1,
                    IType = p.ITEM_TYPE,
                    ITypeGroup = p.ITEM_TYPE_GROUP,
                    Prop1 = p.PROP_1,
                    Prop2 = p.PROP_2,
                    Prop3 = p.PROP_3,
                    Prop4 = p.PROP_4,
                    Prop5 = p.PROP_5,
                    Prop6 = p.PROP_6
                }).ToArray();
            }
               
        }
    }
}
