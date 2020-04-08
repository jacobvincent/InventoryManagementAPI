using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Product
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }
        [JsonProperty("name")]
        public  string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }
        [JsonProperty("maximumQuantity")]
        public int? MaximumQuantity { get; set; }
    }
}
