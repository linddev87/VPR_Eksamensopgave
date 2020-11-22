using App.Interfaces;
using App.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.ActionRequests
{
    /// <summary>
    /// Simple class used to query the Assets context for an Asset which has a description matching the user input.
    /// </summary>
    internal class AssetLookupRequest : IActionRequest
    {
        private string[] Params { get; set; }

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

        /// <summary>
        /// Build a string to show the queried assets in the presentation layer
        /// </summary>
        /// <returns>string containing the found assets</returns>
        private string GetResultString()
        {
            string output;
            try
            {
                List<Asset> assets = LookupAssets();
                output = "";

                if(assets.Count > 0)
                {
                    foreach (var asset in assets)
                    {
                        output += $"{asset.Description} ({asset.Symbol}) \n";
                    }
                }
                else
                {
                    output = "Couldn't find any assets matching your input.";
                }

            }
            catch (Exception e)
            {
                output = "Something went wrong" + e.Message;
            }

            return output;
        }

        /// <summary>
        /// Uses Linq to find assets with a description containing the user input.
        /// </summary>
        /// <returns>A list of matching assets</returns>
        private List<Asset> LookupAssets()
        {
            List<Asset> assets = new List<Asset>();

            var context = new AppDbContext();

            foreach (var param in Params)
            {
                var assetsFound = context.Assets.Where(a => a.Description.ToLower().Contains(param.ToLower()));

                foreach (var asset in assetsFound)
                {
                    assets.Add(asset);
                }
            }

            return assets;
        }
    }
}