using static UXPinTests.Helpers.CommonHelpers;
using UXPinTests.PageObjects.Pages;
using UXPinTests.Utilities;
using NUnit.Framework;

namespace UXPinTests.Tests
{
    public class BaseTest : GlobalParamsFactory
    {
        [SetUp]
        public void SetUp()
        {
            GetBrowser();
            InitializePageObjects();
            CreateScreenshotsDirectory();
            LogMsg("Test Browser: " + Browser.ToString());
            LogMsg("Silent Mode: " + ExecMode.ToString());
            LogMsg("Implicit wait: " + ImplicitWait.ToString() + " seconds");
        }

        [TearDown]
        public void CleanUp()
        {
            Sleep();
            Driver.Close();
            Driver.Quit();
            MaintainScreenshotsDirectory();
        }

        private void InitializePageObjects()
        {
            LoginPage = new LoginPage(Driver);
        }
        internal LoginPage LoginPage { get; set; }
    }
}