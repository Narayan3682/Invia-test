using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InviaTest.BrowserHandle
{
     // This class will contain browser handling finctions
    public class BrowserLaunch
    {
        public static IWebDriver driver;
        public Actions actions;
        public IWebDriver Launch()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            actions = new Actions(driver);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Manage().Window.Maximize();
            return driver;
        }

    }
}
