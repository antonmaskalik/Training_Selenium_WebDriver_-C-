using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace Selenium_WebDriver.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
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

        public void WaitElement(By element, TimeSpan timeout, TimeSpan? intervalTime = null)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            
            if(intervalTime != null)
            {
                wait.PollingInterval = (TimeSpan)intervalTime;
            }

            wait.Until(e => IsElementVisible(element));
        }
    }
}
