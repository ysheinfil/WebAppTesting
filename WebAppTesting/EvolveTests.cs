using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAppTesting
{
    public class EvolveTests:WebAppTests
    {
        public EvolveTests(): base("https://evolve.cbotracking.com/")
        {
            AppName = "Evolve";
            Login();
        }
    
        public bool GotoTestFacility()
        {
            driver.Navigate().GoToUrl(StartingUrl + "authorizations/1/index");
            IWebElement element = driver.FindElement(By.TagName("h1"));
            if(!element.Text.StartsWith("Test"))
            {
                AddError("Go to Test Facility", "Not there.");
                return false;
            }
            return true;
        }

        public bool GoToFirstRecord()
        {
            if(!Regex.IsMatch(driver.Url, @"authorizations\/\d\/index"))
            {
                GotoTestFacility();
            }

            var row = driver.FindElements(By.TagName("tr"))[1];
            var lastName = row.FindElement(By.ClassName("last-name")).Text;
            var firstName = row.FindElement(By.ClassName("first-name")).Text;
            row.Click();

            var sectionTitle = driver.FindElement(By.TagName("h1")).Text;
            var nameOnPage = sectionTitle.Substring(sectionTitle.IndexOf('-') + 1);
            if (nameOnPage.ToLower().EndsWith((lastName + ", " + firstName).ToLower()))
            {

                return true;
            }
            else
            {
                string details = "Name in title " + nameOnPage + " doesn't match what was on the list " + " " + lastName;
                AddError("Go to First Record", details);
            }
            return false;
        }

        public bool GotoAddTestRecord()
        {
            var testFacilityPrefix = StartingUrl + "authorizations/1/";
            driver.Navigate().GoToUrl(testFacilityPrefix + "index");
            driver.Navigate().GoToUrl(testFacilityPrefix + "new");

            var sectionTitle = driver.FindElement(By.TagName("h1")).Text;
            if (sectionTitle.EndsWith("New Client"))
                return true;
            else
                return false;
        }

        public void AddTestRecord()
        {
            GotoAddTestRecord();
            driver.FindElement(By.Id("last_name")).SendKeys("Bendrix");
            driver.FindElement(By.Id("first_name")).SendKeys("Henry");
            driver.FindElement(By.Id("admission_date")).SendKeys("1/1/2019");
            driver.FindElement(By.Id("last_authorized_date")).SendKeys("4/1/2019");
            var levelSection = driver.FindElement(By.Id("level"));
            levelSection.FindElement(By.TagName("b")).Click();
        }

        public override void RunTests()
        {
            AddMessage("Starting Evolve tests.");
            TestMyFacilities();
            AddMessage("Evolve tests done.");
        }
    }
}
