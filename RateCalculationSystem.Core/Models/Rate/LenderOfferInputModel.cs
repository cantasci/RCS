namespace RateCalculationSystem.Core.Models.Rate
{
    /// <summary>
    ///     Input Class for Rate Calculation
    /// </summary>
    /// <typeparam name="T">type of requested amount</typeparam>
    public class LenderOfferInputModel<T>
    {
        public LenderOfferInputModel()
        {
        }

        public LenderOfferInputModel(T amount, int termInMonth, double interestRate)
        {
            Amount = amount;
            TermInMonth = termInMonth;
            InterestReate = interestRate;
        }


        public T Amount { get; set; }
        public int TermInMonth { get; set; }
        public double InterestReate { get; set; }
    }
}