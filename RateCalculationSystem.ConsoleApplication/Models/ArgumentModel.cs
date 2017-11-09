namespace RateCalculationSystem.ConsoleApplication.Models
{
    /// <summary>
    ///     Application arguments
    /// </summary>
    public class ArgumentModel
    {
        public decimal RequestedAmount { get; set; }

        public string MarketDataFilePath { get; set; }
    }
}