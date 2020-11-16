using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Model;

namespace App.Services
{
    public static class DbMaintenance
    {
        public static async Task<bool> UpdateStocks(string exchange)
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
