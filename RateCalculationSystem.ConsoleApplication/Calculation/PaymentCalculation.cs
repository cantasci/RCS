using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateCalculationSystem.ConsoleApplication.Models;
using RateCalculationSystem.Core.Calculations;
using RateCalculationSystem.Core.Calculations.RateCalculation;
using RateCalculationSystem.Core.Models.Rate;

namespace RateCalculationSystem.ConsoleApplication.Calculation
{
    /// <summary>
    /// Get offer for good match with requested amount
    /// </summary>
    public class PaymentCalculation
    {
        public PaymentCalculation() { }

        public PaymentCalculation(int termInMonths)
        {
            this.TermInMonths = termInMonths;
        }

        public int TermInMonths { get; set; }


        /// <summary>
        ///     Check requested amount
        /// </summary>
        /// <param name="requestedAmount"></param>
        /// <param name="marketDatas"></param>
        private void ValidateLoanAmount(decimal requestedAmount, List<MarketData> marketDatas)
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
        private List<RateOutputModel<decimal>> FindBestLender(decimal requestedAmount, List<MarketData> lenderMarkerDatas)
        {
            // init rate calculation object
            ICalculation<LenderOfferInputModel<decimal>, RateOutputModel<decimal>> rateCalculation =
                new RateCalculation();

            // init selected offer collection
            var combineLenderOffer = new List<RateOutputModel<decimal>>();

            // requestedAmount assign to neededAmount
            var neededAmount = requestedAmount;

            foreach (var lenderData in lenderMarkerDatas)
            {
                // if lender has amount which I needed, get all money. Otherwise, get money which is available
                var available = neededAmount >= lenderData.Available
                    ? lenderData.Available
                    : neededAmount;

                // assign input parameters
                var rateInput = new LenderOfferInputModel<decimal>
                {
                    Amount = available,
                    InterestReate = lenderData.Rate,
                    TermInMonth = TermInMonths
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
        public RateOutputModel<decimal> GetOffer(ArgumentModel argumentModel, List<MarketData> fetchMarketData)
        {
            // validate loand amount
            ValidateLoanAmount(argumentModel.RequestedAmount, fetchMarketData);

            // find best lender offer
            var foundLenderOffers = FindBestLender(argumentModel.RequestedAmount, fetchMarketData);

            // check present lender offer
            if (foundLenderOffers.Count == 0)
                throw new Exception(ErrorMessage.ThereIsNoAvailableOffer);

            // assing default values
            var result = new RateOutputModel<decimal>
            {
                InterestRate = 0,
                MonthlyPayment = 0,
                RequstedAmount = argumentModel.RequestedAmount,
                TotalPayment = 0
            };

            // combine selected offers
            foreach (var foundLenderOffer in foundLenderOffers)
            {
                result.InterestRate += (double)(foundLenderOffer.RequstedAmount * (decimal)foundLenderOffer.InterestRate) /
                                       (double)result.RequstedAmount;
                result.MonthlyPayment += foundLenderOffer.MonthlyPayment;
                result.TotalPayment += foundLenderOffer.TotalPayment;
            }

            return result;
        }
    }
}
