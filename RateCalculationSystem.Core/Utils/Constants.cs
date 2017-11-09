using System.Globalization;

namespace RateCalculationSystem.Core.Utils
{
    /// <summary>
    ///     Application Constant Values
    /// </summary>
    public class Constants
    {
        public static string CurrencyFormat = "C";
        public static string PercentageFormat = "c";
        public static CultureInfo CurrentCulture = new CultureInfo("es-ES");
        public static string DefaultDelimeter = ",";
        public static string DefaulFilename = "market.csv";
    }
}