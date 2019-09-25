using System;
using System.Configuration;

namespace UXPinTests.Utilities
{
    public class ConfigReader
    {
        public string InitialUrl => ConfigurationManager.AppSettings["url"];
        public string Login => ConfigurationManager.AppSettings["login"];
        public string Password => ConfigurationManager.AppSettings["password"];
        public int ImplicitWait => Convert.ToInt32(ConfigurationManager.AppSettings["implicitWait"]);
    }
}
