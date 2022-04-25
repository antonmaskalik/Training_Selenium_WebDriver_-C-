using NUnit.Allure.Attributes;
using NUnit.Framework;
using Selenium_WebDriver.Pages.FinalTask;
using System.Collections.Generic;

namespace Selenium_WebDriver
{
    [AllureSuite("Final Task test")]
    [TestFixture]
    public class FinalTaskTests : BaseTest
    {
        const string EMAIL = "anton.maskalik@gmail.com";
        const string PASSWORD = "12345";
        const string PRODUCT_NAME = "Blouse";
        const string WISH_LIST_NAME = "New Wishlist";
        const string FIRST_NAME = "Ivan";
        const string LAST_NAME = "Ivanov";
        const string ADDRESS = "Lenina 10, 25, Gucci";
        const string CITY = "Maimi";
        const string STATE = "Florida";
        const string ZIP_CODE = "00000";
        const string COUNTRY = "United States";
        const string PHONE = "258262485428";

        List<string> _productsAddToCart = new List<string>() { "Blouse", "Printed Dress", "Printed Summer Dress" };
        List<string> _productsAddToWishlists = new List<string>() { "Blouse" };

        [TearDown]
        public void CleanupCreatedData()
        {
            TakeScreenshot();

            HeaderPage headerPage = new HeaderPage(driver);
            headerPage.GoToMyAccount();

            MyAccountPage myAccountPage = new MyAccountPage(driver);
            myAccountPage.GoToMyWishlist();

            MyWishlistPage myWishlistPage = new MyWishlistPage(driver);
            myWishlistPage.RemoveWishlists();

            headerPage.SingOut();            
        }

        [AllureName("Verifies create account")]
        [TestCase(FIRST_NAME, LAST_NAME, PASSWORD, ADDRESS, CITY, STATE, ZIP_CODE, COUNTRY, PHONE)]
        public void CreateAccountTest(string fName, string lName, string password, string address, string city, string state, string zipCode, string country, string phone)
        {
            AuthenticationPage authenticationPage = new AuthenticationPage(driver);
            authenticationPage.GoToUrl();
            string email = authenticationPage.GetRandomEmail();
            authenticationPage.CreateAccount(email);

            Assert.IsTrue(authenticationPage.IsAccountCreated(), "{0} is invalid email address", email);

            authenticationPage.Registration(fName, lName, password, address, city, state, zipCode, country, phone);

            MyAccountPage myAccountPage = new MyAccountPage(driver);

            Assert.IsTrue(myAccountPage.IsAccountRegisteredOrSignedIn(), "Required fields are not filled");
        }

        [AllureName("Verifies LogIn")]
        [Test]
        public void SignInTest()
        {
            AuthenticationPage authenticationPage = new AuthenticationPage(driver);
            authenticationPage.GoToUrl();
            authenticationPage.SignIn(EMAIL, PASSWORD);

            MyAccountPage myAccountPage = new MyAccountPage(driver);

            Assert.IsTrue(myAccountPage.IsAccountRegisteredOrSignedIn(), "Invalid email address {0} or password", EMAIL);
        }

        [AllureName("Verifies add to wishlists")]
        [Test]
        public void AutoCreateWishlistTest()
        {
            AuthenticationPage authenticationPage = new AuthenticationPage(driver);
            authenticationPage.GoToUrl();
            authenticationPage.SignIn(EMAIL, PASSWORD);

            MyAccountPage myAccountPage = new MyAccountPage(driver);
            myAccountPage.GoToMyWishlist();

            MyWishlistPage myWishlistPage = new MyWishlistPage(driver);

            Assert.IsFalse(myWishlistPage.IsWishlistExist(), "Wishlist has already created");

            myWishlistPage.GoToHomePage();

            HomePage homePage = new HomePage(driver);
            homePage.OpenProduct(PRODUCT_NAME);

            if (homePage.IsFrameOpened())
            {
                homePage.AddToWishlist();
            }
            else
            {
                ProductPage productPage = new ProductPage(driver);
                productPage.AddToWishlist();
            }

            HeaderPage headerPage = new HeaderPage(driver);
            headerPage.GoToMyAccount();
            myAccountPage.GoToMyWishlist();

            Assert.IsTrue(myWishlistPage.DoesWishlistContainProducts(_productsAddToWishlists), "Wishlist doesn't contain {0}", PRODUCT_NAME);
        }

        [AllureName("Verifies create new wishlist")]
        [Test]
        public void CreateWishlistTest()
        {
            AuthenticationPage authenticationPage = new AuthenticationPage(driver);
            authenticationPage.GoToUrl();
            authenticationPage.SignIn(EMAIL, PASSWORD);

            MyAccountPage myAccountPage = new MyAccountPage(driver);
            myAccountPage.GoToMyWishlist();

            MyWishlistPage myWishlistPage = new MyWishlistPage(driver);
            myWishlistPage.CreateWishlist(WISH_LIST_NAME);

            Assert.IsTrue(myWishlistPage.IsWishlistExist(WISH_LIST_NAME), "My Wishlists do not contain {0}", WISH_LIST_NAME);

            myWishlistPage.GoToHomePage();

            HomePage homePage = new HomePage(driver);
            homePage.OpenProduct(PRODUCT_NAME);

            if (homePage.IsFrameOpened())
            {
                homePage.AddToWishlist();
            }
            else
            {
                ProductPage productPage = new ProductPage(driver);
                productPage.AddToWishlist();
            }

            HeaderPage headerPage = new HeaderPage(driver);
            headerPage.GoToMyAccount();
            myAccountPage.GoToMyWishlist();

            Assert.IsTrue(myWishlistPage.DoesWishlistContainProducts(_productsAddToWishlists), "{0} doesn't contain {1}", WISH_LIST_NAME, PRODUCT_NAME);
        }

        [AllureName("Verifies add to cart")]
        [Test]
        public void AddToCartTest()
        {
            AuthenticationPage authenticationPage = new AuthenticationPage(driver);
            authenticationPage.GoToUrl();
            authenticationPage.SignIn(EMAIL, PASSWORD);

            HeaderPage headerPage = new HeaderPage(driver);
            headerPage.GoToHomePage();

            HomePage homePage = new HomePage(driver);

            foreach (string product in _productsAddToCart)
            {
                homePage.OpenProduct(product);

                if (homePage.IsFrameOpened())
                {
                    homePage.AddToCard();
                }
                else
                {
                    ProductPage productPage = new ProductPage(driver);
                    productPage.AddToCard();
                    headerPage.GoToHomePage();
                }
            }

            headerPage.GoToCart();
            CartPage cartpage = new CartPage(driver);

            Assert.IsTrue(cartpage.DoesCartContainProductNames(_productsAddToCart), "The Cart doesn't contain all product names");
        }
    }
}
