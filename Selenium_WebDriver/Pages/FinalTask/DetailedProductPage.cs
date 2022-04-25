using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Selenium_WebDriver.Pages.FinalTask
{
    public class DetailedProductPage : HeaderPage
    {
        [FindsBy(How = How.CssSelector, Using = "[title='Add to my wishlist']")]
        private IWebElement _addToWishlistBtn;

        [FindsBy(How = How.CssSelector, Using = "[id='add_to_cart button']")]
        private IWebElement _addToCartBtn;

        [FindsBy(How = How.CssSelector, Using = "[class='continue btn btn-default button exclusive-medium']")]
        private IWebElement _continueShoppingBtn;

        [FindsBy(How = How.XPath, Using = "//a[@class='fancybox-item fancybox-close']")]
        private IWebElement _closePopupBtn;

        public DetailedProductPage(IWebDriver driver) : base(driver) { }

        public void AddToWishlist()
        {
            _addToWishlistBtn.Click();
            _closePopupBtn.Click();
        }

        public void AddToCard()
        {
            _addToCartBtn.Click();
            _continueShoppingBtn.Click();
        }
    }
}
