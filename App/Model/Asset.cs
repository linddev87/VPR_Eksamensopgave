using App.Enums;
using System;


namespace App.Model
{
    /// <summary>
    /// Abstract base class for the 3 asset types.
    /// </summary>
    public abstract class Asset
    {
        public Asset(string description, string displaySymbol, string symbol)
        {
            Description = description;
            DisplaySymbol = displaySymbol;
            Symbol = symbol;
            Shortlisted = false;
        }

        public string Id
        {
            get
            {
                return $"{Exchange}-{Symbol}";
            }
            private set { }
        }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public virtual AssetType AssetType { get; protected set; }
        public string Description { get; private set; }
        public string DisplaySymbol { get; private set; }
        public string Symbol { get; private set; }
        public string Exchange { get; set; }
        public bool Shortlisted { get; private set; }

        public void SetShortlistedStatus(bool shortlist)
        {
            if (shortlist)
            {
                Shortlisted = true;
            }
            else
            {
                Shortlisted = false;
            }
        }

        public override string ToString()
        {
            return $"{Description} ({Symbol})";
        }
    }
}
