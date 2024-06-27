using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ComponentHelper
{
    public class AlertHelper
    {
        private IWebDriver driver;
        private IAlert alert;

        #region 
        public AlertHelper(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void AcceptAlert(IAlert alert)
        {
            alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
        public void CloseAlert()
        {
            alert = driver.SwitchTo().Alert();
            alert.Dismiss();
        }
        public string GetAlertMessage()
        {
            var alertMessage = alert.Text;
            return alertMessage;
        }
        #endregion
    }
}
