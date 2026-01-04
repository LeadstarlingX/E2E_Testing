using OpenQA.Selenium;

namespace EndToEndTesting.Pages
{
    public class CheckoutPage : BasePage
    {
        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement FirstNameField => Driver.FindElement(By.Id("first-name"));
        private IWebElement LastNameField => Driver.FindElement(By.Id("last-name"));
        private IWebElement PostalCodeField => Driver.FindElement(By.Id("postal-code"));
        private IWebElement ContinueButton => Driver.FindElement(By.Id("continue"));
        private IWebElement FinishButton => Driver.FindElement(By.Id("finish"));
        private IWebElement CompleteHeader => Driver.FindElement(By.ClassName("complete-header"));
        private IWebElement BackHomeButton => Driver.FindElement(By.Id("back-to-products"));

        public void EnterShippingDetails(string firstName, string lastName, string zip)
        {
            FirstNameField.SendKeys(firstName);
            Wait();
            LastNameField.SendKeys(lastName);
            Wait();
            PostalCodeField.SendKeys(zip);
            Wait();
            ContinueButton.Click();
            Wait();
        }

        public void FinishCheckout()
        {
            FinishButton.Click();
            Wait();
        }

        public string GetCompleteMessage()
        {
            return CompleteHeader.Text;
        }

        public void BackHome()
        {
            BackHomeButton.Click();
            Wait();
        }
    }
}
