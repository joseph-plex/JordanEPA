using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EPA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEPAService" in both code and config file together.
    [ServiceContract]
    public interface IEPAService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        Dto.COMPANY CompanyFetch(string key);




        #region Company Operations
        [OperationContract]
        Dto.COMPANY CompanyCreate(String description);
        [OperationContract]
        Dto.COMPANY CompanyModify(Dto.COMPANY company);
        [OperationContract]
        void CompanyDelete(Dto.COMPANY company);
   
        #endregion

    }
}
