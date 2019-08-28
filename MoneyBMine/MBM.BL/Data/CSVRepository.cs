using MBM.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL.Data
{
    public class CSVRepository
    {
        /// <summary>
        /// Privately set instance value.
        /// </summary>
        private static CSVRepository instance;
        /// <summary>
        /// enabling the temporary lockout.
        /// </summary>
        private static readonly Stock padlock = new Stock();

        /// <summary>
        /// Private initiation of the CSV Repository
        /// </summary>
        private CSVRepository()
        {

        }

        /// <summary>
        /// Gets an instance of the CSV Data
        /// </summary>
        public static CSVRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new CSVRepository();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets a public list of the records from the CSV file read.
        /// </summary>
        public List<Stock> StockList { get; set; } = new List<Stock>();

        /// <summary>
        /// Reads and populates a list of all the records from a CSV file selected/Passed.
        /// </summary>
        /// <param name="filename"></param>
        public async Task<List<Stock>> ReadCSV(string filepath)
        {
            List<Stock> stocks = new List<Stock>();
            try
            {

                if (File.Exists(filepath))
                {
                    List<string> rows = File.ReadAllLines(filepath).ToList();
                    // Loops through the CSV file and skips the first line as this is headings.
                    foreach (var row in rows.Skip(1))
                    {
                        // Splits the row string into an array using the , as the separator.
                        string[] entry = row.Split(',');
                        Stock stock = new Stock();
                        // populates the model with data from each row of the CSV file.
                        stock.StockExchange = (string)entry[0];
                        stock.StockSymbol = (string)entry[1];
                        stock.Date = DateTime.Parse(entry[2]);
                        stock.StockPriceOpen = double.Parse(entry[3]);
                        stock.StockPriceHigh = double.Parse(entry[4]);
                        stock.StockPriceLow = double.Parse(entry[5]);
                        stock.StockPriceClose = double.Parse(entry[6]);
                        stock.StockVolume = int.Parse(entry[7]);
                        stock.StockPriceAdjClose = double.Parse(entry[8]);

                        // Adds the records to the StockList.
                        stocks.Add(stock);
                    }

                }
            return stocks;
            }
            catch (Exception ex)
            {
                throw new Exception("File was not for or able to be loaded", ex);
            }
        }


    }
}
