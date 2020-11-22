using App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace App.Services
{
    internal class FinnhubCandleResponse
    {
        [JsonProperty("o")]
        private decimal[] OpenPrice { get; set; }
        [JsonProperty("h")]
        private decimal[] HighestPrice { get; set; }
        [JsonProperty("l")]
        private decimal[] LowestPrice { get; set; }
        [JsonProperty("c")]
        private decimal[] ClosingPrice { get; set; }
        [JsonProperty("v")]
        private int[] Volume { get; set; }
        [JsonProperty("t")]
        private double[] Timestamp { get; set; }

        internal List<Candle> Translate()
        {
            List<Candle> candles = new List<Candle>();

            for (int i = 0; i < ClosingPrice.Length; i++)
            {
                Candle candle = new Candle(OpenPrice[i], HighestPrice[i], LowestPrice[i], ClosingPrice[i], Volume[i], Timestamp[i]);

                candles.Add(candle);
            }

            return candles;
        }
    }
}