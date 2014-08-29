using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Dto;
using AutoMapper;

namespace EPA.Dto.Services
{

    public static class Create
    {
        public static PriceAgreementMaterialIUDWrapper PriceAgreementMaterialIUDWrapper(PRICE_AGREEMENT_MATERIALS priceAgreementMaterial)
        {
            return Mapper.Map<PriceAgreementMaterialIUDWrapper>(priceAgreementMaterial);
        }
        public static PriceAgreementIUDWrapper PriceAgreementIUDWrapper(PRICE_AGREEMENT priceAgreement)
        {
            return Mapper.Map<PriceAgreementIUDWrapper>(priceAgreement);
        }
        public static PriceListIUDWrapper PriceAgreementIUDWrapper(PRICE_LIST priceList)
        {
            return Mapper.Map<PriceListIUDWrapper>(priceList);
        }

        public static PriceListMaterialIUDWrapper PriceListMaterialIUDWrapper(PRICE_LIST_MATERIALS priceListMaterials)
        {
            return Mapper.Map<PriceListMaterialIUDWrapper>(priceListMaterials);
        }


    }

    public class ItemFilter
    {
        public int ItemId { get; set; }
        public string IType { get; set; }
        public string ITypeGroup { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        public string Prop3 { get; set; }
        public string Prop4 { get; set; }
        public string Prop5 { get; set; }
        public string Prop6 { get; set; }
    }

    public class ItemTypeFilter
    {
        public string IType { get; set; }
        public string ITypeGroup { get; set; }
        public string PropTitle1 { get; set; }
        public string PropTitle2 { get; set; }
        public string PropTitle3 { get; set; }
        public string PropTitle4 { get; set; }
        public string PropTitle5 { get; set; }
        public string PropTitle6 { get; set; }


    }

    public class PriceAgreementIUDResponse
    {
        public EPA.Dto.PRICE_AGREEMENT PRICE_AGREEMENT { get; set; }
        public EPA.Dto.PRICE_AGREEMENT_MATERIALS[] PRICE_AGREEMENT_MATERIALS { get; set; }
    }

    public class PriceAgreementIUDWrapper : EPA.Dto.PRICE_AGREEMENT
    {
        public int Operation { get; set; }

        public PriceAgreementIUDWrapper() : base() { }
        //public PriceAgreementIUDWrapper(PlexQueryResultTuple plexTuple) : base(plexTuple) { }
        //public PriceAgreementIUDWrapper(PRICE_AGREEMENT priceList)
        //    : this()
        //{
        //    foreach (var p in typeof(PRICE_AGREEMENT).GetProperties())
        //    {
        //        object value = p.GetValue(this);
        //        var conversationType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
        //        value = Convert.ChangeType(value, (value != null) ? conversationType : p.PropertyType);
        //        p.SetValue(this, value);
        //    }
        //}
    }

    public class PriceAgreementMaterialIUDWrapper : PRICE_AGREEMENT_MATERIALS
    {
        public int Operation { get; set; }

        public PriceAgreementMaterialIUDWrapper() : base() { }
        /*  public PriceAgreementMaterialIUDWrapper(PlexQueryResultTuple plexTuple) : base(plexTuple) { }
          public PriceAgreementMaterialIUDWrapper(PRICE_AGREEMENT_MATERIALS priceList)
          {
              this = priceList.Clone() as PRICE_AGREEMENT_MATERIALS;

              foreach (var p in typeof(PRICE_AGREEMENT_MATERIALS).GetProperties())
              {
                  object value = p.GetValue(this);
                  var conversationType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                  value = Convert.ChangeType(value, (value != null) ? conversationType : p.PropertyType);
                  p.SetValue(this, value);
              }
          } */


    }

    public class PriceListIUDResponse
    {
        public PRICE_LIST PRICE_LIST { get; set; }
        public PRICE_LIST_MATERIALS[] PRICE_LIST_MATERIALS { get; set; }
    }
    public class PriceListIUDWrapper : PRICE_LIST
    {
        public int Operation { get; set; }

        public PriceListIUDWrapper() : base() { }

    }
    public class PriceListMaterialIUDWrapper : PRICE_LIST_MATERIALS
    {
        public int Operation { get; set; }


        public PriceListMaterialIUDWrapper() : base() { }
     /*   public PriceListMaterialIUDWrapper(PlexQueryResultTuple plexTuple) : base(plexTuple) { }
        public PriceListMaterialIUDWrapper(PRICE_LIST_MATERIALS priceList)
            : this()
        {
            this = Mapper.Map<PriceListMaterialIUDWrapper>(priceList);


            foreach (var p in typeof(PRICE_LIST_MATERIALS).GetProperties())
            {
                object value = p.GetValue(this);
                var conversationType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                value = Convert.ChangeType(value, (value != null) ? conversationType : p.PropertyType);
                p.SetValue(this, value);
            }
        } */
    }
}
