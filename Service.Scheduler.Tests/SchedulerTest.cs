﻿using Service.Scheduler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Thrift.Transport;
using Thrift.Protocol;
using Apache.Cassandra;

namespace Service.Scheduler.Tests
{
    
    
    /// <summary>
    ///This is a test class for SchedulerTest and is intended
    ///to contain all SchedulerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SchedulerTest
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
        ///A test for ComputeScheduleTime
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DemoService.Scheduler.exe")]
        public void ComputeScheduleTimeTest_When_Null_Passed()
        {
            Scheduler_Accessor target = new Scheduler_Accessor(); // TODO: Initialize to an appropriate value
            string strSchedTime = string.Empty; // TODO: Initialize to an appropriate value
            int nRetryVal = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ComputeScheduleTime(strSchedTime, nRetryVal);
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod()]
        public void TestCassandraConnection()
        {
            
        }
          
        
    }
}
