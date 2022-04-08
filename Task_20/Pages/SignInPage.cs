using OpenQA.Selenium;

namespace Yandex.Pages
{
    public class SignInPage: BasePage
    {
        private By _userNameInput = By.Id("passp-field-login");
        private By _passwordInput = By.Id("passp-field-passwd");
        private By _signInBtn = By.Id("passp:sign-in");

        public SignInPage(IWebDriver driver): base(driver) { }

        public void SignIn(string userName, string password)
        {
            driver.FindElement(_userNameInput).SendKeys(userName);
            driver.FindElement(_signInBtn).Click();

            WaitElement(_passwordInput);

            driver.FindElement(_passwordInput).SendKeys(password);
            driver.FindElement(_signInBtn).Click();
        }
    }
}
