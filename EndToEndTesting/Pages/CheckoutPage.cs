using OpenQA.Selenium;

namespace EndToEndTesting.Pages
{
    public class CheckoutPage
    {
        private readonly IWebDriver _driver;

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement FirstNameField => _driver.FindElement(By.Id("first-name"));
        private IWebElement LastNameField => _driver.FindElement(By.Id("last-name"));
        private IWebElement PostalCodeField => _driver.FindElement(By.Id("postal-code"));
        private IWebElement ContinueButton => _driver.FindElement(By.Id("continue"));
        private IWebElement FinishButton => _driver.FindElement(By.Id("finish"));
        private IWebElement CompleteHeader => _driver.FindElement(By.ClassName("complete-header"));
        private IWebElement BackHomeButton => _driver.FindElement(By.Id("back-to-products"));

        public void EnterShippingDetails(string firstName, string lastName, string zip)
        {
            FirstNameField.SendKeys(firstName);
            LastNameField.SendKeys(lastName);
            PostalCodeField.SendKeys(zip);
            ContinueButton.Click();
        }

        public void FinishCheckout()
        {
            FinishButton.Click();
        }

        public string GetCompleteMessage()
        {
            return CompleteHeader.Text;
        }

        public void BackHome()
        {
            BackHomeButton.Click();
        }
    }
}
