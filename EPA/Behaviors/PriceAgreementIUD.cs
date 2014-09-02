using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Dto.Services;
using AutoMapper;

namespace EPA.Behaviors
{
    public static class PriceAgreementIUD : PriceAgreementIUDResponse // MethodStrategyBase<PriceAgreementIUDResponse>
    {
        // this needs work
        // PRICE_AGREEMENT_ID vs PRICE_AGREEMENT_MATERIAL_ID
        // PRIMARY KEY CREATION
        // NO TRY/CATCH
        // PUT IN TRANSACTION
      //  public Func<Int32> PriceAgreementIdGenerator { get; set; }
      //  public Func<Int32> PriceAgreementMaterialIdGenerator { get; set; }

        public static PriceAgreementIUDResponse Execute(String companyKey, PriceAgreementIUDWrapper priceAgreement, PriceAgreementMaterialIUDWrapper[] priceAgreementMaterials)
        {

            priceAgreementMaterials = priceAgreementMaterials ?? new PriceAgreementMaterialIUDWrapper[0];

            if (priceAgreement == null && priceAgreementMaterials.Count() == 0)
                throw new Exception("Price Agreement or Price Agreement Materials must have a value");

            if (priceAgreement != null)
            {
                if (priceAgreementMaterials.Any(p => p.PRICE_AGREEMENT_ID == priceAgreement.PRICE_AGREEMENT_ID))
                    throw new FormatException("Price Agreement must have the same Id as all Price Agreement Materials");
            }
            else
            {
                var id = priceAgreementMaterials.First().PRICE_AGREEMENT_ID;
                if (!priceAgreementMaterials.Any(p => p.PRICE_AGREEMENT_ID == id))
                    throw new FormatException("All price agreements must have the same Id");
            }
            return new PriceAgreementIUDResponse
            {
                PRICE_AGREEMENT = (priceAgreement != null) ? ProcessPriceAgreement(companyKey, priceAgreement) : null,
                PRICE_AGREEMENT_MATERIALS = ProcessPriceAgreementMaterial(companyKey, priceAgreementMaterials).ToArray()
            };
        }

        private static EPA.Dto.Models.PRICE_AGREEMENT ProcessPriceAgreement(String key, PriceAgreementIUDWrapper priceAgreementWrapper)
        {
            var priceAgreement = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT>(priceAgreementWrapper);
            using (var db = new EPA.Data.Db())
            {

                switch (priceAgreementWrapper.Operation) // MAGIC NUMBERS
                {
                    case 0:
                        //Do nothing, 0 is a valid operation but serves no purpose here
                        return PriceAgreementFetch.Execute(key, null, null, priceAgreement.PRICE_AGREEMENT_ID).FirstOrDefault();
                    // return new PriceAgreementFetch { Repositories = Repositories }.Strategy(key, null, null, priceAgreement.PRICE_AGREEMENT_ID).First();
                    case 1: //Create
                        {

                            var model = new EPA.Models.PRICE_AGREEMENT();
                            model = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT, EPA.Models.PRICE_AGREEMENT>(priceAgreement, model);
                            // model.PRICE_LIST_MATERIAL_ID = PriceAgreementMaterialIdGenerator();
                            // model.PRICE_AGREEMENT_ID = PriceAgreementIdGenerator();
                            db.PRICE_AGREEMENT.Add(model);
                            db.SaveChanges();
                            return (Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT>(model));
                        }
           
                       
                    case 2: //Modify
                        {
                            var model = db.PRICE_AGREEMENT.Where(a => a.PRICE_AGREEMENT_ID == priceAgreement.PRICE_AGREEMENT_ID).FirstOrDefault();
                            if (model == null)
                                throw new Exception("Can't find object to modify based on ID " + priceAgreement.PRICE_AGREEMENT_ID);

                            model = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT, EPA.Models.PRICE_AGREEMENT>(priceAgreement, model);
                            db.SetToModified(model);
                            db.SaveChanges();
                            return (Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT>(model));
                        }
                      
                    case 3: //Delete
                        {
                            var model = db.PRICE_AGREEMENT
                                .Where(a => a.PRICE_AGREEMENT_ID == priceAgreement.PRICE_AGREEMENT_ID)
                                .FirstOrDefault();
                            if (model == null)
                                throw new Exception("Can't find object to delete based on PRICE_AGREEMENT_ID " + priceAgreement.PRICE_AGREEMENT_ID);
                            db.SetToDeleted(model);
                            db.SaveChanges();

                        }
                      
                        return null;
                    default: //not a valid operation
                        throw new NotSupportedException("There is no such operation value");
                }
            }
        }

