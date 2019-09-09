using MBM.DL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL.Data
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        /// <summary>
        /// Sets the Table Name of the database to use for this repository.
        /// </summary>
        private string tableName = "StockExchange";
        public StockRepository()
        {

        }

        /// <summary>
        /// Sets the connection string to the Database
        /// </summary>
        protected static string connectionString = @"Data Source=.\;Initial Catalog=MoneyBMine;Persist Security Info=True;User ID=sa;Password=Mnementh79";

        public async Task<List<Stock>> GetAll()
        {
            List<Stock> stocks = new List<Stock>();
            string query = $"Select * from [MoneyBMine].[dbo].[{tableName}]";
            try
            {

                SqlConnection connection = new SqlConnection(connectionString);
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

        /// <summary>
        /// Gets a list of dates from the SQL Database.
        /// </summary>
        /// <returns>List of Dates in Descending order form SQL Database</returns>
        public async Task<List<string>> GetDate()
        {
            List<string> dates = new List<string>();
            string query = $"Select Distinct Date From [MoneyBMine].[dbo].[{TableName}] Order By [Date] desc";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            DateTime date = new DateTime();
                            date = (DateTime)reader["Date"];
                            dates.Add(date.ToShortDateString());
                        }

                    }
                    connection.Close();
                }
                return dates;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading database to get record dates", ex);
            }
        }

        /// <summary>
        /// A list of records that match the symbol specified
        /// </summary>
        /// <param name="symbol">Symbol to look up</param>
        /// <returns>List of Records that match the specified symbol.</returns>
        public async Task<List<Stock>> GetBySymbol(string symbol)
        {
            List<Stock> stocks = new List<Stock>();
            string query = $"Select * from [MoneyBMine].[dbo].[{tableName}] where [StockSymbol] ='{symbol}' order by [Date] desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                await StockReader(stocks, command);
                connection.Close();
            }
            return stocks;
        }
    }
}
