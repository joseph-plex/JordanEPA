using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel; 
namespace TempTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            TestMethod1();

            
           // EPA.Data.Test.SetMessage( EPA.Data.Test.AddCompany());
        }


        public static void TestMethod1()
        {
            using (var db = new EPA.Data.Db())
            {
                Console.WriteLine("From Database " + db.COMPANIES.FirstOrDefault().EMAIL);
            }

            using (var db = new EPA.Data.MockDb())
            {
                Console.WriteLine("From Mock Database " + db.COMPANIES.FirstOrDefault().EMAIL);
            }
            Console.ReadLine();
        }
        /*
       public static void HostWebService()
        {
            // Step 1 Create a URI to serve as the base address.
            var baseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/EPA.Services/EPAService/"); //  http://localhost:8000/GettingStarted/");

            // Step 2 Create a ServiceHost instance
            ServiceHost selfHost = new ServiceHost(typeof(EPA.Services.EPAService), baseAddress);

            try
            {
                // Step 3 Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(EPA.Services.IEPAService), new WSHttpBinding(), "EPAService");

                // Step 4 Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5 Start the service.
                selfHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
   */
    
    }
}
