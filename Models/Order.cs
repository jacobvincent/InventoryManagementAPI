using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Order
    {
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
        [JsonProperty("customerEmail")]
        public  string CustomerEmail { get; set; }
        [JsonProperty("lineItems")]
        public OrderLineItem LineItems { get; set; }

    }
}
