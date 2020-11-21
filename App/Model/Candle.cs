using App.Enums;
using Newtonsoft.Json;
using System;

namespace App.Model
{
    public class Candle
    {  
        public string Id { 
            get
            {
                return $"{Symbol}-{Timestamp}";
            }
            private set { }
        }
        public string Symbol { get; set; }
        public AssetType SymbolType { get; set; }
        public string Resolution { get; set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        [JsonProperty("o")]
        public Decimal OpenPrice { get; set; }
        [JsonProperty("h")]
        public Decimal HighestPrice { get; set; }
        [JsonProperty("l")]
        public Decimal LowestPrice { get; set; }
        [JsonProperty("c")]
        public Decimal ClosingPrice { get; set; }
        [JsonProperty("t")]
        public DateTime Timestamp { get; set; }
    }
}
