using MBM.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MBM.WebAPI.Data
{
    public interface IStockRepository : IBaseRepository 
    {
        Task<List<Stock>> GetAll();
    }
}