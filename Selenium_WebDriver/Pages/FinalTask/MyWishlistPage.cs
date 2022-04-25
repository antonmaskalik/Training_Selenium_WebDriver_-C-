using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace Selenium_WebDriver.Pages.FinalTask
{
    public class MyWishlistPage : HeaderPage
    {
        private By _wishListName = By.CssSelector("td[style]>[onclick]");
        private By _products = By.CssSelector(".product_infos");
        private By _productName = By.Id("s_title");
        private By _productQuantity = By.CssSelector("[id^='quantity_']");
        private By _wishlistTable = By.CssSelector("[class='table table-bordered']");
        private By _removeWishlistBtns = By.CssSelector(".icon-remove");

        [FindsBy(How = How.Id, Using = "name")]
        private IWebElement _nameInput;

        [FindsBy(How = How.Id, Using = "submitWishlist")]
        private IWebElement _saveBtn;

        [FindsBy(How = How.XPath, Using = "(//td/a)[2]")]
        private IWebElement _viewProductBtn;

        public MyWishlistPage(IWebDriver driver) : base(driver) { }

        public bool IsWishlistExist()
        {
            return IsElementVisible(_wishlistTable);
        }

        public bool IsWishlistExist(string nameWishList)
        {
            List<string> wishListNames = new List<string>();

            foreach (IWebElement element in driver.FindElements(_wishListName))
            {
                wishListNames.Add(element.Text);
            }

            return wishListNames.Contains(nameWishList);
        }

        public bool DoesWishlistContainProducts(List<string> productNames)
        {
            _viewProductBtn.Click();
            
            return DoesItContainProductNames(productNames, _products, _productName, _productQuantity);
        }

        public void CreateWishlist(string name)
        {
            _nameInput.SendKeys(name);
            _saveBtn.Click();
        }

        public void RemoveWishlists()
        {
            if (IsElementVisible(_removeWishlistBtns))
            {
                foreach (var element in driver.FindElements(_removeWishlistBtns))
                {
                    element.Click();

                    CloseAlert();
                }
            }
        }
    }
}
