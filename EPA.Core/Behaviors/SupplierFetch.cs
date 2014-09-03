using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plexxis.Helpers.Extensions;

namespace EPA.Behaviors
{
    public static class SupplierFetch
    {
        public static Dto.Models.SUPPLIER[] Execute(int? supplierId = null)
        {
            // PROBLEM
            // WHY WOULD IT BE NULL ? SHOULD IT RETURN EVERYTHING?

            int useSupplierId = supplierId.ToInt();
            // if (useSupplierId == 0)
            //     return null;

            using (var db = new EPA.Data.Db())
            {

                var q = db.SUPPLIERS.AsNoTracking().AsQueryable();
                if (useSupplierId != 0)
                    q = q.Where(a => a.SUPPLIER_ID == useSupplierId);

                
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.SUPPLIER>(a)).ToArray();
            }


        }
    }
}
