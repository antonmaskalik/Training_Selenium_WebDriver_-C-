using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

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

        public void OpenProducts(IEnumerable<string> productNames, bool addToCart, bool addToWishlist)
        {
            foreach (string name in productNames)
            {
                IWebElement product = driver.FindElement(By.XPath(string.Format(_productFormat, name)));
                product.Click();

                if (addToCart)
                {
                    AddToCard();
                }
                if (addToWishlist)
                {
                    AddToWishlist();
                }
            }
        }

        private void AddToWishlist()
        {
            if (IsFrameOpened())
            {
                driver.SwitchTo().Frame(driver.FindElement(_deatailBoxFrame));
                _addToWishlistBtn.Click();
                CloseFrame();
            }
            else
            {
                ProductPage productPage = new ProductPage(driver);
                productPage.AddToWishlist();
                GoToHomePage();
            }
        }

        private void AddToCard()
        {
            if (IsFrameOpened())
            {
                SwitchToFrame(_deatailBoxFrame);
                _addToCartBtn.Click();
                SwitchToDefaultContent();
                _continueShoppingBtn.Click();
            }
            else
            {
                ProductPage productPage = new ProductPage(driver);
                productPage.AddToCard();
                GoToHomePage();
            }
        }

        private void CloseFrame()
        {
            _closeFrameBtn.Click();
            SwitchToDefaultContent();
            _closeFrameBtn.Click();
        }
    }
}
