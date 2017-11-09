using System.Collections.Generic;

namespace RateCalculationSystem.Core.Reader
{
    public interface IReader
    {
        /// <summary>
        ///     Read all lines of file
        /// </summary>
        void Read();

        /// <summary>
        ///     Get result data
        /// </summary>
        /// <returns></returns>
        List<string[]> GetDatas();
    }
}