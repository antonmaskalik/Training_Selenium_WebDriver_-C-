using OpenQA.Selenium;

namespace Yandex.Pages
{
    public class HomePage: BasePage
    {
        const string URL = "https://yandex.by/";

        private By _signInBtn = By.XPath("//*[contains(@class, 'home-link_hover_inherit')]");
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

        public bool IsUserLoggedIn(string userName)
        {
            WaitElement(_userName);

            return driver.FindElement(_userName).Text.Equals(userName);
        }
    }
}
