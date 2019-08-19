using MBM.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL.CSV
{
    public class CSV
    {
        /// <summary>
        /// Gets or sets a public list of the records from the CSV file read.
        /// </summary>
        public List<Stock> StockList { get; set; } = new List<Stock>();

        /// <summary>
        /// Gets the location of the currently running application
        /// </summary>
        /// <returns>Folder that the program is currently running in.</returns>
        private async Task<string> GetFilePath()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            string filepath = Path.GetDirectoryName(path);
            return filepath;
        }


        /// <summary>
        /// Reads and populates a list of all the records from CSV file.
        /// </summary>
        /// <param name="filename"></param>
        public async void ReadCSV(string filename)
        {
            // Gets the running location of program
            string filepath = await GetFilePath();
            // adds the supplied filename to be looked up.
            filepath += filename;
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
                    StockList.Add(stock);

                }
            }
            else
            {
                // Throws an error messages if the file is not found.
                throw new Exception($"File {filename} was not found at location {filepath} ");
            }
        }


    }
}
