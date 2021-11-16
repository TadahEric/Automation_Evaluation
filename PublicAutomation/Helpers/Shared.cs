using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PublicAutomation.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace PublicAutomation.Helpers
{
    [Binding]
    public class Shared
    {
        private readonly TestRunContext _testRunContext;

        //only elements which are shared across multiple pages should live here
        protected By CookieBanner = By.Id("onetrust-accept-btn-handler");
        protected By HammerSpinner = By.Name("hammer-time-down");

        public Shared(TestRunContext testRunContext)
        {
            _testRunContext = testRunContext;
        }

        public void NavigateToUrl(string url)
        {
            _testRunContext.driver.Navigate().GoToUrl(url);
            WaitUntilUrlChanges(url);
        }

        public void WaitUntilUrlChanges(string expectedUrl)
        {
            var wait = new WebDriverWait(_testRunContext.driver, _testRunContext.waitTimer);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(expectedUrl.ToString()));
        }

        public void WaitUntilElementIsVisible(By by)
        {
            var wait = new WebDriverWait(_testRunContext.driver, _testRunContext.waitTimer);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        public void WaitUntilElementExists(By by)
        {
            var wait = new WebDriverWait(_testRunContext.driver, _testRunContext.waitTimer);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }

        public void WaitUntilElementIsClickable(By by)
        {
            var wait = new WebDriverWait(_testRunContext.driver, _testRunContext.waitTimer);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        public void ClickTheElement(By by)
        {
            WaitUntilElementIsClickable(by);
            MoveToElement(by);
            _testRunContext.driver.FindElement(by).Click();
            WaitForHammerSpinner();
        }
        public void ClickTheElementWithoutScrolling(By by)
        {
            WaitUntilElementIsClickable(by);
            _testRunContext.driver.FindElement(by).Click();
            WaitForHammerSpinner();
        }
        public void FillTheTextBox(By by, string text)
        {
            WaitUntilElementIsVisible(by);
            ClickTheElement(by);
            _testRunContext.driver.FindElement(by).Clear();
            _testRunContext.driver.FindElement(by).SendKeys(Keys.Control + "a");
            _testRunContext.driver.FindElement(by).SendKeys(Keys.Delete);
            _testRunContext.driver.FindElement(by).SendKeys(text);
        }

        public void SubmitTheTextBox(By by)
        {
            WaitUntilElementIsVisible(by);
            _testRunContext.driver.FindElement(by).SendKeys(Keys.Enter);
        }

        public void RemoveCookieBanner()
        {
            ClickTheElement(CookieBanner);
        }

        public void MoveToElement(By by) //will move to first element if multiple of same By exist
        {
            var element = _testRunContext.driver.FindElement(by);

            var windowHeight = _testRunContext.driver.Manage().Window.Size.Height - 132;
            var elementHeight = element.Size.Height;

            var elementLocation = element.Location.Y;

            var scrollOffset = (windowHeight + elementHeight) / 2;

            var scrollLocation = elementLocation - scrollOffset;

            ((IJavaScriptExecutor)_testRunContext.driver).ExecuteScript("window.scrollTo(0,arguments[0])", new[] { scrollLocation });


            //replace with interactive scrolling waiter
            Thread.Sleep(500);
        }

        public void WaitForHammerSpinner()
        {
            //wait for the hammer spinner to go away and the page to load

            var counter = 0;
            while (counter < 50)
            {
                if (_testRunContext.driver.FindElements(HammerSpinner).FirstOrDefault() == null)
                {
                    if (counter > 2) //give the spinner 1 second to appear before assuming it's not there
                    {
                        break;
                    }
                }

                Thread.Sleep(500);
                counter++;
            }
        }


        public void CheckPageDoesNotContain404()
        {
            _testRunContext.driver.PageSource.ToString().Should().NotContain("404 error: page not found");
        }

        public void CheckPageHasLoadedWithNoErrors(string expectedUrl)
        {
            WaitUntilUrlChanges(expectedUrl);
            CheckPageDoesNotContain404();
        }

        public void GoBackToPreviousePage()
        {
            _testRunContext.driver.Navigate().Back();
        }

        public void CheckPageContainsText(string expectedText)
        {
            var counter = 0;
            while (counter < 50)
            {
                if (_testRunContext.driver.PageSource.Contains(expectedText))
                {
                    break;
                }

                Thread.Sleep(1000);
                counter++;
            }

        }
    }
}
