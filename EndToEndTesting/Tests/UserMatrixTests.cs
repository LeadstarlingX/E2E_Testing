using NUnit.Framework;
using EndToEndTesting.Pages;
using System.Linq;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class UserMatrixTests : BaseTest
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CheckoutPage _checkoutPage;
        private CartPage _cartPage;

        [SetUp]
        public void LocalSetup()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(Driver);
            _inventoryPage = new InventoryPage(Driver);
            _cartPage = new CartPage(Driver);
            _checkoutPage = new CheckoutPage(Driver);
        }

        [Test]
        public void TestStandardUser_FullFlow()
        {
            _loginPage.Login("standard_user", "secret_sauce");
            _inventoryPage.AddItemToCart("Sauce Labs Backpack");
            _inventoryPage.GoToCart();
            _cartPage.Checkout();
            _checkoutPage.EnterShippingDetails("John", "Doe", "12345");
            _checkoutPage.FinishCheckout();
            
            Assert.That(_checkoutPage.GetCompleteMessage(), Is.EqualTo("Thank you for your order!"));
        }


        [Test]
        public void TestPerformanceGlitchUser_CheckoutCompletes()
        {
            // Verify glitch user handled by wait
            _loginPage.Login("performance_glitch_user", "secret_sauce");
            
            // Should be on inventory page after (delayed) login
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
            
            _inventoryPage.AddItemToCart("Sauce Labs Backpack");
            _inventoryPage.GoToCart();
            _cartPage.Checkout();
            _checkoutPage.EnterShippingDetails("Test", "User", "00000");
            _checkoutPage.FinishCheckout();
            
            Assert.That(_checkoutPage.GetCompleteMessage(), Is.EqualTo("Thank you for your order!"));
        }


        [Test]
        public void TestLockedOutUser_DetectErrorMessage()
        {
            _loginPage.Login("locked_out_user", "secret_sauce");
            string error = _loginPage.GetErrorMessage();
            Assert.That(error, Does.Contain("Sorry, this user has been locked out."));
        }
    }
}
