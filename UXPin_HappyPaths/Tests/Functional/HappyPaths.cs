using NUnit.Framework;

namespace UXPinTests.Tests.Functional
{
    [TestFixture]
    public class FunctionalTests : BaseTest
    {
        [Test, Category("CreateProject"), Category("AddBox")]
        public void CreateProjectAndAddBox()
        {
            LoginPage.GoTo();
            var dashboardPage = LoginPage.LogIn();
            var projectemptyPage = dashboardPage.CreateProject(InitialProjectName);
            var project = projectemptyPage.CreateNewPrototype();
            project.AddBox(300, 100);
        }

        [Test, Category("PreviewProject"), Category("AddComment")]
        public void PreviewProjectAndAddComment()
        {
            LoginPage.GoTo();
            var dashboardPage = LoginPage.LogIn();
            var projectDatailsPage = dashboardPage.PreviewExistingProject(InitialProjectName);
            var preview = projectDatailsPage.GoToPreview();
            preview.AddComment("this string can be injected");
            preview.AddComment();
            preview.EditComment("just edited");
            preview.AddComment("some another string here");
        }

        [Test, Category("PreviewProject"), Category("EditComment")]
        public void PreviewProjectAndEditComment()
        {
            LoginPage.GoTo();
            var dashboardPage = LoginPage.LogIn();
            var projectDatailsPage = dashboardPage.PreviewExistingProject(InitialProjectName);
            var preview = projectDatailsPage.GoToPreview();
            preview.EditComment("needs improvement");
        }
    }
}