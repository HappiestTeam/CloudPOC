using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudPOC.Common
{
    public static class EnumProvider
    {
        public enum DriverType { Chrome, IE, Firefox};
        public enum LogPriority { Info, Warning, Error };
        public enum LogType { TestCase, TestResult };

    }
}
