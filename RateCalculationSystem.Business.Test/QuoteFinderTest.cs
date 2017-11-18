using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RateCalculationSystem.Business.Quote;
using RateCalculationSystem.Common.Models;
using RateCalculationSystem.Common.Models.Argument;
using RateCalculationSystem.Common.Models.Message;

namespace RateCalculationSystem.Business.Test
{
    [TestClass]
    public class QuoteFinderTest
    {
        /// <summary>
        ///  Expect exception when borrowers make requested amount which is not between 1000 and 15000
        /// </summary>
        [TestMethod]
        public void expect_exception_when_requested_amount_is_out_of_range()
        {
            QuoteFinder quoteFinder = new QuoteFinder(36);
            var emptyMarketData = new List<MarketData>();
            var externalArgument = new ArgumentModel()
            {
                RequestedAmount = 500
            };

            Exception exception = null;
            try
            {

                quoteFinder.GetQuote(externalArgument, emptyMarketData);
            }
            catch (Exception e)
            {
                exception = e;
            }


            var isTrue = CheckException(exception, ErrorMessage.RequestedAmountOutOfRange);
            Assert.IsTrue(isTrue);

        }

        /// <summary>
        /// Expect exception when borrowers make requested amount which is not multiple of 100
        /// </summary>
        [TestMethod]
        public void expect_exception_when_requested_amount_is_not_multiple_of_100()
        {
            QuoteFinder quoteFinder = new QuoteFinder(36);
            var emptyMarketData = new List<MarketData>();
            var externalArgument = new ArgumentModel()
            {
                RequestedAmount = 1010
            };

            Exception exception = null;
            try
            {
                quoteFinder.GetQuote(externalArgument, emptyMarketData);
            }
            catch (Exception e)
            {
                exception = e;
            }


            var isTrue = CheckException(exception, ErrorMessage.RequestedAmountIsNotMultipleOf100);
            Assert.IsTrue(isTrue);


        }

        /// <summary>
        /// Except exception when there is no available lender offer
        /// </summary>
        [TestMethod]
        public void expect_exception_when_there_is_no_lender_offer()
        {
            QuoteFinder quoteFinder = new QuoteFinder(36);
            var emptyMarketData = new List<MarketData>();
            var externalArgument = new ArgumentModel()
            {
                RequestedAmount = 1000
            };

            Exception exception = null;
            try
            {

                quoteFinder.GetQuote(externalArgument, emptyMarketData);
            }
            catch (Exception e)
            {
                exception = e;
            }


            var isTrue = CheckException(exception, ErrorMessage.ThereIsNoAvailableOffer);
            Assert.IsTrue(isTrue);

        }

        private bool CheckException(Exception exception, string message)
        {
            return exception != null &&
                   exception.Message.Equals(message);
        }
    }
}
