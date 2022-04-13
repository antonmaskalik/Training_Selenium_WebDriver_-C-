using OpenQA.Selenium;
using Selenium_WebDriver.Enums;
using SeleniumExtras.PageObjects;

namespace Selenium_WebDriver.Pages
{
    public class AlertsPage: BasePage
    {
        const string URL = "https://demo.seleniumeasy.com/javascript-alert-box-demo.html";
        const string TEXT_OK = "OK!";
        const string TEXT_CANCEL = "Cancel!";

        [FindsBy(How = How.CssSelector, Using = "[class='btn btn-default']")]
        private IWebElement _alertButton;

        [FindsBy(How = How.CssSelector, Using = "[onclick='myConfirmFunction()']")]
        private IWebElement _confirmButton;

        [FindsBy(How = How.CssSelector, Using = "[onclick='myPromptFunction()']")]
        private IWebElement _promptButton;

        [FindsBy(How = How.Id, Using = "confirm-demo")]
        private IWebElement _confirmAnswer;

        [FindsBy(How = How.Id, Using = "prompt-demo")]
        private IWebElement _promptAnswer;

        public AlertsPage(IWebDriver driver): base(driver) { }

        public void GoToURL()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void InvokeAlert(AlertType type)
        {
            switch(type)
            {
                case AlertType.AlertBox:
                {
                    _alertButton.Click();
                    break;
                }

                case AlertType.ConfirmBox:
                {
                    _confirmButton.Click();
                    break;
                }

                case AlertType.PromptBox:
                {
                    _promptButton.Click();
                    break;
                }
            }            
        }

        public void AcceptAlert()
        {
           IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void DismissAlert()
        {
           IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
        }

        public void EnterName(string name)
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.SendKeys(name);
            alert.Accept();
        }

        public bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }  
            catch (NoAlertPresentException)
            {
                return false;
            } 
        }

        public bool IsConfirmBoxAccepted()
        {
            return _confirmAnswer.Text.Contains(TEXT_OK);
        }

        public bool IsConfirmBoxDismissed()
        {
            return _confirmAnswer.Text.Contains(TEXT_CANCEL);
        }

        public bool IsNameInPromptBoxEntered(string name)
        {
            return _promptAnswer.Text.Contains(name);
        }
    }
}
