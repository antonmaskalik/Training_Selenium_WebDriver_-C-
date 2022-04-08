using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Yandex.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsElementVisible(By searchElementBy)
        {
            try
            {
                bool result = driver.FindElement(searchElementBy).Displayed;
                return result;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void WaitElement(By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => IsElementVisible(element));
        }
    }
}
