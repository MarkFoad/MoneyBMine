using System;
using MBM.DL.Models;
using MBM.DL.Services;

namespace MBM.DL.Data
{
    /// <summary>
    /// Base Class repository to be used.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BaseRepository<TModel> : IBaseRepository where TModel : class
    {
        /// <summary>
        /// Gets or sets the Table Name to be used
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// The database to use.
        /// </summary>
        private string databaseName;

        /// <summary>
        /// Gets or sets the Data Access Service for the repository to use.
        /// </summary>
        public DataAccessService DataAccessService { get; private set; }

        /// <summary>
        /// Initializes a new instance to the BaseReposiotry Class.
        /// </summary>
        /// <param name="dataAccessService">The data access service to use, provided by Dependency injection</param>
        /// <param name="databaseName">The data source to use</param>
        /// <param name="tableName">The table name of the data source./</param>
        public BaseRepository(DataAccessService dataAccessService, string databaseName, string tableName)
        {
            Initialize(dataAccessService, databaseName, tableName);
        }

        /// <summary>
        /// Initializes the base repository using the required parameters.
        /// </summary>
        /// <param name="dataAccessService"></param>
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        private void Initialize(DataAccessService dataAccessService,string databaseName, string tableName)
        {
            dataAccessService.DatabaseName = databaseName;
            DataAccessService = dataAccessService;

            TableName = tableName;
            this.databaseName = databaseName;
        }
    }
}