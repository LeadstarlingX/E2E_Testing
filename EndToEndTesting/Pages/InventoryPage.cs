using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace EndToEndTesting.Pages
{
    public class InventoryPage
    {
        private readonly IWebDriver _driver;

        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement SortDropdown => _driver.FindElement(By.ClassName("product_sort_container"));
        private IReadOnlyCollection<IWebElement> InventoryItems => _driver.FindElements(By.ClassName("inventory_item"));
        private IWebElement CartBadge => _driver.FindElement(By.ClassName("shopping_cart_badge"));
        private IWebElement CartLink => _driver.FindElement(By.ClassName("shopping_cart_link"));

        public void SortBy(string sortOption)
        {
            var selectElement = new SelectElement(SortDropdown);
            selectElement.SelectByText(sortOption);
        }

        public void AddItemToCart(string itemName)
        {
            // Finding layout-safe button ID based on item name convention of sauce demo
            // e.g. "Sauce Labs Backpack" -> "add-to-cart-sauce-labs-backpack"
            string itemId = "add-to-cart-" + itemName.ToLower().Replace(" ", "-");
            _driver.FindElement(By.Id(itemId)).Click();
        }

        public int GetCartItemCount()
        {
            try
            {
                return int.Parse(CartBadge.Text);
            }
            catch (NoSuchElementException)
            {
                return 0;
            }
        }

        public void GoToCart()
        {
            CartLink.Click();
        }

        public bool IsItemDisplayed(string itemName)
        {
            return InventoryItems.Any(item => item.Text.Contains(itemName));
        }
    }
}
