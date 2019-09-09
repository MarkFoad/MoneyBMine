using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBM.DL.Data;
using MBM.DL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MBM.WebAPI.Controllers
{
    /// <summary>
    /// The Stock Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository stockRepository;
        /// <summary>
        /// Initializes an instance of the Stock Controller
        /// </summary>
        /// <param name="stockRepository"></param>
        public StocksController(IStockRepository stockRepository)
        {
            this.stockRepository = stockRepository;
        }

        // GET: api/Stocks
        /// <summary>
        /// GEt all stock exchange records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Stock>> Get()
        {
            return await stockRepository.GetAll();
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
