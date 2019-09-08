using MBM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MBM.WebAPI.Data
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository()
        {

        }

        /// <summary>
        /// Sets the database table name to be used.
        /// </summary>
        private string tableName = "StockExchange";

        public async Task<List<Stock>> GetAll()
        {
            List<Stock> stocks = new List<Stock>();
            string query = $"SELECT * FROM [dbo].[{tableName}]";
            try
            {

                SqlConnection connection = new SqlConnection();
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    await StockReader(stocks, command);
                }
                connection.Close();
                return stocks;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to retrieve data from database", ex);
            }
        }


        /// <summary>
        /// Populates a list.
        /// </summary>
        /// <param name="stockList"></param>
        /// <param name="command"></param>
        /// <returns>List of StockExchange Records.</returns>
        private static async Task StockReader(List<Stock> stocks, SqlCommand command)
        {
            try
            {

                using (SqlDataReader row = await command.ExecuteReaderAsync())
                {
                    while (row.Read())
                    {
                        // New empty instance of the stock
                        Stock stock = new Stock();

                        // Populating each value from the database of the "Stock" Container
                        stock.Id = (int)row["Id"];
                        stock.StockExchange = (string)row["StockExchange"];
                        stock.StockSymbol = (string)row["StockSymbol"];
                        stock.Date = (DateTime)row["Date"];
                        stock.StockPriceOpen = (double)row["StockPriceOpen"];
                        stock.StockPriceHigh = (double)row["StockPriceHigh"];
                        stock.StockPriceLow = (double)row["StockPriceLow"];
                        stock.StockPriceClose = (double)row["StockPriceClose"];
                        stock.StockVolume = (int)row["StockVolume"];
                        stock.StockPriceAdjClose = (double)row["StockPriceAdjClose"];

                        // Adds the record details to the stockList 
                        stocks.Add(stock);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading from database - reader", ex);
            }
        }

       
    }
}
