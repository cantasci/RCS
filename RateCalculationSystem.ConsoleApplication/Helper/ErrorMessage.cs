using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCalculationSystem.ConsoleApplication.Helper
{  
    /// <summary>
    ///     All error message of application
    /// </summary>
    internal static class ErrorMessage
    {
        public const string SampleUsage = "Sample usage: RateCalculationSystem.ConsoleApplication.exe market.csv 1000";
        public const string MissingArgument = "Expected 2 arguments. " + SampleUsage;
        public const string ThereIsNoAvailableOffer = "It is not possible to provide a quote at this time";
        public const string RequestAmountIsNotValid = "Requested amount is not valid value";
        public const string CouldNotResolveArguments = "Could not resolve arguments. " + SampleUsage;
        public const string RequestedAmountOutOfRange = "Requested amount must be between 1000 and 15000";
        public const string RequestedAmountIsNotMultipleOf100 = "You should request only loan amount which is multiples of 100";
    }
}
