using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Models;
// using Effort.DataLoaders;
using System.Data.Common;

namespace EPA.UnitTests
{
    class EFTests
    {

       public void TestMethod1()
        {
            using (var db = new EPA.Data.Db())
            {
                Console.WriteLine("From Database " + db.COMPANIES.FirstOrDefault().EMAIL);
            }

            using (var db = new EPA.Data.MockDb())
            {
                Console.WriteLine("From Mock Database " + db.COMPANIES.FirstOrDefault().EMAIL);
            }

        }  
    }
}
