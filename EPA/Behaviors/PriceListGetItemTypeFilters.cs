using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class PriceListGetItemTypeFilters
    {
        public static Dto.Models.PRICE_LIST_ITEM_TYPES[] Execute(string companyKey, int priceListId)
        {

            /*
            PriceListFetch priceListFetch = new PriceListFetch { Repositories = Repositories };
            var priceList = priceListFetch.Strategy(companyKey, null, priceListId).FirstOrDefault();
            if (priceList == null)
                throw new Exception("Price Agreement does not exist for specified company Key");
            return GetRepository<PRICE_LIST_ITEM_TYPES>().RetrieveAll().Where(p => p.PRICE_LIST_ID == priceListId).ToArray();
            */

            var priceLists = PriceListFetch.Execute(companyKey, null, priceListId);
            if (priceLists == null || priceLists.Any() == false)
                throw new Exception("Price Agreement does not exist for specified company Key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST_ITEM_TYPES.AsNoTracking().Where(p => p.PRICE_LIST_ID == priceListId);
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_LIST_ITEM_TYPES>(a)).ToArray();

            }
        }
    }
}
