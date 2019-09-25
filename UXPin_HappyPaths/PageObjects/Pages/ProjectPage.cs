using static UXPinTests.Helpers.CommonHelpers;
using UXPinTests.PageObjects.CommonObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace UXPinTests.PageObjects.Pages
{
    internal class ProjectPage : BasePage
    {
        public ProjectPage(IWebDriver driver) : base(driver)
        {
            Workbench = new Workbench(Driver);
            LeftToolbar = new LeftToolbar(Driver);
            DesignLibraries = new DesignLibraries(Driver);
        }
        public Workbench Workbench { get; }
        public LeftToolbar LeftToolbar { get; }
        public DesignLibraries DesignLibraries { get; }
        private By Canvas => By.Id("canvas");

        public void AddBox(int offsetX, int offsetY)
        {
            int initialBoxesCount = CountElements(Driver, Workbench.Boxes);
            WaitAndClickOnElement(Driver, LeftToolbar.Box);
            int boxesCount = CountElements(Driver, Workbench.Boxes);
            Assert.IsTrue(initialBoxesCount == boxesCount, "Box was created on toolbar click while it shouldn't");
            SelectArea(Driver, Canvas, offsetX, offsetY);
            boxesCount = CountElements(Driver, Workbench.Boxes);
            TakeScreenshot(Driver);
            Assert.IsTrue(boxesCount == initialBoxesCount + 1, "Box is not available after creation");
            LogMsg("Box is created");
        }

        public void AddAlert()
        {
            WaitAndClickOnElement(Driver, DesignLibraries.AlertPrimary);
            TakeScreenshot(Driver);
            LogMsg("Alert is created");
        } 
    }
}