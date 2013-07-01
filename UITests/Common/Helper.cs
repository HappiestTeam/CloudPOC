using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using CloudPOC.Global;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium.Support.UI;
//using MbUnit.Framework;

namespace CloudPOC.Common
{
    class Helper
    {

        public Helper()
        {

        }

        public bool NavigateToHomePage(IWebDriver webDriver)
        {
            webDriver.Navigate().GoToUrl(GlobalValues.baseURL);
            return webDriver.Title.Contains("Url Upload");
        }

        public bool IsTextPresent(IWebDriver webDriver,string text)
        {
            return webDriver.FindElement(By.TagName("body")).Text.Contains(text);
            
        }

        public string CallScript(IWebDriver driver, string script)
        {
            var result = ((IJavaScriptExecutor)driver).ExecuteScript(script);
            return result != null ? result.ToString() : null;
        }

        public void TakeScreenshot(IWebDriver webDriver,string fileName)
        {
            try
            {
                if (webDriver != null)
                {
                    string filePath = ConfigurationSettings.AppSettings["ScreenshotsPath"].ToString();

                    // Take the screenshot            
                    Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
                    string screenshot = ss.AsBase64EncodedString;
                    byte[] screenshotAsByteArray = ss.AsByteArray;

                    // Save the screenshot
                    ss.SaveAsFile(filePath + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt") + "_" + fileName + ".Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    ss.ToString();
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        public bool IsElementPresent(IWebDriver webDriver,By by)
        {
            try
            {
                webDriver.FindElement(by);
                IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(by));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public IWebElement WaitForElement(IWebDriver webDriver,By by, TimeSpan t)
        {
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(webDriver, t);
                return wait.Until(d => d.FindElement(by));

            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public bool WaitForTextOnPage(IWebDriver webDriver,String text, TimeSpan t)
        {
            try
            {
                //driver.FindElement(by);
                WebDriverWait wait = new WebDriverWait(webDriver, t);
                return wait.Until(d => IsTextPresent(d,text));

            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}
