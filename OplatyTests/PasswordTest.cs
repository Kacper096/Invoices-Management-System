using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace OplatyTests
{
    [TestClass]
    public class PasswordTest
    {
        [TestMethod]
        [DataRow("Nowehaslo1@", true)]
        [DataRow("noweHa2lo1@", true)]
        [DataRow("noweHa2lo1@", true)]
        [DataRow("nowe", false)]
        [DataRow(" ", false)]
        [DataRow("2NoHA#$", false)]
        [DataRow("2NoHA#$aaa", false)]
        [DataRow("2NoHA#asdasdasdaaaaaaaaaaaaaaaaaaaa2", false)]
        [DataRow(" 2NoHA#asdas ", false)]
        [DataRow("@$asdkSdaa", false)]
        [DataRow("@$as54Sdaa", true)]
        [DataRow("@$as5_Sdaa", true)]
        public void CanBeAPassword_ComparesPassAndPattern(string password, bool expected)
        {
            Assert.AreEqual(expected, PasswordVerification.CanBeAPassword(password));
        }
    }
}
