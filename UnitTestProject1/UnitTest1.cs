using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xamaein_cinema;
using xamaein_cinema.Data;
using xamaein_cinema.Models;
using Xamarin.Forms;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string start = "#Misha123";
            Registr_Page registr_Page = new Registr_Page();
           bool result = registr_Page.CheckPassword(start);
            Assert.IsTrue(result);

        }
        [TestMethod]
        public void TestMethod2()
        {
            string start = "Иванов";
            Registr_Page registr_Page = new Registr_Page();
            bool result = registr_Page.IsNameOrSurname(start);
            Assert.IsTrue(result);

        }

    }
}
