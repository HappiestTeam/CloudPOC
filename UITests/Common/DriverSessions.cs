using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace CloudPOC.Common
{
   public  static class DriverSessions
    {


        public static Dictionary<string,IWebDriver> ActiveDrivers;

        static DriverSessions()
        {
            ActiveDrivers = new Dictionary<string,IWebDriver>();
        }

        public static void AddSession(string name,IWebDriver driver)
        {
            ActiveDrivers.Add(name, driver);
        }

        public static void RemoveSession(string name)
        {
            ActiveDrivers.Remove(name);
        }

        public static IWebDriver GetSession(string name)
        {
            if (!ActiveDrivers.ContainsKey(name))
                return null;

            if ((IWebDriver)ActiveDrivers[name] != null)
            {
                return ActiveDrivers[name];
            }
            return null;
        }
    }
}
