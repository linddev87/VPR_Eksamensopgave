using App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.NewFolder
{
    class Symbol
    {
        public Symbol(string description, string displaySymbol, string uniqueSymbol)
        {
            Description = description;
            DisplaySymbol = displaySymbol;
            UniqueSymbol = uniqueSymbol;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public virtual SymbolType SymbolType { get; protected set; }
        public string Description { get; private set; }
        public string DisplaySymbol { get; private set; }
        public string UniqueSymbol { get; private set; }
    }

    class StockSymbol : Symbol
    {
        public StockSymbol(string description, string displaySymbol, string uniqueSymbol, string stockType, string currency) : base(description, displaySymbol, uniqueSymbol)
        {
            StockType = stockType;
            Currency = currency;
        }
        public new SymbolType SymbolType = SymbolType.Stock;
        public string StockType { get; private set; }
        public string Currency { get; private set; }
    }
}
