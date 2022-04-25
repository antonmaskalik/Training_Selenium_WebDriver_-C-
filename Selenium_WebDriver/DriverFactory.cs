using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System;
using Selenium_WebDriver.Enums;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Selenium_WebDriver
{
    public class DriverFactory
    {
        const string SAUCE_LABS_URL = "https://oauth-anton.maskalik-018af:8c459e22-2903-4cef-865a-8657aa2277f5@ondemand.eu-central-1.saucelabs.com:443/wd/hub";
        const string SELENOID_URL = "http://localhost:4444/wd/hub";
        const string BROWSER_VERSION = "latest";
        IWebDriver _driver = null;
        DriverOptions _browserOptions;

        public IWebDriver InitDriver(TestsRunMethod testsRunMethod, Browser browser, OSVersion operatingSystem)
        {
            if (_driver == null)
            {
                switch (testsRunMethod)
                {
                    case TestsRunMethod.Locally:
                        {
                            CreateLocallyDriver(browser);
                            break;
                        }
                    case TestsRunMethod.Selenoid:
                        {
                            CreateRemoteDriver(SELENOID_URL, browser);
                            break;
                        }
                    case TestsRunMethod.SauceLabs:
                        {
                            CreateRemoteDriver(SAUCE_LABS_URL, browser, operatingSystem);
                            break;
                        }
                }

                _driver.Manage().Window.Maximize();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            }

            return _driver;
        }

        private void CreateLocallyDriver(Browser browser)
        {
            if (_driver == null)
            {
                SetBrowser(browser);
            }
        }

        private IWebDriver CreateRemoteDriver(string url, Browser browser)
        {
            if (_driver == null)
            {
                Dictionary<string, object> selenoidOptions = new Dictionary<string, object>();
                selenoidOptions.Add("enableVNC", true);
                SetBrowser(browser);
                _browserOptions.AddAdditionalOption("selenoid:options", selenoidOptions);

                _driver = new RemoteWebDriver(new Uri(url), _browserOptions.ToCapabilities());
            }

            return _driver;
        }

        private void CreateRemoteDriver(string url, Browser browser, OSVersion osVersion)
        {
            if (_driver == null)
            {
                SetBrowser(browser);
                SetOS(osVersion);

                _browserOptions.BrowserVersion = BROWSER_VERSION;

                _driver = new RemoteWebDriver(new Uri(url), _browserOptions);
            }
        }

        private void SetBrowser(Browser browser)
        {

            switch (browser)
            {
                case Browser.Chrome:
                    {
                        _driver = new ChromeDriver();
                        _browserOptions = new ChromeOptions();
                        break;
                    }
                case Browser.Firefox:
                    {
                        _driver = new FirefoxDriver();
                        _browserOptions = new FirefoxOptions();
                        break;
                    }
                case Browser.Edge:
                    {
                        _driver = new EdgeDriver();
                        _browserOptions = new EdgeOptions();
                        break;
                    }
                default:
                    {
                        _driver = new ChromeDriver();
                        _browserOptions = new ChromeOptions();
                        break;
                    }
            }
        }

        private void SetOS(OSVersion osVersion)
        {
            _browserOptions.PlatformName = GetDisplayName(osVersion);
        }

        private string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()?
                            .GetMember(enumValue.ToString())?
                            .First()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name;
        }
    }
}
