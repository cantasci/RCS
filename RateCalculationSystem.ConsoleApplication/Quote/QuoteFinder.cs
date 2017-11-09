using System;
using System.Collections.Generic;
using System.Linq;
using RateCalculationSystem.ConsoleApplication.Helper;
using RateCalculationSystem.ConsoleApplication.Models;
using RateCalculationSystem.Core.Calculations;
using RateCalculationSystem.Core.Calculations.RateCalculation;
using RateCalculationSystem.Core.Models.Rate;

namespace RateCalculationSystem.ConsoleApplication.Quote
{
    /// <summary>
    /// Get offer for good match with requested amount
    /// </summary>
    public class QuoteFinder
    {
        public QuoteFinder() { }

        public QuoteFinder(int term)
        {
            this.Term = term;
        }

        public int Term { get; set; }


        /// <summary>
        ///     Check requested amount
        /// </summary>
        /// <param name="requestedAmount"></param>
        /// <param name="marketDatas"></param>
        private void ValidateRequestedAmount(decimal requestedAmount, List<MarketData> marketDatas)
        {
            // validate inputs
            if (requestedAmount < 1000 || requestedAmount > 15000)
                throw new Exception(ErrorMessage.RequestedAmountOutOfRange);

            if (requestedAmount % 100 != 0)
                throw new Exception(ErrorMessage.RequestedAmountIsNotMultipleOf100);

            var totalLenderMoney = marketDatas.Sum(m => m.Available);
            if (requestedAmount > totalLenderMoney)
                throw new Exception(ErrorMessage.ThereIsNoAvailableOffer);
        }


        /// <summary>
        ///    Get offer for good match with requested amount
        /// </summary>
        /// <param name="requestedAmount"></param>
        /// <param name="lenderMarkerDatas"></param>
        private List<MarketRateOutputModel<decimal>> FindPossibleQuote(decimal requestedAmount, List<MarketData> lenderMarkerDatas)
        {
            // init rate calculation object
            ICalculation<MarketRateInputModel<decimal>, MarketRateOutputModel<decimal>> rateCalculation =
                new RateCalculation();

            // init selected offer collection
            var combineLenderOffer = new List<MarketRateOutputModel<decimal>>();

            // requestedAmount assign to neededAmount
            var neededAmount = requestedAmount;

            foreach (var lenderData in lenderMarkerDatas)
            {
                // if lender has amount which I needed, get all money. Otherwise, get money which is available
                var available = neededAmount >= lenderData.Available
                    ? lenderData.Available
                    : neededAmount;

                // assign input parameters
                var rateInput = new MarketRateInputModel<decimal>
                {
                    Amount = available,
                    Rate = lenderData.Rate,
                    Term = Term
                };

                // calculate rate and repayment
                var rateOutput = rateCalculation.Calculate(rateInput);

                // add to collection
                combineLenderOffer.Add(rateOutput);

                // subtraction avaible money from needed amount
                neededAmount -= available;

                // exit loop when we get money which I needed
                if (neededAmount <= 0) break;
            }

            return combineLenderOffer;
        }


        /// <summary>
        ///  Get Result
        /// </summary>
        /// <param name="argumentModel"></param>
        /// <param name="fetchMarketData"></param>
        /// <returns></returns>
        public MarketRateOutputModel<decimal> GetQuote(ArgumentModel argumentModel, List<MarketData> fetchMarketData)
        {
            // validate loand amount
            ValidateRequestedAmount(argumentModel.RequestedAmount, fetchMarketData);

            // find possible quote
            var possibleQuotes = FindPossibleQuote(argumentModel.RequestedAmount, fetchMarketData);

            // check present quotes
            if (possibleQuotes.Count == 0)
                throw new Exception(ErrorMessage.ThereIsNoAvailableOffer);

            // assing default values
            var result = new MarketRateOutputModel<decimal>
            {
                Rate = 0,
                MonthlyPayment = 0,
                RequstedAmount = argumentModel.RequestedAmount,
                TotalPayment = 0
            };

            // combine selected quote
            foreach (var possibleQuote in possibleQuotes)
            {
                result.Rate += (double)(possibleQuote.RequstedAmount * (decimal)possibleQuote.Rate) /
                                       (double)result.RequstedAmount;
                result.MonthlyPayment += possibleQuote.MonthlyPayment;
                result.TotalPayment += possibleQuote.TotalPayment;
            }

            return result;
        }
    }
}
