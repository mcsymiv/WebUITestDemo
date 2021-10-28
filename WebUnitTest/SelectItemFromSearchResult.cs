using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUnitTest
{
    public class SelectItemFromSearchResult
    {

        IWebDriver driver;
        WebDriverWait webDriverWait;
        Actions builder;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.bodo.ua/ua/");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            builder = new Actions(driver);
        }

        public IWebElement FindElement(string Selector, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(By.CssSelector(Selector)));
                // return wait.Until(ElementIsClickable(By.CssSelector(Selector)));
            }
            return driver.FindElement(By.CssSelector(Selector));
        }

        public static Func<IWebDriver, IWebElement> ElementIsClickable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }

        public IWebElement FindElement(By Selector, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(Selector));
            }
            return driver.FindElement(Selector);
        }

        [Test]
        public void TestSite()
        {

            // elements
            IWebElement catalogDropdown = driver.FindElement(By.CssSelector("[id='smartmenu12']"));
            builder
                .MoveToElement(catalogDropdown)
                .Perform();


            IList<IWebElement> categoryDropDownList = driver.FindElements(By.CssSelector("[class=\"sub-menu_item\"] [class=\"js-smartmenu-link-text\"]"));
            IWebElement category = null;

            foreach (IWebElement element in categoryDropDownList) {
                if (element.Text.Equals("Категорії"))
                {
                    category = element;
                }
            }

            builder
                .MoveToElement(category)
                .Perform();

            builder
                .MoveByOffset(1, 1)
                .Perform();

            category.Click();

            IWebElement romanceLink = FindElement(By.LinkText("Романтичні враження"), 3);
            romanceLink.Click();

        }

        [TearDown]
        public void After()
        {
            // driver.Quit();
        }
    }
}
