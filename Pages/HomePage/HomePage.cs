using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Pages.HomePage
{
    public class HomePage
    {
        private IWebDriver driver;
        private ExtentTest test;

        #region WebElements    

        [FindsBy(How = How.XPath, Using = "//a[@title='Logout[abernard]']//span")]
        private IWebElement btnLogout;

        [FindsBy(How = How.Name, Using = "//a[@title='General Configuration']//span")]
        private IWebElement btnConfiguration;

        [FindsBy(How = How.Name, Using = "//a[contains(@title='Unread messages')]//span")]
        private IWebElement btnMessages;

        #endregion

        #region Actions
        public HomePage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            this.test = test;
        }

        public void ClickAndVerifyLogout()
        {

            try
            {
                btnLogout.Click();
                test.Log(Status.Info, "Successfully logged out from the application.");
            }
            catch (Exception e)
            {
                test.Log(Status.Info, $"Unable to log out from the application.Getting error - {e.Message}");

            }
        }

        #endregion
    }
}
