using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Services.Dto;

namespace EPA.Services.Behaviors
{
    public class PriceListIUD : PriceListIUDResponse
    {

        public Func<Int32> PriceListIdGenerator { get; set; }
        public Func<Int32> PriceListMaterialIdGenerator { get; set; }

        public PriceListIUDResponse Strategy(String companyKey, PriceListIUDWrapper pricelist = null, IEnumerable<PriceListMaterialIUDWrapper> pricelistMaterials = null)
        {
            PriceListIUDResponse response = new PriceListIUDResponse();
            pricelistMaterials = pricelistMaterials ?? new PriceListMaterialIUDWrapper[0];

            if (pricelist == null && pricelistMaterials.Count() == 0)
                throw new Exception("pricelist or pricelistMaterials must have a value");

            if (pricelist != null)
            {
                if (pricelistMaterials.Any(p => p.PRICE_LIST_ID != pricelist.PRICE_LIST_ID))
                    throw new FormatException("Pricelist must have the same ID as all PriceListItems");
            }
            else
            {
                var id = pricelistMaterials.First().PRICE_LIST_ID;
                if (!pricelistMaterials.Any(p => p.PRICE_LIST_ID == id))
                    throw new FormatException("All pricelist materials must have the same Id");
            }

            return new PriceListIUDResponse
            {
                PRICE_LIST = (pricelist != null) ? processPriceLists(companyKey, pricelist) : null,
                PRICE_LIST_MATERIALS = processPriceListMaterials(companyKey, pricelistMaterials).ToArray()
            };
        }

        private PRICE_LIST processPriceLists(String key, PriceListIUDWrapper priceList)
        {
            var pricelistRepository = GetRepository<PRICE_LIST>();
            switch (priceList.Operation)
            {
                case 0: //Do nothing, 0 is a valid operation but serves no purpose here
                    return new PriceListFetch { Repositories = Repositories }.Strategy(key, null, priceList.PRICE_LIST_ID).First();
                case 1: //Create
                    priceList.PRICE_LIST_ID = PriceListIdGenerator();
                    pricelistRepository.Insert(priceList);
                    return new PriceListFetch { Repositories = Repositories }.Strategy(key, null, priceList.PRICE_LIST_ID).First();
                case 2: //Modify
                    pricelistRepository.Update(priceList);
                    return new PriceListFetch { Repositories = Repositories }.Strategy(key, null, priceList.PRICE_LIST_ID).First();
                case 3: //Delete
                    pricelistRepository.Delete(p => p.PRICE_LIST_ID == priceList.PRICE_LIST_ID);
                    return null;
                default: //not a valid operation
                    throw new NotSupportedException("There is no such operation value");
            }
        }

        private IEnumerable<PRICE_LIST_MATERIALS> processPriceListMaterials(String key, IEnumerable<PriceListMaterialIUDWrapper> pricelistMaterials)
        {
            var pricelistItemRepository = GetRepository<PRICE_LIST_MATERIALS>();
            var list = pricelistMaterials.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var material = list[i];
                switch (material.Operation)
                {
                    case 0:
                        yield return new PriceListMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                        //Do nothing, 0 is a valid operation but serves no purpose here
                        break;
                    case 1://create
                        material.PRICE_LIST_MATERIAL_ID = PriceListMaterialIdGenerator();
                        pricelistItemRepository.Insert(material);
                        yield return new PriceListMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                        break;
                    case 2://modify
                        pricelistItemRepository.Update(material);
                        yield return new PriceListMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                        break;
                    case 3://delete
                        pricelistItemRepository.Delete(p => p.PRICE_LIST_MATERIAL_ID == material.PRICE_LIST_MATERIAL_ID);
                        break;
                    default: //not a valid operation                
                        throw new NotSupportedException("There is no such operation value");
                }
            }
        }
    }
}
