using AventStack.ExtentReports;
using Framework.ComponentHelper;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Pages.LoginPage
{
    public class LoginPage
    {
        public IWebDriver driver;
        public ExtentTest test;

        #region WebElements    

        [FindsBy(How = How.Id, Using = "txtUserName")]
        private IWebElement txtUserName;

        [FindsBy(How = How.Id, Using = "txtPassword")]
        private IWebElement txtPassword;

        [FindsBy(How = How.Id, Using = "rbtnLogin_input")]
        private IWebElement btnLogin;

        #endregion

        #region Actions
        public LoginPage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            this.test = test;
        }

        public void EnterCredentials(string username, string password)
        {
            try
            {
                WaitHelper.WaitForPageLoad(driver);
                txtUserName.Clear();
                txtUserName.SendKeys(username);
                txtPassword.Clear();
                txtPassword.SendKeys(password);
                btnLogin.Click();
                test.Log(Status.Info, "Successfully logged in to the application.");

            }
            catch (Exception e)
            {
                //test.Log($"Error: {e.Message}");
            }
        }

        #endregion
    }
}
