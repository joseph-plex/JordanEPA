using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
    public static class SupplierFetchByEmail
    {
        public static EPA.Dto.Models.SUPPLIER[] Execute(string email = null)
        {
            using (var db = new EPA.Data.Db())
            {
                var q = db.SUPPLIERS.AsNoTracking().AsQueryable();
                if (string.IsNullOrEmpty(email) == false)
                    q = q.Where(p => string.Equals(p.EMAIL, email, StringComparison.OrdinalIgnoreCase));

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.SUPPLIER>(a)).ToArray();
            }
        }
    }
}
