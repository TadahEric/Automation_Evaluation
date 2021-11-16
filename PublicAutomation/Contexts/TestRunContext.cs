using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace PublicAutomation.Contexts
{
    [Binding]
    public class TestRunContext
    {
        public IWebDriver driver;
        public TimeSpan waitTimer = TimeSpan.FromSeconds(60);

        public string testTraderName;
        
        public string consumerMemberEmail;

        public string consumerMemberFirstName;

        public string consumerMemberLastName;

        public string consumerMemberPassword;
    }
}
