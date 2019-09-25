using static UXPinTests.Helpers.CommonHelpers;
using OpenQA.Selenium;

namespace UXPinTests.PageObjects.Pages
{
    internal class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
            DashboardPage = new DashboardPage(Driver);
        }
        public DashboardPage DashboardPage { get; }
        private By Login => By.Name("login");
        private By Password => By.Name("password");
        private By ConfirmButton => By.Id("loginform_button1");

        public void GoTo()
        {
            GoToUrl(Driver, GlobalParams.InitialUrl);
        }

        public DashboardPage LogIn()
        {
            LogUrl(Driver);
            LogMsg("User: " + GlobalParams.Login);
            LogMsg("Password: " + GlobalParams.Password);
            InputText(Driver, Login, GlobalParams.Login);
            InputText(Driver, Password, GlobalParams.Password);
            TakeScreenshot(Driver);
            WaitAndClickOnElement(Driver, ConfirmButton);
            return new DashboardPage(Driver);
        }
    }
}