using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace Selenium_WebDriver.Pages
{
    public class ProgressBarPage : BasePage
    {
        const string URL = "https://demo.seleniumeasy.com/bootstrap-download-progress-demo.html";
        const char PROCENT_ICON = '%';
        readonly TimeSpan _timeout = TimeSpan.FromSeconds(100);
        readonly TimeSpan _intervalTime = TimeSpan.FromMilliseconds(1);

        [FindsBy(How = How.Id, Using = "cricle-btn")]
        private IWebElement _downloadButton;

        [FindsBy(How = How.ClassName, Using = "percenttext")]
        private IWebElement progressBar;

        public ProgressBarPage(IWebDriver driver) : base(driver) { }

        public void GoToURL()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void ClickDownloadButton()
        {
            _downloadButton.Click();
        }

        public void WaitDownloadPercentages(int percent)
        {
            WebDriverWait wait = new WebDriverWait(driver, _timeout);

            wait.PollingInterval = _intervalTime;
            wait.Until(x => GetPercentages() >= percent);
        }

        public int GetPercentages()
        {
            return int.Parse(progressBar.Text.Trim(new char[] { PROCENT_ICON }));
        }
    }
}
