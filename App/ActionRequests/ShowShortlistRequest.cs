using App.Interfaces;
using App.Model;
using System.Collections.Generic;
using System.Linq;

namespace App.ActionRequests
{
    class ShowShortlistRequest : IActionRequest
    {
        public string[] Params { get; private set; }

        public ShowShortlistRequest()
        {

        }

        public string Run()
        {
            return GetShortlistString();
        }

        private string GetShortlistString()
        {
            AppDbContext context = new AppDbContext();
            List<Asset> assets = new List<Asset>(context.Assets.Where(a => a.Shortlisted));

            var shortlistString = BuildShortlistString(assets);

            return shortlistString;
        }

        private string BuildShortlistString(List<Asset> assets)
        {
            string shortlistString = "";

            if(assets.Count > 0)
            {
                shortlistString = "Shortlist:\n";

                foreach (Asset asset in assets)
                {
                    shortlistString += $"   {asset.ToString()}\n";
                }
            }
            else
            {
                shortlistString = "The shortlist is empty";
            }

            return shortlistString;
        }
    }
}
