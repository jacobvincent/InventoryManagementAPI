using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Filters;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("v1/")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    //[ApiKeyAuth]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryServices _service;
        private const string baseUrl = "http://alltheclouds.com.au/";

        public InventoryController(IInventoryServices service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("SubmitOrder")]
        public async Task<IActionResult> SubmitOrder([FromBody] Order items)
        {
            var apiUrl = Path.Combine(baseUrl, "api/Orders");
            var orderItems = await _service.SubmitOrder(items, apiUrl);

            if (orderItems == null)
            {
                return NotFound();
            }

            return Ok(orderItems);
        }

        [HttpGet]
        [Route("GetProductLists")]
        public async Task<ActionResult<Product>> GetProductLists()
        {
            var apiUrl = Path.Combine(baseUrl, "api/Products");
            var products = await _service.GetProductLists(apiUrl);

            if (products?.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("GetExchangeRates")]
        public async Task<ActionResult<FxRate>> GetExchangeRates()
        {
            var apiUrl = Path.Combine(baseUrl, "api/fx-rates");
            var rates = await _service.GetExchangeRates(apiUrl);

            if (rates?.Count == 0)
            {
                return NotFound();
            }
            return Ok(rates);
        }
    }
}