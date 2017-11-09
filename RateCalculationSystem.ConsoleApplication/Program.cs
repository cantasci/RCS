using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using RateCalculationSystem.ConsoleApplication.Calculation;
using RateCalculationSystem.ConsoleApplication.Helper;
using RateCalculationSystem.ConsoleApplication.Models;
using RateCalculationSystem.Core.Calculations;
using RateCalculationSystem.Core.Calculations.RateCalculation;
using RateCalculationSystem.Core.Models.Rate;

namespace RateCalculationSystem.ConsoleApplication
{
    #region Error Messages

    /// <summary>
    ///     All error message of application
    /// </summary>
    internal static class ErrorMessage
    {
        public const string SampleUsage = "Sample usage: RateCalculationSystem.ConsoleApplication.exe market.csv 1000";
        public const string MissingArgument = "Expected 2 arguments. " + SampleUsage;
        public const string ThereIsNoAvailableOffer = "It is not possible to provide a quote at this time";
        public const string RequestAmountIsNotValid = "Requested amount is not valid value";
        public const string CouldNotResolveArguments = "Could not resolve arguments. " + SampleUsage;
        public const string RequestedAmountOutOfRange = "Requested amount must be between 1000 and 15000";
        public const string RequestedAmountIsNotMultipleOf100 = "You can enter only loan amount which is multiples of 100";
    }

    #endregion

    internal class Program
    {
        // constant values
        private const int TermInMonths = 36;

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                    throw new ArgumentException(ErrorMessage.MissingArgument);

                // change current culture with en-GB
                ChangeCurrentCulture();

                // parse arguments
                var argumentModel = ParseArguments(args);

                // load market data
                var fetchMarketData = FileHelper.GetLenderMarketData(argumentModel.MarketDataFilePath);

                // order lender offer with rate ascending and available descending
                fetchMarketData = fetchMarketData.OrderBy(m => m.Rate).ThenByDescending(m => m.Available).ToList();

                //  init payment calculation engine
                var paymentCalculation = new PaymentCalculation(TermInMonths);

                // get result
                var result = paymentCalculation.GetOffer(argumentModel, fetchMarketData);

                // print result
                PrintResult(result);
            }
            catch (Exception e)
            {
                // print error message
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        ///     Change current culture if needed
        /// </summary>
        private static void ChangeCurrentCulture()
        {
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            if (Thread.CurrentThread.CurrentCulture.Name == culture.Name) return;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        ///     Parse application arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static ArgumentModel ParseArguments(string[] args)
        {
            try
            {
                // parse requested amount
                if (!decimal.TryParse(args[1], out var requestedAmount))
                    throw new FormatException();

                // get path of market data
                var externalMarketFileName = args[0];

                // return arguments
                return new ArgumentModel
                {
                    MarketDataFilePath = externalMarketFileName,
                    RequestedAmount = requestedAmount
                };
            }
            catch (Exception)
            {
                throw new Exception(ErrorMessage.CouldNotResolveArguments);
            }
        }





        /// <summary>
        ///     Print Result
        /// </summary>
        /// <param name="result"></param>
        private static void PrintResult(RateOutputModel<decimal> result)
        {
            Console.WriteLine($"Requested amount: £{result.RequstedAmount:0.##}");
            Console.WriteLine($"Rate: {result.InterestRate:P1}");
            Console.WriteLine($"Monthly repayment: £{result.MonthlyPayment:0.00}");
            Console.WriteLine($"Total repayment: £{result.TotalPayment:0.00}");
        }
    }
}