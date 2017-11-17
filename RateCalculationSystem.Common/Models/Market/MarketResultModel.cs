using RateCalculationSystem.Common.Models.Rate;

namespace RateCalculationSystem.Common.Models.Market
{
    public class MarketResultModel
    {
        public MarketData Data { get; set; }

        public MarketRateOutputModel RepaymentValues { get; set; }
    }
}