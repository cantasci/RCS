using System;
using RateCalculationSystem.Core.Models.Rate;

namespace RateCalculationSystem.Core.Calculations.RateCalculation
{
    /// <summary>
    ///     Decimal Rate Calculation
    /// </summary>
    public class RateCalculation : ICalculation<LenderOfferInputModel<decimal>, RateOutputModel<decimal>>
    {
        /// <summary>
        ///     Calculate montly repayment and total repayment
        /// </summary>
        /// <param name="lenderOfferInput"></param>
        /// <returns></returns>
        public RateOutputModel<decimal> Calculate(LenderOfferInputModel<decimal> lenderOfferInput)
        {
            // check parameters
            if (lenderOfferInput == null) throw new ArgumentNullException("lenderOfferInput");
            if (lenderOfferInput.Amount <= 0) throw new ArgumentException("Argument amount must be greater than zero");
            if (lenderOfferInput.TermInMonth <= 0)
                throw new ArgumentException("Argument termInMonth  must be greater than zero");
            if (lenderOfferInput.InterestReate <= 0)
                throw new ArgumentException("Argument interestRate  must be greater than zero");


            // Monthly interest rate is the yearly rate divided by 12
            //double monthlyRate = lenderOfferInput.InterestReate / 12.3809; // correct answer with yearly rate divided 12.3809. why?
            var monthlyRate = lenderOfferInput.InterestReate / 12.0;

            // Calculate the monthly payment 
            var monthlyPayment = lenderOfferInput.Amount * (decimal) monthlyRate /
                                 (decimal) (1 - Math.Pow(1 + monthlyRate, -lenderOfferInput.TermInMonth));

            // round monthly payment
            monthlyPayment = Math.Round(monthlyPayment, 2);

            // Calculate the total payment
            var totalPayment = Math.Round(monthlyPayment, 2) * lenderOfferInput.TermInMonth;

            return new RateOutputModel<decimal>
            {
                InterestRate = lenderOfferInput.InterestReate,
                MonthlyPayment = monthlyPayment,
                RequstedAmount = lenderOfferInput.Amount,
                TotalPayment = totalPayment
            };
        }
    }
}