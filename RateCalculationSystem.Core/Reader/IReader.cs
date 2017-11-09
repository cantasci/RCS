using System.Collections.Generic;

namespace RateCalculationSystem.Core.Reader
{
    public interface IReader
    {
        /// <summary>
        ///     Read action
        /// </summary>
        void Read();

        /// <summary>
        ///     Get result data
        /// </summary>
        /// <returns></returns>
        List<string[]> GetDatas();
    }
}