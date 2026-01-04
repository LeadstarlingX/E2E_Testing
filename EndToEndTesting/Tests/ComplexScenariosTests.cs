using NUnit.Framework;
using EndToEndTesting.Pages;
using OpenQA.Selenium;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class ComplexScenariosTests : BaseTest
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private ProductDetailsPage _detailsPage;
        private CartPage _cartPage;
        private CheckoutPage _checkoutPage;
        private SideMenuPage _sideMenuPage;

        [SetUp]
        public void LocalSetup()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(Driver);
            _inventoryPage = new InventoryPage(Driver);
            _detailsPage = new ProductDetailsPage(Driver);
            _cartPage = new CartPage(Driver);
            _checkoutPage = new CheckoutPage(Driver);
            _sideMenuPage = new SideMenuPage(Driver);
            
            _loginPage.Login("standard_user", "secret_sauce");
        }

        [Test]
        public void TestFullLifecycle_MultiItemManagement()
        {
            // Add items from inventory
            _inventoryPage.AddItemToCart("Sauce Labs Backpack");
            _inventoryPage.AddItemToCart("Sauce Labs Bike Light");
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(2));

            // Add item from details
            _inventoryPage.ClickProductTitle("Sauce Labs Bolt T-Shirt");
            _detailsPage.AddToCart();
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(3));

            // Remove item from cart
            _inventoryPage.GoToCart();
            Assert.That(_cartPage.HasItem("Sauce Labs Backpack"), Is.True);
            Driver.FindElement(By.Id("remove-sauce-labs-backpack")).Click();
            System.Threading.Thread.Sleep(500); 
            
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(2));

            // Verify checkout prices
            _cartPage.Checkout();
            _checkoutPage.EnterShippingDetails("Complex", "Tester", "90210");
            
            // Step 2 summary
            Assert.That(Driver.Url, Does.Contain("checkout-step-two.html"));
            Assert.That(Driver.PageSource, Does.Contain("Sauce Labs Bike Light"));
            Assert.That(Driver.PageSource, Does.Contain("Sauce Labs Bolt T-Shirt"));
            
            _checkoutPage.FinishCheckout();
            Assert.That(_checkoutPage.GetCompleteMessage(), Is.EqualTo("Thank you for your order!"));

            // Reset and Logout
            _sideMenuPage.OpenMenu();
            _sideMenuPage.ResetAppState();
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(0));
            
            _sideMenuPage.Logout();
            Assert.That(Driver.Url, Is.EqualTo("https://www.saucedemo.com/"));
        }

        [Test]
        public void TestCheckout_ValidationAndCancel()
        {
            _inventoryPage.AddItemToCart("Sauce Labs Onesie");
            _inventoryPage.GoToCart();
            _cartPage.Checkout();

            // Field validation
            Driver.FindElement(By.Id("continue")).Click();
            Assert.That(Driver.PageSource, Does.Contain("Error: First Name is required"));

            Driver.FindElement(By.Id("first-name")).SendKeys("Test");
            Driver.FindElement(By.Id("continue")).Click();
            Assert.That(Driver.PageSource, Does.Contain("Error: Last Name is required"));

            // Cancel checkout
            Driver.FindElement(By.Id("cancel")).Click();
            Assert.That(Driver.Url, Does.Contain("cart.html"));
        }
    }
}
