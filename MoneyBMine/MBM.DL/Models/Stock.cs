using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL.Models
{
    /// <summary>
    /// A Stock Exchange Record.
    /// </summary>
    public class Stock : DataObject<int>
    {
        /// <summary>
        /// Gets or sets the Stock Exchange.
        /// </summary>
        public string StockExchange { get; set; }

        /// <summary>
        /// Gets or sets the stock Symbol
        /// </summary>
        public string StockSymbol { get; set; }
        
        /// <summary>
        /// Gets or sets the Date Value.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the Stock Price Open Value
        /// </summary>
        public double StockPriceOpen { get; set; }

        /// <summary>
        /// Gets or sets the Stock Price High Value
        /// </summary>
        public double StockPriceHigh { get; set; }

        /// <summary>
        /// Gets or sets the Stock Price low value
        /// </summary>
        public double StockPriceLow { get; set; }

        /// <summary>
        /// Gets or sets the Stock Price Close Value
        /// </summary>
        public double StockPriceClose { get; set; }
        
        /// <summary>
        /// Gets or sets the Stock Volume
        /// </summary>
        public int StockVolume { get; set; }
        /// <summary>
        /// Gets or sets the Stock Price Adjusted Close Value.
        /// </summary>
        public double StockPriceAdjClose { get; set; }
    }
}
