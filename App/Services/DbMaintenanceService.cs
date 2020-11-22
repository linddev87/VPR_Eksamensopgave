using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Model;
using System.Linq;
using App.UI;
using App.Services;

namespace App.Services
{
    public static class DbMaintenanceService

    {
        internal static void Run()
        {
            string result = UpdateDbForShortlist();
        }

        private static string UpdateDbForShortlist()
        {
            List<Asset> shortlist = GetShortlist(); 

            shortlist.ForEach(async a => {
                await UpdateDbForSymbol(a);
            });

            return "";
        }

        private static List<Asset> GetShortlist()
        {
             var context = new AppDbContext();
             List<Asset> shortlist = (from asset in context.Assets
                                        where asset.Shortlisted == true
                                        select asset).ToList();
            return shortlist;
        }

        private static async Task UpdateDbForSymbol(Asset asset)
        {
            long to = ConvertToUnix(DateTime.UtcNow.Date);
            long from;

            Candle latestCandle = GetLatestCandleForAsset(asset.Symbol);
            
            if(latestCandle == null)
            {
                from = ConvertToUnix(new DateTime(2020, 1, 1).Date);
            }
            else
            {
                from = ConvertToUnix(latestCandle.Timestamp);
            }

            List<Candle> newCandles = await FinnhubClient.Instance.GetCandlesForSymbol(asset, from, to);

            CommitNewCandlesToDb(newCandles);
        }

        private static long ConvertToUnix(DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeSeconds();
        }

        private static Candle GetLatestCandleForAsset(string symbol)
        {
            Candle candle;
            
            using(var context = new AppDbContext())
            {
                candle = context.Candles.OrderByDescending(c => c.Timestamp).FirstOrDefault();
            }
            
            return candle;
        }

        private static void CommitNewCandlesToDb(List<Candle> newCandles)
        {
            using (var context = new AppDbContext())
            {
                newCandles.ForEach(newCandle => {
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

        public static async Task<bool> UpdateStocksDb(string exchange)
        {
            try
            {
                var stocks = await FinnhubClient.Instance.GetStocksFromExchange(exchange);
                stocks.ForEach(x => x.Exchange = exchange);

                SaveNewStocks(stocks);

                return true;
            }
            catch(Exception e)
            {
                UserInterface.Message(e.Message);
                return false;
            }
        }


        private static void SaveNewStocks(List<Stock> stocks)
        {
            using (var context = new AppDbContext())
            {
                foreach(var stock in stocks)
                {
                    var existingStock = context.Stocks.Find(stock.Id);

                    if(existingStock == null)
                    {
                        try
                        {
                            context.Stocks.Add(stock);
                            UserInterface.Message($"    Added {stock.Id}: {stock.Description}");
                        }
                        catch(Exception e)
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
                catch(Exception e)
                {
                    UserInterface.Message($"Failed to save changes.");
                    UserInterface.Message($"Error message: {e.Message}");
                }
            }
        }
    }
}
