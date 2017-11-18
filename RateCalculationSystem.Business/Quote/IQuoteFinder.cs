using System;
using System.Collections.Generic;
using System.Text;
using RateCalculationSystem.Common.Models;
using RateCalculationSystem.Common.Models.Argument;
using RateCalculationSystem.Common.Models.Rate;

namespace RateCalculationSystem.Business.Quote
{
    public interface IQuoteFinder
    {
        MarketRateOutputModel GetQuote(ArgumentModel argumentModel, List<MarketData> fetchMarketData);
    }
}
