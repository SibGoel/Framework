using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utilities
{
    public class ScreenshotHelper
    {
        private static readonly string screenshotDirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.Parent?.FullName + "\\Screenshots";
        public static string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            // Check if directory exists, create if not
            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }

            // Capture screenshot
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string screenshotFilePath = Path.Combine(screenshotDirectory, $"{screenshotName}_{DateTime.Now:dd.MM.yyyy_HHmmss}.png");
            screenshot.SaveAsFile(string.Format(screenshotFilePath));

            TestContext.AddTestAttachment(screenshotFilePath, "Screenshot");
            TestContext.Progress.WriteLine($"Screenshot captured: {screenshotFilePath}");

            return screenshotFilePath;
        }

        public static void DeleteScreenshot(IWebDriver driver)
        {
            try
            {
                // Check if directory exists
                if (Directory.Exists(screenshotDirectory))
                {
                    // Get all files (screenshots) in the directory
                    string[] screenshotFiles = Directory.GetFiles(screenshotDirectory);

                    // Delete each file
                    foreach (string file in screenshotFiles)
                    {
                        File.Delete(file);
                        Console.WriteLine($"Deleted file: {file}");
                    }

                    Console.WriteLine("All screenshots deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Directory '{screenshotDirectory}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting screenshots: {ex.Message}");
            }

        }
    }
}

