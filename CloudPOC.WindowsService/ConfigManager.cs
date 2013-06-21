using System.Collections.Generic;
using System.Configuration;
using System;

namespace Service.Scheduler
{
    public static class ConfigManager
    {
        private const string CONST_PING_INTERVAL_CONVERTER = "PING_INTERVAL_CONVERTER";
        private const string CONST_PING_INTERVAL_UNIT = "PING_INTREVAL_UNIT";
        private const string CONST_START_TIME = "START_TIME";
        private const string CONST_RETRY_INTREVAL = "RETRY_INTREVAL";
        private const string CONST_LOG_FILE = "LOG_FILE";
      
        public static string LogFile
        {
            get
            {
                return GetConfigurationStringWithChk(CONST_LOG_FILE);
            }
        }

        public static long PingInterval
        {
            get
            {
                if (string.IsNullOrEmpty(GetConfigurationStringWithChk(CONST_PING_INTERVAL_CONVERTER)) ||
                    string.IsNullOrEmpty(GetConfigurationStringWithChk(CONST_PING_INTERVAL_UNIT)))
                    return 0;

                return GetConfigurationStringInt(CONST_PING_INTERVAL_CONVERTER) *
                       GetConfigurationStringInt(CONST_PING_INTERVAL_UNIT); 
            }            
        }

        public static string StartTime
        {
            get
            {
                return GetConfigurationStringWithChk(CONST_START_TIME);
            }
        }

        public static int RetryInterval
        {
            get
            {
                if (string.IsNullOrEmpty(GetConfigurationStringWithChk(CONST_RETRY_INTREVAL))) return 0;

                return GetConfigurationStringInt(CONST_RETRY_INTREVAL);
            }
        }

        public static string GetConfigurationStringWithChk(string key)
        {
            string ConfigurationString = string.Empty;

            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            return ConfigurationString;
        }

        public static int GetConfigurationStringIntWithChk(string key)
        {
            int ConfigValue = 0;
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings[key].ToString());
            }
            return ConfigValue;
        }

        public static int GetConfigurationStringInt(string key)
        {
            try
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings[key].ToString());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Configuration Key: " + key + " not found in the configuration file");
            }
        }


   }
}
