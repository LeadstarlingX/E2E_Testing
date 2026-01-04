using NUnit.Framework;
using EndToEndTesting.Pages;
using OpenQA.Selenium;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class SideMenuTests : BaseTest
    {
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private SideMenuPage _sideMenuPage;

        [SetUp]
        public void DataSetup()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(Driver);
            _loginPage.Login("standard_user", "secret_sauce");
            _inventoryPage = new InventoryPage(Driver);
            _sideMenuPage = new SideMenuPage(Driver);
        }

        [Test]
        public void TestLogout()
        {
            _sideMenuPage.OpenMenu();
            _sideMenuPage.Logout();
            
            Assert.That(Driver.Url, Is.EqualTo("https://www.saucedemo.com/"));
            Assert.That(Driver.FindElements(By.Id("login-button")).Count, Is.GreaterThan(0));
        }

        [Test]
        public void TestResetAppState()
        {
            // Add an item to verify reset
            _inventoryPage.AddItemToCart("Sauce Labs Backpack");
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(1));
            
            _sideMenuPage.OpenMenu();
            _sideMenuPage.ResetAppState();
            
            // Verify cart is empty after reset
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(0));
        }
    }
}
