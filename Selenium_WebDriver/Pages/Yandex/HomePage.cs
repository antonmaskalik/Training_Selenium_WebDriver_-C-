using OpenQA.Selenium;
using System;

namespace Selenium_WebDriver.Pages.Yandex
{
    public class HomePage: BasePage
    {
        const string URL = "https://yandex.by/";
        const int TIMEOUT = 10;
        private By _signInBtn = By.XPath("//*[contains(@class, 'home-link_hover_inherit')]");
        private By _signOutBtn = By.CssSelector("[data-statlog='mail.login.usermenu.exit']");
        private By _userName = By.XPath("//*[@class='username desk-notif-card__user-name']");       
       

        public HomePage(IWebDriver driver): base(driver) { }

        public void GoToURl()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void ClickSignInBtn()
        {
            driver.FindElement(_signInBtn).Click();
        }

        public void ClickSignOutBtn()
        {
            driver.FindElement(_userName).Click();
            driver.FindElement(_signOutBtn).Click();
        }

        public bool IsUserLoggedIn(string userName)
        {
            WaitElement(_userName, TimeSpan.FromSeconds(TIMEOUT));

            return userName.Contains(driver.FindElement(_userName).Text);
        }

        public bool IsUserLoggedOut()
        {
            return IsElementVisible(_signInBtn);
        }
    }
}
