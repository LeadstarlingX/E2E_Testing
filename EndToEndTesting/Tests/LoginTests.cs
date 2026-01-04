using NUnit.Framework;
using EndToEndTesting.Pages;
using EndToEndTesting.Data;

namespace EndToEndTesting.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        [Test]
        public void TestLogin_StandardUser_Success()
        {
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            var loginPage = new LoginPage(Driver);
            loginPage.Login(Constants.Users.StandardUser, Constants.Users.SecretSauce);
            
            // Verify login success
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
        }

        [Test]
        public void TestLogin_LockedOutUser_Failure()
        {
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            var loginPage = new LoginPage(Driver);
            loginPage.Login(Constants.Users.LockedOutUser, Constants.Users.SecretSauce);
            
            Assert.That(loginPage.GetErrorMessage(), Does.Contain("locked out"));
        }
    }
}
