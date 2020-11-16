using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using App.Model;
using App.Services;
using Newtonsoft.Json;

namespace App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DbMaintenance.UpdateStocks("US");

            
            Console.WriteLine("Hello");
        }
    }
}
