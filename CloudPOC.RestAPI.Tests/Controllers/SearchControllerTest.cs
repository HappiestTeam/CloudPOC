using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudASPNETWebApi.Controllers;
using CloudASPNETWebApi.Models;
using System.Configuration;

using Thrift;
using CassandraClient = Apache.Cassandra.Cassandra.Client;
using Apache.Cassandra;
using Aquiles.Helpers.Encoders;
using Aquiles.Cassandra10;
using Aquiles.Core;
using Aquiles.Core.Configuration;
using Aquiles.Core.Cluster;
using Aquiles.Helpers;
using Thrift.Transport;
using Thrift.Protocol;



namespace CloudASPNETWebApi.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTest
    {
        [TestMethod]
        public void Test_To_Search_Valid_Url_In_Database()
        {
            string url = "Test msg";
            string  actualResult;
            SearchController searchController = new SearchController();
            actualResult = searchController.Search(url);
            Assert.AreEqual("Invalid URL", actualResult);
        }


        [TestMethod]
        public void Test_To_Check_Cassandra_Service()
        {
            bool result;
            TTransport transport = new TSocket("localhost", 9160);
            TProtocol protocol = new TBinaryProtocol(transport);
            Cassandra.Client client = new Cassandra.Client(protocol);
            try
            {
                transport.Open();
                result = true;
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(true, result);

        }
    }
}
