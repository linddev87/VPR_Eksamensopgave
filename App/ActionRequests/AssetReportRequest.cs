using App.Interfaces;
using App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace App.ActionRequests
{
    class AssetReportRequest : IActionRequest
    {
        private List<string> Symbols { get; set; }
        private List<Candle> Candles { get; set; }
        public AssetReportRequest(string[] parameters)
        {
            Symbols = SetSymbols(parameters);
            Candles = GetCandlesForSymbols();
        }

        private List<Candle> GetCandlesForSymbols()
        {
            List<Candle> candles = new List<Candle>();

            foreach(var symbol in Symbols)
            {
                List<Candle> toAdd = GetCandlesForSymbol(symbol);

                foreach(var candle in toAdd)
                {
                    candles.Add(candle);
                }
            }

            return candles;
        }

        private List<string> SetSymbols(string[] parameters)
        {
            List<string> symbols = new List<string>();
            if (parameters[0] == "shortlist")
            {
                using (var context = new AppDbContext())
                {
                    symbols = context.Assets.Where(a => a.Shortlisted == true).Select(a => a.Symbol).ToList();
                }
            }
            else
            {
                symbols = parameters.ToList();
            }

            return symbols;
        }

        public string Run()
        {
            return MonthlyReport();
        }

        private List<Candle> GetCandlesForSymbol(string symbol)
        {
            List<Candle> candles = new List<Candle>();

            using (var context = new AppDbContext())
            {
                candles = context.Candles.Where(c => c.Symbol == symbol).ToList<Candle>();
            }

            return candles;
        }

        public string HeaderString()
        {
            string headerString = StringResize("Month:");

            Candle oldestCandle = Candles.OrderBy(c => c.Timestamp).FirstOrDefault();
            DateTime date = oldestCandle.Timestamp;

            do
            {
                headerString += StringResize($"{date.ToString("MMM")} {date.Year}");

                date = date.AddMonths(1);
            } while (date < DateTime.UtcNow);

            return headerString;
        }

        public string MonthlyReport()
        {
            string result = HeaderString();
            
            foreach(var symbol in Symbols)
            {
                List<decimal> closingAverages = GetClosingAverages(symbol);

                result += "\n" + GetReportString(symbol, closingAverages);
            }

            result += "\n";

            return result;
        }

        private string GetReportString(string symbol, List<decimal> closingAverages)
        {
            string result = StringResize(symbol.ToUpper());

            foreach(var avg in closingAverages)
            {
                result += StringResize(avg.ToString());
            }

            return result;
        }

        private string StringResize(string input, int length = 15 )
        {
            string output = "";
            if(input.Length > length)
            {
                output = input.Substring(0, length - 4) + "...";
            }
            else
            {
                int toAdd = length - input.Length;
                output += input;

                for (int i = 0; i < toAdd; i++)
                {
                    output += " ";
                }
            }
            return output;
        }

        private List<decimal> GetClosingAverages(string symbol)
        {
            List<decimal> averages = new List<decimal>();
            DateTime currMonthBeginning = DateTime.UtcNow.Date.AddDays(DateTime.UtcNow.Date.Day * -1);

            for(var i = -12; i <= 0; i++)
            {
                DateTime beginningOfMonth = currMonthBeginning.AddMonths(i).AddDays(1);
                DateTime endOfMonth = currMonthBeginning.AddMonths(i + 1);

                List<Candle> candlesFromMonth = Candles.Where(c => c.Symbol == symbol.ToUpper()).Where(c => c.Timestamp > beginningOfMonth).Where(c => c.Timestamp < endOfMonth).ToList();

                if(candlesFromMonth.Count > 0)
                {
                    decimal averageClose = candlesFromMonth.Select(c => c.ClosingPrice).Average();
                    decimal roundedAverage = Math.Round(averageClose, 2);

                    averages.Add(roundedAverage);
                }
            }

            return averages;
        }

    }
}
