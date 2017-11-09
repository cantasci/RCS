using RateCalculationSystem.Core.Models.Rate;

namespace RateCalculationSystem.ConsoleApplication.Models
{
    public class MarketResultModel<T>
    {
        public MarketData Data { get; set; }

        public MarketRateOutputModel<T> RepaymentValues { get; set; }
    }
}