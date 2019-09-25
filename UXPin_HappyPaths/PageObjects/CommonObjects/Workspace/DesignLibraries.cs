using OpenQA.Selenium;

namespace UXPinTests.PageObjects.CommonObjects
{
    internal class DesignLibraries : BasePage
    {
        public DesignLibraries(IWebDriver driver) : base(driver)
        {
        }
        public By AlertPrimary => By.CssSelector("figure[title='Primary alert']");
    }
}