using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Wrapper
{
    public static class Services
    {

        public static EPA.Wrapper.EPAService.COMPANY CompanyFetch(string key)
        {
            using (var client = new EPAService.JordanEPAServiceClient())
            {
                return client.CompanyFetch("KEY");
            }
        }

    }
}
