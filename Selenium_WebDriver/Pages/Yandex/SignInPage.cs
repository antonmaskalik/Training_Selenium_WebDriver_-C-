using OpenQA.Selenium;
using System;

namespace Selenium_WebDriver.Pages.Yandex
{
    public class SignInPage: BasePage
    {
        const int TIMEOUT = 10;
        private By _userNameInput = By.Id("passp-field-login");
        private By _passwordInput = By.Id("passp-field-passwd");
        private By _signInBtn = By.Id("passp:sign-in");

        public SignInPage(IWebDriver driver): base(driver) { }

        public void SignIn(string userName, string password)
        {
            driver.FindElement(_userNameInput).SendKeys(userName);
            driver.FindElement(_signInBtn).Click();

            WaitElement(_passwordInput, TimeSpan.FromSeconds(TIMEOUT));

            driver.FindElement(_passwordInput).SendKeys(password);
            driver.FindElement(_signInBtn).Click();
        }
    }
}
