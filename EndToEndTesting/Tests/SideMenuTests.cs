using NUnit.Framework;
using EndToEndTesting.Pages;
using OpenQA.Selenium;
using EndToEndTesting.Data;

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
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            _loginPage = new LoginPage(Driver);
            _loginPage.Login(Constants.Users.StandardUser, Constants.Users.SecretSauce);
            _inventoryPage = new InventoryPage(Driver);
            _sideMenuPage = new SideMenuPage(Driver);
        }

        [Test]
        public void TestLogout()
        {
            _sideMenuPage.OpenMenu();
            _sideMenuPage.Logout();
            
            Assert.That(Driver.Url, Is.EqualTo(Constants.BaseUrl));
            Assert.That(Driver.FindElements(By.Id("login-button")).Count, Is.GreaterThan(0));
        }

        [Test]
        public void TestResetAppState()
        {
            _inventoryPage.AddItemToCart(Constants.Products.Backpack);
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(1));
            
            _sideMenuPage.OpenMenu();
            _sideMenuPage.ResetAppState();
            
            // Verify reset success
            Assert.That(_inventoryPage.GetCartItemCount(), Is.EqualTo(0));
        }
    }
}
