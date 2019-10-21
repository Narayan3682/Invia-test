using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using AventStack.ExtentReports;
using InviaTest.TestScripts;
using NUnit.Framework;

namespace InviaTest
{
    [TestFixture]
    public class InviaTest:BaseTest
    {
        [Description("This test script will serach for best hotel in proved city with dates and sort them with descending price order")]
        [Category("P1"),Category("InviaHotel")]
        [Test, Timeout(2500000)]
        public void ChooseBestHotel()
        {            
            var report = extent.CreateTest("Filter and Choose excellent hotel in madrid", "Filter and Choose excellent hotel in madrid");

            #region Launch Browser

            var driver = new BrowserHandle.BrowserLaunch().Launch();
            report.Log(Status.Pass, "Launched the browser");

            #endregion

            #region Navigate to URL

            var url = "https://www.ab-in-den-urlaub.de/";
            driver.Navigate().GoToUrl(url);
            report.Log(Status.Pass, "Navigated to URL: " + url);

            #endregion

            #region Enter cityName in Hotel Seaction

            driver.FindElement(By.XPath("//label[@class='item-hotel']")).Click();
            report.Log(Status.Pass, "Clicked on hotel city dropdown");
            driver.FindElement(By.XPath("//button[@id='CybotCookiebotDialogBodyButtonAccept']")).Click();
            report.Log(Status.Pass, "Clicked on city search button");
            var city = "Madrid";
            driver.FindElement(By.XPath("//input[@id='base[searchTerm]']")).SendKeys(city);
            report.Log(Status.Pass, "Entered city name");
            driver.FindElement(By.XPath("//span[@class='aiduac-group-title']/parent::li//li")).Click();
            report.Log(Status.Pass, "selected city name from search list");

            #endregion

            #region Enter Check-in and out dates and click search for available hotels

            Actions actions = new Actions(driver);
            driver.FindElement(By.XPath("//form[@id='hotel']//div[@class='datepicker datepicker-two-inputs']/div/div")).Click();
            while (!driver.FindElement(By.XPath("//form[@id='hotel']//td[@data-date='2019-11-25']")).Displayed)
            {
                driver.FindElement(By.XPath("//form[@id='hotel']//span[@class='month-button month-button-next icon-arrow-right-bold']")).Click();
            }
            actions.MoveToElement(driver.FindElement(By.XPath("//form[@id='hotel']//td[@data-date='2019-11-25']"))).Click().Build().Perform();
            report.Log(Status.Pass, "Navigated to specified in date and selected the date");


            //driver.FindElement(By.XPath("//form[@id='hotel']//input[@class='datepicker-input datepicker-input-end']")).Click();
            while (!driver.FindElement(By.XPath("//form[@id='hotel']//div[@class='datepicker-layer end-input']//td[@data-date='2019-11-29']")).Displayed)
            {
                driver.FindElement(By.XPath("//form[@id='hotel']//div[@class='datepicker-layer end-input']//span[@class='month-button month-button-next icon-arrow-right-bold']")).Click();
            }
            driver.FindElement(By.XPath("//form[@id='hotel']//div[@class='datepicker-layer end-input']//td[@data-date='2019-11-29']")).Click();
            report.Log(Status.Pass, "Navigated to specified out date and selected the date");


            driver.FindElement(By.XPath("//form[@id='hotel']//input[@id='submit']")).Click();
            report.Log(Status.Pass, "Click on search for hotel results");

            #endregion

            #region Filter with 5star rating

            Thread.Sleep(3000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var x = wait.Until(y => y.FindElement(By.XPath("//label[@for='filter-hotel-category5']")).Location.X > 1);
            actions = new Actions(driver);
            bool staleElement = true;
            while (staleElement)
            {
                try
                {
                    actions.MoveToElement(driver.FindElement(By.XPath("//label[@for='filter-hotel-category5']"))).Perform();
                    driver.FindElement(By.XPath("//label[@for='filter-hotel-category5']")).Click();
                    report.Log(Status.Pass, "Selected star rating hotel");
                    staleElement = false;

                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }
            }


            #endregion

            #region Filter with excellent Rating

            staleElement = true;
            while (staleElement)
            {
                try
                {
                    actions.MoveToElement(driver.FindElement(By.XPath("//label[@for='filter-hotel-rating5']"))).Build().Perform();
                    driver.FindElement(By.XPath("//label[@for='filter-hotel-rating5']")).Click();
                    report.Log(Status.Pass, "Select excellent rating hotels");
                    staleElement = false;

                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }
            }
            driver.FindElement(By.XPath("//li[@data-criterion='price']")).Click();
            if (driver.FindElement(By.XPath("//li[@data-criterion='price']")).GetAttribute("class").Contains("asc"))
                driver.FindElement(By.XPath("//li[@data-criterion='price']")).Click();
            report.Log(Status.Pass, "sorted the results in descending price order");
            var list = driver.FindElements(By.XPath("//div[@class='js-baseFrame-hotelBoxList']//div[@class='js-baseFrame-skeleton-hotel']"));
            foreach (var item in list)
            {

                report.Log(Status.Pass, "List of results "+item.Text);
            }

            #endregion

            #region CloseBroser

            driver.Close();

            #endregion

        }
    }
}
