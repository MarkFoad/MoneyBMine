// <copyright file="BaseRepository.cs" company="Skillage I.T.">
//     Copyright (c) Skillage I.T. All rights reserved.
// </copyright>
// <author> Mark Foad </author>
//-----------------------------------------------------------------------

using MBM.WebAPI.Models;
using MBM.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBM.WebAPI.Data
{

    /// <summary>
    /// The Base Repository for defining common methods and variables.
    /// </summary>
    /// <typeparam name="TModel">The Type of Model</typeparam>
    /// <typeparam name="TKey">The type of key</typeparam>
    public class BaseRepository<TModel, TKey> : IBaseRepository where TModel : DataObject<TKey>
    {
        /// <summary>
        /// The <see cref="DataSource"/> to use.
        /// </summary>
        private DataSource dataSource;


        public BaseRepository(DataAccessService dataAccessService, DataSource dataSource, string tableName)
        {
            Initialize(dataAccessService, dataSource, tableName);
        }
        /// <summary>
        /// Gets or sets the table name for the repository.
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Gets the data access service for the repository to use.
        /// </summary>
        protected DataAccessService DataAccessService { get; private set; }

        /// <summary>
        /// Initializes the base repository using required parameters.
        /// </summary>
        /// <param name="dataAccessService">The data access service to use </param>
        /// <param name="dataSource">The data source to use</param>
        /// <param name="tableName">The table name to use</param>
        private void Initialize(DataAccessService dataAccessService, DataSource dataSource, string tableName)
        {
            dataAccessService.DataSource = dataSource;
            DataAccessService = dataAccessService;

            TableName = tableName;
            this.dataSource = dataSource;
        }
    }
}
