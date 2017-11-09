namespace RateCalculationSystem.Core.Calculations
{
    public interface ICalculation<in TInput, out TOutput>
    {
        /// <summary>
        ///     Calculate Rate and Repayment
        /// </summary>
        /// <param name="rateInput"></param>
        /// <returns></returns>
        TOutput Calculate(TInput rateInput);
    }
}