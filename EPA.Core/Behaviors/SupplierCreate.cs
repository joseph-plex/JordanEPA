using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class SupplierCreate
    {


        public static Dto.Models.SUPPLIER Execute(string description, string email)
        {
            return Execute(new Dto.Models.SUPPLIER { DESCRIPTION = description, EMAIL = email });
        }
        public static Dto.Models.SUPPLIER Execute(Dto.Models.SUPPLIER supplier)
        {

            if (string.IsNullOrEmpty(supplier.DESCRIPTION))
                throw new Exception("Description cannot be empty or null");
            else if (string.IsNullOrEmpty(supplier.EMAIL))
                throw new Exception("EMAIL cannot be empty or null");

            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.SUPPLIER();
                model = Mapper.Map<EPA.Dto.Models.SUPPLIER, EPA.Models.SUPPLIER>(supplier, model);
                model = db.AssignPrimaryKey(model, (() => model.SUPPLIER_ID), Data.Sequence.SUPPLIER_ID);
                db.SUPPLIERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.SUPPLIER>(model);
            }
        }
    }
}