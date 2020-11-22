﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using App.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App.Enums;
using App.UI;

namespace App.Services
{
    public sealed class FinnhubClient
    {
        private static FinnhubClient instance = null;
        private static readonly object padlock = new object();
        private string ApiKey { 
            get
            {
                return Environment.GetEnvironmentVariable("VPR_Eksamensopgave_FinnhubApiKey");
            }
        }
        private string BaseUrl = "https://finnhub.io/api/v1";

        private FinnhubClient() {

        }

        public static FinnhubClient Instance
        {
            get{
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new FinnhubClient();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Stock>> GetStocksFromExchange(string exchange)
        {
            string url = $"/stock/symbol?exchange={exchange}";

            string response = await RequestFinnhubData(url);

            return JsonConvert.DeserializeObject<List<Stock>>(response);
        }

        public async Task<List<Candle>> GetCandlesForSymbol(Asset asset, long from, long to, string resolution = "D")
        {
            string url = $"/{asset.AssetType.ToString().ToLower()}/candle?symbol={asset.Symbol.ToLower()}&resolution={resolution}&from={from}&to={to}";

            string response = await RequestFinnhubData(url);

            UserInterface.Message($"Getting candles for {asset.Symbol}");

            try
            {
                FinnhubCandleResponse candleResponse = JsonConvert.DeserializeObject<FinnhubCandleResponse>(response);

                List<Candle> candles = candleResponse.Translate();

                candles.ForEach(c => {
                    c.Symbol = asset.Symbol;
                    c.SymbolType = asset.AssetType;
                    c.Resolution = resolution;
                });

                return candles;
            }
            catch(Exception e)
            {
                UserInterface.Message($"Could not get candles for {asset.Symbol}: {e.Message}");
                return null;
            }
        }

        private async Task<string> RequestFinnhubData(string endpoint)
        {
            using (HttpClient http = new HttpClient())
            {
                var url = BaseUrl + endpoint + "&token=" + ApiKey;
                var result = await http.GetStringAsync(url);

                return result;
            }
        }
    }
}
