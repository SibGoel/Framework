using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace Framework.ComponentHelper
{
    public class WaitHelper
    {
        public static void WaitForPageLoad(IWebDriver driver, int timeoutInSeconds = 10)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutInSeconds));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
        public static IWebElement WaitForElementToDisplay(IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            DefaultWait<IWebDriver> fluentwait = new DefaultWait<IWebDriver>(driver);
            fluentwait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            fluentwait.PollingInterval = TimeSpan.FromSeconds(3);
            fluentwait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            fluentwait.Message = "Element not found";

            IWebElement element = driver.FindElement(by);
            return element;
        }
        public static void WaitUntilElementIsVisible(IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(300);
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        public static void WaitUntilElementIsInVisible(IWebDriver driver, By locator, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
        public static void ImplicitWait(IWebDriver driver, int timeoutInSeconds = 10)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
        }
        public static void WaitUntilElementToBeClickable(IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
    }
}
