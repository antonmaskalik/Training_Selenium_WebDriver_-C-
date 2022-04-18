using OpenQA.Selenium;
using System;

namespace Selenium_WebDriver.Pages.Yandex
{
    public class MailPage : BasePage
    {
        readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);
        readonly TimeSpan _pulingIntervalTime = TimeSpan.FromMilliseconds(100);
        private By _userName = By.CssSelector("a[class*='user-account_has-ticker_yes']>span[class='user-account__name']");

        public MailPage(IWebDriver driver) : base(driver) { }

        public bool IsLoggedIn(string userName)
        {
            WaitElement(_userName, _timeout, _pulingIntervalTime);

            return driver.FindElement(_userName).Text.Equals(userName);
        }
    }
}
