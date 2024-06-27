using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ComponentHelper
{
    public class NavigationHelper
    {
        //Contains methods to navigate through browser history such as navigating back, forward, and refreshing the page.
        public static void NavigateToUrl(IWebDriver driver, string URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        public static void MaximizeWindow(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

        // Method to navigate back to previous page
        public static void NavigateBack(IWebDriver driver)
        {
            driver.Navigate().Back();
        }

        // Method to navigate forward to next page
        public static void NavigateForward(IWebDriver driver)
        {
            driver.Navigate().Forward();
        }

        // Method to refresh the current page
        public static void RefreshPage(IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }
    }
}
