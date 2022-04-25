using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace Selenium_WebDriver.Pages.FinalTask
{
    public class HomePage : HeaderPage
    {
        readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);

        private string _productFormat = "//a[@title='{0}']/img";
        private By _deatailBoxFrame = By.CssSelector("[id^='fancybox-frame']");

        [FindsBy(How = How.Id, Using = "wishlist_button")]
        private IWebElement _addToWishlistBtn;

        [FindsBy(How = How.XPath, Using = "//*[@class='fancybox-skin']/a[@class='fancybox-item fancybox-close']")]
        private IWebElement _closeFrameBtn;

        [FindsBy(How = How.CssSelector, Using = "#add_to_cart")]
        private IWebElement _addToCartBtn;

        [FindsBy(How = How.CssSelector, Using = "[class='continue btn btn-default button exclusive-medium']")]
        private IWebElement _continueShoppingBtn;

        public HomePage(IWebDriver driver) : base(driver) { }

        public bool IsFrameOpened()
        {
            try
            {
                WaitElement(_deatailBoxFrame, _timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void OpenProduct(string productName)
        {
            IWebElement product = driver.FindElement(By.XPath(string.Format(_productFormat, productName)));
            product.Click();
        }

        public void AddToWishlist()
        {
            driver.SwitchTo().Frame(driver.FindElement(_deatailBoxFrame));

            _addToWishlistBtn.Click();

            CloseFrame();
        }

        public void AddToCard()
        {
            SwitchToFrame(_deatailBoxFrame);

            _addToCartBtn.Click();

            SwitchToDefaultContent();

            _continueShoppingBtn.Click();
        }

        private void CloseFrame()
        {
            _closeFrameBtn.Click();

            SwitchToDefaultContent();

            _closeFrameBtn.Click();
        }
    }
}
