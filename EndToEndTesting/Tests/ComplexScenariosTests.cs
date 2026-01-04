using NUnit.Framework;
using EndToEndTesting.Pages;
using OpenQA.Selenium;
using EndToEndTesting.Data;

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
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            _loginPage = new LoginPage(Driver);
            _inventoryPage = new InventoryPage(Driver);
            _detailsPage = new ProductDetailsPage(Driver);
            _cartPage = new CartPage(Driver);
            _checkoutPage = new CheckoutPage(Driver);
            _sideMenuPage = new SideMenuPage(Driver);
            
            _loginPage.Login(Constants.Users.StandardUser, Constants.Users.SecretSauce);
        }

        [Test]
        public void TestFullLifecycle_MultiItemManagement()
        {
            // Add items from inventory
            _inventoryPage.AddItemToCart(Constants.Products.Backpack);
            _inventoryPage.AddItemToCart(Constants.Products.BikeLight);
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(2));

            // Add item from details
            _inventoryPage.ClickProductTitle(Constants.Products.BoltTShirt);
            _detailsPage.AddToCart();
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(3));

            // Remove item from cart
            _inventoryPage.GoToCart();
            Assert.That(_cartPage.HasItem(Constants.Products.Backpack), Is.True);
            Driver.FindElement(By.Id("remove-sauce-labs-backpack")).Click();
            System.Threading.Thread.Sleep(500); 
            
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(2));

            // Verify checkout prices
            _cartPage.Checkout();
            _checkoutPage.EnterShippingDetails("Complex", "Tester", "90210");
            
            // Step 2 summary
            Assert.That(Driver.Url, Does.Contain("checkout-step-two.html"));
            Assert.That(Driver.PageSource, Does.Contain(Constants.Products.BikeLight));
            Assert.That(Driver.PageSource, Does.Contain(Constants.Products.BoltTShirt));
            
            _checkoutPage.FinishCheckout();
            Assert.That(_checkoutPage.GetCompleteMessage(), Is.EqualTo("Thank you for your order!"));

            // Reset and Logout
            _sideMenuPage.OpenMenu();
            _sideMenuPage.ResetAppState();
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(0));
            
            _sideMenuPage.Logout();
            Assert.That(Driver.Url, Is.EqualTo(Constants.BaseUrl));
        }

        [Test]
        public void TestCheckout_ValidationAndCancel()
        {
            _inventoryPage.AddItemToCart(Constants.Products.Onesie);
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
