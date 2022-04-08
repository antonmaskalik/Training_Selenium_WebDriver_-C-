using NUnit.Framework;
using Yandex.Pages;

namespace Yandex
{
    [TestFixture]
    public class YndexTest: BaseTest
    {
        const string USER_NAME = "WebDriverCSharp";
        const string PASSWORD = "Selenium";

        [TestCase(USER_NAME, PASSWORD)]
        public void SignInTest(string userName, string password)
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToURl();
            homePage.ClickSignInBtn();

            SignInPage signInPage = new SignInPage(driver);
            signInPage.SignIn(userName, password);

            Assert.IsTrue(homePage.IsUserLoggedIn(USER_NAME));
        }
    }
}