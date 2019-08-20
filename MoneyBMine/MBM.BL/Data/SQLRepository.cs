using MBM.BL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL.Data
{
    public class SQLRepository
    {
        /// <summary>
        /// privately set instance of the repository
        /// </summary>
        private static SQLRepository instance;
        /// <summary>
        /// the model to use and lock while getting the instance.
        /// </summary>
        private static readonly Stock padlock = new Stock();
        /// <summary>
        /// Initiate the repository.
        /// </summary>
        private SQLRepository()
        {

        }

        /// <summary>
        /// Creating an instance of the repository
        /// </summary>
        public static SQLRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SQLRepository();
                        }
                    }
                }
                return instance;
            }
        }

        public AddRecordEventHandler AddRecordEventHandler = new AddRecordEventHandler();

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
        public async void AddRecord(Stock values)
        {
            Stock stock = values;
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
                AddRecordEventHandler.AddCompleteEvent();
            }
            else
            {
                throw new Exception("Connection to Database failed");

            }
            // Makes sure the connection is closed on completion.
            connection.Close();

        }

        public async void AddRecord(List<Stock> values)
        {
            List<Stock> stock = values;
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            //Building the query sting and parameters
            string query = $"INSERT INTO [MoneyBMine].[dbo].[{TableName}] ([StockExchange], [StockSymbol], [Date], [StockPriceOpen], [StockPriceHigh], [StockPriceLow], [StockPriceClose], [StockVolume], [StockPriceAdjClose]) ";
            query += " Values (@StockExchange, @StockSymbol, @Date, @StockPriceOpen, @StockPriceHigh,@StockPriceLow, @StockPriceClose, @StockVolume, @StockPriceAdjClose)";
            for (int i = 0; i < stock.Count; i++)
            {

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StockExchange", stock[i].StockExchange);
                    command.Parameters.AddWithValue("@StockSymbol", stock[i].StockSymbol);
                    command.Parameters.AddWithValue("@Date", stock[i].Date);
                    command.Parameters.AddWithValue("@StockPriceOpen", stock[i].StockPriceOpen);
                    command.Parameters.AddWithValue("@StockPriceHigh", stock[i].StockPriceHigh);
                    command.Parameters.AddWithValue("@StockPriceLow", stock[i].StockPriceLow);
                    command.Parameters.AddWithValue("@StockPriceClose", stock[i].StockPriceClose);
                    command.Parameters.AddWithValue("@StockVolume", stock[i].StockVolume);
                    command.Parameters.AddWithValue("@StockPriceAdjClose", stock[i].StockPriceAdjClose);
                    await command.ExecuteNonQueryAsync();
                    AddRecordEventHandler.AddCompleteEvent();

                }
                else
                {
                    throw new Exception("Connection to Database failed");

                }
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
            string query = $"Select * from [MoneyBMine].[dbo].[{TableName}]";
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand(query, connection);
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
                        stockList.Add(stock);
                    }
                }
            }

            return stockList;
        }
    }
}
