using OpenQA.Selenium;

namespace EndToEndTesting.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement UsernameField => Driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => Driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => Driver.FindElement(By.Id("login-button"));
        private IWebElement ErrorMessage => Driver.FindElement(By.ClassName("error-message-container"));

        public void EnterUsername(string username)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);
            Wait();
        }

        public void EnterPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            Wait();
        }

        public void ClickLogin()
        {
            LoginButton.Click();
            Wait();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public string GetErrorMessage()
        {
            return ErrorMessage.Text;
        }
    }
}
