using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Yandex
{
    public class DriverFactory
    {
        IWebDriver _driver = null;

        public IWebDriver InitDriver()
        {
            if (_driver == null)
            {
                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(11);
            }

            return _driver;
        }
    }
}
