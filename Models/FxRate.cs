using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class FxRate
    {
        [JsonProperty("sourceCurrency")]
        public string SourceCurrency { get; set; }
        [JsonProperty("targetCurrency")]
        public  string TargetCurrency { get; set; }
        [JsonProperty("rate")]
        public double Rate { get; set; }

    }
}
