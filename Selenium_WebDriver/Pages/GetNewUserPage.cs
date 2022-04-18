using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace Selenium_WebDriver.Pages
{
    public class GetNewUserPage : BasePage
    {
        const string URL = "https://demo.seleniumeasy.com/dynamic-data-loading-demo.html";
        readonly TimeSpan _timeout = TimeSpan.FromSeconds(20);
        readonly TimeSpan _intervalTime = TimeSpan.FromSeconds(5);

        private By _imgUser = By.CssSelector("#loading>img");

        [FindsBy(How = How.Id, Using = "save")]
        private IWebElement _getUserButtom;

        public GetNewUserPage(IWebDriver driver) : base(driver) { }

        public void GoToURL()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void GetUser()
        {
            _getUserButtom.Click();
        }

        public bool IsUserDisplayed()
        {
            WaitElement(_imgUser, _timeout, _intervalTime);

            return IsElementVisible(_imgUser);
        }
    }
}
