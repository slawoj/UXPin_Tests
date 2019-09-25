using OpenQA.Selenium;

namespace UXPinTests.PageObjects.CommonObjects
{
    internal class Workbench : BasePage
    {
        public Workbench(IWebDriver driver) : base(driver)
        {
        }
        public By Boxes => By.CssSelector("section[id*='workbench'] div[class*='el-box']");
    }
}