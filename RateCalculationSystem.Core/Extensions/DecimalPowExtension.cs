namespace RateCalculationSystem.Core.Extensions
{
    public static class DecimalPowExtension
    {
        public static decimal Pow(this decimal value, int decimals)
        {
            for (var i = 1; i < decimals; i++)
                value *= value;

            return value;
        }

        public static double Pow(this double value, int decimals)
        {
            for (var i = 1; i < decimals; i++)
                value *= value;

            return value;
        }
    }
}