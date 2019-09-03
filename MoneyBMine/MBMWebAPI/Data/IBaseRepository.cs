// <copyright file="IBaseRepository.cs" company="Skillage I.T.">
//     Copyright (c) Skillage I.T. All rights reserved.
// </copyright>
// <author> Mark Foad </author>
//-----------------------------------------------------------------------

namespace MBM.WebAPI.Data
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Gets or sets the table name the repository will deal with.
        /// </summary>
        string TableName { get; set; }
    }
}