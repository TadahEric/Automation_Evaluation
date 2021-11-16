using OpenQA.Selenium;
using PublicAutomation.Contexts;
using PublicAutomation.Helpers;
using TechTalk.SpecFlow;

namespace PublicAutomation.Pages
{
    //this is a generic page for elements that should appear on most/all pages.
    //e.g. the top nav bar, and footer links.

    [Binding]
    class SharedPage
    {
        private readonly TestRunContext _testRunContext;
        private readonly Shared _shared;

        protected By AppContainer = By.CssSelector("[data-guid='AppContainer']");

        //header elements
        protected By TopNavLogo = By.CssSelector("[data-guid='TopNavLogo']");
        protected By TopNavHomeownerLink = By.LinkText("Homeowner");

        //header elements heritage site
        protected By HeartClass = By.ClassName("heart");

        //footer elements
        protected By Consumer = By.LinkText("Why Checkatrade");
        protected By Standard = By.LinkText("The Checkatrade standard");
        protected By Complaints = By.LinkText("Complaints about a member");
        protected By Faq = By.LinkText("FAQ");
        protected By Secure = By.LinkText("Secure contacts");
        protected By Careers = By.LinkText("Careers");
        protected By Partners = By.LinkText("Partners");
        protected By TermsOfUse = By.LinkText("Terms of use");
        protected By TradeAssociations = By.LinkText("Trade Associations");
        protected By Privacy = By.LinkText("Privacy notice");
        protected By SiteMap = By.LinkText("Sitemap");
        protected By Contact = By.LinkText("Contact us");

        protected By Gender2017 = By.LinkText("Gender pay report 2017");
        protected By Gender2018 = By.LinkText("Gender pay report 2018");
        protected By Gender2019 = By.LinkText("Gender pay report 2019");
        protected By Gender2019DownloadLink = By.CssSelector("[href*='/Downloads/gender-pay-2019.pdf']");

        public SharedPage(TestRunContext testRunContext, Shared shared)
        {
            _testRunContext = testRunContext;
            _shared = shared;
        }

        public void NavigateToFooterLink(string footerLink)
        {
            switch (footerLink)
            {
                case "Consumer":
                    _shared.ClickTheElement(Consumer);
                    break;
                case "Standard":
                    _shared.ClickTheElement(Standard);
                    break;
                case "Complaints":
                    _shared.ClickTheElement(Complaints);
                    break;
                case "Faq":
                    _shared.ClickTheElement(Faq);
                    break;
                case "Secure":
                    _shared.ClickTheElement(Secure);
                    break;
                case "Careers":
                    _shared.ClickTheElement(Careers);
                    break;
                case "Partners":
                    _shared.ClickTheElement(Partners);
                    break;
                case "TermsOfUse":
                    _shared.ClickTheElement(TermsOfUse);
                    break;
                case "TradeAssociations":
                    _shared.ClickTheElement(TradeAssociations);
                    break;
                case "Privacy":
                    _shared.ClickTheElement(Privacy);
                    break;
                case "SiteMap":
                    _shared.ClickTheElement(SiteMap);
                    break;
                case "Contact":
                    _shared.ClickTheElement(Contact);
                    break;
                case "Gender2017":
                    _shared.ClickTheElement(Gender2017);
                    break;
                case "Gender2018":
                    _shared.ClickTheElement(Gender2018);
                    break;
                case "Gender2019":
                    _shared.ClickTheElement(Gender2019);
                    break;
            }
        }

        public void CheckFooterPageHasLoadedCorrectly(string expectedUrl)
        {
            _shared.WaitUntilElementIsVisible(AppContainer);
            _shared.CheckPageHasLoadedWithNoErrors(expectedUrl);
        }

        public void CheckFooterPageHasLoadedCorrectlyOnHeritageSite(string expectedUrl)
        {
            _shared.WaitUntilElementIsVisible(HeartClass);
            _shared.CheckPageHasLoadedWithNoErrors(expectedUrl);
        }
        
    }
}
