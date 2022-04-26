using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Selenium_WebDriver.Pages.FinalTask
{
    public class ProductPage:  HeaderPage
    {
        [FindsBy(How = How.CssSelector, Using = "#add_to_cart>button")]
        private IWebElement _addToCartBtn;

        [FindsBy(How = How.CssSelector, Using = "[class='continue btn btn-default button exclusive-medium']")]
        private IWebElement _continueShoppingBtn;

        [FindsBy(How = How.Id, Using = "wishlist_button")]
        private IWebElement _addToWishlistBtn;

        [FindsBy(How = How.CssSelector, Using = "[class='fancybox-item fancybox-close']")]
        private IWebElement _closePopupBtn;

        public ProductPage(IWebDriver driver): base(driver) { }

        public void AddToCard()
        {
            _addToCartBtn.Click();
            _continueShoppingBtn.Click();
        }

        public void AddToWishlist()
        {
            _addToWishlistBtn.Click();
            _closePopupBtn.Click();
        }
    }
}
