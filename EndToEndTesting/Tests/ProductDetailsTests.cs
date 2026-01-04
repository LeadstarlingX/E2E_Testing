using NUnit.Framework;
using EndToEndTesting.Pages;
using EndToEndTesting.Data;

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
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            _loginPage = new LoginPage(Driver);
            _loginPage.Login(Constants.Users.StandardUser, Constants.Users.SecretSauce);
            _inventoryPage = new InventoryPage(Driver);
            _detailsPage = new ProductDetailsPage(Driver);
        }

        [Test]
        public void TestProductDetailsContent()
        {
            string itemName = Constants.Products.Backpack;
            _inventoryPage.ClickProductTitle(itemName);
            
            Assert.That(_detailsPage.GetItemName(), Is.EqualTo(itemName));
            Assert.That(_detailsPage.GetItemDescription(), Is.Not.Empty);
            Assert.That(_detailsPage.GetItemPrice(), Is.EqualTo("$" + Constants.Prices.Backpack.ToString("F2")));
        }

        [Test]
        public void TestAddToCartFromDetails()
        {
            _inventoryPage.ClickProductTitle(Constants.Products.BikeLight);
            _detailsPage.AddToCart();
            
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(1));
        }

        [Test]
        public void TestBackToProducts()
        {
            _inventoryPage.ClickProductTitle(Constants.Products.BoltTShirt);
            _detailsPage.BackToProducts();
            
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
        }
    }
}
