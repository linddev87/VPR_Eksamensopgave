using App.Enums;

namespace App.Model
{
    /// <summary>
    /// Derived class to represent Forex symbols from Finnhub
    /// </summary>
    public class Forex : Asset
    {
        public Forex(string description, string displaySymbol, string symbol) : base(description, displaySymbol, symbol)
        {

        }

        public new AssetType AssetType = AssetType.Forex;
    }
}
