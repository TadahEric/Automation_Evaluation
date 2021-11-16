using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FluentAssertions;
using OpenQA.Selenium;
using PublicAutomation.Contexts;
using PublicAutomation.Helpers;
using TechTalk.SpecFlow;

namespace PublicAutomation.Pages
{
    [Binding]
    class TradesPage
    {
        private readonly TestRunContext _testRunContext;
        private readonly Shared _shared;
        public TradesPage(TestRunContext testRunContext, Shared shared)
        {
            _testRunContext = testRunContext;
            _shared = shared;
        }

    }
}
