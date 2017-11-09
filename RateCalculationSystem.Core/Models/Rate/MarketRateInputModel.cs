namespace RateCalculationSystem.Core.Models.Rate
{
    /// <summary>
    ///     Input Class for Rate Calculation
    /// </summary>
    /// <typeparam name="T">type of requested amount</typeparam>
    public class MarketRateInputModel<T>
    {
        public MarketRateInputModel()
        {
        }

        public MarketRateInputModel(T amount, int term, double interestRate)
        {
            Amount = amount;
            Term = term;
            Rate = interestRate;
        }


        public T Amount { get; set; }
        public int Term { get; set; }
        public double Rate { get; set; }
    }
}