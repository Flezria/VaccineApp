using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaccineApp.Persistency;
using VaccineApp.Services;

namespace VaccineAppTests
{

    [TestClass]
    public class WebserviceTests
    {
        public Webservice Service { get; set; }
        public String ApiKey { get; set; }

        [TestInitialize]
        public void CreateTestObject()
        {
            Service = new Webservice();
            ApiKey = "3u2YyR9iQrt4pCWwzE6BGfmklB9MSm4ZIQEJ8a6gqYg=";
        }

        [TestMethod]
        public void TestMethod1()
        {
            
            //ARRANGE
            var expectedType = typeof(Task<bool>);

            //ACT
            var result = Service.CheckForChild(ApiKey);

            //ASSERT
            Assert.AreEqual(expectedType, result.GetType());
        }
    }
}
