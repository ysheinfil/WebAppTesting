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
    class AuthorizationsTests : WebAppTests
    {
        public AuthorizationsTests():base("https://authorizations.cbotracking.com/")
        {
            AppName = "Authorizations";
            Login();
        }

        public override void RunTests()
        {
            TestMyFacilities();
        }
    }
}
