using App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public class Candle
    {  
        public Guid Id { get; private set; }
        public string Symbol { get; private set; }
        public AssetType SymbolType { get; private set; }
        public string Resolution { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
    }
}