        private static IEnumerable<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS> ProcessPriceAgreementMaterial(String key, IEnumerable<PriceAgreementMaterialIUDWrapper> priceAgreementMaterialsWrapper)
        {
            var list = priceAgreementMaterialsWrapper.ToList();
            List<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS> results = new List<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS>();
            using (var db = new EPA.Data.Db())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var material = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS>(list[i]);

                    switch (list[i].Operation)
                    {
                        case 0:
                            {
                                var priceListMaterial = PriceAgreementMaterialFetch.Execute(key, null,  priceAgreementMaterialId: material.PRICE_AGREEMENT_MATERIAL_ID).FirstOrDefault();
                                if (priceListMaterial != null)
                                    results.Add(priceListMaterial);
                            }
                            //  yield return new PriceAgreementMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                            break;
                        case 1: //create
                            {
                                var model = new EPA.Models.PRICE_AGREEMENT_MATERIALS();
                                model = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS, EPA.Models.PRICE_AGREEMENT_MATERIALS>(material, model);
                                // model.PRICE_LIST_MATERIAL_ID = PriceAgreementMaterialIdGenerator();
                                db.PRICE_AGREEMENT_MATERIALS.Add(model);
                                db.SaveChanges();
                                results.Add(Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS>(model));
                            }
                          
                           // priceAgreementMaterialRepo.Insert(material);
                           // yield return new PriceAgreementMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                            break;
                        case 2:  //modify
                            {
                                var model = db.PRICE_AGREEMENT_MATERIALS.Where(a => a.PRICE_AGREEMENT_MATERIAL_ID == material.PRICE_AGREEMENT_MATERIAL_ID).FirstOrDefault();
                                if (model == null)
                                    throw new Exception("Can't find object to modify based on ID " + material.PRICE_AGREEMENT_MATERIAL_ID);

                                model = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS, EPA.Models.PRICE_AGREEMENT_MATERIALS>(material, model);
                                db.SetToModified(model);
                                db.SaveChanges();
                                results.Add(Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS>(model));
                            }

                            // priceAgreementMaterialRepo.Update(material);
                            // yield return new PriceAgreementMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, null, material.PRICE_AGREEMENT_MATERIAL_ID).First();
                            break;
                        case 3: //delete
                            {
                                var model = db.PRICE_AGREEMENT_MATERIALS
                                    .Where(p => p.PRICE_LIST_MATERIAL_ID == material.PRICE_LIST_MATERIAL_ID && p.PRICE_AGREEMENT_ID == material.PRICE_AGREEMENT_ID)
                                   // .Where(a => a.PRICE_AGREEMENT_MATERIAL_ID == material.PRICE_AGREEMENT_MATERIAL_ID)
                                    .FirstOrDefault();
                                if (model == null)
                                    throw new Exception("Can't find object to delete based on PRICE_LIST_MATERIAL_ID " + material.PRICE_LIST_MATERIAL_ID);
                                db.SetToDeleted(model);
                                db.SaveChanges();
                              
                            }

                           // priceAgreementMaterialRepo.Delete(p => p.PRICE_LIST_MATERIAL_ID == material.PRICE_LIST_MATERIAL_ID && p.PRICE_AGREEMENT_ID == material.PRICE_AGREEMENT_ID);
                            break;
                        default:
                            throw new NotSupportedException("There is no such operation value");
                    }
                    //  }
                }
            }
            return results;
        }
    }
}
