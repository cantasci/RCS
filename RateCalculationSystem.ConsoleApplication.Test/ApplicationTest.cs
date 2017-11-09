using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RateCalculationSystem.ConsoleApplication.Models;
using RateCalculationSystem.ConsoleApplication.Quote;

namespace RateCalculationSystem.ConsoleApplication.Test
{
    [TestClass]
    public class ApplicationTest
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

            // excepted an exception with message "Requested amount must be between 1000 and 15000"
            ExpectException<Exception>(() => quoteFinder.GetQuote(externalArgument, emptyMarketData));
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

            // excepted an exception with message "You should request only loan amount which is multiples of 100"
            ExpectException<Exception>(() => quoteFinder.GetQuote(externalArgument, emptyMarketData));

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

            // excepted an exception with message "It is not possible to provide a quoteFinder at this time"
            ExpectException<Exception>(() => quoteFinder.GetQuote(externalArgument, emptyMarketData));

        }

        /// <summary>
        ///     Generic function for exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void ExpectException<T>(Action action) where T : Exception
        {
            try
            {
                action();
                Assert.Fail("Expected exception " + typeof(T).Name);
            }
            catch (T exception)
            {
                Assert.AreEqual(typeof(T).Name, exception.GetType().Name);
            }
        }
    }
}
