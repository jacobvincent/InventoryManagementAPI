using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public interface IInventoryServices
    {
        Task<List<Product>> GetProductLists(string apiUrl);
        Task<List<FxRate>> GetExchangeRates(string apiUrl);
        Task<Order> SubmitOrder(Order task, string apiUrl);
    }
}
