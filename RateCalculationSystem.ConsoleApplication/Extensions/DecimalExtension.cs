using System;

namespace RateCalculationSystem.ConsoleApplication.Extensions
{
    public class DecimalExtension
    {
        /// <summary>
        ///     Truncate decimal value without rounding
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal Truncate(decimal value, int decimals)
        {
            return Math.Truncate(value * (decimal) Math.Pow(10, decimals)) / (decimal) Math.Pow(10, decimals);
        }
    }
}