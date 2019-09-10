using MBM.DL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MBM.DL.Data
{
    /// <summary>
    /// Interface for the Stock Repository.
    /// </summary>
    public interface IStockRepository
    {
        /// <summary>
        /// List of all the records
        /// </summary>
        /// <returns>A List of all records</returns>
        Task<List<Stock>> GetAll();

        /// <summary>
        /// List of record dates
        /// </summary>
        /// <returns>a Distinct list of record dates</returns>
        Task<List<string>> GetDate();

        /// <summary>
        /// List of stock symbols
        /// </summary>
        /// <returns>Distinct list of symbols</returns>
        Task<List<string>> GetStockSymbols();
    }
}