using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
   public static class SupplierModify
    {
        public static Dto.Models.SUPPLIER Execute(Dto.Models.SUPPLIER supplier)
        {
            if (supplier == null)
                throw new Exception("Your model can't be null");
            else if (supplier.SUPPLIER_ID == 0)
                throw new Exception("Need a Primary Key ID to reference");


            using (var db = new EPA.Data.Db())
            {
                var model = db.SUPPLIERS.Where(a => a.SUPPLIER_ID == supplier.SUPPLIER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find supplier to modify based on SUPPLIER_ID " + supplier.SUPPLIER_ID);

                model = Mapper.Map<EPA.Dto.Models.SUPPLIER, EPA.Models.SUPPLIER>(supplier, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.SUPPLIER>(model);
            }
        }
    }
}
