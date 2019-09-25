using static UXPinTests.Helpers.CommonHelpers;
using OpenQA.Selenium;

namespace UXPinTests.PageObjects.Pages
{
    internal class ProjectEmptyPage : BasePage
    {
        public ProjectEmptyPage(IWebDriver driver) : base(driver)
        {
        }
        private By NewPrototypeButton => By.CssSelector("ul[class*='options-list']>li[class*='start-from-scratch']>div[class*='option-tile']");

        public ProjectPage CreateNewPrototype()
        {
            WaitUntilElementAppear(Driver, NewPrototypeButton);
            MoveToElement(Driver, NewPrototypeButton);
            TakeScreenshot(Driver);
            WaitAndClickOnElement(Driver, NewPrototypeButton);
            LogUrl(Driver);
            return new ProjectPage(Driver);
        }
    }
}