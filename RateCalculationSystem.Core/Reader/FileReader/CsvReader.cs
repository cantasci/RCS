using System;
using System.Collections.Generic;
using System.IO;
using RateCalculationSystem.Core.Utils;

namespace RateCalculationSystem.Core.Reader.FileReader
{
    public class CsvReader : IReader
    {
        public CsvReader()
        {
            Init(Constants.DefaulFilename, Constants.DefaultDelimeter);
        }

        public CsvReader(string filePath, string delimeter)
        {
            Init(filePath, delimeter);
        }

        private void Init(string filePath, string delimeter)
        {
            Delimeter = delimeter;
            FilePath = filePath;
        }

        /// <summary>
        ///     Read csv file
        /// </summary>
        public void Read()
        {
            Headers = new List<string>();
            Datas = new List<string[]>();

            if (!File.Exists(FilePath))
                throw new Exception("Resource file could not find");

            var linesData = File.ReadAllLines(FilePath);
            if (linesData.Length > 0)
            {
                var line = linesData[0];
                var fieldData = line.Split(new[] {Delimeter}, StringSplitOptions.RemoveEmptyEntries);
                Headers.AddRange(fieldData);
            }

            for (var index = 1; index < linesData.Length; index++)
            {
                var line = linesData[index];
                var fieldData = line.Split(new[] {Delimeter}, StringSplitOptions.RemoveEmptyEntries);

                if (fieldData.Length > 0)
                    Datas.Add(fieldData);
            }
        }

        /// <summary>
        ///     Get all data
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetDatas()
        {
            return Datas;
        }
         
        #region Properties

        public string Delimeter { get; set; }
        public string FilePath { get; set; }
        public List<string> Headers { get; set; }
        public List<string[]> Datas { get; set; }

        #endregion
    }
}