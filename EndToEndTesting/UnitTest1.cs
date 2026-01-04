using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.Intrinsics.X86;

namespace EndToEndTesting
{
    public class LegacyTests
    {
        

        [SetUp]
        public void Setup()
        {
           
        }

        //[Test]
        //public void Test1()
        //{
        //    var driver = new ChromeDriver();
        //    driver.Navigate().GoToUrl("https://www.saucedemo.com");



        //    var options = new ChromeOptions();

        //    options.AddExcludedArgument("enable-automation");
        //    options.AddAdditionalChromeOption("useAutomationExtension", false);
        //    options.AddArgument("--disable-blink-features=AutomationControlled");
        //    options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");

        //    driver = new ChromeDriver(options);

        //    IWebElement searchBox = driver.FindElement(By.Name("q"));

        //    searchBox.SendKeys("Youtube");
        //    searchBox.Submit();

        //    Assert.That(driver.Title, Does.Contain("Testing"));
        //}

        [Test]
        public void Standatd_User_Test()
        {
            var driver = new ChromeDriver();
            

            var timeToWait = 500;

            var options = new ChromeOptions();

            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalChromeOption("useAutomationExtension", false);
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");

            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("https://www.saucedemo.com");

            IWebElement usernameBox = driver.FindElement(By.Id("user-name"));
            usernameBox.SendKeys("standard_user");
            IWebElement passwordBox = driver.FindElement(By.Id("password"));
            passwordBox.SendKeys("secret_sauce");

            IWebElement submitButton = driver.FindElement(By.Id("login-button"));
            submitButton.Submit();

            System.Threading.Thread.Sleep(timeToWait);

            IWebElement sauceLabsBackpack_AddToCart = driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            sauceLabsBackpack_AddToCart.Click();

            System.Threading.Thread.Sleep(timeToWait);

            IWebElement cartContainer_Button = driver.FindElement(By.Id("shopping_cart_container"));
            IWebElement cartContainer_Badge = driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.That(cartContainer_Badge.Text, Is.EqualTo("1"));

            cartContainer_Button.Click();

            System.Threading.Thread.Sleep(timeToWait);

            IWebElement checkout_Button = driver.FindElement(By.Id("checkout"));
            checkout_Button.Click();

            System.Threading.Thread.Sleep(timeToWait);

            IWebElement firstName_Box = driver.FindElement(By.Id("first-name"));
            firstName_Box.SendKeys("John");
            IWebElement lastName_Box = driver.FindElement(By.Id("last-name"));
            lastName_Box.SendKeys("Doe");
            IWebElement postalCode_Box = driver.FindElement(By.Id("postal-code"));
            postalCode_Box.SendKeys("12345");

            IWebElement continue_Button = driver.FindElement(By.Id("continue"));
            continue_Button.Click();

            System.Threading.Thread.Sleep(timeToWait);

            IWebElement finish_Button = driver.FindElement(By.Id("finish"));
            finish_Button.Click();

            System.Threading.Thread.Sleep(timeToWait);

            IWebElement thankYou_Message = driver.FindElement(By.Id("checkout_complete_container"));


            Assert.That(thankYou_Message.Displayed);

        }

    }
}
