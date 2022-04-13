using NUnit.Framework;
using OpenQA.Selenium;

namespace Selenium_WebDriver
{
    public class BaseTest
    {
        protected IWebDriver driver;
        DriverFactory driverFactory;

        [SetUp]
        public void SetUp()
        {
            driverFactory = new DriverFactory();
            driver = driverFactory.InitDriver();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
