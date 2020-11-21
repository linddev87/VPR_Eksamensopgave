using App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public class Crypto : Asset
    {
        public Crypto(string description, string displaySymbol, string symbol) : base(description, displaySymbol, symbol)
        {

        }
        public new AssetType AssetType = AssetType.Crypto;
    }
}
