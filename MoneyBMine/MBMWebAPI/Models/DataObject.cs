// <copyright file="DataObject.cs" company="Skillage I.T.">
//     Copyright (c) Skillage I.T. All rights reserved.
// </copyright>
// <author> Mark Foad </author>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBM.WebAPI.Models
{
    /// <summary>
    /// Base Class for most models. Defines common properties.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class DataObject<TKey>
    {
        /// <summary>
        /// Gets or sets the Id of the Data Object.
        /// </summary>
        public TKey Id { get; set; }
    }
}
