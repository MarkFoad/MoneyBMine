using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBM.WebAPI.Models
{
    /// <summary>
    /// Base object for all the models to use.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class DataObject<TKey>
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public TKey Id { get; set; }
    }
}