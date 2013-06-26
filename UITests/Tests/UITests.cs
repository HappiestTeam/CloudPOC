using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using CloudPOC.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using MbUnit.Framework;
//using Gallio.Framework;
using System.Threading;
using CloudPOC.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudPOC.UI_Mapping;

namespace CloudPOC
{
    [TestClass]
    //[Parallelizable]
    public class UITests
    {
        #region Fields
        private Common.DriverProvider driverProvider;
        private TestContext testContextInstance;
        Common.Helper helper;
        Actions driverActions;
        #endregion

        #region Members

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

        #endregion

        /// <summary>
        /// Initialize the TestGroup
        /// </summary>
        [ClassInitialize]
        static public void SetupGroup(TestContext context)
        {

        }

        /// <summary>
        /// Cleanup the TestGroup
        /// </summary>
        [ClassCleanup]
        static public void CleanupGroup()
        {
        }

        /// <summary>
        /// Initialize the Test
        /// </summary>
        [TestInitialize]
        public void Setup()
        {

            driverProvider = new Common.DriverProvider();

        }

        /// <summary>
        /// Cleanup the Test
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {

           
            helper.TakeScreenshot(DriverSessions.GetSession(TestContext.FullyQualifiedTestClassName + TestContext.TestName + TestContext.TestName), TestContext.FullyQualifiedTestClassName + TestContext.TestName);
           
            driverProvider.CloseDriver(DriverSessions.GetSession(TestContext.FullyQualifiedTestClassName + TestContext.TestName));
            DriverSessions.RemoveSession(TestContext.FullyQualifiedTestClassName + TestContext.TestName);

        }

        /// <summary>
        /// Test to validate mart initialization like login, change password etc. and delete the mart.
        /// </summary>
        [TestMethod]
        [TestCategory("SampleTests")]
        public void SampleTest()
        {
            //Test initialization
            IWebDriver driver=null;
            bool testResult = false;
            StringBuilder verificationErrors = new StringBuilder();
            int testRunCount = Convert.ToInt16(ConfigurationSettings.AppSettings["TestRunCount"]);


            do
            {
                //For test Re-run
                if (testRunCount < Global.GlobalValues.TestRunCount)
                {
                    //Log "Re-running the test"
                    TestContext.WriteLine("Re-running the test...");

                }
                testRunCount--;

                //Do cleanup for next Run
                if (verificationErrors.ToString() != String.Empty || !testResult)
                {
                    verificationErrors.Remove(0, verificationErrors.Length);
                    if (driver != null)
                    {
                        //driverProvider.CloseDriver(driver);
                        driverProvider.CloseDriver(DriverSessions.GetSession(TestContext.FullyQualifiedTestClassName + TestContext.TestName));
                        DriverSessions.RemoveSession(TestContext.FullyQualifiedTestClassName + TestContext.TestName);
                    }
                }


                //Browser compability; initialize the driver configured in app.config
                try
                {
                    
                    driver = driverProvider.GetDriver(TestContext.FullyQualifiedTestClassName + TestContext.TestName);
                    if (driver != null)
                    {
                        InitializeDriverOperations(driver);


                        if (helper.NavigateToHomePage(driver))
                        {
                            helper.WaitForElement(driver,By.Id(HomePage.tbxUrl), TimeSpan.FromSeconds(30)).SendKeys(HomePageData.Url);
                            helper.WaitForElement(driver,By.Id(HomePage.btnPushUrl), TimeSpan.FromSeconds(30)).Click();

                            try
                            {
                                IAlert alert = driver.SwitchTo().Alert();
                                string alertMessage = alert.Text;
                                alert.Accept();
                                testResult = true;  
                            }  
                            catch (NoAlertPresentException Ex)
                            {
                                testResult = true;  
                            }  

                            testResult = true;  
                        }
                        
                    }
                    else
                    {
                        verificationErrors.Append("Invalid Driver Type: " + ConfigurationSettings.AppSettings["TargetDriver"].ToString());
                        testResult = false;
                    }

                }

                catch (Exception ex)
                {
                    verificationErrors.Append(ex.Message);
                    testResult = false;

                }

            }
            while (!testResult && testRunCount > 0);
            if (!testResult)
            {
                Assert.Fail(verificationErrors.ToString());
            }
        }


        /// <summary>
        /// Initializes the options for the driver.
        /// </summary>
        /// <param name="driver">driver instance.</param>
        private void InitializeDriverOperations(IWebDriver driver)
        {
            helper = new Common.Helper();
            driverActions = new Actions(DriverSessions.GetSession(TestContext.FullyQualifiedTestClassName + TestContext.TestName));
        }
    }
}
