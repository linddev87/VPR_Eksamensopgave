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
        internal static async Task Run()
        {
            string result = await UpdateDbForShortlist();

            UserInterface.Message(result);
        }

        private static async Task<string> UpdateDbForShortlist()
        {
            List<Asset> shortlist = GetShortlist(); 

            shortlist.ForEach(async a => {
                await UpdateDbForSymbol(a);
            });

            return "Db update complete";
        }

        private static List<Asset> GetShortlist()
        {
             var context = new AppDbContext();

             return (from asset in context.Assets
                     where asset.Shortlisted == true
                     select asset).ToList();
        }

        private static async Task UpdateDbForSymbol(Asset asset)
        {
            long from = 1604188800;
            long to = 1605989685;
            List<Candle> newCandles = await FinnhubClient.Instance.GetCandlesForSymbol(asset, from, to);

            CommitNewCandlesToDb(newCandles);
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
                    }
                });

                context.SaveChanges();
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
                Console.WriteLine(e.Message);
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
                            Console.WriteLine($"    Added {stock.Id}: {stock.Description}");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"Failed to add {stock.Id}");
                            Console.WriteLine($"Error message: {e.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Skipped {stock.Id}");
                    }
                }

                try
                {
                    var result = context.SaveChanges();
                    Console.WriteLine($"Added {result} new stocks");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Failed to save changes.");
                    Console.WriteLine($"Error message: {e.Message}");
                }
            }
        }
    }
}
