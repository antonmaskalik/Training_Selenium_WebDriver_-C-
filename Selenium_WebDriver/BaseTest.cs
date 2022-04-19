using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using Allure.Commons;
using NUnit.Allure.Core;
using Selenium_WebDriver.Enums;

namespace Selenium_WebDriver
{
    [AllureNUnit]
    public class BaseTest
    {
        const string SCREENSHOTS_FOLDER = "..\\..\\..\\Screenshots";
        const string SCREENSHOT_NAME = "Screenshot_";
        const string JPG = ".jpg";
        readonly string PATH_TO_SCREENSHOTS_FOLDER = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SCREENSHOTS_FOLDER);
        protected IWebDriver driver;
        DriverFactory driverFactory;

        [SetUp]
        public void SetUp()
        {
            driverFactory = new DriverFactory();
            driver = driverFactory.InitDriver(false, Browser.Chrome, OSVersion.Windows10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void TakeScreenshot()
        {
            Directory.CreateDirectory(PATH_TO_SCREENSHOTS_FOLDER);
            string pathToScreenshots = Path.Combine(PATH_TO_SCREENSHOTS_FOLDER, SCREENSHOT_NAME + TestContext.CurrentContext.Test.MethodName + JPG);

            Screenshot screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile(pathToScreenshots, ScreenshotImageFormat.Jpeg);
        }

        [OneTimeSetUp]
        public void CleanupResultDirectory()
        {
            AllureExtensions.WrapSetUpTearDownParams(() => { AllureLifecycle.Instance.CleanupResultDirectory(); },
                "Cleanup Allure Results Directory");
        }
    }
}
