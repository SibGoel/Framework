using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Framework.ComponentHelper.Congifurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    public class ReportHelper
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        private static readonly string reportDirPath = (Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.Parent?.FullName) + "\\Report";
        private static readonly string reportFileName = "AutomationReport.html"; // Report file name

        public ReportHelper()
        {
            if (!Directory.Exists(reportDirPath))
            {
                Directory.CreateDirectory(reportDirPath); // Create directory if it doesn't exist
            }


            var htmlReporter = new ExtentHtmlReporter(Path.Combine(reportDirPath, reportFileName));
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Enivorment", AppConfigSettings.RunEnvironment);
            extent.AddSystemInfo("OS: ", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Machine Name: ", Environment.MachineName);
            extent.AddSystemInfo("Browser: ", AppConfigSettings.BrowserName);

            htmlReporter.Config.DocumentTitle = "Extent Report";
            htmlReporter.Config.ReportName = reportFileName;
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
        }

        public static ExtentTest CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
            return test;
        }

        public static void FinalizeReport()
        {
            extent.Flush();
        }

    }
}
