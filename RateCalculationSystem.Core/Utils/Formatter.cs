using System.Collections.Generic;
using System.Text;
using RateCalculationSystem.Core.Models.Formatter;

namespace RateCalculationSystem.Core.Utils
{
    /// <summary>
    ///     Pretty printer for instance of class
    /// </summary>
    public class Formatter
    {
        private readonly List<FormatterItemModel> _displayedItems;

        public Formatter()
        {
            _displayedItems = new List<FormatterItemModel>();
        }

        /// <summary>
        ///     Add item
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Formatter Add(string title, string value)
        {
            _displayedItems.Add(new FormatterItemModel {Title = title, Value = value});
            return this;
        }

        /// <summary>
        ///     Print values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var displayedItem in _displayedItems)
                sb.AppendLine($"{displayedItem.Title}:  {displayedItem.Value}");
            return sb.ToString();
        }
    }
}