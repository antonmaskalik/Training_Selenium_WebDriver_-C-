using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Linq;

namespace Selenium_WebDriver.Pages.FinalTask
{
    public class AuthenticationPage : HeaderPage
    {
        const string URL = "http://automationpractice.com/index.php?controller=authentication&back=my-account";
        const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        const string GMAIL_COM = "@gmail.com";
        const int MIN_VALUE = 1;
        const int MAX_VALUE = 20;

        [FindsBy(How = How.Id, Using = "email_create")]
        private IWebElement _emailAddressInput;

        [FindsBy(How = How.Id, Using = "customer_firstname")]
        private IWebElement _firstNameInput;

        [FindsBy(How = How.Id, Using = "customer_lastname")]
        private IWebElement _lastNameInput;

        [FindsBy(How = How.Id, Using = "passwd")]
        private IWebElement _passwordInput;

        [FindsBy(How = How.Id, Using = "address1")]
        private IWebElement _addressInput;

        [FindsBy(How = How.Id, Using = "city")]
        private IWebElement _cityInput;

        [FindsBy(How = How.Id, Using = "postcode")]
        private IWebElement _zipCodeInput;

        [FindsBy(How = How.Id, Using = "phone_mobile")]
        private IWebElement _phoneInput;

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement _emailInput;

        [FindsBy(How = How.Id, Using = "passwd")]
        private IWebElement _loginPasswordInput;

        [FindsBy(How = How.Id, Using = "SubmitLogin")]
        private IWebElement _sighInBtn;

        [FindsBy(How = How.Id, Using = "submitAccount")]
        private IWebElement _registerBtn;

        [FindsBy(How = How.Id, Using = "SubmitCreate")]
        private IWebElement _createAccountBtn;

        [FindsBy(How = How.Id, Using = "id_state")]
        private IWebElement _stateSelect;

        [FindsBy(How = How.Id, Using = "id_country")]
        private IWebElement _countrySelect;    
      
        public AuthenticationPage(IWebDriver driver): base(driver) { }

        public bool IsAccountCreated()
        {
            return _firstNameInput.Displayed;
        }

        public void GoToUrl()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public string GetRandomEmail()
        {
            Random random = new Random();

            int length = random.Next(MIN_VALUE, MAX_VALUE);

            return new string(Enumerable.Repeat(CHARS, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + GMAIL_COM;
        }

        public void CreateAccount(string email)
        {
            _emailAddressInput.SendKeys(email);
            _createAccountBtn.Click();
        }      

        public void Registration(Member member)
        {
            _firstNameInput.SendKeys(member.FirstName);
            _lastNameInput.SendKeys(member.LastName);
            _passwordInput.SendKeys(member.Password);
            _addressInput.SendKeys(member.Address);
            _cityInput.SendKeys(member.City);
            _zipCodeInput.SendKeys(member.ZipCode);
            _phoneInput.SendKeys(member.Phone);

            SelectElement stateElement = new SelectElement(_stateSelect);
            stateElement.SelectByText(member.State);

            SelectElement countryElement = new SelectElement(_countrySelect);
            countryElement.SelectByText(member.Country);

            _registerBtn.Click();
        }

        public void SignIn(string email, string password)
        {
            _emailInput.SendKeys(email);
            _loginPasswordInput.SendKeys(password);

            _sighInBtn.Click();
        }
    }
}
