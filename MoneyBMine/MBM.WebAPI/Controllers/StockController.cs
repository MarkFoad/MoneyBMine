using MBM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MBM.WebAPI.Controllers
{
    public class StockController : ApiController
    {
        List<Stock> stock = new List<Stock>();
        public StockController()
        {

        }

        // GET api/<controller>
        public List<Stock> Get()
        {


            return stock;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}