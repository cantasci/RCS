using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RateCalculationSystem.Common.Models.Message;
using RateCalculationSystem.Common.Models.Rate;
using RateCalculationSystem.Core.Calculations;
using RateCalculationSystem.Core.Calculations.RateCalculation;

namespace RateCalculationSystem.Core.Test
{
    [TestClass]
    public class RateCalculationTest
    {
        /// <summary>
        ///     Expect exception when argument equals null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void expect_exception_when_argument_equals_null()
        {
            ICalculation<MarketRateInputModel, MarketRateOutputModel> rateCalculation =
                new RateCalculation();

            rateCalculation.Calculate(null);
        }


        /// <summary>
        ///     Except exception when amount equal zero
        /// </summary>
        [TestMethod]
        public void expect_exception_when_amount_equals_zero()
        {
            ICalculation<MarketRateInputModel, MarketRateOutputModel> rateCalculation =
                new RateCalculation();

            var loanAmount = 0;
            var rate = 0.07;
            var term = 36;

            Exception exception = null;
            try
            {
                rateCalculation.Calculate(new MarketRateInputModel(loanAmount, term, rate));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            var isTrue = CheckException(exception, ErrorMessage.ArgumentAmountGreaterThanZero);
            Assert.IsTrue(isTrue);
        }

        /// <summary>
        ///     Except exception when term equal zero
        /// </summary>
        [TestMethod] 
        public void expect_exception_when_term_equals_zero()
        {
            ICalculation<MarketRateInputModel, MarketRateOutputModel> rateCalculation =
                new RateCalculation();

            var loanAmount = 1000;
            var rate = 0.07;
            var term = 0;

            Exception exception = null;
            try
            {
                rateCalculation.Calculate(new MarketRateInputModel(loanAmount, term, rate));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            var isTrue = CheckException(exception, ErrorMessage.ArgumentTermGreaterThanZero);
            Assert.IsTrue(isTrue);
        }

        /// <summary>
        ///     Except exception when arguments equal zero
        /// </summary>
        [TestMethod]
        public void expect_exception_when_interest_rate_equal_zero()
        {
            ICalculation<MarketRateInputModel, MarketRateOutputModel> rateCalculation =
                new RateCalculation();

            var loanAmount = 1000;
            var rate = 0;
            var term = 36;

            Exception exception = null;
            try
            {
                rateCalculation.Calculate(new MarketRateInputModel(loanAmount, term, rate));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            var isTrue = CheckException(exception, ErrorMessage.ArgumentRateGreaterThanZero);
            Assert.IsTrue(isTrue);
        }

        /// <summary>
        ///     Check requested amount
        /// </summary>
        [TestMethod] 
        public void expect_true_for_correct_requested_amount()
        {
            ICalculation<MarketRateInputModel, MarketRateOutputModel> rateCalculation =
                new RateCalculation();

            var loanAmount = 1000;
            var rate = 0.07;
            var term = 36;
            var result = rateCalculation.Calculate(new MarketRateInputModel(loanAmount, term, rate));

            Assert.AreEqual(loanAmount, result.RequstedAmount);
        }


        /// <summary>
        ///     Check monthly repayment
        /// </summary>
        [TestMethod]
        public void expect_true_for_correct_monthly_repayment()
        {
            ICalculation<MarketRateInputModel, MarketRateOutputModel> rateCalculation =
                new RateCalculation();

            var loanAmount = 1000;
            var rate = 0.07;
            var term = 36;
            var result = rateCalculation.Calculate(new MarketRateInputModel(loanAmount, term, rate));

            // excepted result is 30.87 
            var expectedMonthlyPayment = (decimal) 30.87;
            var currentMonthlyPayment = Math.Truncate(result.MonthlyPayment * 100) / 100;
            Assert.AreEqual(expectedMonthlyPayment, currentMonthlyPayment);
        }


        /// <summary>
        ///     check total repayment
        /// </summary>
        [TestMethod]
        public void expect_true_for_correct_total_repayment()
        {
            ICalculation<MarketRateInputModel, MarketRateOutputModel> rateCalculation =
                new RateCalculation();

            var loanAmount = 1000;
            var rate = 0.07;
            var term = 36;
            var result = rateCalculation.Calculate(new MarketRateInputModel(loanAmount, term, rate));

            // excepted result is true
            var expectedTotalPayment = (decimal) 1111.57;
            var currentTotalPayment = Math.Truncate(result.TotalPayment * 100) / 100;
            Assert.AreEqual(expectedTotalPayment, currentTotalPayment);
        }




        private bool CheckException(Exception exception, string message)
        {
            return exception != null &&
                   exception.Message.Equals(message);
        }

    }
}