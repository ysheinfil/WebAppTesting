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

            TestApp(Messages, new EvolveTests());
            TestApp(Messages, new AuthorizationsTests());


            Console.WriteLine();
            foreach (string message in Messages)
            {
                Console.WriteLine(message);
            }
        }

        private static void TestApp(List<string> Messages, WebAppTests appTests)
        {
            Console.WriteLine();
            try
            {
                appTests.RunTests();
                appTests.Logout();
                Messages.AddRange(appTests.Messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while running tests.");
                Console.WriteLine(ex);
            }
        }

    }
}
