using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace Selenium_WebDriver.Pages
{
    public class TablePage : BasePage
    {
        const string URL = "https://demo.seleniumeasy.com/table-sort-search-demo.html";
        const string ATTRIBUTE = "data-order";
        private By _disabledNextButton = By.XPath("//*[@class='paginate_button next disabled']");
        private By _rows = By.XPath(".//tbody/tr");
        private By _age = By.XPath(".//td[4]");
        private By _salary = By.XPath(".//td[6]");
        private By _name = By.XPath(".//td[1]");
        private By _position = By.XPath(".//td[2]");
        private By _office = By.XPath(".//td[3]");

        [FindsBy(How = How.CssSelector, Using = "[name='example_length']")]
        private IWebElement _selectEntries;

        [FindsBy(How = How.CssSelector, Using = "#example_next")]
        private IWebElement _nextButton;

        public TablePage(IWebDriver driver) : base(driver) { }

        public void GoToURL()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void SelectEntries(int count)
        {
            SelectElement select = new SelectElement(_selectEntries);
            select.SelectByValue(count.ToString());
        }

        public List<Person> GetListPeople(int age, int salary)
        {
            int agePerson;
            int salaryPerson;
            List<Person> people = new List<Person>();

            while (true)
            {
                IEnumerable<IWebElement> rows = driver.FindElements(_rows);

                foreach (IWebElement row in rows)
                {
                    agePerson = GetValue(row, _age, null);
                    salaryPerson = GetValue(row, _salary, ATTRIBUTE);

                    if (agePerson > age && salaryPerson <= salary)
                    {
                        Person person = new Person(GetValue(row, _name), GetValue(row, _position), GetValue(row, _office),
                          agePerson, salaryPerson);

                        people.Add(person);
                    }
                }

                if (IsElementVisible(_disabledNextButton))
                {
                    break;
                }
                else
                {
                    _nextButton.Click();
                }
            }

            return people;
        }

        private string GetValue(IWebElement row, By columnTitle)
        {
            return row.FindElement(columnTitle).Text;
        }

        private int GetValue(IWebElement row, By columnTitle, string attribute)
        {
            if (attribute == null)
            {
                return int.Parse(row.FindElement(columnTitle).Text);
            }
            else
            {
                return int.Parse(row.FindElement(columnTitle).GetAttribute(attribute));
            }
        }
    }
}
