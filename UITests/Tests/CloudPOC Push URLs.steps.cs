
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using CloudPOC.TestData;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using CloudPOC.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudPOC.UI_Mapping;
namespace UITests.Tests 
{

    public partial class CloudPOC_PushURLs 
    {
        #region Fields
        private DriverProvider driverProvider;
        private TestContext testContextInstance;
        Helper helper;
        Actions driverActions;
        IWebDriver driver = null;
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

            driverProvider = new DriverProvider();
            try
            {

                driver = driverProvider.GetDriver(TestContext.FullyQualifiedTestClassName + TestContext.TestName);
                if (driver != null)
                {
                    InitializeDriverOperations(driver);
                }
            }

            catch (Exception ex)
            {
               Assert.Fail(ex.Message);

            }
          

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
        /// Initializes the options for the driver.
        /// </summary>
        /// <param name="driver">driver instance.</param>
        private void InitializeDriverOperations(IWebDriver driver)
        {
            helper = new CloudPOC.Common.Helper();
            driverActions = new Actions(DriverSessions.GetSession(TestContext.FullyQualifiedTestClassName + TestContext.TestName));
        }

        private void Given_the_CloudPOC_website_is_accessible_from_web_browser()
        {

            try
            {

                if (!helper.NavigateToHomePage(driver))
                {
                    Assert.Fail("Cloud POC website is not accessible");
                }
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
            
        }

        private void When_I_push_an__URL__to_cloud(string url)
        {
            try
            {

                helper.WaitForElement(driver, By.Id(HomePage.tbxUrl), TimeSpan.FromSeconds(30)).SendKeys(HomePageData.Url);
                helper.WaitForElement(driver, By.Id(HomePage.btnPushUrl), TimeSpan.FromSeconds(30)).Click();

            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);

            }
        }

        private void When_I_push_multiple_URLS__to_cloud(string url)
        {
            
        }

        private void Then_the_URLs_shoud_be_listed_in_the_Response_result_with_status_as(string status)
        {

        }

        private void Then_the_URL_shoud_be_listed_in_the_Response_result_with_status_as(string status)
        {
            
        }

        private void And_the_URL__is_already_pushed_in_to_cloud(string url)
        {
            
        }

        private void When_search_this_URL_in_cloud()
        {

        }

        private void Then_the_search_result_should_be_indicate_URL_is_present_in_cloud()
        {
            
        }


    }
}
