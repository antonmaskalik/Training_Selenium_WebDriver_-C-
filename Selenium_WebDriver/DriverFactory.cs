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

namespace Selenium_WebDriver
{
    public class DriverFactory
    {
        const string REMOTE_URL = "https://oauth-anton.maskalik-018af:8c459e22-2903-4cef-865a-8657aa2277f5@ondemand.eu-central-1.saucelabs.com:443/wd/hub";
        const string BROWSER_VERSION = "latest";
        IWebDriver _driver = null;
        DriverOptions _browserOptions;

        public IWebDriver InitDriver(bool runLocally, Browser browser, OSVersion operatingSystem)
        {
            if (_driver == null)
            {
                if (runLocally)
                {
                    _driver = new ChromeDriver();
                    _driver.Manage().Window.Maximize();
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                }
                else
                {
                    _driver = CreateRemoteDriver(browser, operatingSystem);
                }
            }

            return _driver;
        }

        IWebDriver CreateRemoteDriver(Browser browser, OSVersion osVersion)
        {
            if (_driver == null)
            {
                SetBrowser(browser);
                SetOS(osVersion);

                _driver = new RemoteWebDriver(new Uri(REMOTE_URL), _browserOptions);
            }

            return _driver;
        }

        private void SetBrowser(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    {
                        _browserOptions = new ChromeOptions();
                        break;
                    }
                case Browser.Firefox:
                    {
                        _browserOptions = new FirefoxOptions();
                        break;
                    }
                case Browser.Edge:
                    {
                        _browserOptions = new EdgeOptions();
                        break;
                    }
                default:
                    {
                        _browserOptions = new ChromeOptions();
                        break;
                    }
            }

            _browserOptions.BrowserVersion = BROWSER_VERSION;
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
