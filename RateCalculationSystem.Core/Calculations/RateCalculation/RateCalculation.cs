using System;
using RateCalculationSystem.Common.Models.Rate;

namespace RateCalculationSystem.Core.Calculations.RateCalculation
{
    /// <summary>
    ///     Decimal Rate Calculation
    /// </summary>
    public class RateCalculation : ICalculation<MarketRateInputModel, MarketRateOutputModel>
    {
        /// <summary>
        /// Validate market rate input
        /// </summary>
        /// <param name="marketRateInput"></param>
        private void ValidateCalculationInput(MarketRateInputModel marketRateInput)
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

        public double PMT(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
        {
            var rate = (double)yearlyInterestRate / 100 / 12;
            var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
            return (rate + (rate / denominator)) * loanAmount;
        }

        /// <summary>
        ///     Calculate montly repayment and total repayment
        /// </summary>
        /// <param name="marketRateInput"></param>
        /// <returns></returns>
        public MarketRateOutputModel Calculate(MarketRateInputModel marketRateInput)
        {
            // validate
            ValidateCalculationInput(marketRateInput);

            var monthlyRate = marketRateInput.Rate;

            // divide the apr by 100 to get decimal percentage, if necessary
            if (marketRateInput.Rate > 0)
                monthlyRate = monthlyRate / 100;

            // Monthly rate is the yearly rate divided by 12 
            monthlyRate = marketRateInput.Rate / 12.0;

            // Calculate the monthly payment 
            var monthlyPayment = marketRateInput.Amount * (decimal) monthlyRate /
                                 (decimal) (1 - Math.Pow(1 + monthlyRate, -marketRateInput.Term));
            
            //var monthlyPayment = (decimal)PMT(marketRateInput.Rate, marketRateInput.Term, marketRateInput.Amount)



            // Calculate the total payment
            var totalPayment = monthlyPayment * marketRateInput.Term;

            return new MarketRateOutputModel
            {
                Rate = marketRateInput.Rate,
                MonthlyPayment = monthlyPayment,
                RequstedAmount = marketRateInput.Amount,
                TotalPayment = totalPayment
            };
        }
    }
}