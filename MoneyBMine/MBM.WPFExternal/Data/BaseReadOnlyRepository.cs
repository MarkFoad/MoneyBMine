﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MBM.WPFExternal.Services;

namespace MBM.WPFExternal.Data
{
    public class BaseReadOnlyRepository<TModel>
    {
        /// <summary>
        /// The API access service.
        /// </summary>
        protected readonly APIAccessService ApiService;

        /// <summary>
        /// The endpoint URI
        /// </summary>
        private string endPointURI;

        /// <summary>
        /// Gets or sets the HttpClient to be used for API calls.
        /// </summary>
        public static HttpClient Client { get; set; } = new HttpClient();

        /// <summary>
        /// Gets or sets the end point URI for API calls (Format: EndPointName/)
        /// </summary>
        public string EndPointURI
        {
            get
            {
                return endPointURI;
            }

            set
            {
                endPointURI = value;
                Client.BaseAddress = new Uri(ApiService.APIBaseURI + EndPointURI);
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseReadOnlyRepository{TModel}"/> class.
        /// </summary>
        /// <param name="apiService">The API service to use.</param>
        public BaseReadOnlyRepository(APIAccessService apiService)
        {
            ApiService = apiService;
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("admin" + ":" + "admin")));
        }
        public BaseReadOnlyRepository(APIAccessService apiService, string endpointURI) 
        {
            ApiService = apiService;
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("admin" + ":" + "admin")));
            EndPointURI = endpointURI;
        }
    }
}
