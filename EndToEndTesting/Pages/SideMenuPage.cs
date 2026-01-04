using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace EndToEndTesting.Pages
{
    public class SideMenuPage : BasePage
    {
        private readonly WebDriverWait _wait;

        public SideMenuPage(IWebDriver driver) : base(driver)
        {
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement BurgerMenuButton => Driver.FindElement(By.Id("react-burger-menu-btn"));
        private IWebElement LogoutLink => Driver.FindElement(By.Id("logout_sidebar_link"));
        private IWebElement ResetLink => Driver.FindElement(By.Id("reset_sidebar_link"));
        private IWebElement AllItemsLink => Driver.FindElement(By.Id("inventory_sidebar_link"));
        private IWebElement CloseMenuButton => Driver.FindElement(By.Id("react-burger-menu-cross-btn"));

        public void OpenMenu()
        {
            BurgerMenuButton.Click();
            // Wait for menu visibility
            _wait.Until(d => LogoutLink.Displayed);
            Wait();
        }

        public void Logout()
        {
            LogoutLink.Click();
            Wait();
        }

        public void ResetAppState()
        {
            ResetLink.Click();
            Wait();
        }

        public void CloseMenu()
        {
            CloseMenuButton.Click();
            _wait.Until(d => !LogoutLink.Displayed);
            Wait();
        }

        public void GoToAllItems()
        {
            AllItemsLink.Click();
            Wait();
        }
    }
}
