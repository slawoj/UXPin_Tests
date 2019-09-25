using OpenQA.Selenium;

namespace UXPinTests.PageObjects.CommonObjects
{
    internal class LeftToolbar : BasePage
    {
        public LeftToolbar(IWebDriver driver) : base(driver)
        {
        }
        public By Box => By.CssSelector("li[draggable][class='toolbar-item']>a[class*='elements-box-with-label']");
    }
}