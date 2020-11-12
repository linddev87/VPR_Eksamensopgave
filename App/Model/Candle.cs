using System;
using System.Collections.Generic;
using System.Text;

namespace App.Enums
{
    class Candle
    {  
        public Guid Id { get; private set; }
        public string Symbol { get; private set; }
        public SymbolType SymbolType { get; private set; }
        public string Resolution { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
    }
}
