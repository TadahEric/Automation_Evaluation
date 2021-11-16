using PublicAutomation.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace PublicAutomation.Steps
{
    [Binding]
    class SharedSteps
    {
        private readonly TestRunContext _testRunContext;
        public SharedSteps(TestRunContext testRunContext)
        {
            _testRunContext = testRunContext;
        }

        [Given(@"I have a valid trade")]
        public void GivenIHaveAValidTrade()
        {
            _testRunContext.testTraderName = TestData.TestTraders.TestTradeName;
        }
    }
}
