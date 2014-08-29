using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Dto.Services;

namespace EPA.Services.Behaviors
{
    public class PriceAgreementIUD : PriceAgreementIUDResponse // MethodStrategyBase<PriceAgreementIUDResponse>
    {
        public Func<Int32> PriceAgreementIdGenerator { get; set; }
        public Func<Int32> PriceAgreementMaterialIdGenerator { get; set; }

        public PriceAgreementIUDResponse Strategy(String companyKey, PriceAgreementIUDWrapper priceAgreement, PriceAgreementMaterialIUDWrapper[] priceAgreementMaterials)
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

        private EPA.Dto.PRICE_AGREEMENT ProcessPriceAgreement(String key, PriceAgreementIUDWrapper priceAgreement)
        {
            EPAService service = new EPAService();

            var priceAgreementRepository = GetRepository<PRICE_AGREEMENT>();
            switch (priceAgreement.Operation) // MAGIC NUMBERS
            {
                case 0:
                    //Do nothing, 0 is a valid operation but serves no purpose here
                    return service.PriceAgreementFetch(key, null, null, priceAgreement.PRICE_AGREEMENT_ID).FirstOrDefault();
                   // return new PriceAgreementFetch { Repositories = Repositories }.Strategy(key, null, null, priceAgreement.PRICE_AGREEMENT_ID).First();
                case 1: //Create

                    service.PR
                    priceAgreement.PRICE_AGREEMENT_ID = PriceAgreementIdGenerator();
                    priceAgreementRepository.Insert(priceAgreement);
                    return new PriceAgreementFetch { Repositories = Repositories }.Strategy(key, null, null, priceAgreement.PRICE_AGREEMENT_ID).First();
                case 2: //Modify
                    priceAgreementRepository.Update(priceAgreement);
                    return new PriceAgreementFetch { Repositories = Repositories }.Strategy(key, null, null, priceAgreement.PRICE_AGREEMENT_ID).First();
                case 3: //Delete
                    priceAgreementRepository.Delete(p => p.PRICE_AGREEMENT_ID == priceAgreement.PRICE_AGREEMENT_ID);
                    return null;
                default: //not a valid operation
                    throw new NotSupportedException("There is no such operation value");
            }
        }

        private IEnumerable<EPA.Dto.PRICE_AGREEMENT_MATERIALS> ProcessPriceAgreementMaterial(String key, IEnumerable<PriceAgreementMaterialIUDWrapper> priceAgreementMaterials)
        {
            var priceAgreementMaterialRepo = GetRepository<PRICE_AGREEMENT_MATERIALS>();
            foreach (var material in priceAgreementMaterials)
            {
                switch (material.Operation)
                {
                    case 0:
                        yield return new PriceAgreementMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                        break;
                    case 1:
                        material.PRICE_LIST_MATERIAL_ID = PriceAgreementMaterialIdGenerator();
                        priceAgreementMaterialRepo.Insert(material);
                        yield return new PriceAgreementMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                        break;
                    case 2:
                        priceAgreementMaterialRepo.Update(material);
                        yield return new PriceAgreementMaterialFetch { Repositories = Repositories }.Strategy(key, null, null, null, material.PRICE_LIST_MATERIAL_ID).First();
                        break;
                    case 3:
                        priceAgreementMaterialRepo.Delete(p => p.PRICE_LIST_MATERIAL_ID == material.PRICE_LIST_MATERIAL_ID && p.PRICE_AGREEMENT_ID == material.PRICE_AGREEMENT_ID);
                        break;
                    default:
                        throw new NotSupportedException("There is no such operation value");
                }
            }
        }
    }
}
