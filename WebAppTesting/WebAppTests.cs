using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAppTesting
{
    public abstract class WebAppTests
    {
        public static IWebDriver driver = new ChromeDriver();
        public string StartingUrl;
        public string AppName { get; set; }
        private List<string> _messages;

        public WebAppTests(string startingUrl)
        {
            StartingUrl = startingUrl;
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

        protected List<string> GetMyFacilities()
        {
            List<string> facilities = new List<string>();

            var facilityDropDownLink = driver.FindElement(By.ClassName("facility-dropdown"));
            foreach (IWebElement element in facilityDropDownLink.FindElements(By.TagName("li")))
            {
                facilities.Add(element.FindElement(By.TagName("a")).GetAttribute("href"));
            }
            return facilities;
        }

        public void TestMyFacilities()
        {
            List<string> facilitiesLink = GetMyFacilities();

            foreach (var facilityElement in facilitiesLink)
            {
                driver.Navigate().GoToUrl(facilityElement);

                // Not finding the "Oops" in failed facilities.However, Length == 0, so we'll use that
                var h1Text = driver.FindElement(By.TagName("h1")).Text;
                if (h1Text.Length == 0 || h1Text.Contains("Oops"))
                {
                    AddError("TestFacilities", "Link [" + facilityElement + "] failed.");
                }
            }
        }

    }
}
