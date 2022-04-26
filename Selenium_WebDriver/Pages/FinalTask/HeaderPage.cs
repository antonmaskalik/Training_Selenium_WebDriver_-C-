using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace Selenium_WebDriver.Pages.FinalTask
{
    public class HeaderPage : BasePage
    {
        const string ATTRIBUTE = "value";
        const int PRODUCT_QUANTITY = 1;
        readonly TimeSpan _timeout = TimeSpan.FromSeconds(5);

        [FindsBy(How = How.ClassName, Using = "home")]
        private IWebElement _homeBtn;

        [FindsBy(How = How.CssSelector, Using = ".shopping_cart>a")]
        private IWebElement _cartBtn;

        [FindsBy(How = How.ClassName, Using = "logout")]
        private IWebElement _singOutBtn;

        [FindsBy(How = How.ClassName, Using = "account")]
        private IWebElement _accountBtn;

        public HeaderPage(IWebDriver driver) : base(driver) { }

        public void GoToHomePage()
        {
            _homeBtn.Click();
        }

        public void GoToCart()
        {
            _cartBtn.Click();
        }

        public void GoToMyAccount()
        {
            _accountBtn.Click();
        }

        public void SingOut()
        {
            _singOutBtn.Click();
        }

        protected bool DoesItContainProductNames(List<string> productNames, By products, By productName, By productQuantity)
        {
            bool result = false;

            foreach (string name in productNames)
            {
                foreach (string item in GetProductsInCart(products, productName, productQuantity))
                {
                    if (item.StartsWith(name))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        protected int GetProductQuantity(IWebElement product, By productQuantity)
        {
            return int.Parse(product.FindElement(productQuantity).GetAttribute(ATTRIBUTE));
        }

        protected List<string> GetProductsInCart(By products, By productName, By productQuantity)
        {
            List<string> _products = new List<string>();

            WaitElement(products, _timeout);

            foreach (IWebElement product in driver.FindElements(products))
            {
                if (GetProductQuantity(product, productQuantity) == PRODUCT_QUANTITY)
                {
                    _products.Add(product.FindElement(productName).Text);
                }
            }

            return _products;
        }

        protected void SwitchToFrame(By frame)
        {
            driver.SwitchTo().Frame(driver.FindElement(frame));
        }

        protected void SwitchToDefaultContent()
        {
            driver.SwitchTo().DefaultContent();
        }

        protected void CloseAlert()
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
    }
}
