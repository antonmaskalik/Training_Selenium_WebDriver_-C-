using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium_WebDriver.Pages.FinalTask
{
    public class CartPage : HeaderPage
    {
        private By _productName = By.CssSelector("table p.product-name>a");
        private By _products = By.CssSelector("tbody>[id^='product_']");
        private By _productQuantity = By.CssSelector("[class='cart_quantity text-center']>input");

        public CartPage(IWebDriver driver) : base(driver) { }

        public bool DoesCartContainProductNames(List<string> productNames)
        {
            return DoesItContainProductNames(productNames, _products, _productName, _productQuantity);
        }
    }
}
