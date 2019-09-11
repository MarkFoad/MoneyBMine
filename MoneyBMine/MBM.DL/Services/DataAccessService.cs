using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL.Services
{
    /// <summary>
    /// Data Access service, used for all data in and out.
    /// </summary>
    public class DataAccessService
    {
        /// <summary>
        /// The connection string to use when connection to the database.
        /// </summary>
        private Dictionary<string, string> connectionStrings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessService"/> class.
        /// </summary>
        /// <param name="connectionStrings"></param>
        public DataAccessService(Dictionary<string,string> connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }

        /// <summary>
        /// Gets or set the Database to use
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets a command to return all records from the specified table name.
        /// </summary>
        /// <param name="tableName">The Table name to use</param>
        /// <returns>AN SQL command for retrieving all records.</returns>
        public static SqlCommand GetAllCommand(string tableName)
        {
            return new SqlCommand($"SELECT * FROM [dbo].[{tableName}]");
        }

        /// <summary>
        /// Prepares a <see cref="SqlDataReader" /> using the specified command and returns it.
        /// </summary>
        /// <param name="sqlCommand">The command to execute</param>
        /// <returns>The reader</returns>
        public SqlDataReader GetReader(string sqlCommand)
        {
            return GetReader(new SqlCommand(sqlCommand));
        }

        /// <summary>
        /// Prepares a <see cref="SqlDataReader" /> using the specified command and returns it.
        /// </summary>
        /// <param name="sqlCommand">The command to execute</param>
        /// <returns>The reader</returns>
        public SqlDataReader GetReader(SqlCommand sqlCommand)
        {
            SqlConnection connection = new SqlConnection(connectionStrings[DatabaseName]);
            connection.Open();
            sqlCommand.Connection = connection;

            // When the user of this calls myReader.Close() method the underlying database connection is also to be closed.
            SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

    }
}
