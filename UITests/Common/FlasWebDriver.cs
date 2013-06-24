using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace FlashAutomation
{
    class FlasWebDriver
    {
        private IWebDriver webDriver;
        private string flashObjectId;

        public FlasWebDriver(IWebDriver webDriver, string flashObjectId)
        {
            this.webDriver = webDriver;
            this.flashObjectId = flashObjectId;
        }

        public string CallFunction(string functionName, params string[] funtionParameters)
        {
            var result = ((IJavaScriptExecutor)webDriver).ExecuteScript(CreateJsFunction(functionName, funtionParameters));
            return result != null ? result.ToString() : null;
        }

        private string CreateJsFunction(string functionName, params string[] funtionParameters)
        {
            StringBuilder functionArgs = new StringBuilder();
            if (funtionParameters.Length > 0)
            {

                foreach (string funtionParameter in funtionParameters)
                {
                    functionArgs.Append("\"" + funtionParameter + "\"" + ",");
                }
                functionArgs = functionArgs.Remove(functionArgs.Length - 1, 1);

            }

            return String.Format(
                        "return document.{0}.{1}({2});",
                        flashObjectId,
                        functionName,
                        functionArgs);


            //return String.Format("return this.browserbot.findElement(\"{0}\").{1}({2});",flashObjectId,functionName,functionArgs);

        }
    }
}