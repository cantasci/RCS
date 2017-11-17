namespace RateCalculationSystem.Common.Models.Rate
{
    /// <summary>
    ///     Result of Rate Calculation
    /// </summary>
    public class MarketRateOutputModel
    {
        public decimal RequstedAmount { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal TotalPayment { get; set; }
        public double Rate { get; set; }
    }
}