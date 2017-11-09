using RateCalculationSystem.Core.Utils;

namespace RateCalculationSystem.Core.Extensions
{
    public static class ExtensionFormatToString
    {
        /// <summary>
        ///     Convert value with default currency format
        /// </summary>
        /// <param name="value">unformatted value</param>
        /// <returns></returns>
        public static string ConvertToCurrencyText(this decimal value)
        {
            return value.ToString(Constants.CurrencyFormat, Constants.CurrentCulture);
        }

        /// <summary>
        ///     Convert value with percentage format
        /// </summary>
        /// <param name="value">unformatted value</param>
        /// <returns></returns>
        public static string ConvertToPercentageText(this double value)
        {
            return value.ToString(Constants.PercentageFormat, Constants.CurrentCulture);
        }
    }
}