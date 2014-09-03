using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class SupplierDelete
    {
        public static void Execute(Dto.Models.SUPPLIER supplier)
        {
            if (supplier == null)
                throw new Exception("Your model can't be null");
            else if (supplier.SUPPLIER_ID == 0)
                throw new Exception("Need a Primary KEY to reference");

            using (var db = new EPA.Data.Db())
            {
                var model = db.SUPPLIERS.Where(a => a.SUPPLIER_ID == supplier.SUPPLIER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid supplier");
                db.SetToDeleted(model);
                db.SaveChanges();
            }
        }

        public static void Execute(int supplierId)
        {
            Execute(SupplierFetch.Execute(supplierId).FirstOrDefault());
        }
    }
}
