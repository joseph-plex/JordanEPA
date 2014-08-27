using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;

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
        public string CompanyFetchTest(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            try
            {
                using (var db = new EPA.Models.DbFirstEntities())
                {
                    var c = db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault();
                    if (c != null)
                        return "description: " + c.DESCRIPTION;
                    else
                        return "c is null";
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                return errorMessage; //Display or log the error based on your application.
            }
        
        }


        public EPA.Models.COMPANY[] CompanyFetchArray(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            using (var db = new EPA.Models.DbFirstEntities())
            {
                return new EPA.Models.COMPANY[] {
                    db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault()
                };
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
