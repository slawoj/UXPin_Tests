using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions; //adjust when OpenQA.Selenium.Support.UI.ExpectedConditions will be deprecated;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace UXPinTests.Helpers
{
    public static class CommonHelpers
    {
        static public void GoToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        static public void LogMsg(string message)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(String.Format(message));
        }

        static public void LogUrl(IWebDriver driver)
        {
            string url = "I'm at " + driver.Url.ToString();
            LogMsg(url);
        }

        static public void TakeScreenshot(IWebDriver driver)
        {
            DirectoryInfo screenshotsDirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Screenshots"));
            string screenshotsPath = screenshotsDirectory.ToString();
            string folderName = screenshotsDirectory.GetFileSystemInfos().OrderByDescending(x => x.CreationTime).First().ToString();
            string fileName = DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";
            string screenshotsFullPath = Path.Combine(screenshotsPath, folderName, fileName);
            driver.TakeScreenshot().SaveAsFile(screenshotsFullPath, ScreenshotImageFormat.Jpeg);
            TestContext.AddTestAttachment(screenshotsFullPath);
        }

        static public void Sleep([Optional] int seconds)
        {
            if (seconds == 0)
                Thread.Sleep(1000);
            else
                Thread.Sleep(seconds * 1000);
        }

        static public void SwitchToWindow(IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);
        }

        static public void WaitForElement(IWebDriver driver, By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(240));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
        }

        static public void WaitAndClickOnElement(IWebDriver driver, By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(240));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            driver.FindElement(locator).Click();
        }

        static public void WaitUntilElementAppear(IWebDriver driver, By locator)
        {
            int implicitWait = Convert.ToInt32(driver.Manage().Timeouts().ImplicitWait.TotalSeconds);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(x => CheckIfElementPresent(driver, locator));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(implicitWait);
        }

        static public bool IsElementAvailable(IWebDriver driver, By locator)
        {
            bool isElementVisible;
            int implicitWait = Convert.ToInt32(driver.Manage().Timeouts().ImplicitWait.TotalSeconds);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            var elements = driver.FindElements(locator);
            isElementVisible = elements.Count > 0;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(implicitWait);
            return isElementVisible;
        }

        static public bool CheckIfElementPresent(IWebDriver driver, By element)
        {
            try
            {
                driver.FindElement(element);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }

        static public void MoveToElement(IWebDriver driver, By locator)
        {
            var element = driver.FindElement(locator);
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        static public void DoubleClickOnElement(IWebDriver driver, By locator)
        {
            var element = driver.FindElement(locator);
            Actions action = new Actions(driver);
            action.DoubleClick(element).Perform();
        }

        static public void SelectArea(IWebDriver driver, By locator, int offsetX, int offsetY)
        {
            var element = driver.FindElement(locator);
            ScrollToElement(driver, element);
            WaitForElement(driver, locator);
            Actions action = new Actions(driver);
            action.ClickAndHold(element).Build().Perform();
            action.MoveByOffset(offsetX, offsetY).Build().Perform();
            action.Release().Build().Perform();
        }

        static public void ScrollToElement(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        static public void InputText(IWebDriver driver, By inputLocator, string text)
        {
            var input = driver.FindElement(inputLocator);
            input.Clear();
            input.SendKeys(text);
        }

        static public string GetElementText(IWebDriver driver, By locator)
        {
            string elementText;
            var element = driver.FindElement(locator);
            elementText = element.Text;
            return elementText;
        }

        static public int CountElements(IWebDriver driver, By locator)
        {
            int itemsCount;
            var elements = driver.FindElements(locator);
            itemsCount = elements.Count();
            return itemsCount;
        }

        static public int GetRandomNumber(int maxValue)
        {
            Random random = new Random();
            var randomNumber = random.Next(1, maxValue + 1);
            return randomNumber;
        }
    }
}