using NUnit.Framework;
using EndToEndTesting.Pages;
using OpenQA.Selenium;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class BusinessLogicTests : BaseTest
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private CartPage _cartPage;
        private CheckoutPage _checkoutPage;
        private SideMenuPage _sideMenuPage;

        [SetUp]
        public void LocalSetup()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(Driver);
            _inventoryPage = new InventoryPage(Driver);
            _cartPage = new CartPage(Driver);
            _checkoutPage = new CheckoutPage(Driver);
            _sideMenuPage = new SideMenuPage(Driver);
        }

        [Test]
        public void TestCheckout_MathValidation()
        {
            _loginPage.Login("standard_user", "secret_sauce");
            
            // Add items
            _inventoryPage.AddItemToCart("Sauce Labs Backpack"); // $29.99
            _inventoryPage.AddItemToCart("Sauce Labs Bike Light"); // $9.99
            
            _inventoryPage.GoToCart();
            _cartPage.Checkout();
            _checkoutPage.EnterShippingDetails("Math", "Checker", "11111");

            // Verify summary totals
            string subtotalText = Driver.FindElement(By.ClassName("summary_subtotal_label")).Text;
            string taxText = Driver.FindElement(By.ClassName("summary_tax_label")).Text;
            string totalText = Driver.FindElement(By.ClassName("summary_total_label")).Text;

            double subtotal = ExtractPrice(subtotalText);
            double tax = ExtractPrice(taxText);
            double total = ExtractPrice(totalText);

            Assert.That(subtotal, Is.EqualTo(39.98).Within(0.01), "Subtotal should be the sum of 29.99 and 9.99");
            Assert.That(subtotal + tax, Is.EqualTo(total).Within(0.01), "Subtotal + Tax should equal Total");
        }

        [Test]
        public void TestSession_Isolation()
        {
            // User A adds item
            _loginPage.Login("standard_user", "secret_sauce");
            _inventoryPage.AddItemToCart("Sauce Labs Onesie");
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(1));
            
            _sideMenuPage.OpenMenu();
            _sideMenuPage.Logout();

            // User B logs in
            _loginPage.Login("performance_glitch_user", "secret_sauce");
            
            // Verify session isolation
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(0), "New user should not see items from previous user's session.");
        }

        private double ExtractPrice(string text)
        {
            // Regex match price
            var match = Regex.Match(text, @"[0-9]+\.[0-9]+");
            return double.Parse(match.Value, CultureInfo.InvariantCulture);
        }
    }
}
