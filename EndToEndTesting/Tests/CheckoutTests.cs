using NUnit.Framework;
using EndToEndTesting.Pages;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class CheckoutTests : BaseTest
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CartPage _cartPage;
        private CheckoutPage _checkoutPage;

        [SetUp]
        public void DataSetup()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(Driver);
            _loginPage.Login("standard_user", "secret_sauce");
            
            _inventoryPage = new InventoryPage(Driver);
            _inventoryPage.AddItemToCart("Sauce Labs Backpack");
            _inventoryPage.GoToCart();
            
            _cartPage = new CartPage(Driver);
            _cartPage.Checkout();
            
            _checkoutPage = new CheckoutPage(Driver);
        }

        [Test]
        public void TestFullCheckoutFlow()
        {
            _checkoutPage.EnterShippingDetails("John", "Doe", "12345");
            _checkoutPage.FinishCheckout();
            
            Assert.That(_checkoutPage.GetCompleteMessage(), Is.EqualTo("Thank you for your order!"));
        }
    }
}
