using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using test_binance_api.Models;
using test_binance_api.Models.DTOs;

namespace test_binance_api.Data
{
    public class BinanceContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Asset> Assets { get; set; }

        public BinanceContext(DbContextOptions<BinanceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User to Wallet (1-to-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            // Wallet to Transactions (1-to-Many)
/*            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Transactions)
                .WithOne(t => t.Wallet)
                .HasForeignKey(t => t.IdWallet)
                .OnDelete(DeleteBehavior.Cascade);*/

            // Wallet to CurrentHoldings (1-to-Many)
            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.CurrentHoldings)
                .WithOne()
                .HasForeignKey(c => c.IdWallet) 
                .OnDelete(DeleteBehavior.Cascade);

            // Transactions to Coin (Many-to-1)
/*            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Coin)
                .WithMany()
                .HasForeignKey(t => t.CoinId)
                .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}

