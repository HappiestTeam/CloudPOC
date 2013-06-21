using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;

namespace Service.Scheduler
{
    public class CommonUtil
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public static DateTime GetCurrentISTDateTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        }

        public static void WriteLog(string logpath, String logMessage)
        {
            StreamWriter logWriter = File.AppendText(logpath);
            logWriter.WriteLine(CommonUtil.GetCurrentISTDateTime() + ":" + logMessage);
            logWriter.Close();
            logWriter.Dispose();
        }
    }
}
