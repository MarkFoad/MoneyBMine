using MBM.BL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL.SQL
{
    public class SQL
    {
        /// <summary>
        /// Sets the SQL Connection string
        /// </summary>
        protected static string sqlConnection = @"Data Source=.\;Initial Catalog=MoneyBMine;Persist Security Info=True;User ID=sa;Password=admin";

        /// <summary>
        /// Gets or sets the- table name to be used.
        /// </summary>
        private string TableName = "NYSEData";

        /// <summary>
        /// Adds a new record to the SQL database
        /// </summary>
        public async void AddRecord()
        {
            Stock stock = new Stock();
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            //Building the query sting and parameters
            string query = $"INSERT INTO [MoneyBMine].[dbo].[{TableName}] ([StockExchange], [StockSymbol], [Date], [StockPriceOpen], [StockPriceHigh], [StockPriceLow], [StockPriceClose], [StockVolume], [StockPriceAdjClose]) ";
            query += " Values (@StockExchange, @StockSymbol, @Date, @StockPriceOpen, @StockPriceHigh,@StockPriceLow, @StockPriceClose, @StockVolume, @StockPriceAdjClose)";

            if (connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StockExchange", stock.StockExchange);
                command.Parameters.AddWithValue("@StockSymbol", stock.StockSymbol);
                command.Parameters.AddWithValue("@Date", stock.Date);
                command.Parameters.AddWithValue("@StockPriceOpen", stock.StockPriceOpen);
                command.Parameters.AddWithValue("@StockPriceHigh", stock.StockPriceHigh);
                command.Parameters.AddWithValue("@StockPriceLow", stock.StockPriceLow);
                command.Parameters.AddWithValue("@StockPriceClose", stock.StockPriceClose);
                command.Parameters.AddWithValue("@StockVolume", stock.StockVolume);
                command.Parameters.AddWithValue("@StockPriceAdjClose", stock.StockPriceAdjClose);
                await command.ExecuteNonQueryAsync();

            }
            else
            {
                throw new Exception("Connection to Database failed");
                   
            }
            // Makes sure the connection is closed on completion.
            connection.Close();

        }

        /// <summary>
        /// Gets a list of all records
        /// </summary>
        /// <returns>Enumerable List of all records</returns>
        public async Task<List<Stock>> GetAll()
        {
            List<Stock> stockList = new List<Stock>();
            string query  = $"Select * from [MoneyBMine].[dbo].[{TableName}]";
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            if(connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand(query, connection);
                using(SqlDataReader row = await command.ExecuteReaderAsync())
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
                        stockList.Add(stock);
                     }
                }
            }

            return stockList;
        } 
    }
}
