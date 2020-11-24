using App.Model;
using App.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Services
{
    /// <summary>
    /// Thread safe Singleton client used to fetch data from Finnhub
    /// </summary>
    public sealed class FinnhubClient
    {
        private static FinnhubClient instance = null;
        private static readonly object padlock = new object();
        private string ApiKey
        {
            get
            {
                return Environment.GetEnvironmentVariable("VPR_Eksamensopgave_FinnhubApiKey");
            }
        }
        private readonly string BaseUrl = "https://finnhub.io/api/v1";

        private FinnhubClient()
        {

        }

        public static FinnhubClient Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FinnhubClient();

                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Used to get all stocks from a given exchange. This is a DevOps feature which is not available from the UserInterface.
        /// </summary>
        /// <param name="exchange"></param>
        /// <returns></returns>
        public async Task<List<Stock>> GetStocksFromExchange(string exchange)
        {
            string url = $"/stock/symbol?exchange={exchange}";

            string response = await RequestFinnhubData(url);

            return JsonConvert.DeserializeObject<List<Stock>>(response);
        }

        /// <summary>
        /// Used to define the query string and get Candles from Finnhub for a given Asset.
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="resolution"></param>
        /// <returns>A list of candles</returns>
        public async Task<List<Candle>> GetCandlesForSymbol(Asset asset, long from, long to, string resolution = "D")
        {
            try
            {
                string url = $"/{asset.AssetType.ToString().ToLower()}/candle?symbol={asset.Symbol.ToLower()}&resolution={resolution}&from={from}&to={to}";

                string response = await RequestFinnhubData(url);

                UserInterface.Message($"Getting candles for {asset.Symbol}");

                FinnhubCandleResponse candleResponse = JsonConvert.DeserializeObject<FinnhubCandleResponse>(response);

                List<Candle> candles = candleResponse.Translate();

                if(candles.Count > 0)
                {
                    candles.ForEach(c =>
                    {
                        c.Symbol = asset.Symbol;
                        c.SymbolType = asset.AssetType;
                        c.Resolution = resolution;
                    });
                }

                return candles;
            }
            catch (Exception e)
            {
                UserInterface.Message($"Could not get candles for {asset.Symbol}: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Carry out an actual http request to Finnhub and return the json response
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private async Task<string> RequestFinnhubData(string endpoint)
        {
            using (HttpClient http = new HttpClient())
            {
                var result = "";
                try
                {
                    var url = BaseUrl + endpoint + "&token=" + ApiKey;
                    result = await http.GetStringAsync(url);
                }
                catch (Exception e)
                {
                    UserInterface.Message($"Something went wrong with the HTTP request: {e.Message}");
                }
                return result;
            }
        }
    }
}
