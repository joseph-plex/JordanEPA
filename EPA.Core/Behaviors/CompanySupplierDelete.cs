using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
   public static class CompanySupplierDelete
    {

       public static void Execute(string companyKey, int supplierId)
       {
           if (supplierId == 0)
               throw new Exception("need the supplier Id");

           var companySuppliers = EPA.Behaviors.CompanySupplierFetch.Execute(companyKey, supplierId);

           if (companySuppliers == null || companySuppliers.Any())
               throw new Exception("can't find the company supplier");

           foreach (var companySupplier in companySuppliers)
           {
               using (var db = new EPA.Data.Db())
               {
                   var model = db.COMPANY_SUPPLIERS.Where(a => a.COMPANY_SUPPLIERS_ID == companySupplier.COMPANY_SUPPLIERS_ID).FirstOrDefault();
                   db.SetToDeleted(model);
                   db.SaveChanges();
               }
           }
       }

    }
}
