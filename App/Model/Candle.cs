using App.Enums;
using Newtonsoft.Json;
using System;

namespace App.Model
{
    public class Candle
    {
        public string Id { 
            get
            {
                return $"{Symbol}-{Timestamp}";
            }
            private set { }
        }
        public string Symbol { get; set; }
        public AssetType SymbolType { get; set; }
        public string Resolution { get; set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public decimal OpenPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal ClosingPrice { get; set; }
        public int Volume { get; set; }
        public DateTime Timestamp { get; set; }

        public Candle()
        {

        }

        public Candle(decimal openPrice, decimal highestPrice, decimal lowestPrice, decimal closingPrice, int volume, double unixTimestamp)
        {
            OpenPrice = openPrice;
            HighestPrice = highestPrice;
            LowestPrice = lowestPrice;
            ClosingPrice = closingPrice;
            Volume = volume;
            Timestamp = SetDateTime(unixTimestamp);
        }

        private DateTime SetDateTime(double unixTimestamp)
        {
            DateTime initDate = new DateTime(1970, 1, 1);

            DateTime dateTime = initDate.AddSeconds(unixTimestamp).Date.AddDays(1);

            return dateTime;
        }
    }
}
