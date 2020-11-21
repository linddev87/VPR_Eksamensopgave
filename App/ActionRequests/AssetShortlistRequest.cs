using App.Interfaces;
using App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.ActionRequests
{
    internal class AssetShortlistRequest : IActionRequest
    {
        public string[] Params { get; private set; }
        public bool Shortlist { get; set; }
        private string Symbol { get; set; }
        public AssetShortlistRequest(string[] parameters)
        {
            Symbol = parameters[1];

            if (parameters[0] == "add")
            {
                Shortlist = true;
            }
            else if (parameters[0] == "remove")
            {
                Shortlist = false;
            }
            else
            {
                throw new ArgumentException($"Wrong input: {parameters[0]}. Should be either 'add' or 'remove'.");
            }
        }
        public string Run()
        {
            return ShortListAction();
        }

        public string ShortListAction()
        {
            AppDbContext context = new AppDbContext();
            Asset asset = context.Assets.Where(a => a.Symbol == this.Symbol).FirstOrDefault();

            if (Shortlist)
            {
                asset.SetShortlistedStatus(true);
                context.SaveChanges();
                return $"{asset.ToString()} was added to the shortlist. \nRun 'updatedatabase' before generating an AssetReport for this asset.";
            }
            else
            {
                asset.SetShortlistedStatus(false);
                context.SaveChanges();
                return $"{asset.ToString()} was removed from the shortlist. \n";
            }
        }
    }
}
