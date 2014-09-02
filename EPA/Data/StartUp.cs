using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EPA
{

    public static class StartUp
    {
        public static void Initialize()
        {
             EPA.Data.Mappers.Initialize();
        }

    
       
    }
}