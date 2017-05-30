using NUnit.Framework;
using System;
using System.Transactions;

namespace GigHub.IntegrationsTests
{
    public class Isolated: Attribute, ITestAction
    {
        private TransactionScope _transactionScope; 
        public void BeforeTest(TestDetails testDetails)
        {
            // Create a new transaction scope
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(TestDetails testDetails)
        {
            // After test - dispose the object
            _transactionScope.Dispose();
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}
