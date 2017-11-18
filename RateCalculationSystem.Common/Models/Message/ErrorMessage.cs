namespace RateCalculationSystem.Common.Models.Message
{  
    /// <summary>
    ///     All error message of application
    /// </summary>
    public static class ErrorMessage
    {
        public const string SampleUsage = "Sample usage: RateCalculationSystem.ConsoleApplication.exe market.csv 1000";
        public const string MissingArgument = "Expected 2 arguments. " + SampleUsage;
        public const string ThereIsNoAvailableOffer = "It is not possible to provide a quote at this time";
        public const string RequestAmountIsNotValid = "Requested amount is not valid value";
        public const string CouldNotResolveArguments = "Could not resolve arguments. " + SampleUsage;
        public const string RequestedAmountOutOfRange = "Requested amount must be between 1000 and 15000";
        public const string RequestedAmountIsNotMultipleOf100 = "You should request only loan amount which is multiples of 100";
        public const string ArgumentAmountGreaterThanZero = "Argument amount must be greater than zero";
        public const string ArgumentTermGreaterThanZero = "Argument termInMonth must be greater than zero";
        public const string ArgumentRateGreaterThanZero = "Argument rate must be greater than zero";
    }
}
