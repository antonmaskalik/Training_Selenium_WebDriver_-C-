using NUnit.Framework;
using System.Threading;
using Selenium_WebDriver.Pages.Yandex;
using NUnit.Allure.Attributes;
using Allure.Commons;

namespace Selenium_WebDriver
{
    [TestFixture]
    [AllureSuite("Yandex test")]
    public class YandexTests: BaseTest
    {
        const string USER_NAME_1 = "WebDriverCSharp";
        const string USER_NAME_2 = "SeleniumWeb";
        const string USER_NAME_3 = "seleniumtesttraining@yandex.com";
        const string PASSWORD_1 = "Selenium";
        const string PASSWORD_2 = "1qaz@WSX1qaz";
        const int TIMEOUT = 3000;
        const string OWNER = "Anton Maskalik";

        [AllureName("Verifies Login yandex.by")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner(OWNER)]
        [TestCase(USER_NAME_1, PASSWORD_1)]        
        public void SignInTest(string userName, string password)
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToURl();

            TakeScreenshot();

            homePage.ClickSignInBtn();

            //This is explicit wait
            Thread.Sleep(TIMEOUT);

            SignInPage signInPage = new SignInPage(driver);
            signInPage.SignIn(userName, password);

            Assert.IsTrue(homePage.IsUserLoggedIn(userName), "Login operation failed under username {0}", userName);
        }

        [AllureName("Verifies Logout yandex.com")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner(OWNER)]
        [TestCase(USER_NAME_3, PASSWORD_2)]
        public void SignOutTest(string userName, string password)
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToURl();
            homePage.ClickSignInBtn();

            SignInPage signInPage = new SignInPage(driver);
            signInPage.SignIn(userName, password);

            Assert.IsTrue(homePage.IsUserLoggedIn(userName), "Login operation failed under username {0}", userName);

            homePage.ClickSignOutBtn();

            Assert.IsTrue(homePage.IsUserLoggedOut(), "Logout operation failed");
        }

        [AllureName("Verifies Login mail.yandex.com")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner(OWNER)]
        [TestCase(USER_NAME_1, PASSWORD_1)]
        [TestCase(USER_NAME_2, PASSWORD_1)]
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