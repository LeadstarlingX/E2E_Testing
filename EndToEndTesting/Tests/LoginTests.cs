using NUnit.Framework;
using EndToEndTesting.Pages;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        [Test]
        public void TestLogin_StandardUser_Success()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(Driver);
            loginPage.Login("standard_user", "secret_sauce");
            
            // Verify login success
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
        }

        [Test]
        public void TestLogin_LockedOutUser_Failure()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(Driver);
            loginPage.Login("locked_out_user", "secret_sauce");
            
            Assert.That(loginPage.GetErrorMessage(), Does.Contain("locked out"));
        }
    }
}
