using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using App.Model;
using System.Threading.Tasks;

namespace App.Services
{
    public sealed class FinnhubClient
    {
        private static FinnhubClient instance = null;
        private static readonly object padlock = new object();
        
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

        public async Task<string> GetSymbolsFromExchange(string exchange)
        {
            string apiKey = "brnhl0nrh5reu6jt9nqg";

            using (HttpClient http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("X-Finnhub-Token", apiKey);
                var result = await http.GetStringAsync($"https://finnhub.io/api/v1/stock/symbol?exchange={exchange}");

                return result;
            }
        }
    }
}
