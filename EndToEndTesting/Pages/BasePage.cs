using EndToEndTesting.Data;
using OpenQA.Selenium;

namespace EndToEndTesting.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected int InteractionDelay = Constants.DefaultInteractionDelay;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected void Wait()
        {
            System.Threading.Thread.Sleep(InteractionDelay);
        }
    }
}
