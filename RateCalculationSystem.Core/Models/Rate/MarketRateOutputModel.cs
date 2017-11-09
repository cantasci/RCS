﻿namespace RateCalculationSystem.Core.Models.Rate
{
    /// <summary>
    ///     Result of Rate Calculation
    /// </summary>
    /// <typeparam name="T">type of requested amount</typeparam>
    public class MarketRateOutputModel<T>
    {
        public T RequstedAmount { get; set; }
        public T MonthlyPayment { get; set; }
        public T TotalPayment { get; set; }
        public double Rate { get; set; }


        public override string ToString()
        {
            return new Utils.Formatter()
                .Add("Requested amount", $"£{RequstedAmount:0.00}")
                .Add("Rate", $"{Rate:P1}%")
                .Add("Monthly repayment", $"£{MonthlyPayment:0.00}")
                .Add("Total repayment", $"£{TotalPayment:0.00}")
                .ToString();
        }
    }
}