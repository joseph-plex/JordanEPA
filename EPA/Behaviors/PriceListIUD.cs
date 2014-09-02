using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Dto.Services;
using EPA.Dto.Models;
using AutoMapper;

namespace EPA.Behaviors
{
    public static class PriceListIUD : PriceListIUDResponse
    {

     //   public Func<Int32> PriceListIdGenerator { get; set; }
     //   public Func<Int32> PriceListMaterialIdGenerator { get; set; }

        public static PriceListIUDResponse Execute(String companyKey, PriceListIUDWrapper pricelist = null, IEnumerable<PriceListMaterialIUDWrapper> pricelistMaterials = null)
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

        private static PRICE_LIST processPriceLists(String key, PriceListIUDWrapper priceListWrapper)
        {
            // var pricelistRepository = GetService<PRICE_LIST>();

            {
                var priceList = Mapper.Map<EPA.Dto.Models.PRICE_LIST>(priceListWrapper);
                switch (priceListWrapper.Operation)
                {
                    case 0: //Do nothing, 0 is a valid operation but serves no purpose here
                        return EPA.Services.EPAService.GetService.PriceListFetch(key, null, priceList.PRICE_LIST_ID).FirstOrDefault();
                    case 1: //Create

                        using (var db = new EPA.Data.Db())
                        {
                            var model = new EPA.Models.PRICE_LIST();
                            model = Mapper.Map<EPA.Dto.Models.PRICE_LIST, EPA.Models.PRICE_LIST>(priceList, model);
                            db.PRICE_LIST.Add(model);
                            db.SaveChanges();
                            return Mapper.Map<EPA.Dto.Models.PRICE_LIST>(model);
                        }

                    case 2: //Modify

                        using (var db = new EPA.Data.Db())
                        {
                            var model = db.PRICE_LIST.Where(a => a.PRICE_LIST_ID == priceList.PRICE_LIST_ID).FirstOrDefault();
                            if (model == null)
                                throw new Exception("Can't find object to modify based on ID " + priceList.PRICE_LIST_ID);

                            model = Mapper.Map<EPA.Dto.Models.PRICE_LIST, EPA.Models.PRICE_LIST>(priceList, model);
                            db.SetToModified(model);
                            db.SaveChanges();
                            return Mapper.Map<EPA.Dto.Models.PRICE_LIST>(model);
                        }

                    case 3: //Delete

                        using (var db = new EPA.Data.Db())
                        {
                            var model = db.PRICE_LIST.Where(a => a.PRICE_LIST_ID == priceList.PRICE_LIST_ID).FirstOrDefault();
                            if (model == null)
                                throw new Exception("Can't find object to delete based on ID " + priceList.PRICE_LIST_ID);
                            db.SetToDeleted(model);
                            db.SaveChanges();
                            return null;
                        }

                    default: //not a valid operation
                        throw new NotSupportedException("There is no such operation value");
                }
            }
        }

        public static PRICE_LIST_MATERIALS[] PriceListMaterialFetch(String companyKey, int? companyUserId = null, int? priceListId = null, int? priceListMaterialId = null)
        {

            var priceLists = EPA.Services.EPAService.GetService.PriceListFetch(companyKey, companyUserId, priceListId);
            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST_MATERIALS.AsNoTracking()
                    .Where(p => priceLists.Any(c => c.PRICE_LIST_ID == p.PRICE_LIST_ID));

                if (priceListMaterialId != null)
                {
                    q = q.Where(p => p.PRICE_LIST_MATERIAL_ID == priceListMaterialId);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS>(a)).ToArray();

            }

        }


        private static IEnumerable<PRICE_LIST_MATERIALS> processPriceListMaterials(String key, IEnumerable<PriceListMaterialIUDWrapper> priceListMaterialsWrapper)
        {
            var list = priceListMaterialsWrapper.ToList();

            List<PRICE_LIST_MATERIALS> results = new List<PRICE_LIST_MATERIALS>();
            using (var db = new EPA.Data.Db())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var material = Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS>(list[i]);

                    switch (list[i].Operation)
                    {
                        case 0:
                            // using (var db = new EPA.Data.Db())
                            {
                                var priceListMaterial = PriceListMaterialFetch(key, null, null, material.PRICE_LIST_MATERIAL_ID).FirstOrDefault();
                                if (priceListMaterial != null)
                                    results.Add(priceListMaterial);
                            }
                            //Do nothing, 0 is a valid operation but serves no purpose here
                            break;

                        case 1://create

                          //  using (var db = new EPA.Data.Db())
                            {
                                var model = new EPA.Models.PRICE_LIST_MATERIALS();
                                model = Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS, EPA.Models.PRICE_LIST_MATERIALS>(material, model);
                                db.PRICE_LIST_MATERIALS.Add(model);
                                db.SaveChanges();
                                results.Add(Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS>(model));
                            }
                            break;

                        case 2: //modify

                           // using (var db = new EPA.Data.Db())
                            {
                                var model = db.PRICE_LIST_MATERIALS.Where(a => a.PRICE_LIST_MATERIAL_ID == material.PRICE_LIST_MATERIAL_ID).FirstOrDefault();
                                if (model == null)
                                    throw new Exception("Can't find object to modify based on ID " + material.PRICE_LIST_MATERIAL_ID);

                                model = Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS, EPA.Models.PRICE_LIST_MATERIALS>(material, model);
                                db.SetToModified(model);
                                db.SaveChanges();
                                results.Add(Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS>(model));
                            }
                            break;

                        case 3://delete

                          //  using (var db = new EPA.Data.Db())
                            {
                                var model = db.PRICE_LIST_MATERIALS.Where(a => a.PRICE_LIST_MATERIAL_ID == material.PRICE_LIST_MATERIAL_ID).FirstOrDefault();
                                if (model == null)
                                    throw new Exception("Can't find object to delete based on ID " + material.PRICE_LIST_MATERIAL_ID);
                                db.SetToDeleted(model);
                                db.SaveChanges();
                            
                            }

                            break;

                        default: //not a valid operation                
                            throw new NotSupportedException("There is no such operation value");
                    }
                }
            }
            return results;
        }
    }
}
