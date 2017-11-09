using System.Collections.Generic;
using System.IO;
using System.Linq;
using RateCalculationSystem.ConsoleApplication.Models;
using RateCalculationSystem.Core.Reader;
using RateCalculationSystem.Core.Reader.FileReader;

namespace RateCalculationSystem.ConsoleApplication.Helper
{
    public class FileHelper
    {
        /// <summary>
        ///     Fetch Lender Marker Data
        /// </summary>
        /// <param name="filePath">path of market data file</param>
        /// <returns></returns>
        public static List<MarketData> GetLenderMarketData(string filePath)
        {
            IReader reader = new CsvReader(filePath, ",");
            reader.Read();

            var result = new List<MarketData>();
            foreach (var rowData in reader.GetDatas())
                result.Add(new MarketData
                {
                    Lender = rowData[0],
                    Rate = double.Parse(rowData[1]),
                    Available = int.Parse(rowData[2])
                });

            return result;
        }

        /// <summary>
        ///     Fetch Lender Marker Data with third party
        /// </summary>
        /// <param name="filePath">path of market data file</param>
        /// <returns></returns>
        public static List<MarketData> GetLenderMarketDataWithCsvHelper(string filePath)
        {
            using (var fs = File.OpenRead(filePath))
            {
                var csv = new CsvHelper.CsvReader(new StreamReader(fs));
                return csv.GetRecords<MarketData>().ToList();
            }
        }
    }
}