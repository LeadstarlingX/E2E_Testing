using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace EndToEndTesting.Pages
{
    public class InventoryPage : BasePage
    {
        private readonly WebDriverWait _wait;

        public InventoryPage(IWebDriver driver) : base(driver)
        {
            _wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(10));
        }

        private IWebElement SortDropdown => Driver.FindElement(By.ClassName("product_sort_container"));
        private IReadOnlyCollection<IWebElement> InventoryItems => Driver.FindElements(By.ClassName("inventory_item"));
        private IReadOnlyCollection<IWebElement> InventoryImages => Driver.FindElements(By.CssSelector(".inventory_item_img img"));
        private IWebElement CartBadge => Driver.FindElement(By.ClassName("shopping_cart_badge"));
        private IWebElement CartLink => Driver.FindElement(By.ClassName("shopping_cart_link"));

        public void SortBy(string sortOption)
        {
            var selectElement = new SelectElement(SortDropdown);
            selectElement.SelectByText(sortOption);
            Wait();
        }

        public void AddItemToCart(string itemName)
        {
            // Find button by item name convention (e.g. "Sauce Labs Backpack" -> "add-to-cart-sauce-labs-backpack")
            string itemId = "add-to-cart-" + itemName.ToLower().Replace(" ", "-");
            Driver.FindElement(By.Id(itemId)).Click();
            Wait();
            
            // Wait for badge update
            _wait.Until(d => d.FindElements(By.ClassName("shopping_cart_badge")).Count > 0);
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
            Wait();
            _wait.Until(d => d.Url.Contains("cart.html"));
        }

        public bool IsItemDisplayed(string itemName)
        {
            return InventoryItems.Any(item => item.Text.Contains(itemName));
        }

        public void ClickProductTitle(string itemName)
        {
            // Click item name link
            var item = InventoryItems.First(i => i.Text.Contains(itemName));
            item.FindElement(By.ClassName("inventory_item_name")).Click();
            Wait();
        }

        public System.Collections.Generic.IEnumerable<string> GetInventoryImageSources()
        {
            return InventoryImages.Select(img => img.GetAttribute("src"));
        }
    }
}
