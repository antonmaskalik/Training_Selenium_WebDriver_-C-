using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Selenium_WebDriver.Pages.Yandex
{
    public class MailLoginPage: BasePage
    {
        const string URL = "https://mail.yandex.com/";

        [FindsBy(How = How.CssSelector, Using = "[class*='HeadBanner-Button-Enter']")]
        private IWebElement _entertn;

        [FindsBy(How = How.CssSelector, Using = "#passp-field-login")]
        private IWebElement _userNameInput;

        [FindsBy(How = How.CssSelector, Using = "[id='passp:sign-in']")]
        private IWebElement _logInBtn;

        [FindsBy(How = How.CssSelector, Using = "#passp-field-passwd")]
        private IWebElement _passwordInput;

        public MailLoginPage(IWebDriver driver): base(driver) { }

        public void GoToUrl()
        {
            driver.Navigate().GoToUrl(URL);
        }
        public void LogIn(string userName, string password)
        {
            _entertn.Click();
            _userNameInput.SendKeys(userName);
            _logInBtn.Click();
            _passwordInput.SendKeys(password);
            _logInBtn.Click();
        }
    }
}
