using App.Interfaces;
using App.Model;
using System;
using System.Linq;

namespace App.ActionRequests
{
    /// <summary>
    /// Add or remove assets to/from shortlist
    /// </summary>
    internal class AssetShortlistRequest : IActionRequest
    {
        public string[] Params { get; private set; }
        public bool Shortlist { get; set; }
        private string Symbol { get; set; }
        public string Run()
        {
            return ShortListAction();
        }

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

        /// <summary>
        /// Sets the Asset.Shortlisted property true or false based on user input.
        /// </summary>
        /// <returns></returns>
        public string ShortListAction()
        {
            using (var context = new AppDbContext())
            {
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
}
