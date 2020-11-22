using App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public class Stock : Asset
    {
        public Stock(string description, string displaySymbol, string symbol, string type, string currency) : base(description, displaySymbol, symbol)
        {
            Type = type;
            Currency = currency;
        }
        public new AssetType AssetType = AssetType.Stock;
        public string Type { get; private set; }
        public string Currency { get; set; }
    }
}
