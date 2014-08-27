using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Extensions;

namespace EPA.Data
{
    public static class Test
    {
        public static Ravka.Outcome AddCompany()
        {
          //  using (var db = new EPA.Models.Company...Model.s..Data..EPADb())
         
      
            using (var db = new EPA.Models.DbFirstEntities())
            {
                var newC = new Models.COMPANY {
                     CODE = "hello",
                    DESCRIPTION = "DESCRIPTION",
                    KEY = "KEY",
                    EMAIL = "TEST@TEST.COM",
                    ROW_VERSION = 1,
                };

                // db.COMPANIES.Add(newC);

               // db.InsertOrUpdate(newC);
                var c = db.COMPANIES.Where(a => a.KEY == "KEY").FirstOrDefault();
               

                db.SaveChanges();
                return new Ravka.Outcome(true, "hello" + c.DESCRIPTION);
            }
           
        

            using (var db = new EPA.Data.EPADb())
            {
                db.Companies.Add(new CodeFirst.Company
                {
                    CODE = "hello",
                    DESCRIPTION = "DESCRIPTION",
                    KEY = "KEY",
                    EMAIL = "TEST@TEST.COM",
                    ROW_VERSION = 1,

                });
               return db.Save();
            }
        }

        public static void SetMessage(Ravka.Outcome outcome)
        {
           string txt = (outcome.HasError ? "Error" : "Success") + ": " + outcome.Message;
           Console.WriteLine(txt);
           Console.WriteLine();
        }

    }
}
