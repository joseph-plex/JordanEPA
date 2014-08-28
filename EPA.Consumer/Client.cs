using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using EPA.Consumer.Service;
namespace EPA.Consumer
{
    public class Client : IJordanEPAService
    {
        #region Background

        ChannelFactory<IJordanEPAService> factory;
        IJordanEPAService channel;

        public Client(String uri)
        {
            BasicHttpBinding binding = new BasicHttpBinding()
            {
                UseDefaultWebProxy = false,
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxBufferSize = 2147483647
            };
            factory = new ChannelFactory<IJordanEPAService>(binding, new EndpointAddress(uri));
            factory.Open();
            channel = factory.CreateChannel();
        }

        public IJordanEPAService CreateChannel()
        {
            return factory.CreateChannel();
        }
        public string GetData(int value)
        {
            return channel.GetData(value);
        }

        public Task<string> GetDataAsync(int value)
        {
            return Task<string>.Run(() => GetData(value));
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public Task<CompositeType> GetDataUsingDataContractAsync(CompositeType composite)
        {
            throw new NotImplementedException();
        }


        #endregion


        public COMPANY CompanyFetch(string key)
        {
            return channel.CompanyFetch(key);
        }

        public Task<COMPANY> CompanyFetchAsync(string key)
        {
            throw new NotImplementedException();
        }

        public string CompanyFetchTest(string key)
        {
            return channel.CompanyFetchTest(key);
        }

        public Task<string> CompanyFetchTestAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
