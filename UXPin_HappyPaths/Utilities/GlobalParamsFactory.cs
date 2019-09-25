using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UXPinTests.Utilities
{
    public class GlobalParamsFactory : ConfigReader
    {
        ////////////////////////////////////////////////////////////////////
        private Headless DefaultExecMode => Headless.OFF;
        private Browser DefaultBrowser => Browser.Chrome;
        private string DefaultProjectName => "Project1";
        ////////////////////////////////////////////////////////////////////
        public IWebDriver Driver { get; set; }
        public Browser Browser { get; set; }
        public Headless ExecMode { get; set; }
        public string ProjectName { get; set; }
        private Headless InitialExecMode => GetExecMode();
        public string InitialProjectName => GetProjectName();
        private string OutputDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string ScreenshotsDirectory => Path.Combine(OutputDirectory, "Screenshots");
        private TimeSpan CommandTimeout => TimeSpan.FromSeconds(60);
        private string TestName => GetTestName();

        protected void GetBrowser()
        {
            var getBrowserType = TestContext.Parameters.Get("Browser", DefaultBrowser.ToString());
            Browser = (Browser)Enum.Parse(typeof(Browser), getBrowserType);
            Driver = Create(Browser);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ImplicitWait);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);
            Driver.Manage().Window.Size = new Size(1980, 1080);
        }

        private Headless GetExecMode()
        {
            var getExecMode = TestContext.Parameters.Get("Headless", DefaultExecMode.ToString());
            ExecMode = (Headless)Enum.Parse(typeof(Headless), getExecMode);
            return ExecMode;
        }

        private string GetProjectName()
        {
            var getProjectName = TestContext.Parameters.Get("ProjectName", DefaultProjectName);
            ProjectName = getProjectName;
            return ProjectName;
        }

        protected void CreateScreenshotsDirectory()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            string execInfo = "(ExecInfo=" + TestName + "," + Browser.ToString() + ")";
            string timeStamp = DateTime.Now.ToString("yyMMddHHmm");
            string folderName = testName + " " + execInfo + " " + timeStamp;
            string screenshotsDirectory = Path.Combine(ScreenshotsDirectory, $"{folderName}");
            Directory.CreateDirectory(screenshotsDirectory);
        }

        protected void MaintainScreenshotsDirectory()
        {
            DirectoryInfo screenshotsDirectory = new DirectoryInfo(ScreenshotsDirectory);
            if (screenshotsDirectory.Exists)
            {
                List<FileSystemInfo> foldersToDelete = screenshotsDirectory.GetFileSystemInfos().Where(x => x.CreationTime <= (DateTime.Now.AddDays(-5))).ToList();
                if (foldersToDelete.Count > 0)
                {
                    foreach (var folderName in foldersToDelete)
                        Directory.Delete(Path.Combine(ScreenshotsDirectory, $"{folderName}"), true);
                }
            }
        }

        private IWebDriver Create(Browser browserType)
        {
            switch (browserType)
            {
                case Browser.Chrome:
                    return GetChromeDriver();
                case Browser.Firefox:
                    return GetFirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException("Browser does not exists");
            }
        }

        private IWebDriver GetChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            if (InitialExecMode == Headless.ON)
                options.AddArgument("--headless");
            return new ChromeDriver(OutputDirectory, options, CommandTimeout);
        }

        private IWebDriver GetFirefoxDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            if (InitialExecMode == Headless.ON)
                options.AddArgument("--headless");
            return new FirefoxDriver(OutputDirectory, options, CommandTimeout);
        }

        private string GetTestName()
        {
            var testName = TestContext.CurrentContext.Test.MethodName;
            return testName;
        }
    }
}
