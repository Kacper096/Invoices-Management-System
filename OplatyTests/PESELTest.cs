using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace OplatyTests
{
    [TestClass]
    public class PESELTest
    {
        [TestMethod]
        [DataRow(" 96101600779 ", true, DisplayName = "Pesel approve")]
        [DataRow("96101600779", true, DisplayName = "Pesel approve")]
        [DataRow("99", null, DisplayName = "Not expected")]
        [DataRow("999195037222", null, DisplayName = "Not expected")]
        [DataRow("halooooooooo", null, DisplayName = "Not expected")]
        public void TestValPesel(string param, bool expected)
        {

            bool finish = PESEL.Check(param);
            Assert.AreEqual(expected, finish);
        }

        [TestMethod]
        [DataRow(" ")]
        [DataRow(null)]
        public void ArgumentNull_CheckPesel_ThrowsException(string param)
        {
            Assert.ThrowsException<ArgumentNullException>(() => PESEL.Check(param));
        }
    }
}
