using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using App.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App.Enums;

namespace App.Services
{
    public sealed class FinnhubClient
    {
        private static FinnhubClient instance = null;
        private static readonly object padlock = new object();
        private string ApiKey = "busnc5v48v6vuigkhi40";
        private string BaseUrl = "https://finnhub.io/api/v1";

        private FinnhubClient() {}

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

            List<Candle> candles = JsonConvert.DeserializeObject<List<Candle>>(response);

            candles.ForEach(c => { 
                c.Symbol = asset.Symbol; 
                c.SymbolType = asset.AssetType; 
                c.Resolution = resolution;
            });

            return candles;
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
