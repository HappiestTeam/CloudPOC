using Service.Scheduler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Service.Scheduler.Tests
{
    
    
    /// <summary>
    ///This is a test class for ProcessDataTest and is intended
    ///to contain all ProcessDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProcessDataTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for InitiateDownload
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DemoService.Scheduler.exe")]
        public void InitiateDownloadTest()
        {
            string url = string.Empty; // TODO: Initialize to an appropriate value
            url = "http://www.ecma-international.org/activities/Languages/Introduction%20to%20Csharp.pdf";
            string localFileName = string.Empty; // TODO: Initialize to an appropriate value
            string localFileNameExpected = string.Empty; // TODO: Initialize to an appropriate value
            localFileNameExpected = "Introduction to Csharp.pdf";
            FileInfo fileInfo = null; // TODO: Initialize to an appropriate value
            FileInfo fileInfoExpected = null; // TODO: Initialize to an appropriate value
            fileInfoExpected = new FileInfo("Introduction to Csharp.pdf");
            Uri uri = null; // TODO: Initialize to an appropriate value
            uri = new Uri(url);
            bool expected = false; // TODO: Initialize to an appropriate value
            expected = true;
            bool actual;
            actual = ProcessData_Accessor.InitiateDownload(url, ref localFileName, ref fileInfo, uri);

            Assert.AreEqual(localFileNameExpected, localFileName);
            Assert.AreEqual(fileInfoExpected.Exists, fileInfo.Exists);
            Assert.AreEqual(fileInfoExpected.Name, fileInfo.Name);
            Assert.AreEqual(fileInfoExpected.Length, fileInfo.Length);

            Assert.AreEqual(expected, actual);
        }
    }
}
