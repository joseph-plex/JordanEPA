using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Consumer;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;

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

         //   x__Client c = new x__Client("http://epa.plexxis.com/Jordan/JordanEPAService.svc");
         //   var v = c.GetData(2);


         //   v = v;
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


         [TestMethod]
        public void InsertPrimaryKey()
        {
            using (var db = new EPA.Data.Db())
            {
            

               //  using (var connection = db.Database.Connection OracleRepository.GetOpenIDbConnection())
                  //  return Convert.ToInt32(connection.Query("select c_user_id.nextval from dual")[0, 0]);
              
                string keyForCompany = EPA.Services.IdGenerator.CreateUniqueKeyForCompany();
                var c = new Models.COMPANY
                {
                    DESCRIPTION = "Test Description",
                    CODE = Plexxis.Helpers.Strings.Left(keyForCompany, 5, ""),
                    KEY = keyForCompany,
                    EMAIL = Plexxis.Helpers.Strings.Left(keyForCompany, 5, "") + "@test.com",
                   // COMPANY_ID = nextVal,
                
                };
                var nextVal = db.Database.SqlQuery<int>("select company_id.nextval from dual").First();
                Console.WriteLine("nextVal = " + nextVal);

                PropertyInfo propertyInfo = c.GetType().GetProperty("COMPANY_ID");
                propertyInfo.SetValue(c, Convert.ChangeType(nextVal, propertyInfo.PropertyType), null);

                db.COMPANIES.Add(c);
                db.SaveChanges();
                Assert.AreNotEqual(0, c.COMPANY_ID);
                

            }
        }
            [TestMethod]
         public void InsertAutoMaticPrimaryKey()
         {
             using (var db = new EPA.Data.Db())
             {


                 //  using (var connection = db.Database.Connection OracleRepository.GetOpenIDbConnection())
                 //  return Convert.ToInt32(connection.Query("select c_user_id.nextval from dual")[0, 0]);

                 string keyForCompany = EPA.Services.IdGenerator.CreateUniqueKeyForCompany();
                 var c = new Models.COMPANY
                 {
                     DESCRIPTION = "Test Description",
                     CODE = Plexxis.Helpers.Strings.Left(keyForCompany, 5, ""),
                     KEY = keyForCompany,
                     EMAIL = Plexxis.Helpers.Strings.Left(keyForCompany, 5, "") + "@test.com",
                     // COMPANY_ID = nextVal,

                 };
        
                 c = db.AssignPrimaryKey(c, () => c.COMPANY_ID, Data.Sequence.COMPANY_ID);
                 db.COMPANIES.Add(c);
                 db.SaveChanges();
                 Assert.AreNotEqual(0, c.COMPANY_ID);


             }
         }
 
        /*
         public static T AssignPrimaryKey<T>(T entity, Expression<Func<T>> expr, string sequenceName)
         {
             string classSeperator = ".";
             var ex = ((MemberExpression)expr.Body);
             string keyColumnName = Plexxis.Helpers.Strings.TextAfterThis(Plexxis.Helpers.Strings.TextAfterThis(ex.ToString(), ")."), ".");
             if (classSeperator != ".")
                 keyColumnName = keyColumnName.Replace(".", classSeperator);

             var nextVal = db.Database.SqlQuery<int>("select " + sequenceName + ".nextval from dual").First();

             PropertyInfo propertyInfo = entity.GetType().GetProperty(keyColumnName);
             propertyInfo.SetValue(entity, Convert.ChangeType(nextVal, propertyInfo.PropertyType), null);

             return entity;
             
         }
        */

    }
}
