using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ComponentHelper
{
    public class ElementHelper
    {
        public static void ScrollToView(IWebDriver driver, IWebElement element)
        {
            /*
             * If this method is not working, use following code
             * ((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element);
             */
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoViewIfNeeded()", element);

        }
        public static void Click(IWebDriver driver, By by, int timeout = 5)
        {
            try
            {
                IWebElement element = WaitHelper.WaitForElementToDisplay(driver, by, timeout);
                if (element != null)
                {
                    ScrollToView(driver, element);
                    element.Click();
                }
            }
            catch (StaleElementReferenceException e)
            {
                try
                {
                    Console.WriteLine($"Stale element expection occured, re-trying to perform Click action: {e.Message}");
                    IWebElement element = WaitHelper.WaitForElementToDisplay(driver, by, timeout);
                    ScrollToView(driver, element);
                    element.Click();
                }
                catch (Exception e1)
                {
                    Console.WriteLine($"Exception occurred during Click operation: {e1.Message}");
                }
            }
        }
        public static void SelectDropDownByValue(IWebDriver driver, By by, string value, int timeout)
        {
            try
            {
                SelectElement select = new SelectElement(FindElement(driver, by, timeout));
                select.SelectByValue(value);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to select value from dropdown: {e.Message}");
            }
        }
        public static void MouseOver(IWebDriver driver, By by, int timeout = 5)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(FindElement(driver, by)).Perform();
        }
        public static void SendKeys(IWebDriver driver, By by, string value)
        {
            IWebElement element = WaitHelper.WaitForElementToDisplay(driver, by);
            if (element != null)
            {
                element.Clear();
                element.SendKeys(value);
            }
        }
        public static IWebElement FindElement(IWebDriver driver, By by, int timeout = 5)
        {
            IWebElement element = null;

            try
            {
                element = WaitHelper.WaitForElementToDisplay(driver, by, timeout);
            }
            catch (StaleElementReferenceException e)
            {
                try
                {
                    Console.WriteLine($"Stalement Element expection occured, re-trying to find element: {e.Message}");
                    WaitHelper.WaitForPageLoad(driver);
                    element = WaitHelper.WaitForElementToDisplay(driver, by, timeout);
                }
                catch (Exception e1)
                {
                    Console.WriteLine($"Expection occured: {e1.Message}");

                }
            }
            return element;
        }
    }
}
