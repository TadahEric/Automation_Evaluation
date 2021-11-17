using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using PublicAutomation.Contexts;
using PublicAutomation.Helpers;
using PublicAutomation.Pages;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PublicAutomation.Steps
{
    [Binding]
    class HomepageSteps
    {
        private readonly HomepagePage _homepagePage;
        private readonly TestRunContext _testRunContext;
        private readonly Shared _shared;

        public HomepageSteps(HomepagePage homepagePage, TestRunContext testRunContext, Shared shared)
        {
            _homepagePage = homepagePage;
            _testRunContext = testRunContext;
            _shared = shared;
        }

        [When(@"I search for a trader by name on the home page")]
        public void WhenISearchForATraderByNameOnTheHomePage()
        {
            _homepagePage.SearchForTraderByName(_testRunContext.testTraderName);
        }

        [When(@"I search for (.*) in (.*) on the homepage")]
        public void WhenISearchForInOnTheHomepage(string tradeCategory, string postcode)
        {
            _homepagePage.SearchForTraderByCategoryAndPostcode(tradeCategory, postcode);
        }

        [When(@"I search for (.*) and use the geolocation on the homepage")]
        public void WhenISearchForAndUseTheGeolocationOnTheHomepage(string tradeCategory)
        {
            _homepagePage.SearchForTraderByCategoryAndUseLocation(tradeCategory);
        }


        [Then(@"I should see a (.*) message indicating the search failure")]
        public void ThenIShouldSeeAMessageIndicatingTheSearchFailure(string errorType)
        {
            var searchFormResult = _homepagePage.SearchFormSource();

            switch (errorType)
            {
                default:
                    searchFormResult.Should().Contain("Add postcode");
                    searchFormResult.Should().Contain("Please enter a valid category name");
                    break;

                case "postcode":
                    searchFormResult.Should().Contain("Add postcode");
                    break;

                case "category":
                    searchFormResult.Should().Contain("Please enter a valid category name");
                    break;

            }


            //TODO: replace with switch
            if (errorType == "postcode")
            {
                searchFormResult.Should().Contain("Add postcode");
            }
            else if (errorType == "category")
            {
                searchFormResult.Should().Contain("Please enter a valid category name");
            }
            else
            {
                searchFormResult.Should().Contain("Add postcode");
                searchFormResult.Should().Contain("Please enter a valid category name");
            }
        }
       
        [When(@"I navigate through the recent search link within Recent Searches on the homepage")]
        public void WhenINavigateThroughTheRecentSearchLinkWithinRecentSearchesOnTheHomepage()
        {
            _shared.GoBackToPreviousePage();
            _homepagePage.ClikRecentSearchLink();
            _shared.WaitForHammerSpinner();
        }

        [Then(@"I should be directed to the search results page")]
        public void ThenIShouldBeDirectedToTheSearchResultsPage()
        {
            var RecentSearchpage = _homepagePage.GetHandymanText();
            RecentSearchpage.Should().Contain("Handyman In PO9 3NW");
        }

        [When(@"I search for four Trades in different locations")]
        public void WhenISearchForFourTradesInDifferentLocations()
        {
            var tupleList = new (string trade, string postcode)[]
            {
                  ("Handyman", "PO2 9PH"),
                  ("Plumber", "PO9 3NW"),
                  ("Carpenter", "G21 2QB"),
                  ("Gardener", "SW19 1AB")
            };
            foreach (var item in tupleList)
            {
                _homepagePage.SearchForTraderByCategoryAndPostcode(item.trade, item.postcode);
                _shared.GoBackToPreviousePage();
            }           
        }

        [Then(@"I should see three Recent Searched Trades within the Recent Searches element on the homepage")]
        public void ThenIShouldSeeThreeRecentSearchedTradesWithinTheRecentSearchesElementOnTheHomepage()
        {
            _homepagePage.CheckRecentSearchesLinksOnHomepage();
            
        }


    }
}

