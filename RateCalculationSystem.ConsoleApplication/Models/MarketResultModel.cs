using RateCalculationSystem.Core.Models.Rate;

namespace RateCalculationSystem.ConsoleApplication.Models
{
    public class MarketResultModel<T>
    {
        public MarketData Data { get; set; }

        public RateOutputModel<T> RepaymentValues { get; set; }
    }
}