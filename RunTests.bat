@ECHO OFF
REM change CHCP to UTF-8
CHCP 65001
CLS

.\nunit-console\nunit3-console.exe --params:Headless=OFF;Browser=Chrome;ProjectName=ExternalProject1 --where:namespace==UXPinTests.Tests.Functional /wait /labels:After ".\UXPin_HappyPaths\bin\Debug\UXPin_HappyPaths.dll"

:: --where:cat==$categoryName
:: --params:ParamName=$value
:: Browser {Chrome, Firefox}
:: Headless {ON, OFF}
:: ProjectName {$yourProjectName}
