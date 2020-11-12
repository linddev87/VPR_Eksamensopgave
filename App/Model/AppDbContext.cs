using App.NewFolder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Enums
{
    class AppDbContext : DbContext
    {
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<StockSymbol> StockSymbols { get; set; }

        public DbSet<Candle> Candles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:linddev87stockappdb.database.windows.net,1433;Initial Catalog=StockAppDb;Persist Security Info=False;User ID=linddev87;Password=Bond665Villain;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Symbol>()
                .HasDiscriminator<SymbolType>("SymbolType")
                .HasValue<Symbol>(SymbolType.BaseSymbol)
                .HasValue<StockSymbol>(SymbolType.Stock);
        }
    }
}
