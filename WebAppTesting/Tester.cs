using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTesting
{
    public class Tester
    {
        public void RunTests()
        {
            List<string> Messages = new List<string>();
            var evolveTests = new EvolveTests();
            evolveTests.RunTests();
            evolveTests.Logout();
            Messages.Add("Evolve Tests");
            Messages.AddRange(evolveTests.Messages);

            var authTests = new AuthorizationsTests();
            TestApp(Messages, authTests);

            foreach (string message in Messages)
            {
                Console.WriteLine(message);
            }
        }

        private static void TestApp(List<string> Messages, WebAppTests authTests)
        {
            authTests.RunTests();
            Messages.Add("Authorizations Tests");
            Messages.AddRange(authTests.Messages);
        }
    }
}
