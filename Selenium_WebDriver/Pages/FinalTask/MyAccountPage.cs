using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace Selenium_WebDriver.Pages.FinalTask
{
    internal class MyAccountPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "info-account")]
        private IWebElement _informationHeader;

        [FindsBy(How = How.ClassName, Using = "lnk_wishlist")]
        private IWebElement _myWishlistsBtn;

        public MyAccountPage(IWebDriver driver) : base(driver) { }      

        public bool IsAccountRegisteredOrSignedIn()
        {
            return _informationHeader.Displayed;
        }

        public void GoToMyWishlist()
        {
            _myWishlistsBtn.Click();
        }
    }
}
