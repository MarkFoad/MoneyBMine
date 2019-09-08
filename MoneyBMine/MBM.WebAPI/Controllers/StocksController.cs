using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBM.WebAPI.Data;
using MBM.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase, IStockRepository
    {
        private readonly IStockRepository stockRepository;

        public StocksController(IStockRepository stocksRepository)
        {
            this. stockRepository = stockRepository;
        }

        public string TableName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [HttpGet("Get")]
        public async Task<List<Stock>> GetAll()
        {
            var stocks = await stockRepository.GetAll()  ;
            return stocks;
        }
    }
}