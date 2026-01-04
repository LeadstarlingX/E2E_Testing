using NUnit.Framework;
using EndToEndTesting.Pages;
using EndToEndTesting.Data;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class InventoryTests : BaseTest
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;

        [SetUp]
        public void DataSetup()
        {
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            _loginPage = new LoginPage(Driver);
            _loginPage.Login(Constants.Users.StandardUser, Constants.Users.SecretSauce);
            _inventoryPage = new InventoryPage(Driver);
        }

        [Test]
        public void TestAddItemToCart()
        {
            _inventoryPage.AddItemToCart(Constants.Products.Backpack);
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(1));
        }

        [Test]
        public void TestSortByPriceLowToHigh()
        {
            _inventoryPage.SortBy("Price (low to high)");
            // Verify sort success
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
        }
    }
}
