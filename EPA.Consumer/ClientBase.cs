using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using EPA.Consumer.Service;

namespace EPA.Consumer
{
    public class ClientBase
    {
        #region Background

        ChannelFactory<IEPAService> factory;
       protected IEPAService channel;

        public ClientBase(string uri)
        {
            BasicHttpBinding binding = new BasicHttpBinding()
            {
                UseDefaultWebProxy = false,
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxBufferSize = 2147483647
            };
            factory = new ChannelFactory<IEPAService>(binding, new EndpointAddress(uri));
            factory.Open();
            channel = factory.CreateChannel();
        }

        public IEPAService CreateChannel()
        {
            return factory.CreateChannel();
        }
        #endregion
    }
}
