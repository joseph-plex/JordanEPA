using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class PriceListSetFilters
    {
        public static void Execute(String companyKey, Int32 priceListId, EPA.Dto.Models.PRICE_LIST_ITEM_TYPES[] itemTypeFilters, EPA.Dto.Services.ItemFilter[] itemFilters)
        {
            using (var db = new EPA.Data.Db())
            {
                //Valid the Information
                //  PriceListFetch priceListFetch = new PriceListFetch { Repositories = Repositories };
                {
                    var items = db.PRICE_LIST_MATERIALS.AsNoTracking().Where(p => p.PRICE_LIST_ID == priceListId).ToArray();

                    if (itemTypeFilters.Any(p => p.PRICE_LIST_ID != priceListId))
                        throw new Exception("The itemTypeFilters' PriceList Id must match the priceList Id entered in the method");

                    if (PriceListFetch.Execute(companyKey, null, priceListId).FirstOrDefault() == null)
                        throw new Exception("No such Price List");

                    foreach (var i in itemFilters)
                    {
                        EPA.Models.PRICE_LIST_MATERIALS item = items.FirstOrDefault(p => p.ITEM_ID == i.ItemId);
                        if (item == null)
                            throw new Exception("Item does not exist");
                        if (item.PRICE_LIST_ID != priceListId)
                            throw new Exception("Item does not belong to Price List Specified");
                    }
                }
                //Do stuff
                {

                    var itemTypes = db.PRICE_LIST_ITEM_TYPES.Where(p => p.PRICE_LIST_ID == priceListId).ToList();
                    bool somethingToDo = false;
                    foreach (var itemType in itemTypes)
                    {
                        db.SetToDeleted(itemType);
                        somethingToDo = true;
                    }
                    if (somethingToDo)
                        db.Save();


                    var items = db.PRICE_LIST_MATERIALS.Where(p => p.PRICE_LIST_ID == priceListId).ToArray();


                    somethingToDo = false;
                    for (int i = 0, length = items.Count(); i < length; i++)
                    {
                        var item = items[i];
                        item.PROP_1 = item.PROP_2 = item.PROP_3 = item.PROP_4 = item.PROP_5 = item.PROP_6 = String.Empty;
                        db.SetToModified(item);
                        somethingToDo = true;
                    }
                    if (somethingToDo)
                        db.Save();
                    somethingToDo = false;
                    foreach (var i in itemFilters)
                    {
                        EPA.Models.PRICE_LIST_MATERIALS item = items.FirstOrDefault(p => p.ITEM_ID == i.ItemId);

                        item.ITEM_TYPE_GROUP = i.ITypeGroup;
                        item.PROP_1 = i.Prop1;
                        item.PROP_2 = i.Prop2;
                        item.PROP_3 = i.Prop3;
                        item.PROP_4 = i.Prop4;
                        item.PROP_5 = i.Prop5;
                        item.PROP_6 = i.Prop6;
                        item.ITEM_TYPE = i.IType;
                        db.SetToModified(item);
                        somethingToDo = true;


                    }
                    if (somethingToDo)
                        db.Save();

                    somethingToDo = false;
                    foreach (var itemTypeFilter in itemTypeFilters)
                    {
                        var model = new EPA.Models.PRICE_LIST_ITEM_TYPES();
                        model = Mapper.Map<EPA.Dto.Models.PRICE_LIST_ITEM_TYPES, EPA.Models.PRICE_LIST_ITEM_TYPES>(itemTypeFilter, model);
                        model = db.AssignPrimaryKey(model, (() => model.PRICE_LIST_ITEM_TYPES_ID), Data.Sequence.PRICE_LIST_ITEM_TYPES_ID);
                        db.PRICE_LIST_ITEM_TYPES.Add(model);
                        somethingToDo = true;
                    }

                    if (somethingToDo)
                        db.Save();

                }
            }
        }
    }
}

