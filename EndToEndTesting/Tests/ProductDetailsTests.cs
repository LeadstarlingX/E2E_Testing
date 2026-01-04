using NUnit.Framework;
using EndToEndTesting.Pages;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class ProductDetailsTests : BaseTest
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private ProductDetailsPage _detailsPage;

        [SetUp]
        public void DataSetup()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(Driver);
            _loginPage.Login("standard_user", "secret_sauce");
            _inventoryPage = new InventoryPage(Driver);
            _detailsPage = new ProductDetailsPage(Driver);
        }

        [Test]
        public void TestProductDetailsContent()
        {
            string itemName = "Sauce Labs Backpack";
            _inventoryPage.ClickProductTitle(itemName);
            
            Assert.That(_detailsPage.GetItemName(), Is.EqualTo(itemName));
            Assert.That(_detailsPage.GetItemDescription(), Is.Not.Empty);
            Assert.That(_detailsPage.GetItemPrice(), Is.EqualTo("$29.99"));
        }

        [Test]
        public void TestAddToCartFromDetails()
        {
            _inventoryPage.ClickProductTitle("Sauce Labs Bike Light");
            _detailsPage.AddToCart();
            
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(1));
        }

        [Test]
        public void TestBackToProducts()
        {
            _inventoryPage.ClickProductTitle("Sauce Labs Bolt T-Shirt");
            _detailsPage.BackToProducts();
            
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
        }
    }
}
