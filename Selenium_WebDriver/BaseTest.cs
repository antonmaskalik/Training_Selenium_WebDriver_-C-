using NUnit.Framework;
using OpenQA.Selenium;

namespace Yandex
{
    public class BaseTest
    {
        protected IWebDriver driver;
        DriverFactory driverFactory;

        public BaseTest()
        {
            driverFactory= new DriverFactory();
        }

        [SetUp]
        public void SetUp()
        {     
            driver = driver = driverFactory.InitDriver();
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
