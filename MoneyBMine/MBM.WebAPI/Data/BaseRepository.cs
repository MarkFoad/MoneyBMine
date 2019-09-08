using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBM.WebAPI.Data
{
    public class BaseRepository<TModel> : IBaseRepository where TModel : class
    {
        /// <summary>
        /// Gets or sets the table name for the repository to use.
        /// </summary>
        public string TableName { get; set; }
    }
}
