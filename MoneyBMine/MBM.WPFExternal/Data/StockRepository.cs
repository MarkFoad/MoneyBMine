using MBM.WPFExternal.Models;
using MBM.WPFExternal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.WPFExternal.Data
{

    /// <summary>
    /// Stock Repository to use for data access.
    /// </summary>
    public class StockRepository : BaseReadOnlyRepository<Stock>, IStockRepository
    {
        /// <summary>
        /// Privately set instance of the Repository
        /// </summary>
        private static StockRepository instance;

        /// <summary>
        /// The model to be locked while getting the instance
        /// </summary>
        private static readonly Stock padlock = new Stock();

        /// <summary>
        /// Creating an instance of the repository
        /// </summary>
        public static StockRepository Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (padlock)
                    {
                        if(instance == null)
                        {
                            instance = new StockRepository();
                        }
                    }
                }
                return instance;
            }
        }


        public StockRepository(APIAccessService apiService) : base (apiService)
        {
            EndPointURI = "Stock/";
        }
    }
}
