﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBM.DL.Data;
using MBM.DL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MBM.WebAPI.Controllers
{
    /// <summary>
    /// The Stock Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
   // [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository stockRepository;
        /// <summary>
        /// Initializes an instance of the Stock Controller
        /// </summary>
        /// <param name="stockRepository"></param>
        public StocksController(IStockRepository stockRepository,ILogger<Stock> logger)
        {
            this.stockRepository = stockRepository;
        }

        // GET: api/Stocks
        /// <summary>
        /// GEt all stock exchange records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Stock>> Get()
        {
            return await stockRepository.GetAll();
        }

        /// <summary>
        /// Gets distinct Dates
        /// </summary>
        /// <returns>List of Dates in string format</returns>
        [HttpGet("GetDates")]
        public async Task<List<string>> GetDates()
        {
            return  await stockRepository.GetDate();
        }
        // // GET: api/Stocks/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(int id)
        // {
        //     return "value";
        // }
        // 
        // // POST: api/Stocks
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }
        // 
        // // PUT: api/Stocks/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
        // 
        // // DELETE: api/ApiWithActions/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
