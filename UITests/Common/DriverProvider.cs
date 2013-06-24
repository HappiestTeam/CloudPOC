using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium.Remote;
using System.IO;

namespace CloudPOC.Common
{
    class DriverProvider
    {
  
        public DriverProvider()
        {
        }

        /// <summary>
        /// Gets a driver of perticular type.
        /// </summary>
        /// <param name="driverType">Type of the driver</param>
        /// <param name="testCaseName">The TC requesting the driver</param>
        /// <returns></returns>
        public IWebDriver GetDriver(string testCaseName)
        {
            switch (ConfigurationSettings.AppSettings["TargetDriver"].ToString())
            {
                case "IE":
                    {
                        var options = new InternetExplorerOptions();
                        options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        options.IgnoreZoomLevel = true;
                        IWebDriver driver = new InternetExplorerDriver(ConfigurationSettings.AppSettings["IEDriverServerPath"].ToString(),options);
                        InitializeDriverOperations(driver);

                        //For Grid
                        //System.Environment.SetEnvironmentVariable("webdriver.ie.driver", ConfigurationSettings.AppSettings["IEDriverServerPath"].ToString() +"\\IEDriverServer.exe");
                        //DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
                        //IWebDriver driver = new ScreenShotRemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability);

                        DriverSessions.AddSession(testCaseName, driver);

                        return driver;
                    }
                case "Firefox":
                    {

                        IWebDriver driver = new FirefoxDriver();
                        InitializeDriverOperations(driver);

                        //For Grid
                        //DesiredCapabilities capability = DesiredCapabilities.Firefox();
                        //IWebDriver driver = new ScreenShotRemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability);
                        
                        DriverSessions.AddSession(testCaseName, driver);

                        return driver;
                    }
                case "Chrome":
                    {
                        IWebDriver driver = new ChromeDriver(ConfigurationSettings.AppSettings["ChromeDriverServerPath"].ToString());
                        InitializeDriverOperations(driver);
                         
                        //For Grid
                         //System.Environment.SetEnvironmentVariable("webdriver.ie.driver", ConfigurationSettings.AppSettings["IEDriverServerPath"].ToString() +"\\IEDriverServer.exe");
                        //DesiredCapabilities capability = DesiredCapabilities.Chrome();
                        //IWebDriver driver = new ScreenShotRemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability);

                        DriverSessions.AddSession(testCaseName, driver);


                        return driver;

                    }
                default: 
                    return null;


            }
        }

        public void CloseDriver(IWebDriver webDriver)
        {
            if (webDriver != null)
                webDriver.Quit();
        }

        /// <summary>
        /// Initializes the options for the driver.
        /// </summary>
        /// <param name="driver">driver instance.</param>
        private void InitializeDriverOperations(IWebDriver driver)
        {
           
                driver.Manage().Window.Maximize();
                //https://code.google.com/p/chromedriver/issues/detail?id=109
                if (ConfigurationSettings.AppSettings["TargetDriver"].ToString() != "Chrome")
                {
                    driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(Convert.ToInt16(ConfigurationSettings.AppSettings["DriverPageLoadTimeout"])));
                }
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Convert.ToInt16(ConfigurationSettings.AppSettings["DriverWebElementTimeout"])));
            
            
          

        }
    }

    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public ScreenShotRemoteWebDriver(Uri RemoteAdress, ICapabilities capabilities)
            : base(RemoteAdress, capabilities)
        {
        }

       /// <summary>
       /// Gets screenshot of the driver.
       /// </summary>
       /// <returns>Returns screenshot</returns>
        public Screenshot GetScreenshot()
        {
            // Get the screenshot as base64.
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();

            // ... and convert it.
            return new Screenshot(base64);
        }

        
    }

}
