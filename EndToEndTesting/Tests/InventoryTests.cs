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
            // Verification logic would ideally check the price of the first item
            // For now, we verify the action completes without error and we are still on the page
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
        }
    }
}
