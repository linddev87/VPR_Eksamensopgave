using App.Enums;

namespace App.Model
{
    /// <summary>
    /// Derived class to represent Stock symbols from Finnhub
    /// </summary>
    public class Stock : Asset
    {
        // Note the two Stock-specific properties
        public string Type { get; private set; }
        public string Currency { get; set; }

        public Stock(string description, string displaySymbol, string symbol, string type, string currency) : base(description, displaySymbol, symbol)
        {
            Type = type;
            Currency = currency;
        }

        public new AssetType AssetType = AssetType.Stock;

    }
}
