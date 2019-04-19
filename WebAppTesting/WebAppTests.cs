using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAppTesting
{
    public abstract class WebAppTests
    {
        public static IWebDriver driver;
        public string StartingUrl;
        public string AppName { get; set; }
        private List<string> _messages;

        public WebAppTests(string startingUrl)
        {
            StartingUrl = startingUrl;
            var ShowBrowser = ConfigurationManager.AppSettings["ShowBrowser"];
            if (ShowBrowser.ToUpper().Equals("YES") || ShowBrowser.ToUpper().Equals("TRUE"))
            {                
                driver = new ChromeDriver();
            }
            else
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--headless");
                driver = new ChromeDriver(options);
            }
        }


        public void Login()
        {
            _messages = new List<string>();
            driver.Navigate().GoToUrl(StartingUrl);
            driver.FindElement(By.Id("username")).SendKeys("ysheinfil");
            driver.FindElement(By.Id("password")).SendKeys("4770WPr!");
            driver.FindElement(By.ClassName("btn-primary")).Click();

        }

        public void Logout()
        {
            driver.Navigate().GoToUrl(StartingUrl + "users/logout");
        }

        public IReadOnlyList<string> Messages => _messages;

        public abstract void RunTests();

        /// <summary>
        /// Add to the Errors list
        /// </summary>
        /// <param name="testName">Name of the test</param>
        /// <param name="failure">Description of the failure</param>
        /// <returns></returns>
        public void AddError(string testName, string failure)
        {
            _messages.Add(testName + ": " + failure);
        }

        protected Dictionary<string,string> GetMyFacilities()
        {
            Dictionary<string, string> facilitiesLinks = new Dictionary<string, string>();

            var facilityDropDownLink = driver.FindElement(By.ClassName("facility-dropdown"));
            foreach (IWebElement element in facilityDropDownLink.FindElements(By.TagName("li")))
            {
                var aLink = element.FindElement(By.TagName("a"));
                var fac = aLink.GetAttribute("innerText");
                facilitiesLinks.Add(aLink.GetAttribute("href"), RemoveEndingNumber(fac));
            }
            return facilitiesLinks;
        }

        public static string RemoveEndingNumber(string fac)
        {
            string retVal;
            if (Regex.IsMatch(fac, @"\d+$"))
            {
                retVal = fac.Remove(fac.LastIndexOf(' '));
            }
            else
                retVal = fac;
            return retVal;
        }

        public void TestMyFacilities()
        {
            Dictionary<string, string> facilitiesLink = GetMyFacilities();
            Console.Write(AppName);
            foreach (var facilityElement in facilitiesLink)
            {
                driver.Navigate().GoToUrl(facilityElement.Key);
                Console.Write(".");
                // Not finding the "Oops" in failed facilities.However, Length == 0, so we'll use that
                var h1Text = driver.FindElement(By.TagName("h1")).Text;
                if (h1Text.Length == 0 || h1Text.Contains("Oops"))
                {
                    AddError(AppName + " TestFacilities", "Facility " + facilityElement.Value + " failed.");
                }
            }
        }

    }
}
