using System;
using RateCalculationSystem.Core.Models.Rate;

namespace RateCalculationSystem.Core.Calculations.RateCalculation
{
    /// <summary>
    ///     Decimal Rate Calculation
    /// </summary>
    public class RateCalculation : ICalculation<MarketRateInputModel<decimal>, MarketRateOutputModel<decimal>>
    {
        /// <summary>
        /// Validate market rate input
        /// </summary>
        /// <param name="marketRateInput"></param>
        private void ValidateCalculationInput(MarketRateInputModel<decimal> marketRateInput)
        {
            // check parameters
            if (marketRateInput == null)
                throw new ArgumentNullException("marketRateInput");
            if (marketRateInput.Amount <= 0)
                throw new ArgumentException("Argument amount must be greater than zero");
            if (marketRateInput.Term <= 0)
                throw new ArgumentException("Argument termInMonth  must be greater than zero");
            if (marketRateInput.Rate <= 0)
                throw new ArgumentException("Argument rate  must be greater than zero");
        }

        /// <summary>
        ///     Calculate montly repayment and total repayment
        /// </summary>
        /// <param name="marketRateInput"></param>
        /// <returns></returns>
        public MarketRateOutputModel<decimal> Calculate(MarketRateInputModel<decimal> marketRateInput)
        {
            // validate
            ValidateCalculationInput(marketRateInput);

            var monthlyRate = marketRateInput.Rate;

            // divi the apr by 100 to get decimal percentage, if necessary
            if (marketRateInput.Rate > 0)
                monthlyRate = monthlyRate / 100;


            // Monthly rate is the yearly rate divided by 12 
            monthlyRate = marketRateInput.Rate / 12.0;

            // Calculate the monthly payment 
            var monthlyPayment = marketRateInput.Amount * (decimal) monthlyRate /
                                 (decimal) (1 - Math.Pow(1 + monthlyRate, -marketRateInput.Term));
            
            // Calculate the total payment
            var totalPayment = monthlyPayment * marketRateInput.Term;

            return new MarketRateOutputModel<decimal>
            {
                Rate = marketRateInput.Rate,
                MonthlyPayment = monthlyPayment,
                RequstedAmount = marketRateInput.Amount,
                TotalPayment = totalPayment
            };
        }
    }
}