using NUnit.Framework;
using System.Threading;
using Selenium_WebDriver.Pages.Yandex;

namespace Selenium_WebDriver
{
    [TestFixture]
    public class YandexTests: BaseTest
    {
        const string USER_NAME_1 = "WebDriverCSharp";
        const string USER_NAME_2 = "SeleniumWeb";
        const string PASSWORD = "Selenium";
        const int TIMEOUT = 3000;

        [TestCase(USER_NAME_1, PASSWORD)]
        public void SignInTest(string userName, string password)
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToURl();
            homePage.ClickSignInBtn();

            //This is explicit wait
            Thread.Sleep(TIMEOUT);

            SignInPage signInPage = new SignInPage(driver);
            signInPage.SignIn(userName, password);

            Assert.IsTrue(homePage.IsUserLoggedIn(userName), "Login operation failed under username {0}", userName);
        }

        [TestCase(USER_NAME_1, PASSWORD)]
        [TestCase(USER_NAME_2, PASSWORD)]
        public void MailLoginTest(string userName, string password)
        {
            MailLoginPage mailLoginPage = new MailLoginPage(driver);
            mailLoginPage.GoToUrl();
            mailLoginPage.LogIn(userName, password);

            MailPage mailPage = new MailPage(driver);

            Assert.IsTrue(mailPage.IsLoggedIn(userName), "Login operation failed under username {0}", userName);
        }
    }
}