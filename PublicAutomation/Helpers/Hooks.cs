using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PublicAutomation.Contexts;
using PublicAutomation.TestData;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace PublicAutomation.Helpers
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestRunContext _testRunContext;
        private readonly Shared _shared;

        public Hooks(ScenarioContext scenarioContext, TestRunContext testRunContext, Shared shared)
        {
            _scenarioContext = scenarioContext;
            _testRunContext = testRunContext;
            _shared = shared;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new ChromeOptions();

            options.AddArguments(
               "--no-sandbox",
               "--verbose",
               //"--headless",
               "--disable-gpu",
               "--disable-web-security",
               "--disable-infobars",
               "--disable-dev-shm-usage",
               "--disable-browser-side-navigation",
               "--disable-popup-blocking",
               "--disable-extensions",
               "--ignore-certificate-errors",
               "--ignore-ssl-errors",
               "--disable-features=VizDisplayCompositor",
               "--disable-background-networking",
               "--dns-prefetch-disable",
               "--ignore-certificate-errors",
               "--allow-running-insecure-content",
               "--allow-insecure-localhost"
              );

            options.AddUserProfilePreference("geolocation", true);

            
            try
            {
                _testRunContext.driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options, _testRunContext.waitTimer);
            }
            catch
            {
                _testRunContext.driver = new ChromeDriver(options);
            }

            _testRunContext.driver.Manage().Window.Size = new Size(1920, 1080);
            _testRunContext.driver.Manage().Timeouts().PageLoad = _testRunContext.waitTimer;


            //initial navigation, then clear cookie banner.
            _shared.NavigateToUrl(TestUrls.EnvironmentUrl);
            _shared.RemoveCookieBanner();
        }



        [AfterScenario]
        public void AfterScenario()
        { 
            _testRunContext.driver.Close();
            _testRunContext.driver.Quit();
        }
    }
}
