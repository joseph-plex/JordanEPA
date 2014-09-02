using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;
using AutoMapper;
namespace EPA
{
    class Program
    {
        static void Main(string[] args)
        {
            EPA.StartUp.Initialize();
            TestDtoToModel();
            // Effort.Provider.EffortProviderConfiguration.RegisterProvider();
          //  TestMethod1();


            // EPA.Data.Test.SetMessage( EPA.Data.Test.AddCompany());
        }

        public static void TestDtoToModel()
        {
            EPA.Services.EPAService svc = new Services.EPAService();
            var companyDto = svc.CompanyFetch("KEY");
            companyDto.DESCRIPTION = null; // "Updated from Dto - " + companyDto.DESCRIPTION;
           Console.WriteLine("expecting change... " + companyDto.DESCRIPTION);
            
            var resultDto = svc.CompanyModify(companyDto);
            Console.WriteLine("result dto - " + resultDto.DESCRIPTION);

            var newCompanyDto = svc.CompanyFetch("KEY");
            Console.WriteLine("new from model - " + resultDto.DESCRIPTION);
            Console.ReadLine();
        }

        public static void TestMethod1()
        {
            using (var db = new EPA.Data.Db())
            {
                var company = db.COMPANIES.Where(a => a.CODE == "XXX").FirstOrDefault();
                if (company != null)
                {

                    Console.WriteLine("From  Database " + company.EMAIL);


                    var companyDto = Mapper.Map<EPA.Dto.Models.COMPANY>(company);

                    Console.WriteLine("From DTO " + companyDto.EMAIL);


                    var company_supplier = new EPA.Models.COMPANY_SUPPLIERS()
                     {
                         COMPANY_ID = company.COMPANY_ID,
                         DESCRIPTION = "test description",
                         EMAIL = "test@mrTesty",
                         COMPANY_SUPPLIERS_ID = 345,

                     };

                    db.COMPANY_SUPPLIERS.Add(company_supplier);
                    var outcome = db.Save();
                    Console.WriteLine(outcome.Message);
                    //   db.SaveChanges();
                }

                /*   else
                   {

                       var c = new EPA.Models.COMPANY()
                       {
                            COMPANY_ID = 50,
                            KEY = "UniqueTestKEY",
                           CODE = "XXX",
                           DESCRIPTION = "A test description",
                           EMAIL = "test@mrTesty.com",

                       };
                       db.COMPANIES.Add(c);
                       db.SaveChanges();
                       // var outcome = db.Save();
                       // Console.WriteLine(outcome.HasError + " - " + outcome.Message);
                   } */
            }
            Console.ReadLine();
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
