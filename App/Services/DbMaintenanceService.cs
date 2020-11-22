using App.Model;
using App.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public static class DbMaintenanceService

    {
        internal static void Run()
        {
            UpdateDbForShortlist();
        }

        /// <summary>
        /// Initiates the database update for shortlisted Asset Candles
        /// </summary>
        private static string UpdateDbForShortlist()
        {
            List<Asset> shortlist = GetShortlist();

            shortlist.ForEach(async a =>
            {
                await UpdateDbForSymbol(a);
            });

            return "";
        }

        /// <summary>
        /// Query the Assets context for all shortlisted assets
        /// </summary>
        /// <returns>List of shortlisted assets</returns>
        private static List<Asset> GetShortlist()
        {
            var context = new AppDbContext();
            List<Asset> shortlist = (from asset in context.Assets
                                     where asset.Shortlisted == true
                                     select asset).ToList();
            return shortlist;
        }

        /// <summary>
        /// Get all candles for a given symbol from Finnhub and add them to the database
        /// </summary>
        /// <param name="asset"></param>
        private static async Task UpdateDbForSymbol(Asset asset)
        {
            long to = ConvertToUnix(DateTime.UtcNow.Date);
            long from;

            //Get the most recent candle for the stock. Used to set the "from" date in the Finnhub request.
            Candle latestCandle = GetLatestCandleForAsset(asset.Symbol);

            if (latestCandle == null)
            {
                //If no candle for the symbol exists, set the from date to jan 1 2020
                from = ConvertToUnix(new DateTime(2020, 1, 1).Date);
            }
            else
            {
                from = ConvertToUnix(latestCandle.Timestamp);
            }

            List<Candle> newCandles = await FinnhubClient.Instance.GetCandlesForSymbol(asset, from, to);

            CommitNewCandlesToDb(newCandles);
        }

        /// <summary>
        /// Simple DateTime to Unix (epoch) time converter
        /// </summary>
        /// <param name="date"></param>
        private static long ConvertToUnix(DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Returns the latest candle for a given symbol
        /// </summary>
        /// <param name="symbol"></param>
        private static Candle GetLatestCandleForAsset(string symbol)
        {
            Candle candle;

            using (var context = new AppDbContext())
            {
                candle = context.Candles.OrderByDescending(c => c.Timestamp).Where(c => c.Symbol == symbol).FirstOrDefault();
            }

            return candle;
        }

        /// <summary>
        /// Check if candle exists in database. If not, save it. 
        /// </summary>
        /// <param name="newCandles"></param>
        private static void CommitNewCandlesToDb(List<Candle> newCandles)
        {
            using (var context = new AppDbContext())
            {
                newCandles.ForEach(newCandle =>
                {
                    var existing = context.Candles.Where(c => c.Id == newCandle.Id).FirstOrDefault();

                    if (existing == null)
                    {
                        context.Candles.Add(newCandle);
                        UserInterface.Message($"    {newCandle.Id} was added.");
                    }
                });

                int candles = context.SaveChanges();
                UserInterface.Message($"{candles} candles were saved to the DB for {newCandles[0].Symbol}.");
            }
        }

        /// <summary>
        /// Method used to populate the Assets table in the database. This is a DevOps feature and cannot be called by the UserInterface by default.
        /// </summary>
        /// <param name="exchange"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateStocksDb(string exchange)
        {
            try
            {
                var stocks = await FinnhubClient.Instance.GetStocksFromExchange(exchange);
                stocks.ForEach(x => x.Exchange = exchange);

                SaveNewStocks(stocks);

                return true;
            }
            catch (Exception e)
            {
                UserInterface.Message(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Check if an asset already exists in the Assets table and create it if it doesn't. 
        /// </summary>
        /// <param name="stocks"></param>
        private static void SaveNewStocks(List<Stock> stocks)
        {
            using (var context = new AppDbContext())
            {
                foreach (var stock in stocks)
                {
                    var existingStock = context.Stocks.Find(stock.Id);

                    if (existingStock == null)
                    {
                        try
                        {
                            context.Stocks.Add(stock);
                            UserInterface.Message($"    Added {stock.Id}: {stock.Description}");
                        }
                        catch (Exception e)
                        {
                            UserInterface.Message($"Failed to add {stock.Id}");
                            UserInterface.Message($"Error message: {e.Message}");
                        }
                    }
                    else
                    {
                        UserInterface.Message($"Skipped {stock.Id}");
                    }
                }

                try
                {
                    var result = context.SaveChanges();
                    UserInterface.Message($"Added {result} new stocks");
                }
                catch (Exception e)
                {
                    UserInterface.Message($"Failed to save changes.");
                    UserInterface.Message($"Error message: {e.Message}");
                }
            }
        }
    }
}
