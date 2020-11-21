using App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public class Forex : Asset
    {
        public Forex(string description, string displaySymbol, string symbol) : base(description, displaySymbol, symbol)
        {

        }
        public new AssetType AssetType = AssetType.Forex;
    }
}
