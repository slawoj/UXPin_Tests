RunTests.bat with all assets should work out of the box with default parameters

Tests:
-CreateProjectAndAddBox
-PreviewProjectAndAddComment
-PreviewProjectAndEditComment

Instructions:

1) Adjustable in RunTests.bat file:
-nunit console path, 
-compiled test solution path, 
-test namespace filter, 
-test categories filter, 
-console parameters (browser,headless mode, uxpin project name)

2) Adjustable in App.config (.\UXPin_Tests\UXPin_HappyPaths\bin\Debug):
-initial url (default: "https://app.uxpin.com/")
-login
-password
-implicitWait (default: 60 seconds)

Tech used:
C#, NUnit framework, Selenium framework 
