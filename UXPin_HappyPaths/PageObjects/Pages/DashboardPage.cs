using static UXPinTests.Helpers.CommonHelpers;
using UXPinTests.Translations;
using UXPinTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace UXPinTests.PageObjects.Pages
{
    internal class DashboardPage : BasePage
    {
        public DashboardPage(IWebDriver driver) : base(driver)
        {
            DashboardTranslations = new DashboardTranslations();
        }
        public DashboardTranslations DashboardTranslations { get; }
        private By NewProjectButton => By.CssSelector("div[id='project-bar'] a[class*='btn-new-project']");
        private By ProjectNameInput => By.Id("project-name");
        private By ProjectConfirmationButton => By.CssSelector("div[id ='project-new-modal-box'] button[class='btn-solid']");
        private By ProjectTile(string projectName) => By.CssSelector(String.Format("div[class*='projects-group'] section[class*='project-box'] h3[class='name'][title='{0}']", projectName));
        private By ProjectTilesEmpty => By.CssSelector("li[class*='add-first-project'] a[class*='add-first-project']");
        private By ProjectTilesContainer => By.CssSelector("section[class*='project-box'] label:not([title='']),li[class*='add-first-project'] a[class*='add-first-project']");
        private By ProjectTileMoreActions(string projectName) => By.XPath(String.Format("//a/h3[@title='{0}']/ancestor::section/a[contains(@class,'header')]/following-sibling::footer/div[contains(@class,'more-actions')]", projectName));
        private By ProjectTileMoreActionsDeleteButton => By.CssSelector("a[href][class*='icon-general-trash']");
        private By ProjectDeletePopupConfirmDeleteButton => By.CssSelector("div[id*='project-delete-modal'] button[class*='icon-general-trash']");
        bool IsProjectTileEmptyAvailable => IsElementAvailable(Driver, ProjectTilesEmpty);
        bool IsProjectTileAvailable(string projectName) => IsElementAvailable(Driver, ProjectTile(projectName));

        public ProjectEmptyPage CreateProject(string projectName)
        {
            WaitUntilElementAppear(Driver, ProjectTilesContainer);
            LogUrl(Driver);
            TakeScreenshot(Driver);
            if (IsProjectTileEmptyAvailable)
            {
                string projectTilesEmptyText = GetElementText(Driver, ProjectTilesEmpty);
                string projectTilesEmptyTranslation = DashboardTranslations.ProjectTilesEmptyTooltipText.First(x => x.Key == Lang.en).Value;
                Assert.AreEqual(projectTilesEmptyText, projectTilesEmptyTranslation, "Tooltip translation is wrong. Should be {0}, but was {1}", projectTilesEmptyText, projectTilesEmptyTranslation);
            }
            if (IsProjectTileAvailable(projectName))
            {
                LogMsg(projectName + " already exists");
                MoveToElement(Driver, ProjectTile(projectName));
                MoveToElement(Driver, ProjectTileMoreActions(projectName));
                MoveToElement(Driver, ProjectTileMoreActionsDeleteButton);
                WaitAndClickOnElement(Driver, ProjectTileMoreActionsDeleteButton);
                WaitAndClickOnElement(Driver, ProjectDeletePopupConfirmDeleteButton);
                LogMsg(projectName + " deleted");
            }
            WaitAndClickOnElement(Driver, NewProjectButton);
            InputText(Driver, ProjectNameInput, projectName);
            WaitAndClickOnElement(Driver, ProjectConfirmationButton);
            LogMsg(projectName + " is successfully created");
            return new ProjectEmptyPage(Driver);
        }

        public ProjectDetailsPage PreviewExistingProject(string projectName)
        {
            WaitUntilElementAppear(Driver, ProjectTilesContainer);
            LogUrl(Driver);
            TakeScreenshot(Driver);
            if (!(IsProjectTileAvailable(projectName)))
            {
                Assert.Ignore("Project {0} is not available. Please create project first", projectName);
            }
            WaitAndClickOnElement(Driver, ProjectTile(projectName));
            return new ProjectDetailsPage(Driver);
        }
    }
}