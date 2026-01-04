using OpenQA.Selenium;

namespace EndToEndTesting.Pages
{
    public class ProductDetailsPage : BasePage
    {
        public ProductDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement ItemName => Driver.FindElement(By.ClassName("inventory_details_name"));
        private IWebElement ItemDescription => Driver.FindElement(By.ClassName("inventory_details_desc"));
        private IWebElement ItemPrice => Driver.FindElement(By.ClassName("inventory_details_price"));
        private IWebElement AddToCartButton => Driver.FindElement(By.Id("add-to-cart"));
        private IWebElement RemoveButton => Driver.FindElement(By.Id("remove"));
        private IWebElement BackToProductsButton => Driver.FindElement(By.Id("back-to-products"));

        public string GetItemName() => ItemName.Text;
        public string GetItemDescription() => ItemDescription.Text;
        public string GetItemPrice() => ItemPrice.Text;

        public void AddToCart()
        {
            AddToCartButton.Click();
            Wait();
        }

        public void RemoveFromCart()
        {
            RemoveButton.Click();
            Wait();
        }

        public void BackToProducts()
        {
            BackToProductsButton.Click();
            Wait();
        }
    }
}
