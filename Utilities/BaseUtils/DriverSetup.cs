using Framework.ComponentHelper.Congifurations;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using System.Configuration;

namespace Framework.Utilities.BaseUtils
{
    public class DriverSetup
    {
        private static string RunEnvironment = AppConfigSettings.RunEnvironment;
        private static string browser_name = AppConfigSettings.BrowserName;
        public static IWebDriver LocalBrowserSetup(IWebDriver driver)
        {

            if (BrowserType.Chrome.ToString().Equals(browser_name))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());

                ChromeOptions chromeOptions = new ChromeOptions();

                chromeOptions.AddArgument("--incognito"); // Launch Chrome in incognito mode
                chromeOptions.AddArgument("--start-maximized"); // Start Chrome maximized
                chromeOptions.AddArgument("--disable-notifications"); // Disable notifications

                // Set other capabilities as needed
                chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.cookies", 2);

                driver = new ChromeDriver();
            }
            else if (BrowserType.Firefox.ToString().Equals(browser_name))
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                driver = new FirefoxDriver();
            }
            else if (BrowserType.IE.ToString().Equals(browser_name))
            {
                new DriverManager().SetUpDriver(new InternetExplorerConfig());
                driver = new InternetExplorerDriver();
            }
            else
            {
                Console.WriteLine("Default Browser is initated, please check browser details in app.config file");
                new DriverManager().SetUpDriver(new ChromeConfig());

                ChromeOptions chromeOptions = new ChromeOptions();

                // Add Chrome capabilities using ChromeOptions
                chromeOptions.AddArgument("--incognito"); // Launch Chrome in incognito mode
                chromeOptions.AddArgument("--start-maximized"); // Start Chrome maximized
                chromeOptions.AddArgument("--disable-notifications"); // Disable notifications

                // Set other capabilities as needed
                chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.cookies", 2); // Block third-party cookies

                driver = new ChromeDriver();
            }

            return driver;

        }

        public static RemoteWebDriver RemoteBrowserSetup(IWebDriver driver)
        {
            if (BrowserType.Chrome.ToString().Equals(browser_name))
            {
                string? username = ConfigurationManager.AppSettings["BrowserStackUserName"];
                string? key = ConfigurationManager.AppSettings["BrowserStackKey"];

                ChromeOptions capabilities = new ChromeOptions();
                capabilities.BrowserVersion = "latest";
                Dictionary<string, object> browserstackOptions = new();
                browserstackOptions.Add("os", "Windows");
                browserstackOptions.Add("osVersion", "10");
                browserstackOptions.Add("projectName", "Selenium C# NUnit Project");
                browserstackOptions.Add("sessionName", "parallel_test");
                browserstackOptions.Add("seleniumVersion", "4.3.0");
                browserstackOptions.Add("userName", username);
                browserstackOptions.Add("accessKey", key);
                browserstackOptions.Add("browserName", browser_name);
                capabilities.AddAdditionalOption("bstack:options", browserstackOptions);

                driver = new RemoteWebDriver(
        new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), capabilities);
            }

            return (RemoteWebDriver)driver;
        }
    }
}
