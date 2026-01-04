using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using EndToEndTesting.Data;

namespace EndToEndTesting.Tests
{
    public class BaseTest
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalChromeOption("useAutomationExtension", false);
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
            options.AddUserProfilePreference("safebrowsing.enabled", true); 
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-notifications");


            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.ImplicitWaitTimeout);
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Teardown()
        {
            try 
            {
                Driver?.Quit();
                Driver?.Dispose();
            }
            catch (Exception)
            {
                // Ignore teardown errors
            }
        }
    }
}
