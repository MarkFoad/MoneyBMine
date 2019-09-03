using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBM.WebAPI.Models
{
    public class Stock
    { /// <summary>
      /// Gets or sets the Stock Record Id
      /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the Stock Exchange  of the record
        /// </summary>
        public string StockExchange { get; set; }
        /// <summary>
        /// Gets or sets the Stock Symbol of the record
        /// </summary>
        public String StockSymbol { get; set; }
        /// <summary>
        /// Gets or sets the Date of the Record
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the Stock Price Open value 
        /// </summary>
        public double StockPriceOpen { get; set; }
        /// <summary>
        /// Gets or sets the Stock Price High Value
        /// </summary>
        public double StockPriceHigh { get; set; }
        /// <summary>
        /// Gets or sets the Stock Price Low Value
        /// </summary>
        public double StockPriceLow { get; set; }
        /// <summary>
        /// Gets or sets the Stock Price Close Value
        /// </summary>
        public double StockPriceClose { get; set; }
        /// <summary>
        /// Gets or sets the Stock Volume value
        /// </summary>
        public int StockVolume { get; set; }
        /// <summary>
        /// Gets or sets the Adjusted Stock Price Close
        /// </summary>
        public double StockPriceAdjClose { get; set; }

    }
}
