using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public class InventoryServices : IInventoryServices
    {

        private readonly IConfiguration _configuration;


        private readonly HttpClient _client;
        public InventoryServices(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<FxRate>> GetExchangeRates(string apiUrl)
        {
            var apiKey = _configuration.GetSection("api-key").Value;
            string contentType = "application/json";
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            var userAgent = "d-fens HttpClient";
            _client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            _client.DefaultRequestHeaders.Add("api-key", apiKey);
            var httpResponse = await _client.GetAsync(apiUrl);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve the rates");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var fxRates = JsonConvert.DeserializeObject<List<FxRate>>(content);

            return fxRates;
        }

        public async Task<List<Product>> GetProductLists(string apiUrl)
        {

            //var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = _configuration.GetSection("api-key").Value;
            string contentType = "application/json";
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            var userAgent = "d-fens HttpClient";
            _client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            _client.DefaultRequestHeaders.Add("api-key", apiKey);

            var httpResponse = await _client.GetAsync(new Uri(apiUrl));

            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve the products");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);

            return products;
        }

        public async Task<Order> SubmitOrder(Order order, string apiUrl)
        {
            var content = JsonConvert.SerializeObject(order);
            var apiKey = _configuration.GetSection("api-key").Value;
            string contentType = "application/json";
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            var userAgent = "d-fens HttpClient";
            _client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            _client.DefaultRequestHeaders.Add("api-key", apiKey);

            var httpResponse = await _client.PostAsync(apiUrl, new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to submit an order");
            }

            var placedOrder = JsonConvert.DeserializeObject<Order>(await httpResponse.Content.ReadAsStringAsync());
            return placedOrder;
        }
    }
}
