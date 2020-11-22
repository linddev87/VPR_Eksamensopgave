using App.Interfaces;
using App.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.ActionRequests
{
    /// <summary>
    /// Used to build and return an AssetReport for a list of symbols
    /// </summary>
    class AssetReportRequest : IActionRequest
    {
        private List<string> Symbols { get; set; }
        private List<Candle> Candles { get; set; }
        public AssetReportRequest(string[] parameters)
        {
            Symbols = SetSymbols(parameters);
            Candles = GetCandlesForSymbols();
        }

        public string Run()
        {
            return MonthlyReport();
        }

        /// <summary>
        /// Get candles for all symbols in the AssetReportRequest
        /// </summary>
        private List<Candle> GetCandlesForSymbols()
        {
            List<Candle> candles = new List<Candle>();

            foreach (var symbol in Symbols)
            {
                List<Candle> toAdd = GetCandlesForSymbol(symbol);

                foreach (var candle in toAdd)
                {
                    candles.Add(candle);
                }
            }

            return candles;
        }

        /// <summary>
        /// Helper method to determine which symbols should be included in the AssetReport. 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private List<string> SetSymbols(string[] parameters)
        {
            List<string> symbols = new List<string>();

            //If the first argument was 'shortlist' => use the shortlisted assets.
            if (parameters[0] == "shortlist")
            {
                using (var context = new AppDbContext())
                {
                    symbols = context.Assets.Where(a => a.Shortlisted == true).Select(a => a.Symbol).ToList();
                }
            }
            // OR create a list of specific symbols based on the arguments supplied by the user.
            else
            {
                symbols = parameters.ToList();
            }

            return symbols;
        }

        /// <summary>
        /// Get the relevant candles for a specific symbol from the Candles context.
        /// </summary>
        /// <param name="symbol">required string for a symbol</param>
        private List<Candle> GetCandlesForSymbol(string symbol)
        {
            List<Candle> candles = new List<Candle>();

            using (var context = new AppDbContext())
            {
                candles = context.Candles.Where(c => c.Symbol == symbol).ToList<Candle>();
            }

            return candles;
        }

        /// <summary>
        /// Build the header row for the report table
        /// </summary>
        public string HeaderString()
        {
            string headerString = StringResize("Month:", 20);

            Candle oldestCandle = Candles.OrderBy(c => c.Timestamp).FirstOrDefault();
            DateTime date = oldestCandle.Timestamp;

            do
            {
                headerString += StringResize($"{date.ToString("MMM")} {date.Year}");

                date = date.AddMonths(1);
            } while (date < DateTime.UtcNow);

            return headerString;
        }

        /// <summary>
        /// Shows the average ClosingPrice for an asset month by month
        /// </summary>
        /// <returns>An AssetReport in string format</returns>
        public string MonthlyReport()
        {
            string result = HeaderString();

            foreach (var symbol in Symbols)
            {
                List<decimal> closingAverages = GetClosingAverages(symbol);

                result += "\n" + GetReportString(symbol, closingAverages);
            }

            result += "\n";

            return result;
        }

        /// <summary>
        /// Builds a line in an AssetReport
        /// </summary>
        /// <param name="symbol">symbol string</param>
        /// <param name="closingAverages">List of closing averages for the symbol</param>
        /// <returns>A line to be appended to an AssetReport string</returns>
        private string GetReportString(string symbol, List<decimal> closingAverages)
        {
            Stock asset;
            using (var context = new AppDbContext())
            {
                asset = (Stock)context.Assets.Where(a => a.Symbol == symbol.ToUpper()).FirstOrDefault();
            }

            string result = StringResize($"{symbol.ToUpper()}({asset.Currency})", 20);

            foreach (var avg in closingAverages)
            {
                result += StringResize(avg.ToString());
            }

            return result;
        }

        /// <summary>
        /// Used to consistently resize strings in order to make reports pretty
        /// </summary>
        /// <param name="input">String to resize</param>
        /// <param name="length">Length to resize to</param>
        /// <returns>Resized string</returns>
        private string StringResize(string input, int length = 12)
        {
            string output = "";
            if (input.Length > length)
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

        /// <summary>
        /// Query the Candle context to get all the candles for a given symbol month by month and calculate the average closing price.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private List<decimal> GetClosingAverages(string symbol)
        {
            List<decimal> averages = new List<decimal>();

            //Get the first day of the current month. We use this as anchor for the following time calculations
            DateTime currMonthBeginning = DateTime.UtcNow.Date.AddDays(DateTime.UtcNow.Date.Day * -1);

            //Go back 12 months and calculate the average closing price for each month
            for (var i = -12; i <= 0; i++)
            {
                DateTime beginningOfMonth = currMonthBeginning.AddMonths(i).AddDays(1);
                DateTime endOfMonth = currMonthBeginning.AddMonths(i + 1);

                //Query to get only the candles relevant for this month
                List<Candle> candlesFromMonth = Candles.Where(c => c.Symbol == symbol.ToUpper()).Where(c => c.Timestamp > beginningOfMonth).Where(c => c.Timestamp < endOfMonth).ToList();

                //If the query returned any data, calculate the average and add it to the list of averages.
                if (candlesFromMonth.Count > 0)
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
