using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selenium_WebDriver.Pages
{
    public class MultiSelectPage : BasePage
    {
        const string URL = "https://demo.seleniumeasy.com/basic-select-dropdown-demo.html";

        List<string> _optionsText = new List<string>();

        [FindsBy(How = How.Id, Using = "multi-select")]
        private IWebElement _selectElement;

        public MultiSelectPage(IWebDriver driver) : base(driver) { }

        public void GoToURL()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void RandomSelectOptions(int countOptions)
        {
            Random random = new Random();
            SelectElement select = new SelectElement(_selectElement);

            IEnumerable<IWebElement> options = select.Options
                .OrderBy(x => random.Next())
                .Take(countOptions);

            foreach (IWebElement option in options)
            {
                select.SelectByValue(option.Text);
                _optionsText.Add(option.Text);
            }
        }

        public List<string> GetRandomOptions()
        {
            _optionsText.Sort();

            return _optionsText;
        }

        public List<string> GetSelectedOptions()
        {
            SelectElement select = new SelectElement(_selectElement);
            List<string> slectedOptions = new List<string>();

            foreach (IWebElement option in select.AllSelectedOptions)
            {
                slectedOptions.Add(option.Text);
            }

            slectedOptions.Sort();

            return slectedOptions;
        }
    }
}
