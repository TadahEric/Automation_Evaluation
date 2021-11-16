using FluentAssertions;
using OpenQA.Selenium;
using PublicAutomation.Contexts;
using PublicAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;

namespace PublicAutomation.Pages
{
    [Binding]
    class SearchPage
    {
        private readonly TestRunContext _testRunContext;
        private readonly Shared _shared;

        protected By Contacts = By.CssSelector("[data-guid='CompanyHeroInfo']");
        protected By SendMessage = By.Name("send-message");
        protected By CompanyHeroName = By.CssSelector("[data-guid='CompanyHeroName']");
        protected By TradeCard = By.CssSelector("[data-guid='TradeCard']"); //not unique
        protected By TradeCardName = By.CssSelector("[data-guid='TradeCardName']"); //not unique
        protected By TradeCardsMiles = By.CssSelector(".sc-5djnoi-4.foygis");
        protected By TradeCardRating = By.CssSelector(".sc-5djnoi-9.LWwpZ");
        protected By TradeCardFeed = By.CssSelector(".sc-5djnoi-10.hBWoaN");
        protected By ContactName = By.CssSelector("[data-guid='CompanyHeroContactName']");
        protected By Location = By.CssSelector("[data-guid='CompanyHeroLocation']");
        protected By ShortListAdd = By.CssSelector("[data-guid='CompanyHeroShortlist']");
        protected By ShortListTopNav = By.CssSelector("[data-guid='TopNavShortlist']");
        protected By ShortListRemove = By.Name("remove"); //not unique if multiple shortlist cards exist
        protected By ReviewsPanel = By.CssSelector("[data-guid='Reviews']");
        protected By Miles = By.CssSelector("[data-guid='TradeCardLocation']");
        protected By BreadCrumbs = By.CssSelector("[href*='ictd[master]=vid']");
        protected By CrumbActive = By.CssSelector("[data-guid='SearchTitle']");
        protected By SearchSortBy = By.CssSelector("[data-guid='SearchSortBy']");
        protected By SearchSortFilters = By.CssSelector(".sc-6143n9-1-div.sc-1642ohb-1.dctWWa.sc-1txq46s-2.sc-1txq46s-4.hwpqSv");
        protected By VettingPanel = By.CssSelector("[data-guid='Verification']");
        protected By SaveTrade = By.XPath("//*[@id='__next']/div/div[2]/div/div[2]/div/button[2]");
        protected By GotIt = By.XPath("//*[@id='search-dock']/div[2]/div/div/div[2]/div/button");

        protected By HammerDown = By.Name("hammer-time-down");
        protected By HammerUp = By.Name("hammer-time-up");

        protected By PartialTradeLinks = By.XPath("//a[contains(@href, '/trades/')]");

        public SearchPage(TestRunContext testRunContext, Shared shared)
        {
            _testRunContext = testRunContext;
            _shared = shared;
        }

        public void WaitForHammerUpDownSpinner()
        {
            //this is largely a copy of WaitForHammerSpinner but specialised for the search page.
            //may be worth combining/refactoring later.
            var counter = 0;
            while (counter < 100)
            {
                if (_testRunContext.driver.FindElements(HammerUp).FirstOrDefault() == null)
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

        public void ViewTheFirstSearchResult()
        {
            WaitForHammerUpDownSpinner();

            _shared.ClickTheElement(TradeCardName);
        }

        public void SortSearchResultsBy(string sortBy)
        {
            _shared.ClickTheElement(SearchSortBy);
            var filters = _testRunContext.driver.FindElements(SearchSortFilters);

            foreach(var filter in filters)
            {
                if (filter.Text.Contains(sortBy))
                {
                    filter.Click();
                    break;
                }
            }
        }

        public void CheckSearchResultsAreInCorrectOrder(string sortBy)
        {
            _shared.WaitUntilElementIsVisible(TradeCard);

            switch (sortBy)
            {
                case "defaultSort":
                    {
                        //is there any kind of validation we can do on the default sort?
                        break;
                    }

                case "Nearest Me":
                    {
                        var tradeCards = _testRunContext.driver.FindElements(TradeCardsMiles);

                        List<double> ratings = new List<double>();

                        foreach (var tradeCard in tradeCards)
                        {
                            ratings.Add(double.Parse(Regex.Match(tradeCard.Text, @"\d+\.?d*").ToString()));
                        }

                        ratings.Should().BeInAscendingOrder("the trade cards should have been sorted by distance from search location");

                        break;
                    }

                case "Most Feedback":
                    {
                        var tradeCards = _testRunContext.driver.FindElements(TradeCardFeed);

                        List<double> ratings = new List<double>();

                        foreach (var tradeCard in tradeCards)
                        {
                            ratings.Add(double.Parse(tradeCard.Text.Replace("(", "").Replace("Reviews)", "")));
                        }

                        ratings.Should().BeInDescendingOrder("the trade cards should have been sorted by the number of reviews");

                        break;
                    }

                case "Highest Rated":
                    {
                        var tradeCards = _testRunContext.driver.FindElements(TradeCardRating);

                        List<double> ratings = new List<double>();

                        foreach (var tradeCard in tradeCards)
                        {
                            ratings.Add(double.Parse(tradeCard.Text.Replace("(", "").Replace("Reviews)", "")));
                        }

                        ratings.Should().BeInAscendingOrder("the trade cards should have been sorted by their rating");

                        break;
                    }
            }
        }
    }
}
