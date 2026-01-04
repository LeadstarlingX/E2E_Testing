using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace EndToEndTesting.Pages
{
    public class CartPage : BasePage
    {
        private readonly WebDriverWait _wait;

        public CartPage(IWebDriver driver) : base(driver)
        {
            _wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(10));
        }

        private IWebElement CheckoutButton => Driver.FindElement(By.Id("checkout"));
        private IWebElement RemoveButton => Driver.FindElement(By.Name("remove-sauce-labs-backpack"));
        private IWebElement CartList => Driver.FindElement(By.ClassName("cart_list"));

        public void Checkout()
        {
            _wait.Until(d => d.FindElement(By.Id("checkout")).Displayed && d.FindElement(By.Id("checkout")).Enabled);
            CheckoutButton.Click();
            Wait();
        }

        public bool HasItem(string itemName)
        {
            return Driver.PageSource.Contains(itemName);
        }
    }
}
