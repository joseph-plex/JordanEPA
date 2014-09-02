using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Behaviors
{
   public static class WinDevVersionFetchLatestVersion
    {

       public static Dto.Models.WINDEV_CLIENT_VERSION Execute()
       {
           

           using (var db = new EPA.Data.Db())
           {
               var model = db.WINDEV_CLIENT_VERSION.AsNoTracking().OrderByDescending(a => a.ID).FirstOrDefault();
               return Mapper.Map<EPA.Dto.Models.WINDEV_CLIENT_VERSION>(model);
           }
       }

    }
}
