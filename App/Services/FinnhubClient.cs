using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using App.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Services
{
    public sealed class FinnhubClient
    {
        private static FinnhubClient instance = null;
        private static readonly object padlock = new object();
        private string ApiKey = "brnhl0nrh5reu6jt9nqg";
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

        public async Task<List<Stock>>GetStocksFromExchange(string exchange)
        {
            string endpoint = $"/stock/symbol?exchange={exchange}";

            string response = await RequestFinnhubData(endpoint);

            return JsonConvert.DeserializeObject<List<Stock>>(response);
        }

        private async Task<string> RequestFinnhubData(string endpoint)
        {
            using (HttpClient http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("X-Finnhub-Token", ApiKey);
                var result = await http.GetStringAsync(BaseUrl + endpoint);

                return result;
            }
        }
    }
}
