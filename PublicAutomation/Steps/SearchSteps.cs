using FluentAssertions;
using PublicAutomation.Contexts;
using PublicAutomation.Helpers;
using PublicAutomation.Pages;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace PublicAutomation.Steps
{
    [Binding]
    class SearchSteps
    {
        private readonly SearchPage _searchPage;
        private readonly TestRunContext _testRunContext;
        private readonly Shared _shared;
        private readonly TradesPage _tradesPage;
        public SearchSteps(SearchPage searchPage, TestRunContext testRunContext, Shared shared, TradesPage tradesPage)
        {
            _searchPage = searchPage;
            _testRunContext = testRunContext;
            _shared = shared;
            _tradesPage = tradesPage;
        }

        [When(@"I view the first result")]
        public void WhenIViewTheFirstResult()
        {
            _searchPage.ViewTheFirstSearchResult();
        }


        [When(@"I sort the results by (.*)")]
        public void WhenISortTheResultsBy(string sortBy)
        {
            _searchPage.SortSearchResultsBy(sortBy);
        }

        [Then(@"I see the results are sorted by (.*)")]
        public void ThenISeeTheResultsAreSortedBy(string sortBy)
        {
            _searchPage.CheckSearchResultsAreInCorrectOrder(sortBy);
        }

    }
}
