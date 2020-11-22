using App.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Candle> Candles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = System.Environment.GetEnvironmentVariable("VPR_Eksamensopgave_ConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasDiscriminator<AssetType>("SymbolType")
                .HasValue<Stock>(AssetType.Stock);

            modelBuilder.Entity<Asset>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Candle>()
                .HasKey(c => c.Id);
        }
    }
}