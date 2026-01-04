using OpenQA.Selenium;
using System.Linq;

namespace EndToEndTesting.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement CheckoutButton => _driver.FindElement(By.Id("checkout"));
        private IWebElement RemoveButton => _driver.FindElement(By.Name("remove-sauce-labs-backpack"));
        private IWebElement CartList => _driver.FindElement(By.ClassName("cart_list"));

        public void Checkout()
        {
            CheckoutButton.Click();
        }

        public bool HasItem(string itemName)
        {
            return _driver.PageSource.Contains(itemName);
        }
    }
}
