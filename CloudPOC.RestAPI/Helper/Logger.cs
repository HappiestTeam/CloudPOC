using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

namespace CloudASPNETWebApi.Helper
{
    public class Logger
    {
        static string infoFilePath = ConfigurationManager.AppSettings["LogInfoFilePath"];
        static string exceptionFilePath = ConfigurationManager.AppSettings["LogExceptionFilePath"];

        //Check if the Log file and Exception file exists or not
        Logger()
        {
            if (!File.Exists(exceptionFilePath))
                File.Create(exceptionFilePath);

            if (!File.Exists(infoFilePath))
                File.Create(infoFilePath);

        }

        public static void logInformation(string infoMsg)
        {
            StreamWriter sw = File.AppendText(infoFilePath);
            sw.WriteLine(infoMsg + "  " + DateTime.Now, sw.NewLine);
            sw.Close();
        }

        public static void logException(string exceptionMsg)
        {
            StreamWriter sw = File.AppendText(exceptionFilePath);
            sw.WriteLine(exceptionMsg + "  " + DateTime.Now, sw.NewLine);
            sw.Close();
        }
    }
}