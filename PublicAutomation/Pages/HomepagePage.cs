using System;
using System.Collections.Generic;
using FluentAssertions;
using OpenQA.Selenium;
using PublicAutomation.Contexts;
using PublicAutomation.Helpers;
using TechTalk.SpecFlow;

namespace PublicAutomation.Pages
{
    [Binding]

    public class HomepagePage
    {
        private readonly TestRunContext _testRunContext;
        private readonly Shared _shared;

        //some of this is actually in the header or footer and can be moved to a shared page.
        protected By AdviceCentre = By.LinkText("Visit Advice Centre");
        protected By RequestAQuote = By.LinkText("Request a quote");
        protected By Postcode = By.Id("location");
        protected By FeebackLink = By.Id("ctl00_ctl00_content_content_ctlFeedbackLink");
        protected By SecureNumber = By.Id("ctl00_ctl00_content_lblTelNo");
        protected By ReviewMenu = By.XPath("//a[contains(@href, '/give-feedback')]");
        protected By SearchName = By.Name("traderLabel");
        protected By TradeBanner = By.CssSelector(".g22pnn-3.eICMke");
        protected By CountyDrop = By.CssSelector(".sc-1ggamfr-3.kKngmF");
        protected By CountyList = By.CssSelector(".ButtonBase-sc-6143n9-1-a.ButtonBase__LinkBase-sc-6143n9-2.dDdFXh");
        protected By TradeTiles = By.CssSelector(".yjxg7g-5.cyyNvr");
        protected By TradeCardName = By.CssSelector("[data-guid='TradeCardName']");
        protected By SearchPostcode = By.CssSelector("[data-guid='SearchFormLocation']");
        protected By SearchTrades = By.CssSelector("[data-guid='SearchFormCategory']");
        protected By SearchTradeBtn = By.CssSelector("[data-guid='SearchFormCta']");
        protected By SearchSwitch = By.CssSelector("[data-guid='SearchSwitch']");
        protected By SearchButton = By.CssSelector(".sc-6143n9-1.sc-1cxbop2-1.jHKxGr.sc-1cxbop2-3.sc-1cxbop2-4.sc-1aooezg-1.llfJgt");
        protected By SearchTitle = By.CssSelector("[data-guid='SearchTitle']");
        protected By TopNavHomeowner = By.CssSelector("[data-guid='TopNavHomeowner']");
        protected By PopularCats = By.CssSelector(".sc-6143n9-1-div.sc-1642ohb-1.dctWWa.sc-92do2z-9.dpPOuu");
        protected By TradesMessage = By.XPath("//*[@id='search-form-home']/div[1]/div/div[2]/div");
        protected By LocationError = By.XPath("//*[@id='__next']/div/div[2]/div[2]/div/h1");
        protected By PostcodeError = By.XPath("//*[@id='search-form-home']/div[2]/div[2]/div");
        protected By SearchForm = By.Id("search-form-home");
        protected By SearchResultSuggestions = By.Id("search-page-suggestions");
        protected By TopSearchResult = By.Id("search-page-suggestions-select-value--0");
        protected By footerArea = By.CssSelector("[data-guid='FooterWrapper']");
        protected By TopNavLogo = By.CssSelector("[data-guid='TopNavLogo']");
        protected By UseLocationButton = By.Name("location-pin-target");
        protected By RecentSearchLink = By.CssSelector("a[href*='/search?page=1&categoryId=']");      
        protected By DisplayText = By.CssSelector("[data-guid='SearchTitle']>h1");
        protected By AllRecentSearchesLinkes = By.CssSelector("a[href*='/search?page=1&categoryId=']");
        
        public HomepagePage(TestRunContext testRunContext, Shared shared)
        {
            _testRunContext = testRunContext;
            _shared = shared;
        }

        public void CheckHomepageHasLoaded()
        {
            _shared.WaitUntilElementIsVisible(footerArea);
        }

        public void SearchForTraderByName(string traderName)
        {
            _shared.ClickTheElement(SearchSwitch);
            _shared.FillTheTextBox(SearchName, _testRunContext.testTraderName);
            _shared.ClickTheElement(TopSearchResult);
        }

        public void SearchForTraderByCategoryAndPostcode(string tradeCategory, string postcode)
        {
            _shared.FillTheTextBox(SearchTrades, tradeCategory);
            _shared.SubmitTheTextBox(SearchTrades);          
            _shared.FillTheTextBox(SearchPostcode, postcode);
            _shared.ClickTheElement(SearchTradeBtn); 
            _shared.WaitForHammerSpinner();
        }

        public void SearchForTraderByCategoryAndUseLocation(string tradeCategory)
        {
            _shared.FillTheTextBox(SearchTrades, tradeCategory);
            _shared.SubmitTheTextBox(SearchTrades);
            _shared.ClickTheElement(UseLocationButton);
            _shared.ClickTheElement(SearchTradeBtn);
            _shared.WaitForHammerSpinner();
        }
        public void SearchForTraderByCategoryButDontSelectCategory(string tradeCategory, string postcode)
        {
            _shared.FillTheTextBox(SearchTrades, tradeCategory);
            _shared.FillTheTextBox(SearchPostcode, postcode);
            _shared.ClickTheElement(SearchTradeBtn);
            _shared.WaitForHammerSpinner();
        }

        public string SearchFormSource()
        {
            var element = _testRunContext.driver.FindElement(SearchForm);
            var elementText = element.Text;
            return elementText;
        }

        public void ClikRecentSearchLink()
        {
            _shared.ClickTheElement(RecentSearchLink);
        }

        public string GetHandymanText()
        {
            var textElement = _testRunContext.driver.FindElement(DisplayText).Text;
            return textElement;
        }

        public void CheckRecentSearchesLinksOnHomepage()
        {
            _shared.WaitUntilElementIsVisible(AllRecentSearchesLinkes);         
            IList<IWebElement> linksNames = _testRunContext.driver.FindElements(AllRecentSearchesLinkes);
            Console.WriteLine("The count of Recent Searches links is:" + linksNames.Count);
            string[] expectedTrades = new string[3] {"Gardener", "Carpenter", "Plumber"};

            for(int i=0; i<linksNames.Count; i++)
            {
                Console.WriteLine("Recent Searches are: " + linksNames[i].Text);
                _ = linksNames[i].Text.Should().Contain(expectedTrades[i]);


            }
        }

    }
}

