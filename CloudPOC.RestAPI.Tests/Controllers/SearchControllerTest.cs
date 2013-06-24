using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudASPNETWebApi.Controllers;
using CloudASPNETWebApi.Models;
using System.Configuration;


namespace CloudASPNETWebApi.Tests.Controllers
{
    [TestClass,Ignore]
    public class SearchControllerTest
    {
        [TestMethod]
        public void Test_To_Search_Valid_Url_In_Database()
        {
            string message = ConfigurationManager.AppSettings["SearchValue"];
            bool actualResult;
            SearchController searchController = new SearchController();
            actualResult = searchController.Search(message);
            Assert.AreEqual(true, actualResult);
        }

    }
}
