using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Data
{
    public static class Mappers
    {
        private static bool _MappersAreSet = false;
        public static void Initialize()
        {
            if (!_MappersAreSet)
            {
                // TUTORIAL - http://visualstudiomagazine.com/blogs/tool-tracker/2013/11/updating--entities-with-automapper.aspx
                // MIGHT NEED TO ADD THIS FOR NULLS // .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
                AutoMapper.Mapper.CreateMap<EPA.Models.COMPANY, EPA.Dto.Models.COMPANY>();
                AutoMapper.Mapper.CreateMap<EPA.Models.COMPANY_SUPPLIERS, EPA.Dto.Models.COMPANY_SUPPLIERS>();
                AutoMapper.Mapper.CreateMap<EPA.Models.COMPANY_USER_SUPPLIERS, EPA.Dto.Models.COMPANY_USER_SUPPLIERS>();
                AutoMapper.Mapper.CreateMap<EPA.Models.COMPANY_USERS, EPA.Dto.Models.COMPANY_USERS>();
                AutoMapper.Mapper.CreateMap<EPA.Models.PRICE_AGREEMENT, EPA.Dto.Models.PRICE_AGREEMENT>();
                AutoMapper.Mapper.CreateMap<EPA.Models.PRICE_AGREEMENT_ADJUSTMENTS, EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS>();
                AutoMapper.Mapper.CreateMap<EPA.Models.PRICE_AGREEMENT_MATERIALS, EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS>();
                AutoMapper.Mapper.CreateMap<EPA.Models.PRICE_LIST, EPA.Dto.Models.PRICE_LIST>();
                AutoMapper.Mapper.CreateMap<EPA.Models.PRICE_LIST_ITEM_TYPES, EPA.Dto.Models.PRICE_LIST_ITEM_TYPES>();
                AutoMapper.Mapper.CreateMap<EPA.Models.PRICE_LIST_MATERIALS, EPA.Dto.Models.PRICE_LIST_MATERIALS>();
                AutoMapper.Mapper.CreateMap<EPA.Models.SUPPLIER, EPA.Dto.Models.SUPPLIER>();
                AutoMapper.Mapper.CreateMap<EPA.Models.WINDEV_CLIENT_VERSION, EPA.Dto.Models.WINDEV_CLIENT_VERSION>();




                // THE OTHER WAY
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.COMPANY, EPA.Models.COMPANY>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.COMPANY_SUPPLIERS, EPA.Models.COMPANY_SUPPLIERS>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.COMPANY_USER_SUPPLIERS, EPA.Models.COMPANY_USER_SUPPLIERS>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.COMPANY_USERS, EPA.Models.COMPANY_USERS>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_AGREEMENT, EPA.Models.PRICE_AGREEMENT>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS, EPA.Models.PRICE_AGREEMENT_ADJUSTMENTS>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS, EPA.Models.PRICE_AGREEMENT_MATERIALS>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_LIST, EPA.Models.PRICE_LIST>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_LIST_ITEM_TYPES, EPA.Models.PRICE_LIST_ITEM_TYPES>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_LIST_MATERIALS, EPA.Models.PRICE_LIST_MATERIALS>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.SUPPLIER, EPA.Models.SUPPLIER>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.WINDEV_CLIENT_VERSION, EPA.Models.WINDEV_CLIENT_VERSION>();



                // SERVICE DTOs
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_AGREEMENT, EPA.Dto.Services.PriceAgreementIUDWrapper>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS, EPA.Dto.Services.PriceAgreementMaterialIUDWrapper>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_LIST, EPA.Dto.Services.PriceListIUDWrapper>();
                AutoMapper.Mapper.CreateMap<EPA.Dto.Models.PRICE_LIST_MATERIALS, EPA.Dto.Services.PriceListMaterialIUDWrapper>();

                // FOR INSERTS
                // AutoMapper.Mapper.CreateMap<EPA.Dto.Services.PriceListMaterialIUDWrapper, EPA.Models.PRICE_LIST_MATERIALS >();
                // AutoMapper.Mapper.CreateMap<EPA.Dto.Services.PriceListIUDWrapper, EPA.Models.PRICE_LIST>();


                _MappersAreSet = true;
            }
        }

        public class NullIntToIntConverter : AutoMapper.TypeConverter<int?, int>
        {
            protected override int ConvertCore(int? source)
            {
                return Plexxis.Helpers.Numbers.ToInt(source);
            }
        }

        public class IntToNullIntConverter : AutoMapper.TypeConverter<int, int?>
        {

            protected override int? ConvertCore(int source)
            {
                if (source == 0)
                    return (int?)null;
                else
                    return source;
            }
        }
    }
}
