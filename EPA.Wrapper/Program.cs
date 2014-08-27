using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Wrapper
{
    class Program
    {
        static void Main(string[] args)
        {



            {
                Console.WriteLine("Wrapper Connecting to WebService");
                var c2 = EPA.Wrapper.Services.CompanyFetch("KEY");
                // var client = new EPAClient.EPALive.JordanEPAServiceClient(); // .EPA.Services.COMPANY();
                // var c2 = client.CompanyFetch("KEY");
                Console.WriteLine(c2.DESCRIPTION);

                var c3 = EPA.Wrapper.Services.CompanyFetch("KEY");
                Console.WriteLine("Second Try - " + c2.DESCRIPTION);
                Console.ReadLine();
            }

        }
    }
}