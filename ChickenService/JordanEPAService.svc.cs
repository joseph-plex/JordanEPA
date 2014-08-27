﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace ChickenService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class JordanEPAService : IJordanEPAService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public EPA.Models.COMPANY CompanyFetch(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            using (var db = new EPA.Models.DbFirstEntities())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault();
            }
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}