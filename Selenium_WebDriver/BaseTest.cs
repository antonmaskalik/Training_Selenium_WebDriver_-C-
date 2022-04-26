using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using Allure.Commons;
using NUnit.Allure.Core;
using Selenium_WebDriver.Enums;
using NUnit.Framework.Interfaces;

namespace Selenium_WebDriver
{
    [AllureNUnit]
    public class BaseTest
    {
        const string SCREENSHOTS_FOLDER = "..\\..\\..\\Screenshots";
        const string SCREENSHOT_NAME = "Screenshot_";
        const string PNG = ".png";
        readonly string PATH_TO_SCREENSHOTS_FOLDER = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SCREENSHOTS_FOLDER);
        protected IWebDriver driver;
        DriverFactory driverFactory;       

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driverFactory = new DriverFactory();
            driver = driverFactory.InitDriver(TestsRunMethod.Locally, Browser.Chrome, OSVersion.Windows8_1);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        public void TakeScreenshot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string filename = TestContext.CurrentContext.Test.MethodName + SCREENSHOT_NAME + DateTime.Now.Ticks + PNG;
                string path = PATH_TO_SCREENSHOTS_FOLDER + filename;

                screenshot.SaveAsFile(path, ScreenshotImageFormat.Jpeg);
                TestContext.AddTestAttachment(path);
                AllureLifecycle.Instance.AddAttachment(filename, PNG, path);
            }
        }

        [OneTimeSetUp]
        public void CleanupResultDirectory()
        {
            AllureExtensions.WrapSetUpTearDownParams(() => { AllureLifecycle.Instance.CleanupResultDirectory(); },
                "Cleanup Allure Results Directory");
        }
    }
}
