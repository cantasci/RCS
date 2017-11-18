using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using RateCalculationSystem.Business.Quote;
using RateCalculationSystem.Common.Models;
using RateCalculationSystem.Common.Models.Argument;
using RateCalculationSystem.Common.Models.Message;
using RateCalculationSystem.Common.Models.Rate;
using RateCalculationSystem.Core.Helper;
using FileHelper = RateCalculationSystem.ConsoleApplication.Helper.FileHelper;

namespace RateCalculationSystem.ConsoleApplication
{ 

    internal class Program
    {
        // constant values
        private const int Terms = 36;

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
                
                
                var paymentCalculation = new QuoteFinder(Terms);
                
                // find result
                var result = paymentCalculation.GetQuote(argumentModel, fetchMarketData);

                // print result
                PrintResult(result);

                Console.Read();
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
                if (!decimal.TryParse(args[1], out var requestedAmount))
                    throw new FormatException();
                
                return new ArgumentModel
                {
                    MarketDataFilePath = args[0],
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
        private static void PrintResult(MarketRateOutputModel result)
        {
            Console.WriteLine($"Requested amount: £{result.RequstedAmount:0.##}");
            Console.WriteLine($"Rate: {result.Rate:P1}");
            Console.WriteLine($"Monthly repayment: £{result.MonthlyPayment:0.00}");
            Console.WriteLine($"Total repayment: £{result.TotalPayment:0.00}");
        }
    }
}