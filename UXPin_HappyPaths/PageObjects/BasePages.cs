using UXPinTests.Tests;
using OpenQA.Selenium;

namespace UXPinTests.PageObjects
{
    internal class BasePage
    {
        protected IWebDriver Driver { get; set; }
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            GlobalParams = new BaseTest();
        }
        public BaseTest GlobalParams { get; }
    }
}