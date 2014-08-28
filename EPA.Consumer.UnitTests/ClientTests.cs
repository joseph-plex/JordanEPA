using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Consumer;
using System.Data;

namespace EPA.Consumer.UnitTests
{
    [TestClass]
    public class ClientTests
    {
        private static EPA.Services.EPAService GetService
        {
            get
            {
                return new EPA.Services.EPAService();
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            // using (EPA.Models.DbFirstEntities db = Effort.ObjectContextFactory.CreateTransient<EPA.Models.DbFirstEntities>())
            {

            }
            //var mockSet = new Mock<DbSet<Blog>>(); 

            Client c = new Client("http://epa.plexxis.com/Jordan/JordanEPAService.svc");
            var v = c.GetData(2);


            v = v;
        }

        [TestMethod]
        public void CompanyModify()
        {
       
            Random r = new Random();
            string randomDescription = "Random Description #" + r.Next();

            var companyDto = GetService.CompanyFetch("UniqueTestKEY");
            companyDto.DESCRIPTION = randomDescription;

            // MODIFY
            GetService.CompanyModify(companyDto);

            var updatedCompanyDto = GetService.CompanyFetch("UniqueTestKEY");
            Assert.AreEqual(companyDto.DESCRIPTION, updatedCompanyDto.DESCRIPTION);

        }
        /*
        [TestMethod]
        public void TestCompanyFetch()
        {

            Random r = new Random();
            string randomDescription = "Random Description #" + r.Next();

            var companyDto = GetService.TestCompanyFetch();
            companyDto.DESCRIPTION = randomDescription;

            // MODIFY
            GetService.CompanyModify(companyDto);

            var updatedCompanyDto = GetService.CompanyFetch(companyDto.KEY);
            Assert.AreEqual(companyDto.DESCRIPTION, updatedCompanyDto.DESCRIPTION);

        } */
        [TestMethod]
        public void TestDb()
        {
            string email1, email2;
            using (var db = new EPA.Data.Db())
            {
                email1 = db.COMPANIES.FirstOrDefault().EMAIL;

            }

            using (var db = new EPA.Data.MockDb())
            {
                email2 = db.COMPANIES.FirstOrDefault().EMAIL;
            }
            Assert.AreEqual(email1, email2);

        }

    }
}
