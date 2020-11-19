using App.Interfaces;
using App.Model;
using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    internal class AssetLookupRequest : IActionRequest
    {
        public string Action { get; private set; }
        public string[] Params { get; private set; }

        public AssetLookupRequest()
        {

        }

        public AssetLookupRequest(string[] parameters)
        {
            Params = parameters;
        }

        public string Run()
        {
            return GetResultString();
        }

        private string GetResultString()
        {
            List<Asset> assets = LookupAssets();
            string output = "";

            foreach(var asset in assets)
            {
                output += asset.Description + "\n";
            }

            return output;
        }

        private List<Asset> LookupAssets()
        {
            List<Asset> assets = new List<Asset>();

            var context = new AppDbContext();

            foreach (var param in Params)
            {
                var asset = context.Assets.Where(a => a.Symbol == param).FirstOrDefault();
                assets.Add(asset);
            }

            return assets;
        }
    }
}