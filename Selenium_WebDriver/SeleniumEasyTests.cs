using NUnit.Framework;
using Selenium_WebDriver.Enums;
using Selenium_WebDriver.Pages;
using System.Collections.Generic;

namespace Selenium_WebDriver
{
    [TestFixture]
    public class SeleniumEasyTests : BaseTest
    {
        const int COUNT_OPTIONS = 3;
        const string NAME = "Name";
        const int PROGRESS_PERCENTAGES = 50;
        const int NULL_PERCENTAGES = 0;
        const int COUNT = 10;
        const int AGE = 18;
        const int SALARY = 200000;

        [TestCase(COUNT_OPTIONS)]
        public void SelectOptionsTest(int countOptions)
        {
            MultiSelectPage multiSelectPage = new MultiSelectPage(driver);
            multiSelectPage.GoToURL();
            multiSelectPage.RandomSelectOptions(countOptions);

            Assert.AreEqual(multiSelectPage.GetRandomOptions(), multiSelectPage.GetSelectedOptions(), "Options selected incorrect");
        }

        [Test]
        public void AlertBoxTest()
        {
            AlertsPage alertsPage = new AlertsPage(driver);
            alertsPage.GoToURL();
            alertsPage.InvokeAlert(AlertType.AlertBox);

            Assert.IsTrue(alertsPage.IsAlertPresent(), "Asert didn't present");

            alertsPage.AcceptAlert();

            Assert.IsFalse(alertsPage.IsAlertPresent(), "Assert didn't confirm");
        }

        [Test]
        public void ConfirmBoxTest()
        {
            AlertsPage alertsPage = new AlertsPage(driver);
            alertsPage.GoToURL();
            alertsPage.InvokeAlert(AlertType.ConfirmBox);

            Assert.IsTrue(alertsPage.IsAlertPresent(), "Confirm box didn't present");

            alertsPage.AcceptAlert();

            Assert.IsTrue(alertsPage.IsConfirmBoxAccepted(), "Confirm box didn't accept");

            alertsPage.InvokeAlert(AlertType.ConfirmBox);
            alertsPage.DismissAlert();

            Assert.IsTrue(alertsPage.IsConfirmBoxDismissed(), "Confirm box didn't dismiss");
        }

        [TestCase(NAME)]
        public void PromptBoxTest(string name)
        {
            AlertsPage alertsPage = new AlertsPage(driver);
            alertsPage.GoToURL();
            alertsPage.InvokeAlert(AlertType.PromptBox);

            Assert.IsTrue(alertsPage.IsAlertPresent(), "Prompt box didn't present");

            alertsPage.EnterName(name);

            Assert.IsTrue(alertsPage.IsNameInPromptBoxEntered(name), "Entered {0} in prompt box is invalid", name);
        }

        [Test]
        public void GetNewUserTest()
        {
            GetNewUserPage getNewUserPage = new GetNewUserPage(driver);
            getNewUserPage.GoToURL();
            getNewUserPage.GetUser();

            Assert.IsTrue(getNewUserPage.IsUserDisplayed(), "User was not display");
        }

        [TestCase(PROGRESS_PERCENTAGES)]
        public void ProgressBarTest(int progressPercentages)
        {
           ProgressBarPage progressBarPage = new ProgressBarPage(driver);
            progressBarPage.GoToURL();
            progressBarPage.ClickDownloadButton();
            progressBarPage.WaitDownloadPercentages(progressPercentages);

            Assert.IsTrue(progressBarPage.GetPercentages() >= progressPercentages, "Progress bar loaded less than {0}%", progressPercentages);

            driver.Navigate().Refresh();

            Assert.IsTrue(progressBarPage.GetPercentages() == NULL_PERCENTAGES, "Progress bar loaded more than {0}%", NULL_PERCENTAGES);
        }

        [Test]
        public void TableTest()
        {      
            TablePage page = new TablePage(driver);

            page.GoToURL();
            page.SelectEntries(COUNT);

            List<Person> people = page.GetListPeople(AGE, SALARY);

            Assert.IsTrue(people.TrueForAll(x => x.Age > AGE && x.Salary <= SALARY), "The list of people contains person whose age or salary does not match the conditions");
        }
    }
}
