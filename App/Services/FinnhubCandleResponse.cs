using App.Model;
using App.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace App.Services
{
    /// <summary>
    /// Transport class used to convert Finnhub json response to Candles
    /// </summary>
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
            try
            {
                List<Candle> candles = new List<Candle>();

                if(ClosingPrice != null)
                {
                    for (int i = 0; i < ClosingPrice.Length; i++)
                    {
                        Candle candle = new Candle(OpenPrice[i], HighestPrice[i], LowestPrice[i], ClosingPrice[i], Volume[i], Timestamp[i]);

                        candles.Add(candle);
                    }
                }


                return candles;
            } catch(Exception e)
            {
                UserInterface.Message($"Something went wrong when attempting to translate the response from Finnub: {e.Message}");
                return null;
            }

        }
    }
}