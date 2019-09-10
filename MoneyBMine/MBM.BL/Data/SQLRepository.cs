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
        public AddRecordEventHandler AddRecordEventHandler = new AddRecordEventHandler();


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


        /// <summary>
        /// Sets the SQL Connection string
        /// </summary>
        protected static string sqlConnection = @"Data Source=.\;Initial Catalog=MoneyBMine;Persist Security Info=True;User ID=sa;Password=Mnementh79";

        /// <summary>
        /// Gets or sets the- table name to be used.
        /// </summary>
        //private string TableName = "StockExchange";
        private string TableName = "NYSEData";


        /// <summary>
        /// Adds a new record to the SQL database
        /// </summary>
        public async void AddRecord(Stock values)
        {
            Stock stock = values;
            try
            {

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

                // Makes sure the connection is closed on completion.
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error writing record to the database", ex);
            }

        }
        /// <summary>
        /// Adding List of records into SQL database (bulk)
        /// </summary>
        /// <param name="stock"></param>
        public async void AddRecord(List<Stock> stock)
        {
            try
            {
                List<Stock> Stocks = await GetAll();

                SqlConnection connection = new SqlConnection(sqlConnection);
                connection.Open();
                //Building the query sting and parameters
                string query = $"INSERT INTO [MoneyBMine].[dbo].[{TableName}] ([StockExchange], [StockSymbol], [Date], [StockPriceOpen], [StockPriceHigh], [StockPriceLow], [StockPriceClose], [StockVolume], [StockPriceAdjClose]) ";
                query += " Values (@StockExchange, @StockSymbol, @Date, @StockPriceOpen, @StockPriceHigh,@StockPriceLow, @StockPriceClose, @StockVolume, @StockPriceAdjClose)";
                if (connection.State == ConnectionState.Open)
                {
                    for (int i = 0; i < stock.Count; i++)
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

                        AddRecordEventHandler.RecordCountEvent();
                    }
                }

                // Makes sure the connection is closed on completion.
                connection.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Bulk write records to SQL database", ex);
            }
        }

        /// <summary>
        /// Gets a list of all records
        /// </summary>
        /// <returns>Enumerable List of all records</returns>
        public async Task<List<Stock>> GetAll()
        {
            List<Stock> stocks = new List<Stock>();
            string query = $"Select * from [MoneyBMine].[dbo].[{TableName}]";
            try
            {

                SqlConnection connection = new SqlConnection(sqlConnection);
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
        public async Task<List<string>> GetDates()
        {
            List<string> dates = new List<string>();
            string query = $"Select Distinct Date From [MoneyBMine].[dbo].[{TableName}] Order By [Date] desc";

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection))
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

        public async Task<List<Stock>> GetBySymbol(string symbol)
        {
            List<Stock> stocks = new List<Stock>();
            string query = $"Select * from [MoneyBMine].[dbo].[{TableName}] where [StockSymbol] ='{symbol}' order by [Date] desc";
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                await StockReader(stocks, command);
                connection.Close();
            }
            return stocks;
        }

        /// <summary>
        /// Gets all the records for the date specified
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A list of records for a specified date</returns>
        public async Task<List<Stock>> GetByDate(DateTime date, string symbol)
        {
            try
            {
                List<Stock> stocks = new List<Stock>();
                string query = "";
                string dateString = date.ToString("yyyy-MM-dd");
                if (date != null && symbol == string.Empty)
                {
                    query = $"Select * from [MoneyBMine].[dbo].[{TableName}] where [Date] ='{dateString}' order by [Date] desc";
                }
                else
                {
                    // if a stock symbol was slected this query string is used.
                    if (date != null && symbol != string.Empty)
                    {
                        query = $"Select * from [MoneyBMine].[dbo].[{TableName}] where [Date] ='{dateString}' AND StockSymbol = '{symbol}' order by [Date] desc";
                    }
                }

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    await StockReader(stocks, command);
                    connection.Close();
                }
                return stocks;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to return records for the date selected", ex);
            }
        }

        /// <summary>
        /// Gets all records bewtween the dates specified.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="finishDate"></param>
        /// <returns>a List of all record between specified dates.</returns>
        public async Task<List<Stock>> GetByDate(DateTime startDate, DateTime finishDate, string symbol)
        {

            List<Stock> stocks = new List<Stock>();
            string finishDateString;
            string startDateString;
            try
            {
                string query = "";
                if (startDate != null)
                {
                    if (startDate > finishDate)
                    {
                        startDateString = finishDate.ToString("yyyy-MM-dd");
                        finishDateString = startDate.ToString("yyyy-MM-dd");


                    }
                    else
                    {

                        startDateString = startDate.ToString("yyyy-MM-dd");
                        finishDateString = finishDate.ToString("yyyy-MM-dd");
                    }

                    if (symbol == string.Empty)
                    {

                        query = $"SELECT * FROM [MoneyBMine].[dbo].[{TableName}] WHERE [Date] >='{startDateString}' AND [Date] <= '{finishDateString}' ORDER BY [Date] desc";
                    }
                    else
                    {
                        query = $"SELECT * FROM [MoneyBMine].[dbo].[{TableName}] WHERE [Date] >='{startDateString}' AND [Date] <= '{finishDateString}' AND StockSymbol = '{symbol} 'ORDER BY [Date] desc";
                    }
                    using (SqlConnection connection = new SqlConnection(sqlConnection))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        await StockReader(stocks, command);
                        connection.Close();
                    }
                }
                return stocks;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to return records between dates selected", ex);
            }
        }


        public async Task<List<string>> GetStockSymbols()
        {
            List<Stock> stocks = new List<Stock>();
            List<string> symbols = new List<string>();
            symbols.Add("");

            string query = $"Select Distinct StockSymbol From [MoneyBMine].[dbo].[{TableName}]";
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        symbols.Add((string)reader["StockSymbol"]);
                    }
                }

            }
            return symbols;
        }

        /// <summary>
        /// Gets the memory usage from the SQLK database server 
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetMemoryUtilization()
        {
            List<double> memoryInUse = new List<double>();
            try
            {

                string query = "declare @PhysicalMemoryInUseKB bigint declare @totalSystemMemoryBytes bigint " +
                    "SELECT @PhysicalMemoryInUseKB = physical_memory_in_use_kb from sys.dm_os_process_memory " +
                    "Select @totalSystemMemoryBytes = physical_memory_kb from sys.dm_os_sys_info " +
                    "select convert (float, @physicalMemoryInUseKB) * 1024 / convert(float, @totalSystemMemoryBytes) as memory_usage";

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            double memory;
                            memory = (double)reader["memory_usage"];

                            memoryInUse.Add(memory);
                        }
                    }
                    connection.Close();
                }
                return memoryInUse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to return Memory utilization values!", ex);
            }
        }

        public async Task<double> GetHDDFree()
        {
            List<double> spaceFree = new List<double>();
            string query = $"SELECT Distinct CONVERT(float,dovs.available_bytes/1048576.0) AS FreeSpaceInMB FROM sys.master_files mf " +
                $"CROSS APPLY sys.dm_os_volume_stats(mf.database_id, mf.FILE_ID) dovs";
            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        double memory;
                        memory = (double)reader["FreeSpaceInMB"];

                        spaceFree.Add(memory);
                    }
                }
            }
            return spaceFree.FirstOrDefault();
        }

    }
}
