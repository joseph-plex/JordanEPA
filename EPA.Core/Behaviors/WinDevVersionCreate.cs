using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EPA.Behaviors
{
    public static partial class WinDevVersionCreate
    {


        public static Dto.Models.WINDEV_CLIENT_VERSION Execute(Dto.Models.WINDEV_CLIENT_VERSION winDevVersion)
        {

            using (var db = new EPA.Data.Db())
            {
                if (string.IsNullOrEmpty(winDevVersion.URL))
                {
                    // get the last one
                    var lastModel = db.WINDEV_CLIENT_VERSION
                        .AsNoTracking()
                        .Where(a => a.URL != "" && a.URL != null)
                        .OrderByDescending(a => a.WINDEV_CLIENT_VERSION_ID)
                        .FirstOrDefault();


                    if (lastModel != null)
                    {
                        winDevVersion.URL = lastModel.URL;
                    }
                }


                var model = new EPA.Models.WINDEV_CLIENT_VERSION()
                {
                    DATE_UPDATED = DateTime.Now,
                    URL = winDevVersion.URL,
                    VERSION = winDevVersion.VERSION,
                };
                model = db.AssignPrimaryKey(model, (() => model.WINDEV_CLIENT_VERSION_ID), Data.Sequence.WINDEV_CLIENT_VERSION_ID);
                db.WINDEV_CLIENT_VERSION.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.WINDEV_CLIENT_VERSION>(model);
            }
        }
    }
}
