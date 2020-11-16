﻿using App.Enums;
using App.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public abstract class Asset
    {
        public Asset(string description, string displaySymbol, string symbol)
        {
            Description = description;
            DisplaySymbol = displaySymbol;
            Symbol = symbol;
        }

        public Guid Guid { get; private set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public virtual AssetType AssetType { get; protected set; }
        public string Description { get; private set; }
        public string DisplaySymbol { get; private set; }
        public string Symbol { get; private set; }
        public string Exchange { get; set; }
        public string Id 
        { 
            get
            {
                return $"{Exchange}-{Symbol}";
            } 
            private set { }
        }
    }

    public class Stock : Asset
    {
        public Stock(string description, string displaySymbol, string symbol, string type, string currency) : base(description, displaySymbol, symbol)
        {
            Type = type;
            Currency = currency;
        }
        public new AssetType AssetType = AssetType.Stock;
        public string Type { get; private set; }
        public string Currency { get; private set; }
    }
}