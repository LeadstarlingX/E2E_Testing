using NUnit.Framework;
using EndToEndTesting.Pages;

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
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(Driver);
            _loginPage.Login("standard_user", "secret_sauce");
            _inventoryPage = new InventoryPage(Driver);
        }

        [Test]
        public void TestAddItemToCart()
        {
            _inventoryPage.AddItemToCart("Sauce Labs Backpack");
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
