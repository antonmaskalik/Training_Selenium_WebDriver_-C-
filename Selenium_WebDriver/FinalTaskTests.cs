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
        const string WISH_LIST_NAME = "New Wishlist";

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
        [Test]
        public void CreateAccountTest()
        {
            AuthenticationPage authenticationPage = new AuthenticationPage(driver);
            authenticationPage.GoToUrl();
            string email = authenticationPage.GetRandomEmail();
            authenticationPage.CreateAccount(email);

            Assert.IsTrue(authenticationPage.IsAccountCreated(), "{0} is invalid email address", email);

            Member member = new Member();
            authenticationPage.Registration(member);

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
            homePage.OpenProducts(_productsAddToWishlists, false, true);

            HeaderPage headerPage = new HeaderPage(driver);
            headerPage.GoToMyAccount();
            myAccountPage.GoToMyWishlist();

            Assert.IsTrue(myWishlistPage.DoesWishlistContainProducts(_productsAddToWishlists), "Wishlist doesn't contain these products");
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
            homePage.OpenProducts(_productsAddToWishlists, false, true);

            ProductPage productPage = new ProductPage(driver);
            productPage.AddToWishlist();

            HeaderPage headerPage = new HeaderPage(driver);
            headerPage.GoToMyAccount();
            myAccountPage.GoToMyWishlist();

            Assert.IsTrue(myWishlistPage.DoesWishlistContainProducts(_productsAddToWishlists), "{0} doesn't contain these products", WISH_LIST_NAME);
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
            homePage.OpenProducts(_productsAddToCart, true, false);
            headerPage.GoToCart();

            CartPage cartpage = new CartPage(driver);

            Assert.IsTrue(cartpage.DoesCartContainProductNames(_productsAddToCart), "The Cart doesn't contain all product names");
        }
    }
}
