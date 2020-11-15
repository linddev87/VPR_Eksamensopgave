using System;
using System.Net.Http;
using System.Threading.Tasks;
using App.Services;

namespace App
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //using var client = new HttpClient();
            //var content = await client.GetStringAsync("http://webcode.me");

            //Console.WriteLine(content);

            var res = await FinnhubClient.Instance.GetSymbolsFromExchange("US");

            Console.WriteLine(res);
        }
    }
}
