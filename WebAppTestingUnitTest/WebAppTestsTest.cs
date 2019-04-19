using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppTesting;

namespace WebAppTestingUnitTest
{
    [TestClass]
    public class WebAppTestsTest
    {
        [TestMethod]
        public void RemoveEndingNumber_Removes2DigitNumber_Test()
        {
            var x = "Hello dudes";
            var y = x + " 99";

            Assert.AreEqual(x, WebAppTests.RemoveEndingNumber(y));
        }

        [TestMethod]
        public void RemoveEndingNumber_NothingToRemove_Test()
        {
            var x = "Hello dudes";

            Assert.AreEqual(x, WebAppTests.RemoveEndingNumber(x));
        }
        
    }
}
