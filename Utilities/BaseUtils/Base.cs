using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports;
using Framework.ComponentHelper.Congifurations;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace Framework.Utilities.BaseUtils
{
    public class Base
    {
        public ExtentReports extent;
        public ExtentTest test;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public ThreadLocal<ExtentTest> extent_test = new ThreadLocal<ExtentTest>();
        private string logFilePath;


        [OneTimeSetUp]
        public void Setup()
        {
            ReportHelper r = new ReportHelper();

            ScreenshotHelper.DeleteScreenshot(driver.Value);

            string logFileName = TestContext.CurrentContext.Test.Name;
            logFilePath = LogHelper.CreateLogFile(logFileName);
        }

        [SetUp]
        public void Start_Browser()
        {
            try
            {
                test = ReportHelper.CreateTest(TestContext.CurrentContext.Test.Name);
                SetBrowser();

                ComponentHelper.NavigationHelper.NavigateToUrl(driver.Value, AppConfigSettings.AppURL);
                ComponentHelper.NavigationHelper.MaximizeWindow(driver.Value);
                //driver.Value.Manage().Window.Maximize();
                LogHelper.WriteLog(logFilePath, "Successfully started the browser and naviagted to the Application");
            }
            catch
            {
                LogHelper.WriteLog(logFilePath, "Couldnot successfully start the browser and naviagte to the Application");
            }
        }

        public IWebDriver GetDriver()
        {
            return driver.Value;
        }

        private void SetBrowser()
        {
            AppConfigReader.SetFrameworkSettings();
            string RunEnvironment = AppConfigSettings.RunEnvironment;

            if (RunEnvironment == null)
            {
                throw new ConfigurationErrorsException("RunEnvironment is not defined in appsettings.config");
            }

            switch (RunEnvironment.ToLowerInvariant())
            {
                case "local":
                    driver.Value = DriverSetup.LocalBrowserSetup(driver.Value);
                    break;
                case "remote":
                    driver.Value = DriverSetup.RemoteBrowserSetup(driver.Value);
                    break;
                default:
                    throw new ConfigurationErrorsException($"Unknown RunEnvironment value '{RunEnvironment}' in app.config");
            }
        }

        [TearDown]
        public void StoreTestResults()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                string screenshotName = TestContext.CurrentContext.Test.MethodName;

                string screenshotPath = ScreenshotHelper.CaptureScreenshot(driver.Value, screenshotName);

                test.Fail(screenshotPath, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                test.Fail(stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                test.Log(Status.Pass, MarkupHelper.CreateLabel("TestCase Status : " + status, ExtentColor.Green));
            }
            ReportHelper.FinalizeReport();


            driver.Value.Quit();
        }

        public static MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot scr = (ITakesScreenshot)driver;
            var screenshot = scr.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Dispose();
            extent_test.Dispose();
        }
    }
}

