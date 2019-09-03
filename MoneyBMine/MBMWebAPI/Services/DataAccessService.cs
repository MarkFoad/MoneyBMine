//-----------------------------------------------------------------------
// <copyright file="DataAccessService.cs" company="Skillage I.T.">
//     Copyright (c) Skillage I.T.. All rights reserved.
// </copyright>
// <author> Mark Foad </author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MBM.WebAPI.Services
{
    /// <summary>
    /// Data access service, used for all data access in and out
    /// </summary>
    public class DataAccessService
    {
        /// <summary>
        /// Used to determine the connection sting to be used.
        /// </summary>
        private DataSource dataSource = DataSource.MoneyBMine;

        /// <summary>
        /// The connection string to use when connecting to the database.
        /// </summary>
        private Dictionary<DataSource, string> connectionStrings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessService"/> class.
        /// </summary>
        /// <param name="connectionStrings">The connection strings to use</param>
        public DataAccessService(Dictionary<DataSource, string> connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }

        /// <summary>
        /// Gets or sets the <see cref="DataSource"/> to use.
        /// </summary>
        public DataSource DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }

        /// <summary>
        /// Gets a SQL command to "Get All" records from the specified tableName;
        /// </summary>
        /// <param name="tableName">The Table Name</param>
        /// <returns>AN SQL Command for returning records.</returns>
        public static SqlCommand GetAllCommand(string tableName)
        {
            return new SqlCommand($"SELECT * FROM [dbo].[{tableName}]");
        }

        /// <summary>
        /// prepares a <see cref="SqlDataReader"/> using the specified command and returns the reader command string.
        /// </summary>
        /// <param name="sqlCommand">The command to execute</param>
        /// <returns>The reader</returns>
        public SqlDataReader GetReader(string sqlCommand)
        {
            return GetReader(new SqlCommand(sqlCommand));
        }

        /// <summary>
        /// Prepares the <see cref="SqlDataReader"/> using the specified command and returns the reader.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public SqlDataReader GetReader(SqlCommand sqlCommand)
        {
            SqlConnection connection = new SqlConnection(connectionStrings[DataSource]);
            connection.Open();
            sqlCommand.Connection = connection;

            // When the user of this calls myReader.Close() method the underlying database connection is also closed.           
            SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        

    }
}
