using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurrencyListApp.Models
{
    /// <summary>
    /// Model for Currency API
    /// </summary>
    public class ExchangeRate
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonProperty("license")]
        public Uri License { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }

        public static ExchangeRate FromJson(string json) => 
            JsonConvert.DeserializeObject<ExchangeRate>(json, JsonDataConverter.Settings);
    }
}