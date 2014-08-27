using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPA.Consumer;
namespace EPA.Consumer.UnitTests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Client c = new Client("http://epa.plexxis.com/Jordan/JordanEPAService.svc");
            var v = c.GetData(2);
            v = v;
        }
    }
}
