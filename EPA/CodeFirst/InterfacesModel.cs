using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EPA.Models.Interfaces
{


    public interface IEntity 
    {
       int ID { get; set; }
        DateTime DATE_MADE { get; set; }

    }


    public interface ICompany 
    {
        string Company { get; set; }
    }
}