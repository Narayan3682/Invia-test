using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InviaTest.TestScripts
{
    [SetUpFixture]
    public class BaseTest
    {
        public static ExtentReports extent;
        public static ExtentHtmlReporter htmlReporter;
        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {  
            string fileName = Assembly.GetExecutingAssembly().Location.ToString() + "../../Report" + DateTime.Now.ToString("HH_ss_MM") + ".html";

            htmlReporter = new ExtentHtmlReporter(fileName);
            htmlReporter.Configuration().Theme = Theme.Dark;
            htmlReporter.Configuration().DocumentTitle = "InviaTest";
            htmlReporter.Configuration().ReportName = "InviaTest";

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {

            extent.Flush();
        }
    }
}
