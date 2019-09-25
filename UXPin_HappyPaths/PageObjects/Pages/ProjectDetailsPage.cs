using static UXPinTests.Helpers.CommonHelpers;
using OpenQA.Selenium;

namespace UXPinTests.PageObjects.Pages
{
    internal class ProjectDetailsPage : BasePage
    {
        public ProjectDetailsPage(IWebDriver driver) : base(driver)
        {
        }
        private By ProjectTile => By.CssSelector("body[class*='visible'] section[class='box-body']");
        private By PreviewButton => By.CssSelector("div[class='hover-actions'] a[class*='preview']");
        private By EditButton => By.CssSelector("div[class='hover-actions'] a[class*='edit']");

        public ProjectPage GoToEdit()
        {
            ClickOnProjectTileButton(EditButton);
            TakeScreenshot(Driver);
            LogUrl(Driver);
            return new ProjectPage(Driver);
        }

        public PreviewPage GoToPreview()
        {
            ClickOnProjectTileButton(PreviewButton);
            SwitchToWindow(Driver);
            TakeScreenshot(Driver);
            LogUrl(Driver);
            return new PreviewPage(Driver);
        }

        private void ClickOnProjectTileButton(By button)
        {
            WaitUntilElementAppear(Driver, ProjectTile);
            LogUrl(Driver);
            MoveToElement(Driver, ProjectTile);
            MoveToElement(Driver, button);
            WaitAndClickOnElement(Driver, button);
            TakeScreenshot(Driver);
        }
    }
}