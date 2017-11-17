namespace RateCalculationSystem.Common.Models.Rate
{
    /// <summary>
    ///     Input Class for Rate Calculation
    /// </summary>
    /// <typeparam name="T">type of requested amount</typeparam>
    public class MarketRateInputModel
    {
        public MarketRateInputModel()
        {
        }

        public MarketRateInputModel(decimal amount, int term, double interestRate)
        {
            Amount = amount;
            Term = term;
            Rate = interestRate;
        }

        #region Public Property

        public decimal Amount { get; set; }
        public int Term { get; set; }
        public double Rate { get; set; } 

        #endregion
    }
}