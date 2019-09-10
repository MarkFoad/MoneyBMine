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
        
        public StockRepository(APIAccessService apiService) : base (apiService)
        {
            EndPointURI = "Stock/";
        }
    }
}
