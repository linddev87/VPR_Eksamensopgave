using App.Enums;

namespace App.Model
{
    /// <summary>
    /// Derived class to represent Crypto symbols from Finnhub
    /// </summary>
    public class Crypto : Asset
    {
        public Crypto(string description, string displaySymbol, string symbol) : base(description, displaySymbol, symbol)
        {

        }
        public new AssetType AssetType = AssetType.Crypto;
    }
}
