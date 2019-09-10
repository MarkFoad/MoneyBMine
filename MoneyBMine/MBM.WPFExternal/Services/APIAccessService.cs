using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.WPFExternal.Services
{
    public class APIAccessService
    {
        /// <summary>
        /// Gets or sets the URI for the API calls. e.g. www.www.someapiwebsite.com.com
        /// </summary>
        public string BaseURI { get; set; }

        /// <summary>
        /// Gets or sets the api base URI for API calls. e.g. www.someapiwebsite.com/api/
        /// </summary>
        public string APIBaseURI { get; set; }

        public APIAccessService(string baseURI, string apiBaseURI)
        {
            BaseURI = baseURI;
            APIBaseURI = apiBaseURI;
        }
    }
}
