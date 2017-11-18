using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RateCalculationSystem.ConsoleApplication.Helper;

namespace RateCalculationSystem.ConsoleApplication.Test
{
    [TestClass]
    public class ApplicationTest
    {
        [TestMethod]
        public void could_not_file_when_load_market_data()
        {
            
            Exception exception = null;
            try
            {
                var fetchMarketData = FileHelper.GetLenderMarketData(null);
            }
            catch (Exception e)
            {
                exception = e;
            }
            var isTrue = CheckException(exception, "Resource file could not find");
            Assert.IsTrue(isTrue);
        }

        private bool CheckException(Exception exception, string message)
        {
            return exception != null &&
                   exception.Message.Equals(message);
        }


    }
}
